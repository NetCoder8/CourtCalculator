using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace CourtCalculator
{
    [Activity(Label = "Court Calculator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        private Button btnGo;
        private RadioButton radGetCourtDate;
        private RadioButton radGetWaiverFine;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            btnGo = FindViewById<Button>(Resource.Id.btnGo);
            radGetCourtDate = FindViewById<RadioButton>(Resource.Id.radCourtDate);
            radGetWaiverFine = FindViewById<RadioButton>(Resource.Id.radWaiver);

            btnGo.Click += BtnGo_Click;
        }

        private void BtnGo_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent();

            if (radGetCourtDate.Checked)
            {
                intent = new Intent(this, typeof(CalcActivity));
            }
            else if (radGetWaiverFine.Checked)
            {
                intent = new Intent(this, typeof(WaiverActivity));
            }

            StartActivity(intent);
        }
    }
}