using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;

namespace CourtCalculator
{
    [Activity(Label = "State Misdemeanor Court Dates")]
    public class StateMisdActivity : AppCompatActivity
    {
        private TextView tuesday, wednesday, thursday, friday;
        private DateTime selectedDate;
        private Button mainMenu;
        private int selectedMonth;
        private int selectedDay;
        private int selectedYear;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.StateMisdActivity);

            selectedMonth = Intent.GetIntExtra("Month", 0);
            selectedDay = Intent.GetIntExtra("Day", 0);
            selectedYear = Intent.GetIntExtra("Year", 0);

            selectedDate = new DateTime(selectedYear, selectedMonth, selectedDay);

            tuesday = FindViewById<TextView>(Resource.Id.StateMisdTue);
            wednesday = FindViewById<TextView>(Resource.Id.StateMisdWed);
            thursday = FindViewById<TextView>(Resource.Id.StateMisdThurs);
            friday = FindViewById<TextView>(Resource.Id.StateMisdFri);
            mainMenu = FindViewById<Button>(Resource.Id.btnMainMenu);

            tuesday.Text = CalcCourtDate.calcStateMisdTue(selectedDate).ToShortDateString();
            wednesday.Text = CalcCourtDate.calcStateMisdWed(selectedDate).ToShortDateString();
            thursday.Text = CalcCourtDate.calcStateMisdThurs(selectedDate).ToShortDateString();
            friday.Text = CalcCourtDate.calcStateMisdFri(selectedDate).ToShortDateString();

            mainMenu.Click += MainMenu_Click;
        }

        private void MainMenu_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}