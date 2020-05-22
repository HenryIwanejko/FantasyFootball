using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using FantasyFootball.Utilities;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FantasyFootballSQLDB;

namespace FantasyFootball
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterTeamActivity : Activity
    {
        private readonly SQLiteRepository sqlLiteRepository = new SQLiteRepository();

        private EditText fantasyTeamName;
        private EditText managerFirstName;
        private EditText managerLastName;

        private TextView errorMessage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register_team);
            AddEventHandlers();
            RetreiveElements();
        }

        private void AddEventHandlers()
        {
            FindViewById<Button>(Resource.Id.registerTeamBackBtn).Click += RegisterTeamActivity_Click;
            FindViewById<Button>(Resource.Id.registerTeamSubmitBtn).Click += SubmitButton_Click;
        }

        private void RetreiveElements()
        {
            fantasyTeamName = FindViewById<EditText>(Resource.Id.registerTeamFantasyNameTxtBx);
            managerFirstName = FindViewById<EditText>(Resource.Id.registerTeamFirstNameTxtBx);
            managerLastName = FindViewById<EditText>(Resource.Id.registerTeamLastNameTxtBx);
            errorMessage = FindViewById<TextView>(Resource.Id.registerTeamErrorMessageTxtView);
        }

        private bool ValidateFields()
        {
            if (Util.ValidateText(fantasyTeamName.Text, managerFirstName.Text, managerLastName.Text)) {
                errorMessage.Text = "";
                return true;
            }
            else
            {
                errorMessage.Text = "Please enter valid inputs into the fields";
                return false;
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                int nextFantasyId = sqlLiteRepository.GetNextFantasyTeamId();
                if (nextFantasyId < 2)
                {
                    FantasyTeam fantasyTeam = new FantasyTeam(nextFantasyId, fantasyTeamName.Text, managerFirstName.Text, managerLastName.Text);
                    int dbResponse = sqlLiteRepository.AddFantasyTeam(fantasyTeam);
                    if (dbResponse == 1)
                    {
                        StartActivity(typeof(MainActivity));
                    }
                    else
                    {
                        errorMessage.Text = "Error contacting the database";
                    }
                }
                else
                {
                    errorMessage.Text = "Only 2 teams can be registered, delete one on the admin page to register";
                }
            }
        }

        private void RegisterTeamActivity_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}