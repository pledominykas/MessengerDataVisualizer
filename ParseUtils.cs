using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerDataVisualizer
{
    /// <summary>
    /// Class containing utility functions for parsing and converting JSON values
    /// </summary>
    static class ParseUtils
    {
        public static DateTime GetDateTimeFromTimeStamp(long timeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(timeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static DateTime GetDateTimeFromTimeStampMs(long timeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(timeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
