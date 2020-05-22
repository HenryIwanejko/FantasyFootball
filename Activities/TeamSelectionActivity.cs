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

namespace FantasyFootball.Activities
{
    [Activity(Label = "TeamSelectionActivity")]
    public class TeamSelectionActivity : Activity
    {
        private Button createTeamButton1;
        private Button createTeamButton2;
        private Button deleteTeamButton1;
        private Button deleteTeamButton2;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_team_selector);
            RetreiveElements();
            AddEventHandlers();
        }

        private void AddEventHandlers()
        {
            createTeamButton1.Click += TeamSelectionCreateTeam1_Click;
            createTeamButton2.Click += TeamSelectionCreateTeam2_Click;
            deleteTeamButton1.Click += TeamSelectionDeleteTeam1_Click;
            deleteTeamButton2.Click += TeamSelectionDeleteTeam2_Click;
            FindViewById<Button>(Resource.Id.teamSelectorBackBtn).Click += TeamSelectionBackBtn_Click;
        }

        private void RetreiveElements()
        {
            createTeamButton1 = FindViewById<Button>(Resource.Id.teamSelectorCreateTeam1Btn);
            createTeamButton2 = FindViewById<Button>(Resource.Id.teamSelectorCreateTeam2Btn);
            deleteTeamButton1 = FindViewById<Button>(Resource.Id.teamSelectorDeleteTeam1Btn);
            deleteTeamButton2 = FindViewById<Button>(Resource.Id.teamSelectorDeleteTeam2Btn);
        }

        private void TeamSelectionCreateTeam1_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterTeamActivity));
        }

        private void TeamSelectionCreateTeam2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterTeamActivity));
        }

        private void TeamSelectionDeleteTeam2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TeamSelectionDeleteTeam1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TeamSelectionBackBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}