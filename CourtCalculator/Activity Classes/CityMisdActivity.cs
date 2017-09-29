using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;

namespace CourtCalculator
{
    [Activity(Label = "City Misdemeanor Court Dates")]
    public class CityMisdActivity : AppCompatActivity
    {
        private TextView monday, tuesday, wednesday, thursday, friday;
        private DateTime selectedDate;
        private Button mainMenu;
        private int selectedMonth;
        private int selectedDay;
        private int selectedYear;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.CityMisdActivity);

            selectedMonth = Intent.GetIntExtra("Month", 0);
            selectedDay = Intent.GetIntExtra("Day", 0);
            selectedYear = Intent.GetIntExtra("Year", 0);

            selectedDate = new DateTime(selectedYear, selectedMonth, selectedDay);

            monday = FindViewById<TextView>(Resource.Id.CityMisdMon);
            tuesday = FindViewById<TextView>(Resource.Id.CityMisdTues);
            wednesday = FindViewById<TextView>(Resource.Id.CityMisdWed);
            thursday = FindViewById<TextView>(Resource.Id.CityMisdThurs);
            friday = FindViewById<TextView>(Resource.Id.CityMisdFri);

            mainMenu = FindViewById<Button>(Resource.Id.btnMainMenu);

            monday.Text = CalcCourtDate.calcCityMisdDateAtoE(selectedDate).ToShortDateString();
            tuesday.Text = CalcCourtDate.calcCityMisdDateFtoK(selectedDate).ToShortDateString();
            wednesday.Text = CalcCourtDate.calcCityMisdDateLtoP(selectedDate).ToShortDateString();
            thursday.Text = CalcCourtDate.calcCityMisdDateQtoT(selectedDate).ToShortDateString();
            friday.Text = CalcCourtDate.calcCityMisdDateUtoZ(selectedDate).ToShortDateString();

            mainMenu.Click += MainMenu_Click;
        }

        private void MainMenu_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop);
            StartActivity(intent);
        }
    }
}