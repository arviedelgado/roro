using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roro.Activities.Excel
{
    public abstract class ExcelException : Exception
    {

    }

    public sealed class ExcelNotFoundException : ExcelException
    {

    }

    public sealed class ExcelWorkbookNotFoundException : ExcelException
    {

    }

    public sealed class ExcelWorksheetNotFoundException : ExcelException
    {

    }

}
