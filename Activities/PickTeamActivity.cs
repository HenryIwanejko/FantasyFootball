using System;
using System.Collections.Generic;
using System.Configuration;
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
using Android.Text.Method;
using System.Data.Entity.Migrations.Model;

namespace FantasyFootball
{
    [Activity(Label = "PickTeamActivity")]
    public class PickTeamActivity : Activity
    {
        private readonly ISQLiteRepository sqlLiteRepository = new SQLiteRepository();

        // Elements
        private Button backBtn;
        private Button submitBtn;
        private ListView playerSelectionLstView;
        private Spinner positionSpinner;
        private TextView teamName;
        private TextView teamCost;

        // State variables
        private List<Position> positions;
        private List<Player> players;
        private List<FantasyTeam> teams;
        private FantasyTeam currentTeam;
        private int positionCounter = 0;
        private int teamCounter = 0;
        private int selectionCounter = 0;
        private Player selectedPlayer;

        Dictionary<FantasyTeam, List<Player>> userTeams = new Dictionary<FantasyTeam, List<Player>>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_pick_team);
            RetrieveElements();
            AddEventHandlers();
            if (ValidateTeams())
            {
                InitialiseTeams();
                PositionSelector();
            }
        }

        private FantasyTeam SelectInitialFantasyTeam()
        {
            int randomIndex = new Random().Next(1);
            return teams[randomIndex];
        }

        private bool ValidateTeams()
        {
            if (teams == null || teams.Count != 2)
            {
                Toast.MakeText(this, "Requires 2 teams to be registered to select teams", ToastLength.Long).Show();
                StartActivity(typeof(MainActivity));
                return false;
            }
            return true;
        }

        private void InitialiseTeams()
        {
            currentTeam = SelectInitialFantasyTeam();
            foreach (var team in teams)
            {
                userTeams.Add(team, new List<Player>());
            }
            teamCost.Text = "Total Team Cost: £0m";
            teamName.Text = currentTeam.FantasyTeamName;
        }

        private void SetTeamDetails()
        {
            int teamIndex = SwitchPlayerIndex(teams.IndexOf(currentTeam));
            if (selectionCounter == 1) 
            {
                if (positionCounter == 1 || positionCounter == 3)
                {
                    teamIndex = SwitchPlayerIndex(teamIndex);
                }
                selectionCounter = 0;
            }
            else
            {
                selectionCounter += 1;
            }
            currentTeam = teams[teamIndex];
            decimal teamPrice = CalculateTeamCost();
            teamCost.Text = $"Total Team Cost: £{teamPrice}m";
            teamName.Text = currentTeam.FantasyTeamName;
        }

        private decimal CalculateTeamCost()
        {
            decimal total = 0;
            List<Player> userPlayers = userTeams.FirstOrDefault(x => x.Key.FantasyTeamID == currentTeam.FantasyTeamID).Value;
            foreach (var player in userPlayers)
            {
                total += player.Price;
            }
            return total;
        }

        private int SwitchPlayerIndex(int currentTeamIndex)
        {
            return currentTeamIndex == 0 ? 1 : 0;
        }

        private void PositionSelector()
        {
            if (positionCounter < 4)
            {
                Position position = positions[positionCounter];
                positionSpinner.Adapter = new PositionsSpinnerAdapter(this, position);
            }
            else
            {
                positionSpinner.Adapter = new PositionsSpinnerAdapter(this, positions);
            }
        }

        private void RetrieveElements()
        {
            backBtn = FindViewById<Button>(Resource.Id.pickTeamBackBtn);
            submitBtn = FindViewById<Button>(Resource.Id.pickTeamSubmitBtn);
            playerSelectionLstView = FindViewById<ListView>(Resource.Id.pickTeamPlayerSelectionLstView);
            positionSpinner = FindViewById<Spinner>(Resource.Id.pickTeamPositionSpinner);
            teamName = FindViewById<TextView>(Resource.Id.pickTeamChoseTeamTxtView);
            teamCost = FindViewById<TextView>(Resource.Id.pickTeamTeamCostTxtView);
            this.teams = sqlLiteRepository.GetFantasyTeams();
            this.positions = sqlLiteRepository.GetPositions();
        }

        private void AddEventHandlers()
        {
            positionSpinner.ItemSelected += PositionSpinner_ItemSelected;
            backBtn.Click += PickTeamActivity_Click;
            submitBtn.Click += SubmitBtn_Click;
            playerSelectionLstView.ItemClick += PlayerSelectionLstView_ItemClick;
        }

        private void PlayerSelectionLstView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selectedPlayer = this.players[e.Position];
        }

        private void PopulateListView()
        {
            playerSelectionLstView.Adapter = new PlayersListViewAdapter(this, this.players);
        }

        private void CleansePlayerData()
        {
            foreach(var team in userTeams.Values)
            {
                Dictionary<int, int> premierTeams = new Dictionary<int, int>();
                foreach(var player in team)
                {
                    if (premierTeams.ContainsKey(player.PremierTeamID))
                    {
                        premierTeams[player.PremierTeamID] += 1;
                    }
                    else
                    {
                        premierTeams.Add(player.PremierTeamID, 1);
                    }
                    players.RemoveAll(x => x.PlayerID == player.PlayerID);
                    if (premierTeams[player.PremierTeamID] >= 2)
                    {
                        players.RemoveAll(x => x.PremierTeamID == player.PremierTeamID);
                    }
                }
            }
        }

        private void RetrievePlayerData(int positionIndex)
        {
            Position position = positions[positionIndex];
            players = sqlLiteRepository.GetPlayers(position.PositionID);
            CleansePlayerData();
        }

        private void PositionSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            RetrievePlayerData(positionCounter < 4 ? positionCounter : e.Position);
            PopulateListView();
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null) 
            {
                AddPlayerToTeam();
                SetNextPlayerSelection();
            }
            else
            {
                Toast.MakeText(this, "Please select a player", ToastLength.Short).Show();
            }
        }

        private void AddPlayerToTeam()
        {
            userTeams[currentTeam].Add(selectedPlayer);
            this.players.Remove(selectedPlayer);
            this.selectedPlayer = null;
        }

        private void SetNextPlayerSelection()
        {
            SetTeamDetails();
            if (teamCounter >= 1)
            {
                positionCounter += 1;
                if (positionCounter < 5)
                {
                    if (positionCounter < 4)
                    {
                        PositionSelector();
                        RetrievePlayerData(positionCounter);
                        PopulateListView();
                    }
                    else
                    {
                        PositionSelector();
                        PopulateListView();
                    }
                    teamCounter = 0;
                } 
                else
                {
                    Toast.MakeText(this, "5 Selected each", ToastLength.Long).Show();
                }
            }
            else
            {
                PopulateListView();
                teamCounter += 1;
            }
        }

        private void PickTeamActivity_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}