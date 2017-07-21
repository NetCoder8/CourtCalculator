using System;
using System.Globalization;

namespace CourtCalculator
{
    internal class CalcCourtDate
    {
        public static bool IsBusinessDay(DateTime date)
        {
            // Check to see if it's the weekend
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            // Check if New Year's Day
            if (date.Month == 1 && date.Day == 1)
            {
                return false;
            }

            // Check if New Years Court Holiday
            if (date.Month == 12 && date.Day == 31 && date.DayOfWeek == DayOfWeek.Friday)
            {
                return false;
            }

            if (date.Month == 1 && date.Day == 2 && date.DayOfWeek == DayOfWeek.Monday)
            {
                return false;
            }

            // Check if Christmas
            if (date.Month == 12 && date.Day == 25)
            {
                return false;
            }

            // Check for Christmas Court Holiday
            if (date.Month == 12 && date.Day == 24 && date.DayOfWeek == DayOfWeek.Friday)
            {
                return false;
            }

            if (date.Month == 12 && date.Day == 26 && date.DayOfWeek == DayOfWeek.Monday)
            {
                return false;
            }

            // Check for 4th of July
            if (date.Month == 7 && date.Day == 4)
            {
                return false;
            }

            // Check for July 4th court holiday
            if (date.Month == 7 && date.Day == 3 && date.DayOfWeek == DayOfWeek.Friday)
            {
                return false;
            }

            if (date.Month == 7 && date.Day == 5 && date.DayOfWeek == DayOfWeek.Monday)
            {
                return false;
            }

            // Check for Columbus Day
            if (date.Month == 10 && GetWeekNumberOfMonth(date) == 2 && date.DayOfWeek == DayOfWeek.Monday)
            {
                return false;
            }

            // Check Thanksgiving (4th Thursday of November)
            if (date.Month == 11 && GetWeekNumberOfMonth(date) == 4 && date.DayOfWeek == DayOfWeek.Thursday)
            {
                return false;
            }

            // Check for Friday after Thanksgiving
            if (date.Month == 11 && GetWeekNumberOfMonth(date) == 4 && date.DayOfWeek == DayOfWeek.Friday)
            {
                return false;
            }

            // Check for Memorial Day (Last Monday of May)
            if (date.Month == 5 && date.DayOfWeek == DayOfWeek.Monday && date.Day > (31 - 7))
            {
                return false;
            }

            // Check for Labor Day (1st Monday of September)
            if (date.Month == 9 && GetWeekNumberOfMonth(date) == 1 && date.DayOfWeek == DayOfWeek.Monday)
            {
                return false;
            }

            // Check for President's Day (3rd Monday of February)
            if (date.Month == 2 && GetWeekNumberOfMonth(date) == 3 && date.DayOfWeek == DayOfWeek.Monday)
            {
                return false;
            }

            // Check for Veterans day (November 11th)
            if (date.Month == 11 && date.Day == 11)
            {
                return false;
            }

            // check for Veterans Day court holiday
            if (date.Month == 11 && date.Day == 10 && date.DayOfWeek == DayOfWeek.Friday)
            {
                return false;
            }

            if (date.Month == 11 && date.Day == 12 && date.DayOfWeek == DayOfWeek.Monday)
            {
                return false;
            }

            // Check for MLK day (3rd Monday of January)
            if (date.Month == 1 && GetWeekNumberOfMonth(date) == 3 && date.DayOfWeek == DayOfWeek.Monday)
            {
                return false;
            }

            // Check for Arbor Day
            if (date.Month == 4 && date.DayOfWeek == DayOfWeek.Friday && date.Day > (30 - 7))
            {
                return false;
            }

            return true;
        }

        public static DateTime calcCityTueDate(DateTime date)
        {
            DateTime courtDate = new DateTime();
            DateTime theDate = date;

            // Calculate the court date
            if (theDate.DayOfWeek == DayOfWeek.Sunday)
            {
                theDate = theDate.AddDays(30);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Monday)
            {
                theDate = theDate.AddDays(29);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                theDate = theDate.AddDays(35);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                theDate = theDate.AddDays(34);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Thursday)
            {
                theDate = theDate.AddDays(33);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Friday)
            {
                theDate = theDate.AddDays(32);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Saturday)
            {
                theDate = theDate.AddDays(31);
            }

            // checks to see if the date is a business day
            for (int i = 0; i < 4; i++)
            {
                if (CalcCourtDate.IsBusinessDay(theDate))
                {
                    courtDate = theDate;
                }
                else
                {
                    theDate = theDate.AddDays(7);
                }
            }

            return courtDate;
        }

        public static DateTime calcCityThursDate(DateTime date)
        {
            DateTime courtDate = new DateTime();
            DateTime theDate = date;

            // Calculate the court date
            if (theDate.DayOfWeek == DayOfWeek.Sunday)
            {
                theDate = theDate.AddDays(32);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Monday)
            {
                theDate = theDate.AddDays(31);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                theDate = theDate.AddDays(30);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                theDate = theDate.AddDays(29);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Thursday)
            {
                theDate = theDate.AddDays(35);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Friday)
            {
                theDate = theDate.AddDays(34);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Saturday)
            {
                theDate = theDate.AddDays(33);
            }

            // checks to see if the date is a business day
            for (int i = 0; i < 4; i++)
            {
                if (CalcCourtDate.IsBusinessDay(theDate))
                {
                    courtDate = theDate;
                }
                else
                {
                    theDate = theDate.AddDays(7);
                }
            }

            return courtDate;
        }

        public static DateTime calcStateTrafficDateAtoN(DateTime date)
        {
            DateTime courtDate = new DateTime();
            DateTime theDate = date;

            // Calculate the court date
            if (theDate.DayOfWeek == DayOfWeek.Sunday)
            {
                theDate = theDate.AddDays(31);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Monday)
            {
                theDate = theDate.AddDays(30);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                theDate = theDate.AddDays(29);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                theDate = theDate.AddDays(35);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Thursday)
            {
                theDate = theDate.AddDays(34);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Friday)
            {
                theDate = theDate.AddDays(33);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Saturday)
            {
                theDate = theDate.AddDays(32);
            }

            // checks to see if the date is a business day
            for (int i = 0; i < 4; i++)
            {
                if (CalcCourtDate.IsBusinessDay(theDate))
                {
                    courtDate = theDate;
                }
                else
                {
                    theDate = theDate.AddDays(7);
                }
            }

            return courtDate;
        }

        public static DateTime calcStateTrafficDateOtoZ(DateTime date)
        {
            DateTime courtDate = new DateTime();
            DateTime theDate = date;

            // Calculate the court date
            if (theDate.DayOfWeek == DayOfWeek.Sunday)
            {
                theDate = theDate.AddDays(31);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Monday)
            {
                theDate = theDate.AddDays(30);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                theDate = theDate.AddDays(29);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                theDate = theDate.AddDays(35);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Thursday)
            {
                theDate = theDate.AddDays(34);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Friday)
            {
                theDate = theDate.AddDays(33);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Saturday)
            {
                theDate = theDate.AddDays(32);
            }

            // checks to see if the date is a business day
            for (int i = 0; i < 4; i++)
            {
                if (CalcCourtDate.IsBusinessDay(theDate))
                {
                    courtDate = theDate;
                }
                else
                {
                    theDate = theDate.AddDays(7);
                }
            }

            return courtDate;
        }

        public static DateTime calcCityMisdDateAtoE(DateTime date)
        {
            DateTime courtDate = new DateTime();
            DateTime theDate = date;

            // Calculate the court date
            if (theDate.DayOfWeek == DayOfWeek.Sunday)
            {
                theDate = theDate.AddDays(29);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Monday)
            {
                theDate = theDate.AddDays(35);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                theDate = theDate.AddDays(34);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                theDate = theDate.AddDays(33);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Thursday)
            {
                theDate = theDate.AddDays(32);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Friday)
            {
                theDate = theDate.AddDays(31);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Saturday)
            {
                theDate = theDate.AddDays(30);
            }

            // checks to see if the date is a business day
            for (int i = 0; i < 4; i++)
            {
                if (CalcCourtDate.IsBusinessDay(theDate))
                {
                    courtDate = theDate;
                }
                else
                {
                    theDate = theDate.AddDays(7);
                }
            }

            return courtDate;
        }

        public static DateTime calcCityMisdDateFtoK(DateTime date)
        {
            DateTime courtDate = new DateTime();
            DateTime theDate = date;

            // Calculate the court date
            if (theDate.DayOfWeek == DayOfWeek.Sunday)
            {
                theDate = theDate.AddDays(30);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Monday)
            {
                theDate = theDate.AddDays(29);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                theDate = theDate.AddDays(35);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                theDate = theDate.AddDays(34);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Thursday)
            {
                theDate = theDate.AddDays(33);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Friday)
            {
                theDate = theDate.AddDays(32);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Saturday)
            {
                theDate = theDate.AddDays(31);
            }

            // checks to see if the date is a business day
            for (int i = 0; i < 4; i++)
            {
                if (CalcCourtDate.IsBusinessDay(theDate))
                {
                    courtDate = theDate;
                }
                else
                {
                    theDate = theDate.AddDays(7);
                }
            }

            return courtDate;
        }

        public static DateTime calcCityMisdDateLtoP(DateTime date)
        {
            DateTime courtDate = new DateTime();
            DateTime theDate = date;

            // Calculate the court date
            if (theDate.DayOfWeek == DayOfWeek.Sunday)
            {
                theDate = theDate.AddDays(31);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Monday)
            {
                theDate = theDate.AddDays(30);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                theDate = theDate.AddDays(29);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                theDate = theDate.AddDays(35);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Thursday)
            {
                theDate = theDate.AddDays(34);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Friday)
            {
                theDate = theDate.AddDays(33);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Saturday)
            {
                theDate = theDate.AddDays(32);
            }

            // checks to see if the date is a business day
            for (int i = 0; i < 4; i++)
            {
                if (CalcCourtDate.IsBusinessDay(theDate))
                {
                    courtDate = theDate;
                }
                else
                {
                    theDate = theDate.AddDays(7);
                }
            }

            return courtDate;
        }

        public static DateTime calcCityMisdDateQtoT(DateTime date)
        {
            DateTime courtDate = new DateTime();
            DateTime theDate = date;

            // Calculate the court date
            if (theDate.DayOfWeek == DayOfWeek.Sunday)
            {
                theDate = theDate.AddDays(32);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Monday)
            {
                theDate = theDate.AddDays(31);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                theDate = theDate.AddDays(30);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                theDate = theDate.AddDays(29);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Thursday)
            {
                theDate = theDate.AddDays(35);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Friday)
            {
                theDate = theDate.AddDays(34);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Saturday)
            {
                theDate = theDate.AddDays(33);
            }

            // checks to see if the date is a business day
            for (int i = 0; i < 4; i++)
            {
                if (CalcCourtDate.IsBusinessDay(theDate))
                {
                    courtDate = theDate;
                }
                else
                {
                    theDate = theDate.AddDays(7);
                }
            }

            return courtDate;
        }

        public static DateTime calcCityMisdDateUtoZ(DateTime date)
        {
            DateTime courtDate = new DateTime();
            DateTime theDate = date;

            // Calculate the court date
            if (theDate.DayOfWeek == DayOfWeek.Sunday)
            {
                theDate = theDate.AddDays(33);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Monday)
            {
                theDate = theDate.AddDays(32);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                theDate = theDate.AddDays(31);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                theDate = theDate.AddDays(30);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Thursday)
            {
                theDate = theDate.AddDays(29);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Friday)
            {
                theDate = theDate.AddDays(35);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Saturday)
            {
                theDate = theDate.AddDays(34);
            }

            // checks to see if the date is a business day
            for (int i = 0; i < 4; i++)
            {
                if (CalcCourtDate.IsBusinessDay(theDate))
                {
                    courtDate = theDate;
                }
                else
                {
                    theDate = theDate.AddDays(7);
                }
            }

            return courtDate;
        }

        public static DateTime calcStateMisdTue(DateTime date)
        {
            DateTime courtDate = new DateTime();
            DateTime theDate = date;

            // Calculate the court date
            if (theDate.DayOfWeek == DayOfWeek.Sunday)
            {
                theDate = theDate.AddDays(30);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Monday)
            {
                theDate = theDate.AddDays(29);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                theDate = theDate.AddDays(35);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                theDate = theDate.AddDays(34);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Thursday)
            {
                theDate = theDate.AddDays(33);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Friday)
            {
                theDate = theDate.AddDays(32);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Saturday)
            {
                theDate = theDate.AddDays(31);
            }

            // checks to see if the date is a business day
            for (int i = 0; i < 4; i++)
            {
                if (CalcCourtDate.IsBusinessDay(theDate))
                {
                    courtDate = theDate;
                }
                else
                {
                    theDate = theDate.AddDays(7);
                }
            }

            return courtDate;
        }

        public static DateTime calcStateMisdWed(DateTime date)
        {
            DateTime courtDate = new DateTime();
            DateTime theDate = date;

            // Calculate the court date
            if (theDate.DayOfWeek == DayOfWeek.Sunday)
            {
                theDate = theDate.AddDays(31);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Monday)
            {
                theDate = theDate.AddDays(30);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                theDate = theDate.AddDays(29);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                theDate = theDate.AddDays(35);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Thursday)
            {
                theDate = theDate.AddDays(34);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Friday)
            {
                theDate = theDate.AddDays(33);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Saturday)
            {
                theDate = theDate.AddDays(32);
            }

            // checks to see if the date is a business day
            for (int i = 0; i < 4; i++)
            {
                if (CalcCourtDate.IsBusinessDay(theDate))
                {
                    courtDate = theDate;
                }
                else
                {
                    theDate = theDate.AddDays(7);
                }
            }

            return courtDate;
        }

        public static DateTime calcStateMisdThurs(DateTime date)
        {
            DateTime courtDate = new DateTime();
            DateTime theDate = date;

            // Calculate the court date
            if (theDate.DayOfWeek == DayOfWeek.Sunday)
            {
                theDate = theDate.AddDays(32);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Monday)
            {
                theDate = theDate.AddDays(31);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                theDate = theDate.AddDays(30);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                theDate = theDate.AddDays(29);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Thursday)
            {
                theDate = theDate.AddDays(35);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Friday)
            {
                theDate = theDate.AddDays(34);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Saturday)
            {
                theDate = theDate.AddDays(33);
            }

            // checks to see if the date is a business day
            for (int i = 0; i < 4; i++)
            {
                if (CalcCourtDate.IsBusinessDay(theDate))
                {
                    courtDate = theDate;
                }
                else
                {
                    theDate = theDate.AddDays(7);
                }
            }

            return courtDate;
        }

        public static DateTime calcStateMisdFri(DateTime date)
        {
            DateTime courtDate = new DateTime();
            DateTime theDate = date;

            // Calculate the court date
            if (theDate.DayOfWeek == DayOfWeek.Sunday)
            {
                theDate = theDate.AddDays(33);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Monday)
            {
                theDate = theDate.AddDays(32);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Tuesday)
            {
                theDate = theDate.AddDays(31);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Wednesday)
            {
                theDate = theDate.AddDays(30);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Thursday)
            {
                theDate = theDate.AddDays(29);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Friday)
            {
                theDate = theDate.AddDays(35);
            }
            else if (theDate.DayOfWeek == DayOfWeek.Saturday)
            {
                theDate = theDate.AddDays(34);
            }

            // checks to see if the date is a business day
            for (int i = 0; i < 4; i++)
            {
                if (CalcCourtDate.IsBusinessDay(theDate))
                {
                    courtDate = theDate;
                }
                else
                {
                    theDate = theDate.AddDays(7);
                }
            }

            return courtDate;
        }

        public static int GetWeekNumberOfMonth(DateTime date)
        {
            //date = date.Date;
            //DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            //DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            //if (firstMonthMonday > date)
            //{
            //    firstMonthDay = firstMonthDay.AddMonths(-1);
            //    firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            //}
            //return (date - firstMonthMonday).Days / 7 + 1;
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);

            while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(1);

            return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }
    }
}