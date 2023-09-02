using System;

namespace ProjectCopyServer.Common;

public static class TimeHelper
{
    
    /// var theTime = new DateTime().FromTimeStamp(1693668836851)
    public static DateTime FromTimeStamp(this DateTime _, long timeStampMillis)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
            .AddMilliseconds(timeStampMillis);
    }
    
    /// var utcString = DateTime.UtcNow.ToUtcString()
    public static string ToUtcString(this DateTime dateTime)
    {
        return dateTime.ToString("o");
    }
    
    /// var milliSeconds = DateTime.UtcNow.ToUtcMilliSeconds()
    public static long ToUtcMilliSeconds(this DateTime dateTime)
    {
        return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
    }
    
    // var seconds = DateTime.UtcNow.ToUtcSeconds()
    public static long ToUtcSeconds(this DateTime dateTime)
    {
        return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
    }
    
}