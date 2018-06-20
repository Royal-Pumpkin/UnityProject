using System;
using System.Collections;

public class TimeUtil
{
    public static string pattern = "yyyyMMddHHmmss";
    public static string GetTime()
    {
        return DateTime.Now.ToString(pattern);
    }
    public static bool ValidTime(string time)
    {
        DateTime temp;

        return DateTime.TryParseExact(time, pattern, null, System.Globalization.DateTimeStyles.None, out temp);
    }
    public static string TimeAfterOnHour(string time)
    {
        DateTime temp;
        DateTime.TryParseExact(time, pattern, null, System.Globalization.DateTimeStyles.None, out temp);
        return temp.ToString(pattern);
    }
    
}
