using System;
using System.Runtime.InteropServices;

namespace Roro.Activities.Excel
{
    internal sealed class ExcelBot : IDisposable
    {
        private const string EXCEL_PROG_ID = "Excel.Application";

        private const uint RPC_SERVER_UNAVAILABLE = 0x800706BA;

        public static ExcelBot Shared = new ExcelBot();

        private dynamic App { get; set; }

        public dynamic GetApp()
        {
            if (this.App == null)
            {
                try
                {
                    if (Type.GetTypeFromProgID(EXCEL_PROG_ID) is Type excelType)
                    {
                        this.App = Activator.CreateInstance(excelType);
                    }
                    else
                    {
                        throw new ExcelNotFoundException();
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                try
                {
                    var version = this.App.Version;
                }
                catch
                {
                    this.Dispose();
                    return this.GetApp();
                }
            }
            this.App.Visible = true;
            return this.App;
        }

        public dynamic GetWorkbookByName(string wbName, bool throwIfNotFound)
        {
            var xlWb = this.GetApp().Workbooks.Item(wbName);
            if (xlWb == null)
            {
                if (throwIfNotFound)
                {
                    throw new ExcelWorkbookNotFoundException();
                }
                else
                {
                    return null;
                }
            }
            xlWb.Activate();
            return xlWb;
        }

        public dynamic GetWorksheetByName(string wbName, string wsName, bool throwIfNotFound)
        {
            var xlWs = this.GetWorkbookByName(wbName, true).Worksheets.Item(wsName);
            if (xlWs == null)
            {
                if (throwIfNotFound)
                {
                    throw new ExcelWorksheetNotFoundException();
                }
                else
                {
                    return null;
                }
            }
            xlWs.Activate();
            return xlWs;
        }

        public dynamic GetRange(string wbName, string wsName, string rangeAddr)
        {
            return ExcelBot.Shared.GetWorksheetByName(wbName, wsName, true).Range(rangeAddr);
        }

        private ExcelBot()
        {
            ;
        }

        public void Dispose()
        {
            try
            {
                this.App.Quit();
            }
            catch
            {

            }
            try
            {
                Marshal.FinalReleaseComObject(this.App);
            }
            catch (COMException ex)
            {
                switch ((uint)ex.ErrorCode)
                {
                    // 
                    case RPC_SERVER_UNAVAILABLE:
                        break;

                    default:
                        throw;
                }
            }

            this.App = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }


}
