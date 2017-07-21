using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;

namespace CourtCalculator
{
    [Activity(Label = "Calc Activity")]
    public class CalcActivity : AppCompatActivity
    {
        private Button btnSelectDate, btnMainMenu, btnGo;
        private RadioButton radCityTraffic, radCityMisd, radStateTraffic, radStateMisd;
        private DateTime dateSelected;

        //public readonly string YEAR = "Year";
        //public readonly string MONTH = "Month";
        //public readonly string DAY = "Day";
        private int selectedYear = 0;

        private int selectedMonth = 0;
        private int selectedDay = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.CalcActivity);

            btnSelectDate = FindViewById<Button>(Resource.Id.btnSelectDate);
            btnMainMenu = FindViewById<Button>(Resource.Id.btnMainMenu);
            btnGo = FindViewById<Button>(Resource.Id.btnGo);
            radCityTraffic = FindViewById<RadioButton>(Resource.Id.radCityTraffic);
            radCityMisd = FindViewById<RadioButton>(Resource.Id.radCityMisd);
            radStateTraffic = FindViewById<RadioButton>(Resource.Id.radStateTraffic);
            radStateMisd = FindViewById<RadioButton>(Resource.Id.radStateMisd);

            btnSelectDate.Click += BtnSelectDate_Click;
            btnMainMenu.Click += BtnMainMenu_Click;
            btnGo.Click += BtnGo_Click;
        }

        // Button click to get a court date
        private void BtnGo_Click(object sender, EventArgs e)
        {
            if (selectedYear == 0 || selectedMonth == 0 || selectedDay == 0)
            {
                Toast.MakeText(this, "No offense Date chosen. Please choose an offense date", ToastLength.Short).Show();
            }
            else if (radCityMisd.Checked)
            {
                var intent = new Intent(this, typeof(CityMisdActivity));
                intent.PutExtra("Month", selectedMonth);
                intent.PutExtra("Day", selectedDay);
                intent.PutExtra("Year", selectedYear);
                StartActivity(intent);
            }
            else if (radCityTraffic.Checked)
            {
                var intent = new Intent(this, typeof(CityTrafficActivity));
                intent.PutExtra("Month", selectedMonth);
                intent.PutExtra("Day", selectedDay);
                intent.PutExtra("Year", selectedYear);
                StartActivity(intent);
            }
            else if (radStateMisd.Checked)
            {
                var intent = new Intent(this, typeof(StateMisdActivity));
                intent.PutExtra("Month", selectedMonth);
                intent.PutExtra("Day", selectedDay);
                intent.PutExtra("Year", selectedYear);
                StartActivity(intent);
            }
            else if (radStateTraffic.Checked)
            {
                var intent = new Intent(this, typeof(StateTrafficActivity));
                intent.PutExtra("Month", selectedMonth);
                intent.PutExtra("Day", selectedDay);
                intent.PutExtra("Year", selectedYear);
                StartActivity(intent);
            }
        }

        // Button click to return to the main menu
        private void BtnMainMenu_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        // Button click to display date picker dialog
        private void BtnSelectDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                dateSelected = time;
                selectedDay = time.Day;
                selectedMonth = time.Month;
                selectedYear = time.Year;

                Toast.MakeText(this, "Date of offense selected: " + dateSelected.ToShortDateString(), ToastLength.Short).Show();
            });

            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
    }
}