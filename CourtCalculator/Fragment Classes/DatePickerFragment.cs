using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace CourtCalculator
{
    internal class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
    {
        // Tag can be any string of your choice
        public static readonly string TAG = "X:" + typeof(DatePickerDialog).Name.ToUpper();

        // Initialize this value to prevent NullReferenceException
        private Action<DateTime> dateSelectedHandler = delegate { };

        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag.dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity, this, currently.Year, currently.Month - 1, currently.Day);

            return dialog;
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            DateTime selectedDate = new DateTime(year, month + 1, dayOfMonth);
            dateSelectedHandler(selectedDate);
        }
    }
}