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
        private TextView team1BudgetTextView;
        private TextView team2BudgetTextView;
        private TextView team1ErrorTextView;
        private TextView team2ErrorTextView;
        private Button backBtn;

        private PickTeamCompletionService pickTeamCompletionService = new PickTeamCompletionService();

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
            PlayersListViewAdapter team1PlayersAdapter = new PlayersListViewAdapter(this, team1.Value);
            team1ListView.Adapter = team1PlayersAdapter;
            team1PlayersAdapter.NotifyDataSetChanged();

            KeyValuePair<FantasyTeam, List<Player>> team2 = userTeams[1];
            team2TextView.Text = team2.Key.FantasyTeamName;
            PlayersListViewAdapter team2PlayersAdapter = new PlayersListViewAdapter(this, team2.Value);
            team2ListView.Adapter = team2PlayersAdapter;
            team2PlayersAdapter.NotifyDataSetChanged();

            team1TotalCostTextView.Text = $"Total Cost: £{pickTeamCompletionService.CalculateTeamCost(team1.Value)}m";
            decimal averagePlayerCost1 = pickTeamCompletionService.CalculateAveragePlayerCost(team1.Value);
            decimal team1Budget = pickTeamCompletionService.CalculateTeamBudget(team1.Value);
            team1AverageCostTextView.Text = $"Average Player Cost: £{averagePlayerCost1}m";
            team1BudgetTextView.Text = $"Team Budget: {team1Budget}";
            team1ErrorTextView.Text = $"{pickTeamCompletionService.VerifyTeamBudget(team1Budget, averagePlayerCost1)}";
            
            team2TotalCostTextView.Text = $"Total Cost: £{pickTeamCompletionService.CalculateTeamCost(team2.Value)}m";
            decimal averagePlayerCost2 = pickTeamCompletionService.CalculateAveragePlayerCost(team2.Value);
            decimal team2Budget = pickTeamCompletionService.CalculateTeamBudget(team2.Value);
            team2AverageCostTextView.Text = $"Average Player Cost: £{averagePlayerCost2}m";
            team2BudgetTextView.Text = $"Team Budget: {team2Budget}";
            team2ErrorTextView.Text = $"{pickTeamCompletionService.VerifyTeamBudget(team2Budget, averagePlayerCost2)}";
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
            team1BudgetTextView = FindViewById<TextView>(Resource.Id.teamCompletionBudget1Field);
            team2BudgetTextView = FindViewById<TextView>(Resource.Id.teamCompletionBudget2Field);
            team1ErrorTextView = FindViewById<TextView>(Resource.Id.teamCompletionErrorMessage1Field);
            team2ErrorTextView = FindViewById<TextView>(Resource.Id.teamCompletionErrorMessage2Field);
            backBtn = FindViewById<Button>(Resource.Id.teamCompletionBackBtn);
        }
    }
}