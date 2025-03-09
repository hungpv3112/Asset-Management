using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public static class DateTimeUtils
{
    public readonly static DateTime BASE_DATE = new DateTime(1970, 1, 1, 0, 0, 0);
    public static DateTime Now
    {
        get
        {
            return DateTime.Now;
        }
    }

    public static DateTime UtcNow
    {
        get
        {
            return DateTime.UtcNow;
        }
    }

    public static DateTime NextDay()
    {
        var nextDay = Now.AddDays(1);
        return new DateTime(nextDay.Year, nextDay.Month, nextDay.Day);
    }

    public static DateTime NextWeek(DateTime date)
    {
        var dayOfWeek = date.DayOfWeek;
        DateTime monDayNextWeek = date;
        switch (dayOfWeek)
        {
            case DayOfWeek.Sunday:
                monDayNextWeek = monDayNextWeek.AddDays(1);
                break;
            case DayOfWeek.Monday:
                monDayNextWeek = monDayNextWeek.AddDays(7);
                break;
            case DayOfWeek.Tuesday:
                monDayNextWeek = monDayNextWeek.AddDays(6);
                break;
            case DayOfWeek.Wednesday:
                monDayNextWeek = monDayNextWeek.AddDays(5);
                break;
            case DayOfWeek.Thursday:
                monDayNextWeek = monDayNextWeek.AddDays(4);
                break;
            case DayOfWeek.Friday:
                monDayNextWeek = monDayNextWeek.AddDays(3);
                break;
            case DayOfWeek.Saturday:
                monDayNextWeek = monDayNextWeek.AddDays(2);
                break;
        }
        monDayNextWeek = ResetTimeTo0H(monDayNextWeek);

        return monDayNextWeek;
    }


    public static bool IsLastWeek(DateTime dateTime)
    {
        var dayOfWeek = dateTime.DayOfWeek;
        DateTime monDayNextWeek = dateTime;
        switch (dayOfWeek)
        {
            case DayOfWeek.Sunday:
                monDayNextWeek = monDayNextWeek.AddDays(1);
                break;
            case DayOfWeek.Monday:
                monDayNextWeek = monDayNextWeek.AddDays(7);
                break;
            case DayOfWeek.Tuesday:
                monDayNextWeek = monDayNextWeek.AddDays(6);
                break;
            case DayOfWeek.Wednesday:
                monDayNextWeek = monDayNextWeek.AddDays(5);
                break;
            case DayOfWeek.Thursday:
                monDayNextWeek = monDayNextWeek.AddDays(4);
                break;
            case DayOfWeek.Friday:
                monDayNextWeek = monDayNextWeek.AddDays(3);
                break;
            case DayOfWeek.Saturday:
                monDayNextWeek = monDayNextWeek.AddDays(2);
                break;
        }
        monDayNextWeek = ResetTimeTo0H(monDayNextWeek);

        return monDayNextWeek <= Now;
    }

    public static bool IsLastWeekUTC(DateTime dateTime)
    {
        var dayOfWeek = dateTime.DayOfWeek;
        DateTime monDayNextWeek = dateTime;
        switch (dayOfWeek)
        {
            case DayOfWeek.Sunday:
                monDayNextWeek = monDayNextWeek.AddDays(1);
                break;
            case DayOfWeek.Monday:
                monDayNextWeek = monDayNextWeek.AddDays(7);
                break;
            case DayOfWeek.Tuesday:
                monDayNextWeek = monDayNextWeek.AddDays(6);
                break;
            case DayOfWeek.Wednesday:
                monDayNextWeek = monDayNextWeek.AddDays(5);
                break;
            case DayOfWeek.Thursday:
                monDayNextWeek = monDayNextWeek.AddDays(4);
                break;
            case DayOfWeek.Friday:
                monDayNextWeek = monDayNextWeek.AddDays(3);
                break;
            case DayOfWeek.Saturday:
                monDayNextWeek = monDayNextWeek.AddDays(2);
                break;
        }
        monDayNextWeek = ResetTimeTo0H(monDayNextWeek);

        return monDayNextWeek <= UtcNow;
    }

    public static DateTime ResetTimeTo0H(DateTime dateTime)
    {
        dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        return dateTime;
    }

    static DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public static DateTime GetDateTimeFromMiniSecond(long miniSecond)
    {

        DateTime date = start.AddMilliseconds(miniSecond);
        return date;
    }

    public static DateTime GetDateTimeFromTick(long ticks)
    {
        return new DateTime(ticks);
    }

    public static TimeSpan GetTimeSpanFromNowToNextTime(long nextTime)
    {
        var dateTime = GetDateTimeFromMiniSecond(nextTime);
        return GetTimeSpanFromNowToNextTime(dateTime);
    }

    public static TimeSpan GetTimeSpanFromNowToNextTime(DateTime nextTime)
    {
        return nextTime - Now;
    }

    public static TimeSpan GetTimeSpanFromTimeToNextTime(long time, long nextTime)
    {
        var datetime = GetDateTimeFromMiniSecond(time);
        var dateNextTime = GetDateTimeFromMiniSecond(nextTime);

        return GetTimeSpanFromTimeToNextTime(datetime, dateNextTime);
    }

    public static TimeSpan GetTimeSpanFromTimeToNextTime(DateTime time, DateTime nextTime)
    {
        return nextTime - time;
    }

    public static DateTime GetNextDay()
    {
        var nextDay = Now.AddDays(1);
        return new DateTime(nextDay.Year, nextDay.Month, nextDay.Day);
    }

    public static DateTime GetNextDay(DateTime date)
    {
        var nextDay = date.AddDays(1);
        return new DateTime(nextDay.Year, nextDay.Month, nextDay.Day);
    }

    public static DateTime Get8HUtcToday()
    {
        var date = UtcNow.Date.AddHours(8);
        return date;
    }

    public static DateTime GetNextWeek(DateTime date)
    {
        var dayOfWeek = date.DayOfWeek;
        DateTime monDayNextWeek = date;
        switch (dayOfWeek)
        {
            case DayOfWeek.Sunday:
                monDayNextWeek = monDayNextWeek.AddDays(1);
                break;
            case DayOfWeek.Monday:
                monDayNextWeek = monDayNextWeek.AddDays(7);
                break;
            case DayOfWeek.Tuesday:
                monDayNextWeek = monDayNextWeek.AddDays(6);
                break;
            case DayOfWeek.Wednesday:
                monDayNextWeek = monDayNextWeek.AddDays(5);
                break;
            case DayOfWeek.Thursday:
                monDayNextWeek = monDayNextWeek.AddDays(4);
                break;
            case DayOfWeek.Friday:
                monDayNextWeek = monDayNextWeek.AddDays(3);
                break;
            case DayOfWeek.Saturday:
                monDayNextWeek = monDayNextWeek.AddDays(2);
                break;
        }
        monDayNextWeek = ResetTimeTo0H(monDayNextWeek);

        return monDayNextWeek;
    }

    public static DateTime GetMondayOfWeek(DateTime date)
    {
        var dayOfWeek = date.DayOfWeek;
        DateTime monDay = date;
        switch (dayOfWeek)
        {
            case DayOfWeek.Sunday:
                monDay = monDay.AddDays(-6);
                break;
            case DayOfWeek.Monday:
                break;
            case DayOfWeek.Tuesday:
                monDay = monDay.AddDays(-1);
                break;
            case DayOfWeek.Wednesday:
                monDay = monDay.AddDays(-2);
                break;
            case DayOfWeek.Thursday:
                monDay = monDay.AddDays(-3);
                break;
            case DayOfWeek.Friday:
                monDay = monDay.AddDays(-4);
                break;
            case DayOfWeek.Saturday:
                monDay = monDay.AddDays(-5);
                break;
        }
        monDay = ResetTimeTo0H(monDay);

        return monDay;
    }

    public static DateTime GetNextMonth(DateTime date)
    {
        int month = date.Month;
        int year = date.Year;
        if (month == 12)
        {
            year++;
            month = 1;
        }
        else
        {
            month++;
        }
        return new DateTime(year, month, 1);
    }

    public static TimeSpan GetTimeSpanFromMiliSecond(long miliseconds)
    {
        return TimeSpan.FromMilliseconds(miliseconds);
    }

    public static TimeSpan GetTimeSpanFromSecond(long seconds)
    {
        return TimeSpan.FromSeconds(seconds);
    }

    public static long GetMiliSecondFromTimeSpan(TimeSpan timeSpan)
    {
        return (long)timeSpan.TotalMilliseconds;
    }

    public static long GetMiliSecondFromDateTime(DateTime datetime)
    {
        return (long)(datetime - BASE_DATE).TotalMilliseconds;
    }


    public static long GetMiliSecondFromDateTimeNow()
    {
        return (long)(Now - BASE_DATE).TotalMilliseconds;
    }

    public static long GetMiliSecondFromDateTimeUtcNow()
    {
        return (long)(UtcNow - BASE_DATE).TotalMilliseconds;
    }

    public static DateTime GetDateTimeFromMiliSecond(double miliseconds)
    {
        return BASE_DATE.Add(TimeSpan.FromMilliseconds(miliseconds));
    }

    public static bool IsDatesInSameWeek(DateTime preDate, DateTime currentDate)
    {
        if (preDate.Date == currentDate.Date)
            return true;

        int dayOfWeekPreDate = (preDate.DayOfWeek == DayOfWeek.Sunday) ? 7 : (int)preDate.DayOfWeek;
        int dayOfWeekCurrentDate = (currentDate.DayOfWeek == DayOfWeek.Sunday) ? 7 : (int)currentDate.DayOfWeek;

        if (dayOfWeekPreDate > dayOfWeekCurrentDate)
            return false;
        else if (currentDate.Date.Subtract(preDate.Date).Days >= Enum.GetNames(typeof(DayOfWeek)).Length)
            return false;

        return true;
    }

    public static bool IsSameDay(long time1, long time2)
    {
        var dateTime1 = GetDateTimeFromMiliSecond(time1);
        var dateTime2 = GetDateTimeFromMiliSecond(time2);
        return dateTime1.Year == dateTime2.Year && dateTime1.DayOfYear == dateTime2.DayOfYear;
    }

    public static bool IsSameDay(DateTime dateTime1, DateTime dateTime2)
    {
        return dateTime1.Year == dateTime2.Year && dateTime1.DayOfYear == dateTime2.DayOfYear;
    }

    public static int CompareDay(long time1, long time2)
    {
        var dateTime1 = GetDateTimeFromMiliSecond(time1);
        var dateTime2 = GetDateTimeFromMiliSecond(time2);
        return CompareDay(dateTime1, dateTime2);
    }

    public static int CompareDay(DateTime dateTime1, DateTime dateTime2)
    {
        if (dateTime1.Year != dateTime2.Year)
        {
            return dateTime1.Year.CompareTo(dateTime2.Year);
        }

        return dateTime1.DayOfYear.CompareTo(dateTime2.DayOfYear);
    }

    public static int GetNumDayFromBaseTime(DateTime dateTime)
    {
        return GetNumDayFrombaseTime(dateTime.Year) + dateTime.DayOfYear;
    }

    public static int GetNumDayFrombaseTime(int year)
    {
        int day = 0;

        for (int i = 1971; i <= year; i++)
        {
            day += i % 4 == 0 ? 361 : 360;
        }

        return day;
    }

    public static string GetTimespanInThePassFromNow(long time)
    {
        DateTime currentDate = start.AddMilliseconds(time).ToLocalTime();
        return GetTimespanInThePassFromNow(currentDate);
    }

    public static string GetTimespanInThePassFromNow(DateTime dateTime)
    {
        if (dateTime > DateTime.Now)
        {
            var day1 = GetNumDayFromBaseTime(dateTime);
            var day2 = GetNumDayFromBaseTime(DateTime.Now);
            if (day1 > day2)
            {
                return dateTime.ToString();
            }
        }
        StringBuilder sbTime = new StringBuilder();
        DateTime currentDate = dateTime;
        DateTime now = DateTime.Now;

        if (currentDate.Year < now.Year)
        {
            return currentDate.Year.ToString();
        }
        else
        {
            void AppenHour()
            {
                if (currentDate.Hour < 10)
                {
                    sbTime.Append(0);
                }
                sbTime.Append(currentDate.Hour);
                sbTime.Append(":");
                if (currentDate.Minute < 10)
                {
                    sbTime.Append(0);
                }
                sbTime.Append(currentDate.Minute);
            }
            if (currentDate.DayOfYear <= now.DayOfYear - 7)
            {
                if (currentDate.Month < 10)
                {
                    sbTime.Append(0);
                }
                sbTime.Append(currentDate.Month);
                sbTime.Append("/");
                if (currentDate.Day < 10)
                {
                    sbTime.Append(0);
                }
                sbTime.Append(currentDate.Day);

                sbTime.Append(" ");

                AppenHour();
                return sbTime.ToString();

            }
            else
            {
                if (currentDate.DayOfYear == now.DayOfYear)
                {
                    AppenHour();
                    return sbTime.ToString();
                }
                else
                {
                    sbTime.Append(currentDate.DayOfWeek.ToString().Substring(0, 3));
                    sbTime.Append(" ");

                    AppenHour();
                    return sbTime.ToString();
                }
            }
        }
    }

    public static string GetTimeMonthYear(long time)
    {
        var dateTime = GetDateTimeFromMiliSecond(time);
        return dateTime.Month < 10 ? $"0{dateTime.Month} / {dateTime.Year}" :  $"{dateTime.Month} / {dateTime.Year}";
    }


    public static DateTime GetDateTimeForDayOfWeek(DayOfWeek dayOfWeek)
    {
        DateTime now = Now;
        // Lấy ngày trong tuần hiện tại (0 = Sunday, 1 = Monday, ..., 6 = Saturday)
        int currentDayOfWeek = (int)now.DayOfWeek;
        int desiredDayOfWeek = (int)dayOfWeek;

        currentDayOfWeek = currentDayOfWeek == 0 ? 6 : currentDayOfWeek - 1;
        desiredDayOfWeek = desiredDayOfWeek == 0 ? 6 : desiredDayOfWeek - 1;

        // Tính toán số ngày cần thêm hoặc bớt để đạt được ngày mong muốn
        int daysDifference = desiredDayOfWeek - currentDayOfWeek;

        return now.AddDays(daysDifference).Date;
    }

}
