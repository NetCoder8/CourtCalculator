using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;

namespace CourtCalculator
{
    [Activity(Label = "City Traffic Court Dates")]
    public class CityTrafficActivity : AppCompatActivity
    {
        private TextView tuesday, thursday;
        private DateTime selectedDate;
        private Button mainMenu;
        private int selectedMonth;
        private int selectedDay;
        private int selectedYear;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.CityTrafficActivity);

            selectedMonth = Intent.GetIntExtra("Month", 0);
            selectedDay = Intent.GetIntExtra("Day", 0);
            selectedYear = Intent.GetIntExtra("Year", 0);

            selectedDate = new DateTime(selectedYear, selectedMonth, selectedDay);

            tuesday = FindViewById<TextView>(Resource.Id.CityTrafficTues);
            thursday = FindViewById<TextView>(Resource.Id.CityTrafficThurs);
            mainMenu = FindViewById<Button>(Resource.Id.btnMainMenu);

            tuesday.Text = CalcCourtDate.calcCityTueDate(selectedDate).ToShortDateString();
            thursday.Text = CalcCourtDate.calcCityThursDate(selectedDate).ToShortDateString();

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