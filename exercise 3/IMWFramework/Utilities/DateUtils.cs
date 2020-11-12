using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exercice_2.IMWFramework.Utilities
{
    public static class DateUtils
    {
        public static int DateTimeToTimeStamp(DateTime time)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (time.ToUniversalTime() - unixStart).Ticks;
            return (int)((double)unixTimeStampInTicks / TimeSpan.TicksPerSecond);

        }

        public static DateTime TimeStampToDateTime(int timeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(timeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}
