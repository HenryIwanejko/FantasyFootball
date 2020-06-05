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
using FantasyFootballShared;
using FantasyFootball.Adapters;
using FantasyFootballShared.Utilities;
using Newtonsoft.Json;
using Java.Util;

namespace FantasyFootball.Activities
{
    [Activity(Label = "TeamCompletionActivity")]
    public class TeamCompletionActivity : Activity
    {
        private TextView team1TextView;
        private TextView team2TextView;
        private ListView team1ListView;
        private ListView team2ListView;
        private TextView team1TotalCostTextView;
        private TextView team2TotalCostTextView;
        private TextView team1AverageCostTextView;
        private TextView team2AverageCostTextView;
        private Button backBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_pick_teams_completion);
            RetreiveElements();
            AddEventHandlers();
            PopulateFields();
        }

        private void PopulateFields()
        {
            Dictionary<int, KeyValuePair<FantasyTeam, List<Player>>> userTeams = JsonConvert.DeserializeObject<Dictionary<int, KeyValuePair<FantasyTeam, List<Player>>>>(Intent.GetStringExtra("teamData"));
            KeyValuePair<FantasyTeam, List<Player>> team1 = userTeams[0];
            team1TextView.Text = team1.Key.FantasyTeamName;
            team1ListView.Adapter = new PlayersListViewAdapter(this, team1.Value);
            team1TotalCostTextView.Text = $"Total Cost: £{Util.CalculateTeamCost(team1.Value)}m";
            team1AverageCostTextView.Text = $"Average Player Cost: £{Util.CalculateAveragePlayerCost(team1.Value)}m";
            KeyValuePair<FantasyTeam, List<Player>> team2 = userTeams[1];
            team2TextView.Text = team2.Key.FantasyTeamName;
            team2ListView.Adapter = new PlayersListViewAdapter(this, team2.Value);
            team2TotalCostTextView.Text = $"Total Cost: £{Util.CalculateTeamCost(team2.Value)}m";
            team2AverageCostTextView.Text = $"Average Player Cost: £{Util.CalculateAveragePlayerCost(team2.Value)}m";
        }

        private void AddEventHandlers()
        {
            backBtn.Click += BackBtn_Click;
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }

        private void RetreiveElements()
        {
            team1TextView = FindViewById<TextView>(Resource.Id.teamCompletionTeam1TxtView);
            team2TextView = FindViewById<TextView>(Resource.Id.teamCompletionTeam2TxtView);
            team1ListView = FindViewById<ListView>(Resource.Id.teamCompletionTeam1LstView);
            team2ListView = FindViewById<ListView>(Resource.Id.teamCompletionTeam2LstView);
            team1TotalCostTextView = FindViewById<TextView>(Resource.Id.teamCompletionTeam1CostField);
            team2TotalCostTextView = FindViewById<TextView>(Resource.Id.teamCompletionTeam2CostField);
            team1AverageCostTextView = FindViewById<TextView>(Resource.Id.teamCompletionAveragePlayerCost1Field);
            team2AverageCostTextView = FindViewById<TextView>(Resource.Id.teamCompletionAveragePlayerCost2Field);
            backBtn = FindViewById<Button>(Resource.Id.teamCompletionBackBtn);
        }
    }
}