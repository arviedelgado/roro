using System;
using System.Runtime.InteropServices;

namespace Roro.Activities.Excel
{
    internal sealed class ExcelBot : IDisposable
    {
        private const string EXCEL_PROG_ID = "Excel.Application";

        private const uint RPC_SERVER_UNAVAILABLE = 0x800706BA;

        public static readonly ExcelBot Shared = new ExcelBot();

        private dynamic App { get; set; }

        public dynamic GetInstance()
        {
            if (this.App == null)
            {
                try
                {
                    var excelType = Type.GetTypeFromProgID(EXCEL_PROG_ID);
                    if (excelType == null)
                    {
                        throw new TypeLoadException("The Excel application is not installed.");
                    }
                    this.App = Activator.CreateInstance(excelType);
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
                    return this.GetInstance();
                }
            }
            this.App.Visible = true;
            return this.App;
        }

        public dynamic GetWorkbook(dynamic xlApp, string workbookName)
        {
            dynamic xlWb = null;
            if (workbookName == string.Empty)
            {
                xlWb = xlApp.ActiveWorkbook ?? xlApp.Workbooks.Add();
            }
            else
            {
                xlWb = xlApp.Workbooks.Item(workbookName);
            }
            xlWb.Activate();
            return xlWb;
        }

        public dynamic GetWorksheet(dynamic xlWb, string worksheetName)
        {
            dynamic xlWs = null;
            if (worksheetName == string.Empty)
            {
                xlWs = xlWb.ActiveSheet;
            }
            else
            {
                xlWs = xlWb.Item(worksheetName);
            }
            xlWs.Activate();
            return xlWs;
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
