using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Roro.Activities
{
    public static class Extensions
    {
        public static string Humanize(this string str)
        {
            return Regex.Replace(str.Split('.').Last(), "([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", "$1 ");
        }
    }
}
