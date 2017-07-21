using Android.Content;
using Android.Widget;
using System.Collections.Generic;

namespace CourtCalculator.Utility_Classes
{
    internal class Validation
    {
        public static bool isPresent(EditText tb, string message, Context c)
        {
            string courtCost = tb.Text.ToString().Trim();

            if (courtCost == "")
            {
                Toast.MakeText(c, message, ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        public static bool isArrayPresent(List<Offense> offenseList, string message, Context c)
        {
            if (offenseList.Count == 0)
            {
                Toast.MakeText(c, message, ToastLength.Short).Show();
                return false;
            }
            return true;
        }
    }
}