using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FantasyFootball.Activities;
using FantasyFootballShared;

namespace FantasyFootball
{
    [Activity(Label = "AdminActivity")]
    public class AdminActivity : Activity
    {
        private readonly AdminService adminService = new AdminService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_admin);
            AddEventHandlers();
        }

        // Map elements to event handlers
        private void AddEventHandlers()
        {
            FindViewById<Button>(Resource.Id.adminBackBtn).Click += BackBtn_Click;
            FindViewById<Button>(Resource.Id.adminResetFantasyTeamBtn).Click += ResetFantasyTeamBtn_Click;
            FindViewById<Button>(Resource.Id.adminEditPlayersBtn).Click += EditPlayers_Click;
            FindViewById<Button>(Resource.Id.adminEditPremierTeamsBtn).Click += EditPremierTeam_Click;
        }

        // On edit button click go to edit premier team page
        private void EditPremierTeam_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(EditPremierTeamMainActivity));
            Finish();
        }

        //On edit players button clicked go to edit players page
        private void EditPlayers_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(EditPlayerMainActivity));
            Finish();
        }

        // On Reset button click delete all fantasy teams from the database
        private void ResetFantasyTeamBtn_Click(object sender, EventArgs e)
        {
            string messageText = adminService.ResetFantasyTeams() ? "FantasyTeams have been reset" : "Error resetting FantasyTeams";
            Toast.MakeText(this, messageText, ToastLength.Short).Show();
        }

        // On back button click go to main page.
        private void BackBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}