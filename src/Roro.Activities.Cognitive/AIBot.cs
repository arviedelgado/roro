using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Graphics.Imaging;
using Windows.Media.Core;
using Windows.Media.FaceAnalysis;
using Windows.Media.Ocr;
using Windows.Media.Playback;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.Storage.Streams;

namespace Roro.Activities.Cognitive
{
    public static class AIBot
    {
        public static void ImageToFace()
        {
            //Console.WriteLine("Detecting face..");
            Task.Run(async () =>
            {
                var faceDetector = await FaceDetector.CreateAsync();
                var screenBitmap = GetBitmapFromScreen();
                var softwareBitmap = await GetSoftwareBitmapFromBitmap(screenBitmap);
                if (!FaceDetector.IsBitmapPixelFormatSupported(softwareBitmap.BitmapPixelFormat))
                {
                    //Console.WriteLine("Converting to supported bitmap pixel format..");
                    //Console.WriteLine("srcBitmap Width={0}, Height={1}", screenBitmap.Width, screenBitmap.Height);
                    //Console.WriteLine("dstBitmap Width={0}, Height={1}", softwareBitmap.PixelWidth, softwareBitmap.PixelHeight);
                    softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, FaceDetector.GetSupportedBitmapPixelFormats().First());
                    //Console.WriteLine("Converted successfully");
                }
                //Console.WriteLine(screenBitmap.PixelFormat);
                //Console.WriteLine(softwareBitmap.BitmapPixelFormat);
                screenBitmap = await GetBitmapFromSoftwareBitmap(softwareBitmap);
                //Console.WriteLine(screenBitmap.PixelFormat);
                //Console.WriteLine(softwareBitmap.BitmapPixelFormat);
                using (var g = Graphics.FromImage(screenBitmap))
                {
                    var detectedFaces = await faceDetector.DetectFacesAsync(softwareBitmap);
                    //Console.WriteLine("Detected faces: {0}", detectedFaces.Count);
                    foreach (var detectedFace in detectedFaces)
                    {
                        var facebox = detectedFace.FaceBox;
                        g.DrawRectangle(Pens.Red, new Rectangle((int)facebox.X, (int)facebox.Y, (int)facebox.Width, (int)facebox.Height));
                        //Console.WriteLine("Face at X={0}, Y={1}, Width={2}, Height={3}", facebox.X, facebox.Y, facebox.Width, facebox.Height);
                    }
                }
                //screenBitmap.Save("screenbitmap" + DateTime.Now.Ticks + ".png", ImageFormat.Png);
            }).Wait();
        }

        public static string ScreenToText(int x, int y, int width, int height)
        {
            if (width == 0)
            {
                width = SystemInformation.VirtualScreen.Width;
                height = SystemInformation.VirtualScreen.Height;
            }
            return ImageToText(GetBitmapFromScreenRect(new Rectangle(x, y, width, height)));
        }

        public static string ImageToText(Bitmap screenBitmap)
        {
            var task = Task.Run<OcrResult>(async () =>
            {
                var ocrEngine = OcrEngine.TryCreateFromUserProfileLanguages();
                var minScreenBitmap = new Bitmap(Math.Max(100, screenBitmap.Width), Math.Max(100, screenBitmap.Height));
                Graphics.FromImage(minScreenBitmap).DrawImage(screenBitmap, Point.Empty);
                var softwareBitmap = await GetSoftwareBitmapFromBitmap(minScreenBitmap);
                return await ocrEngine.RecognizeAsync(softwareBitmap);
            });
            task.Wait();
            return task.Result.Text;
        }

        private static Bitmap GetBitmapFromScreenRect(Rectangle r)
        {
            var srcBitmap = new Bitmap(r.Width, r.Height, PixelFormat.Format32bppArgb);
            Graphics.FromImage(srcBitmap).CopyFromScreen(r.Location, Point.Empty, srcBitmap.Size, CopyPixelOperation.SourceCopy);
            return srcBitmap;
        }

        private static Bitmap GetBitmapFromScreen()
        {
            return GetBitmapFromScreenRect(Screen.PrimaryScreen.Bounds);
        }

        private static async Task<SoftwareBitmap> GetSoftwareBitmapFromBitmap(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                using (IRandomAccessStream ras = new InMemoryRandomAccessStream())
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    await ms.CopyToAsync(ras.AsStreamForWrite());
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(ras);
                    return await decoder.GetSoftwareBitmapAsync();
                }
            }
        }

        private static async Task<Bitmap> GetBitmapFromSoftwareBitmap(SoftwareBitmap softwareBitmap)
        {
            using (IRandomAccessStream ras = new InMemoryRandomAccessStream())
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.BmpEncoderId, ras);
                encoder.SetSoftwareBitmap(SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8));
                await encoder.FlushAsync();
                Bitmap bmp = new Bitmap(ras.AsStreamForRead());
                return bmp.Clone(new Rectangle(Point.Empty, bmp.Size), PixelFormat.Format32bppArgb);
            }
        }

        public static string SpeechToText()
        {
            //Console.WriteLine("Recognizing..");
            Task<string> task = Task.Run<string>(async () =>
            {
                SpeechRecognizer speechRecognizer = new SpeechRecognizer();
                await speechRecognizer.CompileConstraintsAsync();
                speechRecognizer.UIOptions.ShowConfirmation = false;
                speechRecognizer.UIOptions.IsReadBackEnabled = false;
                try
                {
                    SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeWithUIAsync();
                    return speechRecognitionResult.Text;
                }
                catch (Exception ex)
                {
                    if (ex.HResult == -2147199735)
                    {
                        MessageBox.Show("Your need to enable speech recognition on your computer.\n\n" +
                            "Open Settings > Privacy > Speech, inking, & typing > click 'Get to know me'.", "Error");
                    }
                    throw ex;
                }
            });
            task.Wait();
            //Console.WriteLine("Speech Recognized: {0}", task.Result);
            return task.Result;
        }

        public static void TextToSpeech(string text)
        {
            //Console.WriteLine("Synthesizing..");
            Task task = Task.Run(async () =>
            {
                SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
                speechSynthesizer.Voice = SpeechSynthesizer.AllVoices.First(x => x.Gender == VoiceGender.Female);
                SpeechSynthesisStream speechSynthesisStream = await speechSynthesizer.SynthesizeTextToStreamAsync(text);
                // Starting with Windows 10, version 1607,
                // apps should use the MediaPlayer for media playback.
                // mediaPlayer = BackgroundMediaPlayer.Current;
                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Source = MediaSource.CreateFromStream(speechSynthesisStream, speechSynthesisStream.ContentType);
                mediaPlayer.Play();
            });
            task.Wait();
            //Console.WriteLine("Speech Synthesized: {0}", text);
        }

    }
}

