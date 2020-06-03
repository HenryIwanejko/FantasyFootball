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
using FantasyFootballSQLDB;
using FantasyFootball.Adapters;
using Newtonsoft.Json;

namespace FantasyFootball.Activities
{
    [Activity(Label = "TeamCompletionActivity")]
    public class TeamCompletionActivity : Activity
    {
        private TextView team1TextView;
        private TextView team2TextView;
        private ListView team1ListView;
        private ListView team2ListView;
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
            Dictionary<FantasyTeam, List<Player>> userTeams = JsonConvert.DeserializeObject<Dictionary<FantasyTeam, List<Player>>>(Intent.GetStringExtra("teamData"));
            KeyValuePair<FantasyTeam, List<Player>> team1 = userTeams.FirstOrDefault(team => team.Key.FantasyTeamID == 0);
            team1TextView.Text = team1.Key.FantasyTeamName;
            team1ListView.Adapter = new PlayersListViewAdapter(this, team1.Value);
            KeyValuePair<FantasyTeam, List<Player>> team2 = userTeams.FirstOrDefault(team => team.Key.FantasyTeamID == 1);
            team2TextView.Text = team2.Key.FantasyTeamName;
            team2ListView.Adapter = new PlayersListViewAdapter(this, team2.Value);
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
            backBtn = FindViewById<Button>(Resource.Id.teamCompletionBackBtn);
        }
    }
}