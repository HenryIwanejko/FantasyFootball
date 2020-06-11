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
using FantasyFootballShared;
using FantasyFootball.Adapters;
using Newtonsoft.Json;
using FantasyFootball.Activities;
using FantasyFootballShared.Utilities;

namespace FantasyFootball
{
    [Activity(Label = "PickTeamActivity")]
    public class PickTeamActivity : Activity
    {
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

        private readonly PickTeamService pickTeamService = new PickTeamService();
        private readonly Dictionary<FantasyTeam, List<Player>> userTeams = new Dictionary<FantasyTeam, List<Player>>();

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

        // Choose a random team to go first
        private FantasyTeam SelectInitialFantasyTeam()
        {
            int randomIndex = new Random().Next(1);
            return teams[randomIndex];
        }

        // Validate that 2 teams have been registered.
        private bool ValidateTeams()
        {
            if (pickTeamService.ValidateFantasyTeams(teams))
            {
                Toast.MakeText(this, "Requires 2 teams to be registered to select teams", ToastLength.Long).Show();
                StartActivity(typeof(MainActivity));
                return false;
            }
            return true;
        }

        // Get first team and update UI with that teams data.
        private void InitialiseTeams()
        {
            currentTeam = SelectInitialFantasyTeam();
            pickTeamService.InitialiseUserTeamData(teams, userTeams);
            teamCost.Text = "Total Team Cost: £0m";
            teamName.Text = currentTeam.FantasyTeamName;
        }

        // On team switch update UI with that team data.
        private void SetTeamDetails()
        {
            currentTeam = pickTeamService.SetTeamDetails(teams, currentTeam, ref positionCounter, ref selectionCounter);
            decimal teamPrice = Util.CalculateTeamCost(userTeams, currentTeam);
            teamCost.Text = $"Total Team Cost: £{teamPrice}m";
            teamName.Text = currentTeam.FantasyTeamName;
        }

        // Get specific position to select from until the last player choice
        private void PositionSelector()
        {
            if (positionCounter < 4)
            {
                Position position = positions[positionCounter];
                positionSpinner.Adapter = new PositionsSpinnerAdapter(this, position);
            }
            else
            {
                positions.Remove(positions[0]);
                positionSpinner.Adapter = new PositionsSpinnerAdapter(this, positions);
            }
        }

        // Retrieve the each element used in the UI xml file
        private void RetrieveElements()
        {
            backBtn = FindViewById<Button>(Resource.Id.pickTeamBackBtn);
            submitBtn = FindViewById<Button>(Resource.Id.pickTeamSubmitBtn);
            playerSelectionLstView = FindViewById<ListView>(Resource.Id.pickTeamPlayerSelectionLstView);
            positionSpinner = FindViewById<Spinner>(Resource.Id.pickTeamPositionSpinner);
            teamName = FindViewById<TextView>(Resource.Id.pickTeamChoseTeamTxtView);
            teamCost = FindViewById<TextView>(Resource.Id.pickTeamTeamCostTxtView);
            this.teams = pickTeamService.GetFantasyTeams();
            this.positions = pickTeamService.GetPositions();
        }

        // Map elements to event handlers
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

        // For a specific position get all players from the database.
        private void RetrievePlayerData(int positionIndex)
        {
            Position position = positions[positionIndex];
            this.players = pickTeamService.RetrieveAndCleansePlayerData(userTeams, position);
        }

        // Retrieve player data and update UI
        private void PositionSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            RetrievePlayerData(positionCounter < 4 ? positionCounter : e.Position);
            PopulateListView();
        }

        // On submit add player to current team if one is selected.
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

        // Add selected player to the current team choosing
        private void AddPlayerToTeam()
        {
            userTeams[currentTeam].Add(selectedPlayer);
            this.players.Remove(selectedPlayer);
            this.selectedPlayer = null;
        }

        // Calculates which player is selecting next, if last pick then goes to the completion page
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
                    Intent pickTeamActivity = new Intent(this, typeof(TeamCompletionActivity));
                    Dictionary<int, KeyValuePair<FantasyTeam, List<Player>>> dto = pickTeamService.PackageUpData(userTeams);
                    pickTeamActivity.PutExtra("teamData", JsonConvert.SerializeObject(dto));
                    StartActivity(pickTeamActivity);
                    this.Finish();
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