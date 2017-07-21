using System.Collections.Generic;

namespace CourtCalculator.Utility_Classes
{
    public class Offense
    {
        public string OffenseName { get; set; }
        public int Cost { get; set; }

        public Offense()
        {
        }

        public Offense(string theOffenseName, int theCost)
        {
            Cost = theCost;
            OffenseName = theOffenseName;
        }

        public static string displayOffenses(List<Offense> theOffenseList)
        {
            List<Offense> offenseList = theOffenseList;
            string theOffenses = "";

            foreach (Offense o in offenseList)
            {
                theOffenses += o.ToString() + "\n";
            }

            return theOffenses;
        }

        public override string ToString()
        {
            return OffenseName.ToString() + " " + Cost.ToString("c");
        }
    }
}