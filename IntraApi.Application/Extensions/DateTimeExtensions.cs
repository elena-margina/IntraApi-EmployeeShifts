using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraApi.Application.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek startOfWeek)
        {
            int diff = dateTime.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dateTime.AddDays(-diff).Date;
        }
    }
}
