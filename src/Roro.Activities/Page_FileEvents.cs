using System;
using System.IO;
using System.Windows.Forms;

namespace Roro.Activities
{
    public partial class Page
    {
        public string FilePath { get; private set; }

        public string FileName => string.IsNullOrEmpty(this.FilePath) ? "New*" : new FileInfo(this.FilePath).Name;

        private const string FileDialogFilter = "Roro Workflow (*.xml)|*.xml";

        public static Page Create()
        {
            return new Page();
        }

        public static Page Open()
        {
            using (var f = new OpenFileDialog())
            {
                f.Filter = FileDialogFilter;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    return Page.Open(f.FileName);
                }
                else
                {
                    return null;
                }
            }
        }

        public static Page Open(string path)
        {
            try
            {
                var data = File.ReadAllText(path);
                var page = DataContractSerializerHelper.ToObject<Page>(data);
                page.FilePath = path;
                return page;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public bool Save()
        {
            if (string.IsNullOrEmpty(this.FilePath))
            {
                using (var f = new SaveFileDialog())
                {
                    f.Filter = FileDialogFilter;
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        this.FilePath = f.FileName;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return SaveAs(this.FilePath);
        }

        public bool SaveAs(string path)
        {
            var data = DataContractSerializerHelper.ToString(this);
            File.WriteAllText(path, data);
            this.FilePath = path;
            return true;
        }

        public bool Close()
        {
            // TODO: ask user to save if has changes.
            // Show asterisks on filename as well to indicate there are unsaved changes.
            // Add the mentioned features when we have Undo Redo feature.
            return true;
        }
    }
}
