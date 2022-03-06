using System;
using System.Text;
using UnityEngine;

namespace DY
{
    public class Time : MonoBehaviour
    {
        /// <summary>
        /// 다음 날 00:00 반환 -> Datetime
        /// </summary>
        /// <returns></returns>
        public static DateTime GetTomorrow_Datetime()
        {
            DateTime temp_DT = DateTime.Now.AddDays(1);
            string temp_str = temp_DT.Year.ToString() + "/" + temp_DT.Month.ToString() + "/" + (temp_DT.Day).ToString() + " 00:00:00";

            return Convert.ToDateTime(temp_str);
        }

        /// <summary>
        /// 다음 날 (00:00)까지 남은 시간 반환 -> double
        /// </summary>
        /// <returns></returns>
        public static double GetTomorrow_TotalSecondsRemaining()
        {
            DateTime tmep_DT_tomorrow = GetTomorrow_Datetime();

            TimeSpan span = tmep_DT_tomorrow - DateTime.Now;

            return span.TotalSeconds;
        }

        /// <summary>
        /// 받은 DateTime에서 day만 받은 plus만큼 증가 -> Datetime
        /// </summary>
        /// <returns></returns>
        public static DateTime GetPlusDay_Datetime(DateTime DT, int plus)
        {
            DateTime temp_DT = DT.AddDays(plus);

            return temp_DT;
        }

        /// <summary>
        /// 받은 DateTime(미래)까지 남은 시간 반환 -> double
        /// </summary>
        /// <returns></returns>
        public static double Get_TotalSecondsRemaining(DateTime DT)
        {
            TimeSpan span = DT - DateTime.Now;

            return span.TotalSeconds;
        }

        /// <summary>
        /// 현재 시간에서 1시간 더한 매 정각 반환(현 시간이 3시면 4시반환) - Datetime
        /// </summary>
        /// <returns></returns>
        public static DateTime GetHour_Datetime()
        {
            DateTime temp_DT = DateTime.Now.AddHours(1);
            string temp_str = temp_DT.Year.ToString() + "/" + temp_DT.Month.ToString() + "/" + temp_DT.Day.ToString() + " " + temp_DT.Hour.ToString() + ":00:00";

            return Convert.ToDateTime(temp_str);
        }

        /// <summary>
        /// 현재 시간에서 1시간 더한 매 정각(현 시간이 3시면 4시반환)까지 남은 시간 반환 - double
        /// </summary>
        /// <returns></returns>
        public static double GetHour_TotalSecondsRemaining()
        {
            DateTime tmep_DT_tomorrow = GetHour_Datetime();

            TimeSpan span = tmep_DT_tomorrow - DateTime.Now;

            return span.TotalSeconds;
        }

        /// <summary>
        /// string or int 형식의 시간(초)을 string으로 반환
        /// </summary>
        /// <param name="time"></param>
        /// <param name="plusZero"></param>
        /// <param name="plusSecond"></param>
        /// <param name="colon"></param>
        /// <returns></returns>
        public static string TimeToString(object time, bool plusZero = false, bool plusSecond = false, bool colon = false)
        {
            double _time = 0;
            if (!double.TryParse(time.ToString(), out _time))
                Debug.Parse("Time", time.ToString());

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
            {
                minute = "0";
                second = ((int)_time).ToString();
            }

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

        /// <summary>
        /// 일일 출석 보상 로그인 후 갱신 체크 - 마지막 로그인 날 보다 하루가 더 지났을 경우 true
        /// </summary>
        /// <param name="nowday"></param>
        /// <param name="oldDay"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 일일 출석 보상 Update()에서 다음 날 갱신 체크 - 로그인 후 GetTomorrow로 받아온 날과 같아지면
        /// </summary>
        /// <param name="nowday"></param>
        /// <param name="tomorrow"></param>
        /// <returns></returns>
        public static bool IsDailyRewardUpdateCheck(DateTime nowday, DateTime tomorrow)
        {
            TimeSpan span = nowday - tomorrow;
            if (span.TotalDays >= 0) return true;
            else return false;
        }

        /// <summary>
        /// 광고 보상 로그인 후 갱신 체크 - 마지막 로그인 날 보다 한시간이 더 지났을 경우 true 
        /// </summary>
        /// <param name="nowHour"></param>
        /// <param name="oldHour"></param>
        /// <returns></returns>
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
    }
}