using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using CourtCalculator.Utility_Classes;
using System.Collections.Generic;

namespace CourtCalculator.Fragment_Classes
{
    internal class dialogTotalCost : DialogFragment
    {
        public List<Offense> TheOffenseList { get; set; }
        public int TotalCost { get; set; }
        public int CourtCost { get; set; }
        public int FineAmount { get; set; }
        public bool DialogVisible { get; set; }

        public delegate void IsShowing(bool theValue);
        public event IsShowing Change;

        public dialogTotalCost(List<Offense> theList, int theCost, int courtCost, int fineAmount, bool visible)
        {
            TheOffenseList = theList;
            TotalCost = theCost;
            CourtCost = courtCost;
            FineAmount = fineAmount;
            DialogVisible = visible;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            List<Offense> theList = TheOffenseList;
            int theTotalCost = TotalCost;
            int fineAmount = FineAmount;
            int courtCost = CourtCost;

            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.costOutput, container, false);

            TextView offensesAdded = view.FindViewById<TextView>(Resource.Id.offensesAdded);
            TextView fine = view.FindViewById<TextView>(Resource.Id.fineAmount);
            TextView court = view.FindViewById<TextView>(Resource.Id.courtCost);
            TextView totalCost = view.FindViewById<TextView>(Resource.Id.totalCost);
            Button dismiss = view.FindViewById<Button>(Resource.Id.btnDismiss);

            offensesAdded.Text = Offense.displayOffenses(theList);

            fine.Text = "The total fine is " + fineAmount.ToString("c");

            court.Text = "The court cost is " + courtCost.ToString("c");

            totalCost.Text = "The total cost is " + theTotalCost.ToString("c");

            dismiss.Click += delegate
            {
                this.Dismiss();
                Change(false);
            };

            return view;
        }
    }
}