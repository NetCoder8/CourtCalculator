using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using CourtCalculator.Utility_Classes;
using System.Collections.Generic;

namespace CourtCalculator.Fragment_Classes
{
    internal class dialogOffenses : DialogFragment
    {
        public List<Offense> OffenseList { get; set; }
        public bool DialogVisible { get; set; }

        public dialogOffenses(List<Offense> theList, bool visible)
        {
            OffenseList = theList;
            DialogVisible = visible;
        }

        public delegate void IsRetained(bool theValue);
        public event IsRetained ChangeValue;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            List<Offense> theList = OffenseList;

            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.offensesAddedLayout, container, false);

            TextView offenses = view.FindViewById<TextView>(Resource.Id.offenseAdded);
            Button exit = view.FindViewById<Button>(Resource.Id.btnOffenseDialogExit);

            offenses.Text = Offense.displayOffenses(theList);

            exit.Click += delegate
            {
                ChangeValue(false);
                Dismiss();
            };

            return view;
        }
    }
}