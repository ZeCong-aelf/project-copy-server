using System;

namespace ProjectCopyServer.Common;

public static class TimeHelper
{
    public static DateTime FromTimeStamp(this DateTime _, long timeStampMillis)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
            .AddMilliseconds(timeStampMillis);
    }
    
    public static string ToUtcString(this DateTime dateTime)
    {
        return dateTime.ToString("o");
    }
    
    public static long ToUtcMilliSeconds(this DateTime dateTime)
    {
        return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
    }
    
    public static long ToUtcSeconds(this DateTime dateTime)
    {
        return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
    }
    
}