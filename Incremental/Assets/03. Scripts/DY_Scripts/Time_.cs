using System;
using System.Text;
using UnityEngine;

public class Time_ : MonoBehaviour
{
    // 다음 날 00:00 반환 - Datetime
    public static DateTime GetTomorrow_Datetime()
    {
        DateTime temp_DT = DateTime.Now.AddDays(1);
        string temp_str = temp_DT.Year.ToString() + "/" + temp_DT.Month.ToString() + "/" + (temp_DT.Day).ToString() + " 00:00:00";

        return Convert.ToDateTime(temp_str);
    }

    // 다음 날 (00:00)까지 남은 시간 반환 -> double
    public static double GetTomorrow_TotalSeconds()
    {
        DateTime temp_DT = DateTime.Now.AddDays(1);
        string temp_str = temp_DT.Year.ToString() + "/" + temp_DT.Month.ToString() + "/" + (temp_DT.Day).ToString() + " 00:00:00";
        DateTime tmep_DT_tomorrow = Convert.ToDateTime(temp_str);

        TimeSpan span = tmep_DT_tomorrow - DateTime.Now;

        return span.TotalSeconds;
    }

    // 현재 시간에서 매 정각 반환(현 시간이 3시면 4시반환) - Datetime
    public static DateTime GetHour_Datetime()
    {
        DateTime temp_DT = DateTime.Now.AddHours(1);
        string temp_str = temp_DT.Year.ToString() + "/" + temp_DT.Month.ToString() + "/" + (temp_DT.Day).ToString() + " " + temp_DT.Hour.ToString() + ":00:00";

        return Convert.ToDateTime(temp_str);
    }

    // 현재 시간에서 매 정각(현 시간이 3시면 4시반환)까지 남은 시간 반환 - double
    public static double GetHour_TotalSeconds()
    {
        DateTime temp_DT = DateTime.Now.AddHours(1);
        string temp_str = temp_DT.Year.ToString() + "/" + temp_DT.Month.ToString() + "/" + (temp_DT.Day).ToString() + " " + temp_DT.Hour.ToString() + ":00:00";
        DateTime tmep_DT_tomorrow = Convert.ToDateTime(temp_str);

        TimeSpan span = tmep_DT_tomorrow - DateTime.Now;

        return span.TotalSeconds;
    }

    // string or int 형식의 시간(초)을 string으로 반환
    public static string TimeToString(object time, bool plusZero = false, bool plusSecond = false, bool colon = false)
    {
        double _time = 0;
        if (!double.TryParse(time.ToString(), out _time))
            DebugError.Parse("Time_", time.ToString());

        StringBuilder temp = new StringBuilder();

        string hour = string.Empty;
        string minute = string.Empty;
        string second = string.Empty;

        if (_time >= 3600)
        {
            hour = ((int)_time / 3600).ToString();
            minute = (((int)_time % 3600) / 60).ToString();
            second = (((int)_time % 3600) % 60).ToString();
        }
        else if (_time < 3600 && _time >= 60)
        {
            minute = ((int)_time / 60).ToString();
            second = ((int)_time % 60).ToString();
        }
        else
            second = ((int)_time).ToString();

        if (plusZero)
        {
            if (hour.Length == 1) hour = "0" + hour;
            if (minute.Length == 1) minute = "0" + minute;
            if (second.Length == 1) second = "0" + second;
        }

        if (colon)
        {
            if (hour.Length != 0) temp.Append(hour + ":");
            if (minute.Length != 0) temp.Append(minute + ":");

            if (plusSecond || (hour.Length == 0 && minute.Length == 0))
                temp.Append(second);
        }
        else
        {
            if (hour.Length != 0) temp.Append(hour + "시간 ");
            if (minute.Length != 0) temp.Append(minute + "분");

            if (plusSecond || (hour.Length == 0 && minute.Length == 0))
                temp.Append(" " + second + "초");
        }

        return temp.ToString();
    }

    // 일일 출석 보상 로그인 후 갱신 체크 - 마지막 로그인 날 보다 하루가 더 지났을 경우 true
    public static bool IsDailyRewardUpdateLogin(DateTime nowday, DateTime oldDay)
    {
        TimeSpan span = nowday - oldDay;

        // oldDay보다 span은 당연히 +여야함, 날이 다른 경우에 span이 + 이면 하루 이상 지난 것
        if (span.TotalDays >= 0 && nowday.Day != oldDay.Day)
            return true;

        // oldDay보다 span은 당연히 +여야함, 날이 같은 경우에 span이 +10이상 이면 한달 이상 지난 것
        if (nowday.Day == oldDay.Day && span.TotalDays >= 10)
            return true;

        return false;
    }

    // 일일 출석 보상 Update()에서 다음 날 갱신 체크 - 로그인 후 GetTomorrow로 받아온 날과 같아지면
    public static bool IsDailyRewardUpdateCheck(DateTime nowday, DateTime tomorrow)
    {
        TimeSpan span = nowday - tomorrow;
        if (span.TotalDays >= 0 && nowday.Day == tomorrow.Day) return true;
        else return false;
    }

    // 광고 보상 로그인 후 갱신 체크 - 마지막 로그인 날 보다 한시간이 더 지났을 경우 true
    public static bool IsADRewardUpdateLogin(DateTime nowHour, DateTime oldHour)
    {
        TimeSpan span = nowHour - oldHour;

        // oldHour보다 span은 당연히 +여야함, 시간이 다른 경우에 span이 + 이면 한시간 이상 지난 것
        if (span.TotalHours >= 0 && nowHour.Hour != oldHour.Hour)
            return true;

        // oldHour보다 span은 당연히 +여야함, 시간이 같은 경우에 span이 +10이상 이면 한시간 이상 지난 것
        if (nowHour.Hour == oldHour.Hour && span.TotalHours >= 10)
            return true;

        return false;
    }

    // 광고 보상 Update()에서 정각 갱신 체크 - 로그인 후 GetHour로 받아온 시간과 같아지면
    public static bool IsADRewardUpdateCheck(DateTime nowHour, DateTime hour)
    {
        TimeSpan span = nowHour - hour;
        if (span.TotalHours >= 0 && nowHour.Hour == hour.Hour) return true;
        else return false;
    }

    // 일일 퀘스트 남은 시간 string 반환
    public static string update_Time(DateTime tomorrow, DateTime today, bool plusZero = false)
    {
        string temp = string.Empty;
        TimeSpan span = tomorrow - today;

        temp = TimeToString(span.TotalSeconds, plusZero: true, plusSecond: false);

        return temp;
    }
}