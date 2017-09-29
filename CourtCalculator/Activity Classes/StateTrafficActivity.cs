using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;

namespace CourtCalculator
{
    [Activity(Label = "State Traffic Court Dates")]
    public class StateTrafficActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TextView wednesdayAtoN, wednesdayOtoZ;
            DateTime selectedDate;
            Button mainMenu;
            int selectedMonth;
            int selectedDay;
            int selectedYear;

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.StateTrafficActivity);

            selectedMonth = Intent.GetIntExtra("Month", 0);
            selectedDay = Intent.GetIntExtra("Day", 0);
            selectedYear = Intent.GetIntExtra("Year", 0);

            selectedDate = new DateTime(selectedYear, selectedMonth, selectedDay);

            wednesdayAtoN = FindViewById<TextView>(Resource.Id.StateTrafficAtoN);
            wednesdayOtoZ = FindViewById<TextView>(Resource.Id.StateTrafficOtoZ);
            mainMenu = FindViewById<Button>(Resource.Id.btnMainMenu);

            wednesdayAtoN.Text = CalcCourtDate.calcStateTrafficDateAtoN(selectedDate).ToShortDateString();
            wednesdayOtoZ.Text = CalcCourtDate.calcStateTrafficDateOtoZ(selectedDate).ToShortDateString();

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