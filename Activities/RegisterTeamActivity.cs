using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FantasyFootballShared;

namespace FantasyFootball
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterTeamActivity : Activity
    {

        private EditText fantasyTeamName;
        private EditText managerFirstName;
        private EditText managerLastName;
        private TextView errorMessage;

        private readonly RegisterTeamService registerTeamService = new RegisterTeamService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register_team);
            AddEventHandlers();
            RetreiveElements();
        }

        // Map elements to event handlers
        private void AddEventHandlers()
        {
            FindViewById<Button>(Resource.Id.registerTeamBackBtn).Click += RegisterTeamActivity_Click;
            FindViewById<Button>(Resource.Id.registerTeamSubmitBtn).Click += SubmitButton_Click;
        }

        // Retrieve the each element used in the UI xml file
        private void RetreiveElements()
        {
            fantasyTeamName = FindViewById<EditText>(Resource.Id.registerTeamFantasyNameTxtBx);
            managerFirstName = FindViewById<EditText>(Resource.Id.registerTeamFirstNameTxtBx);
            managerLastName = FindViewById<EditText>(Resource.Id.registerTeamLastNameTxtBx);
            errorMessage = FindViewById<TextView>(Resource.Id.registerTeamErrorMessageTxtView);
        }

        /*
        * On the click event handler:
        * Validate input fields
        * Check database to see if team name exists
        * Add to database
        */
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            KeyValuePair<bool, string> insertedIntoDatabase = registerTeamService.InsertedTeamToDatabase(fantasyTeamName.Text, managerFirstName.Text, managerLastName.Text);
            errorMessage.Text = insertedIntoDatabase.Value;
            if (insertedIntoDatabase.Key == true)
            {
                Toast.MakeText(this, $"Fantasy team {fantasyTeamName.Text} has been created", ToastLength.Short).Show();
                StartActivity(typeof(MainActivity));
            }
        }

        // On back button click go to main page
        private void RegisterTeamActivity_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}