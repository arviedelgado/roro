using System;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Roro.Core.Activities.Excel
{
    public sealed class ExcelBot : IDisposable
    {
        public static readonly ExcelBot Shared = new ExcelBot();

        private readonly IList<Application> Apps = new List<Application>();

        private ExcelBot()
        {
            ; // singleton pattern
        }

        internal Application Get()
        {
            var xlApp = new Application()
            {
                Visible = true
            };
            this.Apps.Add(xlApp);
            return xlApp;
        }

        internal Application Get(string name)
        {
            Console.WriteLine("Get: {0}", name);
            foreach (var xlApp in this.Apps)
            {
                var xlWbs = xlApp.Workbooks;
                if (xlWbs.Count == 0)
                {
                    this.Apps.Remove(xlApp);
                    this.Release(xlWbs, xlApp);
                    continue;
                }
                var xlWb = xlWbs.Item[1];
                if (xlWb.FullName == name)
                {
                    this.Release(xlWb, xlWbs);
                    return xlApp;
                }
                this.Release(xlWb, xlWbs);
            }
            throw new Exception(string.Format("ERROR: Workbook not found - {0}", name));
        }

        internal void Release(params object[] objs)
        {
            foreach (var obj in objs)
            {
                if (obj is Application xlApp)
                {
                    this.Apps.Remove(xlApp);
                    xlApp.Quit();
                }
                else if (obj is Workbook xlWb)
                {
                    xlWb.Close(SaveChanges : false);
                }
                Marshal.FinalReleaseComObject(obj);
            }
            GC.Collect();
        }

        public void Dispose()
        {
            while (this.Apps.Count() > 0)
            {
                this.Release(this.Apps.Last());
            }
        }
    }
}
