using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.Extensions
{
    public static class DateTimeExt
    {
        public const string _ddmmyyyyFormat = "dd/MM/yyyy";
        public static string ToDDMMYYYY(this DateTime date)
        {
            
            return date.ToString(_ddmmyyyyFormat);
        }
    }
}
