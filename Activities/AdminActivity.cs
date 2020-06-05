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
        private AdminService adminService = new AdminService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_admin);
            AddEventHandlers();
        }

        private void AddEventHandlers()
        {
            FindViewById<Button>(Resource.Id.adminBackBtn).Click += BackBtn_Click;
            FindViewById<Button>(Resource.Id.adminResetFantasyTeamBtn).Click += ResetFantasyTeamBtn_Click;
            FindViewById<Button>(Resource.Id.adminEditPlayersBtn).Click += EditPlayers_Click;
            FindViewById<Button>(Resource.Id.adminEditPremierTeamsBtn).Click += EditPremierTeam_Click;
        }

        private void EditPremierTeam_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditPlayers_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(EditPlayerMainActivity));
        }

        private void ResetFantasyTeamBtn_Click(object sender, EventArgs e)
        {
            string messageText = adminService.ResetFantasyTeams() ? "FantasyTeams have been reset" : "Error resetting FantasyTeams";
            Toast.MakeText(this, messageText, ToastLength.Short).Show();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}