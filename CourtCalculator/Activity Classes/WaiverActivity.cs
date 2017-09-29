using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using CourtCalculator.Fragment_Classes;
using CourtCalculator.Utility_Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Javax.Security.Auth;

namespace CourtCalculator
{
    [Activity(Label = "Waiver Activity")]
    public class WaiverActivity : AppCompatActivity
    {
        private Button mainMenu;
        private Button startOver;
        private Button addOffense;
        private Button calculate;
        private EditText cost;
        private int totalCost = 0;
        private int courtCost = 0;
        private int totalFineAmount = 0;
        private Spinner offenseType;
        private Spinner offense;
        private List<Offense> offenseList = new List<Offense>();
        private FloatingActionButton fab;
        private bool showingCostAlert;
        private bool showingOffenseAlert;
        private bool priceCalculated;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Create your application here
            SetContentView(Resource.Layout.WaiverActivity);

            offenseType = FindViewById<Spinner>(Resource.Id.offenseType);
            offense = FindViewById<Spinner>(Resource.Id.offense);
            addOffense = FindViewById<Button>(Resource.Id.btnAdd);
            startOver = FindViewById<Button>(Resource.Id.btnStartOver);
            calculate = FindViewById<Button>(Resource.Id.btnCalcFine);
            mainMenu = FindViewById<Button>(Resource.Id.btnMainMenu);
            cost = FindViewById<EditText>(Resource.Id.cost);
            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);

            // Arrays
            var blankOffenseValue = ArrayAdapter.CreateFromResource(this, Resource.Array.blankOffense,
                Android.Resource.Layout.SimpleListItem1);
            var blankOffenseTypeValue = ArrayAdapter.CreateFromResource(this, Resource.Array.offenseType,
                Android.Resource.Layout.SimpleListItem1);

            offenseType.ItemSelected += OffenseType_ItemSelected;
            blankOffenseTypeValue.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
            offenseType.Adapter = blankOffenseTypeValue;



            // main menu button
            mainMenu.Click += MainMenu_Click;

            // add offense button
            addOffense.Click += AddOffense_Click;

            // clears the form
            startOver.Click += StartOver_Click;

            // calculates the fine
            calculate.Click += Calculate_Click;

            // floating action button
            fab.Click += Fab_Click;
        }





        // floating action button on click method
        private void Fab_Click(object sender, System.EventArgs e)
        {
            ShowOffenseDialog();
        }

        // Method to show the Offense Dialog Box
        private void ShowOffenseDialog()
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialogOffenses dialogOffenses = new dialogOffenses(offenseList, showingOffenseAlert);
            dialogOffenses.SetStyle(DialogFragmentStyle.NoTitle, 0);
            dialogOffenses.RetainInstance = true;
            showingOffenseAlert = true;
            dialogOffenses.ChangeValue += DialogOffenses_ChangeValue;
            dialogOffenses.Show(transaction, "Offenses");
        }

        // Delegate that prevents dialog from redisplaying
        private void DialogOffenses_ChangeValue(bool changeValue)
        {
            showingOffenseAlert = changeValue;
        }

        // method for the calculate fine button
        private void Calculate_Click(object sender, System.EventArgs e)
        {
            bool isValid = false;

            if (Validation.isArrayPresent(offenseList, "No offense(s) added. Please add at least one offense", this)
                && Validation.isPresent(cost, "Please enter a court cost", this))
            {
                isValid = true;
            }

            if (isValid)
            {
                courtCost = Convert.ToInt32(cost.Text);

                foreach (Offense o in offenseList)
                {
                    totalCost += o.Cost;
                }

                totalFineAmount = totalCost + courtCost;

                // Pulls up output dialog

                ShowCostDialog();

                // Disables the buttons
                calculate.Enabled = false;
                addOffense.Enabled = false;
                cost.Enabled = false;

                priceCalculated = true;
            }
        }

        private void ShowCostDialog()
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialogTotalCost costOutput = new dialogTotalCost(offenseList, totalFineAmount, courtCost, totalCost, showingCostAlert);
            costOutput.SetStyle(DialogFragmentStyle.NoTitle, 0);
            costOutput.RetainInstance = true;
            showingCostAlert = true;
            costOutput.Change += CostOutput_Change;
            costOutput.Show(transaction, "Total Cost");
        }

        private void CostOutput_Change(bool theValue)
        {
            showingCostAlert = theValue;
        }

        private void StartOver_Click(object sender, System.EventArgs e)
        {
            offenseList.Clear();
            var blankOffenseTypeValue = ArrayAdapter.CreateFromResource(this, Resource.Array.offenseType, Android.Resource.Layout.SimpleListItem1);
            blankOffenseTypeValue.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
            offenseType.Adapter = blankOffenseTypeValue;
            totalCost = 0;
            courtCost = 0;
            totalFineAmount = 0;
            cost.Text = "";

            calculate.Enabled = true;
            addOffense.Enabled = true;
            cost.Enabled = true;
        }

        // add offense button on click method
        private void AddOffense_Click(object sender, System.EventArgs e)
        {
            string selectedOffense = offense.SelectedItem.ToString();
            string selectedOffenseType = offenseType.SelectedItem.ToString();

            if (selectedOffense == "Select an offense")
            {
                Toast.MakeText(this, "Please select an offense", ToastLength.Short).Show();
            }
            else if (selectedOffenseType == "Select an offense type")
            {
                Toast.MakeText(this, "Please select an offense type", ToastLength.Short).Show();
            }
            //Speeding Violations
            else if (selectedOffense == "1-5 MPH over")
            {
                checkForOffenseInList("1-5 MPH over", 10);
            }
            else if (selectedOffense == "6-10 MPH over")
            {
                checkForOffenseInList("6-10 MPH over", 25);
            }
            else if (selectedOffense == "11-15 MPH over")
            {
                checkForOffenseInList("11-15 MPH over", 75);
            }
            else if (selectedOffense == "16-20 MPH over")
            {
                checkForOffenseInList("16-20 MPH over", 125);
            }
            else if (selectedOffense == "21-35 MPH over")
            {
                checkForOffenseInList("21-35 MPH over", 200);
            }
            else if (selectedOffense == "36+ MPH over")
            {
                checkForOffenseInList("36+ MPH over", 300);
            }
            // Turning/Passing Violations
            else if (selectedOffense == "Improperly overtake vehicle")
            {
                checkForOffenseInList("Improperly overtake vehicle", 25);
            }
            else if (selectedOffense == "Hindering vehicle from passing")
            {
                checkForOffenseInList("Hindering vehicle from passing", 25);
            }
            else if (selectedOffense == "Overtake pass vehicle within a street intersection")
            {
                checkForOffenseInList("Overtake pass vehicle within a street intersection", 25);
            }
            else if (selectedOffense == "Passing on viaduct")
            {
                checkForOffenseInList("Passing on viaduct", 25);
            }
            else if (selectedOffense == "No passing zone")
            {
                checkForOffenseInList("No passing zone", 25);
            }
            else if (selectedOffense == "Change course w/o safety/signal")
            {
                checkForOffenseInList("Change course w/o safety/signal", 25);
            }
            else if (selectedOffense == "Illegal U-turn")
            {
                checkForOffenseInList("Illegal U-turn", 25);
            }
            else if (selectedOffense == "Improper turn")
            {
                checkForOffenseInList("Improper turn", 25);
            }
            // Traffic Control Device/Traffic Sign offenses
            else if (selectedOffense == "Fail to obey traffic control device")
            {
                checkForOffenseInList("Fail to obey traffic control device", 25);
            }
            else if (selectedOffense == "Evasion of traffic control device")
            {
                checkForOffenseInList("Evasion of traffic control device", 50);
            }
            else if (selectedOffense == "Violate automatic traffic control signal")
            {
                checkForOffenseInList("Violate automatic traffic control signal", 75);
            }
            else if (selectedOffense == "Violate stop sign")
            {
                checkForOffenseInList("Violate stop sign", 75);
            }
            // Vehicle defects/requirements
            else if (selectedOffense == "Defective steering wheel")
            {
                checkForOffenseInList("Defective steering wheel", 25);
            }
            else if (selectedOffense == "Impaired operator visibility")
            {
                checkForOffenseInList("Impaired operator visibility", 25);
            }
            else if (selectedOffense == "Muffler")
            {
                checkForOffenseInList("Muffler", 25);
            }
            else if (selectedOffense == "Rearview mirror")
            {
                checkForOffenseInList("Rearview mirror", 25);
            }
            else if (selectedOffense == "Unsafe tires")
            {
                checkForOffenseInList("Unsafe tires", 25);
            }
            else if (selectedOffense == "Vehicle requirements/width or height (specify)")
            {
                checkForOffenseInList("Vehicle requirements/width or height (specify)", 25);
            }
            else if (selectedOffense == "Two headlights required at night")
            {
                checkForOffenseInList("Two headlights required at night", 25);
            }
            //Noise Decibel Violations
            else if (selectedOffense == "Unnecessary noise/squealing tires")
            {
                checkForOffenseInList("Unnecessary noise/squealing tires", 25);
            }
            // Registration of Vehicles
            else if (selectedOffense == "No valid registration")
            {
                checkForOffenseInList("No valid registration", 25);
            }
            else if (selectedOffense == "No proof of ownership")
            {
                checkForOffenseInList("No proof of ownership", 50);
            }
            else if (selectedOffense == "Fictitious plates")
            {
                checkForOffenseInList("Fictitious plates", 50);
            }
            else if (selectedOffense == "Fail to display two plates/plates not clear or visible")
            {
                checkForOffenseInList("Fail to display two plates/plates not clear or visible", 25);
            }
            else if (selectedOffense == "Utility trailer plates")
            {
                checkForOffenseInList("Utility trailer plates", 25);
            }
            else if (selectedOffense == "Expired intransit decal (s)")
            {
                checkForOffenseInList("Expired intransit decal (s)", 50);
            }
            else if (selectedOffense == "Failure to return cancelled plates (s)")
            {
                checkForOffenseInList("Failure to return cancelled plates (s)", 100);
            }
            else if (selectedOffense == "Misuse of dealer plates (s)")
            {
                checkForOffenseInList("Misuse of dealer plates (s)", 50);
            }
            else if (selectedOffense == "Misuse of local plates or farm plates (s)")
            {
                checkForOffenseInList("Misuse of local plates or farm plates (s)", 100);
            }
            else if (selectedOffense == "Operate unregistered NE-plated COMMERCIAL truck/tractor (1-20 days)(s)")
            {
                checkForOffenseInList("Operate unregistered NE-plated COMMERCIAL truck/tractor (1-20 days)(s)", 25);
            }
            else if (selectedOffense == "Operate unregistered NE-plated COMMERCIAL truck/tractor (21-30days)(s)")
            {
                checkForOffenseInList("Operate unregistered NE-plated COMMERCIAL truck/tractor (21-30days)(s)", 35);
            }
            else if (selectedOffense == "Operate unregistered NE-plated COMMERCIAL truck/tractor (31-40 days)(s)")
            {
                checkForOffenseInList("Operate unregistered NE-plated COMMERCIAL truck/tractor (31-40 days)(s)", 50);
            }
            else if (selectedOffense == "Operate unregistered NE-plated COMMERCIAL truck/tractor (41-50 days)(s)")
            {
                checkForOffenseInList("Operate unregistered NE-plated COMMERCIAL truck/tractor (41-50 days)(s)", 100);
            }
            else if (selectedOffense == "Operate unregistered NE-plated COMMERCIAL truck/tractor (51+ days)(s)")
            {
                checkForOffenseInList("Operate unregistered NE-plated COMMERCIAL truck/tractor (51+ days)(s)", 200);
            }
            // Passenger/Load violations
            else if (selectedOffense == "No seat belt")
            {
                checkForOffenseInList("No seat belt", 25);
            }
            else if (selectedOffense == "Child restraint violation")
            {
                checkForOffenseInList("Child restraint violation", 25);
            }
            else if (selectedOffense == "Excess passengers (too many in front seat)")
            {
                checkForOffenseInList("Excess passengers (too many in front seat)", 50);
            }
            else if (selectedOffense == "Motorcycle passenger restrictions")
            {
                checkForOffenseInList("Motorcycle passenger restrictions", 25);
            }
            else if (selectedOffense == "Obstructing view of driver")
            {
                checkForOffenseInList("Obstructing view of driver", 50);
            }
            else if (selectedOffense == "Motorcycle helmet required (s)")
            {
                checkForOffenseInList("Motorcycle helmet required (s)", 50);
            }
            // One way street and alley
            else if (selectedOffense == "Wrong way on a one way OR wrong direction in alley")
            {
                checkForOffenseInList("Wrong way on a one way OR wrong direction in alley", 25);
            }
            // Bicycles, Snowmobiles, toy vehicles, mini-bike violations
            else if (selectedOffense == "Riding bicycle on sidewalk in congested area")
            {
                checkForOffenseInList("Riding bicycle on sidewalk in congested area", 25);
            }
            else if (selectedOffense == "Failure to keep to right of roadway")
            {
                checkForOffenseInList("Failure to keep to right of roadway", 25);
            }
            else if (selectedOffense == "Too many person on bicycle")
            {
                checkForOffenseInList("Too many person on bicycle", 25);
            }
            else if (selectedOffense == "Unlawful operation of snowmobile")
            {
                checkForOffenseInList("Unlawful operation of snowmobile", 50);
            }
            //Ciy park violations
            else if (selectedOffense == "Unlawful parking of vehicle")
            {
                checkForOffenseInList("Unlawful parking of vehicle", 25);
            }
            else if (selectedOffense == "Park closed-enter after hours")
            {
                checkForOffenseInList("Park closed-enter after hours", 25);
            }
            else if (selectedOffense == "Possess/consume alcohol in park")
            {
                checkForOffenseInList("Possess/consume alcohol in park", 100);
            }
            // Lane line violations
            else if (selectedOffense == "Drive to left of center of road")
            {
                checkForOffenseInList("Drive to left of center of road", 25);
            }
            else if (selectedOffense == "Improper lane change")
            {
                checkForOffenseInList("Improper lane change", 25);
            }
            else if (selectedOffense == "Motorcycle lane restriction")
            {
                checkForOffenseInList("Motorcycle lane restriction", 25);
            }
            // ROW violations
            else if (selectedOffense == "FTYROW to emergency vehicle")
            {
                checkForOffenseInList("FTYROW to emergency vehicle", 100);
            }
            else if (selectedOffense == "FTYROW (Violate Yield Sign)")
            {
                checkForOffenseInList("FTYROW (Violate Yield Sign)", 25);
            }
            else if (selectedOffense == "FTYROW pedestrian")
            {
                checkForOffenseInList("FTYROW pedestrian", 25);
            }
            else if (selectedOffense == "FTYROW private road or driveway")
            {
                checkForOffenseInList("FTYROW private road or driveway", 25);
            }
            // Operators license violations
            else if (selectedOffense == "No valid drivers license")
            {
                checkForOffenseInList("No valid drivers license", 75);
            }
            else if (selectedOffense == "No valid motorcycle drivers license")
            {
                checkForOffenseInList("No valid motorcycle drivers license", 75);
            }
            else if (selectedOffense == "No valid drivers license on person")
            {
                checkForOffenseInList("No valid drivers license on person", 25);
            }
            else if (selectedOffense == "No current operators license (non-resident)")
            {
                checkForOffenseInList("No current operators license (non-resident)", 25);
            }
            // Pedestrian violations
            else if (selectedOffense == "Pedestrian fail to obey traffic control device")
            {
                checkForOffenseInList("Pedestrian fail to obey traffic control device", 25);
            }
            else if (selectedOffense == "Hitchhiking in roadway")
            {
                checkForOffenseInList("Hitchhiking in roadway", 25);
            }
            // Driving where not allowed violations
            else if (selectedOffense == "Drive onto controlled access roadway")
            {
                checkForOffenseInList("Drive onto controlled access roadway", 25);
            }
            else if (selectedOffense == "Drive within sidewalk space")
            {
                checkForOffenseInList("Drive within sidewalk space", 25);
            }
            else if (selectedOffense == "Drive on sidewalk")
            {
                checkForOffenseInList("Drive on sidewalk", 25);
            }
            else if (selectedOffense == "Drive on median")
            {
                checkForOffenseInList("Drive on median", 25);
            }
            else if (selectedOffense == "Driving on shoulder")
            {
                checkForOffenseInList("Driving on shoulder", 25);
            }
            // Driving rules - other violations
            else if (selectedOffense == "Backing without safety")
            {
                checkForOffenseInList("Backing without safety", 25);
            }
            else if (selectedOffense == "Drive onto controlled access roadway")
            {
                checkForOffenseInList("Drive onto controlled access roadway", 25);
            }
            else if (selectedOffense == "Driving too fast for conditions")
            {
                checkForOffenseInList("Driving too fast for conditions", 100);
            }
            else if (selectedOffense == "Fail to dim headlights")
            {
                checkForOffenseInList("Fail to dim headlights", 25);
            }
            else if (selectedOffense == "Following too closely")
            {
                checkForOffenseInList("Following too closely", 50);
            }
            else if (selectedOffense == "Handheld wireless communication device violation (s)")
            {
                checkForOffenseInList("Handheld wireless communication device violation (s)", 200);
            }
            else if (selectedOffense == "Improper use of horn")
            {
                checkForOffenseInList("Improper use of horn", 25);
            }
            else if (selectedOffense == "Impeding traffic")
            {
                checkForOffenseInList("Impeding traffic", 25);
            }
            else if (selectedOffense == "Interfere with use of street")
            {
                checkForOffenseInList("Interfere with use of street", 25);
            }
            else if (selectedOffense == "Lights required after dark")
            {
                checkForOffenseInList("Lights required after dark", 25);
            }
            else if (selectedOffense == "Motorcycle footrests")
            {
                checkForOffenseInList("Motorcycle footrests", 25);
            }
            else if (selectedOffense == "Opening door against traffic")
            {
                checkForOffenseInList("Opening door against traffic", 25);
            }
            else if (selectedOffense == "Possession of open alcohol container")
            {
                checkForOffenseInList("Possession of open alcohol container", 50);
            }
            else if (selectedOffense == "Slow moving vehicle emblem")
            {
                checkForOffenseInList("Slow moving vehicle emblem", 25);
            }
            else if (selectedOffense == "Studded tires")
            {
                checkForOffenseInList("Studded tires", 25);
            }
            else if (selectedOffense == "Using a radar detector for motor carriers")
            {
                checkForOffenseInList("Using a radar detector for motor carriers", 30);
            }
            else if (selectedOffense == "Negligent driving")
            {
                checkForOffenseInList("Negligent driving", 100);
            }
            else if (selectedOffense == "Careless driving")
            {
                checkForOffenseInList("Careless driving", 100);
            }
            //Littering and spilling violations
            else if (selectedOffense == "All littering violations")
            {
                checkForOffenseInList("All littering violations", 100);
            }
            else if (selectedOffense == "Deposit materials on road/ditch (s)")
            {
                checkForOffenseInList("Deposit materials on road/ditch (s)", 100);
            }
            else if (selectedOffense == "Deposit rubbish on highway (s)")
            {
                checkForOffenseInList("Deposit rubbish on highway (s)", 100);
            }
            else if (selectedOffense == "Uncovered materials on vehicle")
            {
                checkForOffenseInList("Uncovered materials on vehicle", 100);
            }
            else if (selectedOffense == "Spilling load - Allow contents of truck to fall upon street")
            {
                checkForOffenseInList("Spilling load - Allow contents of truck to fall upon street", 100);
            }
            // Misc violations
            else if (selectedOffense == "Occupy business lot after hours")
            {
                checkForOffenseInList("Occupy business lot after hours", 25);
            }
            else if (selectedOffense == "Public consumption of alcohol")
            {
                checkForOffenseInList("Public consumption of alcohol", 50);
            }
            else if (selectedOffense == "Trespass (City)")
            {
                checkForOffenseInList("Trespass (City)", 50);
            }
            else if (selectedOffense == "Shooting from highway")
            {
                checkForOffenseInList("Shooting from highway", 100);
            }
            else if (selectedOffense == "Hunting/fishing w/o permit")
            {
                checkForOffenseInList("Hunting/fishing w/o permit", 100);
            }
            else if (selectedOffense == "Possession of drug paraphernalia")
            {
                checkForOffenseInList("Possession of drug paraphernalia", 100);
            }
            else if (selectedOffense == "Fireworks possession or discharge")
            {
                checkForOffenseInList("Fireworks possession or discharge", 50);
            }
            else if (selectedOffense == "Junk vehicle on private property")
            {
                checkForOffenseInList("Junk vehicle on private property", 100);
            }
            else if (selectedOffense == "Urinating in public")
            {
                checkForOffenseInList("Urinating in public", 50);
            }
            else if (selectedOffense == "Smoking prohibited 1st offense")
            {
                checkForOffenseInList("Smoking prohibited 1st offense", 100);
            }
            else if (selectedOffense == "Proprietor permitting smoking 1st offense")
            {
                checkForOffenseInList("Proprietor permitting smoking 1st offense", 100);
            }
            else if (selectedOffense == "Emergency water restrictions")
            {
                checkForOffenseInList("Emergency water restrictions", 100);
            }
            // Animal control violations
            else if (selectedOffense == "All animal violations (non-bite)")
            {
                checkForOffenseInList("All animal violations (non-bite)", 25);
            }
            // handicap violations
            else if (selectedOffense == "Handicap violation 1st Offense")
            {
                checkForOffenseInList("Handicap violation 1st Offense", 150);
            }
            else if (selectedOffense == "Handicap violation 2nd Offense")
            {
                checkForOffenseInList("Handicap violation 2nd Offense", 200);
            }
            else if (selectedOffense == "Handicap violation 3rd Offense")
            {
                checkForOffenseInList("Handicap violation 3rd Offense", 300);
            }
            // Required stops RR xing or school bus
            else if (selectedOffense == "Required stop at RR Xing/disobey signal")
            {
                checkForOffenseInList("Required stop at RR Xing/disobey signal", 100);
            }
            else if (selectedOffense == "Unlawful overtaking/passing of school bus")
            {
                checkForOffenseInList("Unlawful overtaking/passing of school bus", 500);
            }
        }

        // Method that checks to see if an offense has been added to the offense list
        private void checkForOffenseInList(string theOffenseName, int theCost)
        {
            bool inCollection = false;

            if (offenseList.Count == 0)
            {
                var theOffense = new Offense(theOffenseName, theCost);
                offenseList.Add(theOffense);
                Toast.MakeText(this, theOffense.OffenseName + " has been added", ToastLength.Short).Show();
                return;
            }

            if (offenseList.Count > 0)
            {
                foreach (Offense o in offenseList)
                {
                    if (o.OffenseName == theOffenseName)
                    {
                        Toast.MakeText(this, "Offense has already been added", ToastLength.Short).Show();
                        inCollection = true;
                    }
                }
            }

            if (offenseList.Count > 0 && !inCollection)
            {
                var theOffense = new Offense(theOffenseName, theCost);
                offenseList.Add(theOffense);
                Toast.MakeText(this, theOffense.OffenseName + " has been added", ToastLength.Short).Show();
            }
        }

        // Main menu button on click method
        private void MainMenu_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop);
            StartActivity(intent);
        }

        // Arrays for the Offense Type Spinner
        private void OffenseType_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner offenseType = (Spinner)sender;

            string selectedItem = offenseType.GetItemAtPosition(e.Position).ToString();

            if (selectedItem == "Select an offense type")
            {
                var blankOffenseValue = ArrayAdapter.CreateFromResource(this, Resource.Array.blankOffense, Android.Resource.Layout.SimpleListItem1);
                blankOffenseValue.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = blankOffenseValue;
            }
            else if (selectedItem == "Speeding Violations")
            {
                var speedingViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.speedingViolations, Android.Resource.Layout.SimpleListItem1);
                speedingViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = speedingViolations;
            }
            else if (selectedItem == "Turning/Passing")
            {
                var turningPassingViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.turningPassingViolations, Android.Resource.Layout.SimpleListItem1);
                turningPassingViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = turningPassingViolations;
            }
            else if (selectedItem == "Traffic Control Device/Traffic Signs")
            {
                var trafficControlViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.TrafficControlViolations, Android.Resource.Layout.SimpleListItem1);
                trafficControlViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = trafficControlViolations;
            }
            else if (selectedItem == "Vehicle Defects/Requirements")
            {
                var vehicleDefectViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.VehicleDefectViolations, Android.Resource.Layout.SimpleListItem1);
                vehicleDefectViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = vehicleDefectViolations;
            }
            else if (selectedItem == "Noise/Decibel Violations")
            {
                var noiseDecibelViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.NoiseDecibelViolations, Android.Resource.Layout.SimpleListItem1);
                noiseDecibelViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = noiseDecibelViolations;
            }
            else if (selectedItem == "Registration Of Vehicles")
            {
                var registrationViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.registrationViolations, Android.Resource.Layout.SimpleListItem1);
                registrationViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = registrationViolations;
            }
            else if (selectedItem == "Passengers/Load Violations")
            {
                var passengerViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.passengerLoadViolations, Android.Resource.Layout.SimpleListItem1);
                passengerViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = passengerViolations;
            }
            else if (selectedItem == "One Way Street And Alley")
            {
                var oneWayViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.OneWayStreetViolations, Android.Resource.Layout.SimpleListItem1);
                oneWayViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = oneWayViolations;
            }
            else if (selectedItem == "Bicycles, Snowmobiles, Toy Vehicles, Mini-Bikes")
            {
                var bikeViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.bikeViolations, Android.Resource.Layout.SimpleListItem1);
                bikeViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = bikeViolations;
            }
            else if (selectedItem == "City Park Violations")
            {
                var cityParkViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.cityParkViolations, Android.Resource.Layout.SimpleListItem1);
                cityParkViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = cityParkViolations;
            }
            else if (selectedItem == "Lane Line Violations")
            {
                var laneViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.laneLineViolations, Android.Resource.Layout.SimpleListItem1);
                laneViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = laneViolations;
            }
            else if (selectedItem == "Right Of Way")
            {
                var rowViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.rowViolations, Android.Resource.Layout.SimpleListItem1);
                rowViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = rowViolations;
            }
            else if (selectedItem == "Operators License")
            {
                var licenseViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.licenseViolations, Android.Resource.Layout.SimpleListItem1);
                licenseViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = licenseViolations;
            }
            else if (selectedItem == "Pedestrian Violations")
            {
                var pedestrianViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.pedestrianViolations, Android.Resource.Layout.SimpleListItem1);
                pedestrianViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = pedestrianViolations;
            }
            else if (selectedItem == "Driving Where Not Allowed")
            {
                var drivingNotAllowedViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.driveNotAllowedViolations, Android.Resource.Layout.SimpleListItem1);
                drivingNotAllowedViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = drivingNotAllowedViolations;
            }
            else if (selectedItem == "Driver Rules - Other")
            {
                var drivingOtherViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.driveRulesOtherViolations, Android.Resource.Layout.SimpleListItem1);
                drivingOtherViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = drivingOtherViolations;
            }
            else if (selectedItem == "Littering And Spilling")
            {
                var litteringViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.litteringViolations, Android.Resource.Layout.SimpleListItem1);
                litteringViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = litteringViolations;
            }
            else if (selectedItem == "Miscellaneous")
            {
                var miscViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.miscViolations, Android.Resource.Layout.SimpleListItem1);
                miscViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = miscViolations;
            }
            else if (selectedItem == "Animal Control (non-bite cases only)")
            {
                var animalViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.animalViolations, Android.Resource.Layout.SimpleListItem1);
                animalViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = animalViolations;
            }
            else if (selectedItem == "Handicap Parking Tickets")
            {
                var handicapViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.handicapViolations, Android.Resource.Layout.SimpleListItem1);
                handicapViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = handicapViolations;
            }
            else if (selectedItem == "Required Stops RR Xing Or School Bus")
            {
                var requiredStopViolations = ArrayAdapter.CreateFromResource(this, Resource.Array.requiredStopsViolations, Android.Resource.Layout.SimpleListItem1);
                requiredStopViolations.SetDropDownViewResource(Android.Resource.Layout.SimpleListItem1);
                offense.Adapter = requiredStopViolations;
            }
        }



        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            string savedArray = JsonConvert.SerializeObject(offenseList);
            int selectedOffense = offense.SelectedItemPosition;


            outState.PutString("COURT_COST_TEXT", cost.Text);
            outState.PutString("OFFENSE_ARRAY", savedArray);
            outState.PutInt("SELECTED_OFFENSE", selectedOffense);

            outState.PutInt("COURT_COST", courtCost);
            outState.PutInt("TOTAL_COST", totalCost);
            outState.PutInt("TOTAL_FINE", totalFineAmount);

            outState.PutBoolean("OFFENSE_DIALOG", showingOffenseAlert);
            outState.PutBoolean("COST_DIALOG", showingCostAlert);
            outState.PutBoolean("ALREADY_CALCULATED", priceCalculated);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            var passedArray = savedInstanceState.GetString("OFFENSE_ARRAY");
            var selectedOffense = savedInstanceState.GetInt("SELECTED_OFFENSE");

            cost.Text = savedInstanceState.GetString("COURT_COST_TEXT");
            offenseList = JsonConvert.DeserializeObject<List<Offense>>(passedArray);

            courtCost = savedInstanceState.GetInt("COURT_COST");
            totalCost = savedInstanceState.GetInt("TOTAL_COST");
            totalFineAmount = savedInstanceState.GetInt("TOTAL_FINE");

            showingOffenseAlert = savedInstanceState.GetBoolean("OFFENSE_DIALOG");
            showingCostAlert = savedInstanceState.GetBoolean("COST_DIALOG");
            priceCalculated = savedInstanceState.GetBoolean("ALREADY_CALCULATED");

            var focusLayout = (RelativeLayout)FindViewById(Resource.Id.layout1);
            focusLayout.Focusable = true;
            focusLayout.FocusableInTouchMode = true;
            focusLayout.RequestFocus();

            if (showingOffenseAlert)
            {
                ShowOffenseDialog();
            }

            if (showingCostAlert)
            {
                ShowCostDialog();
            }

            if (priceCalculated)
            {
                calculate.Enabled = false;
                addOffense.Enabled = false;
                cost.Enabled = false;
            }
        }
    }
}