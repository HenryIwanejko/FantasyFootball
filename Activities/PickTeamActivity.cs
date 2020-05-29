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

namespace FantasyFootball
{
    [Activity(Label = "PickTeamActivity")]
    public class PickTeamActivity : Activity
    {
        private readonly ISQLiteRepository sqlLiteRepository = new SQLiteRepository();

        private PlayersListViewAdapter listViewAdapter;

        // Elements
        private Button backBtn;
        private Button submitBtn;
        private ListView playerSelectionLstView;
        private Spinner positionSpinner;
        private TextView teamName;

        // State variables
        private List<Position> positions;
        private List<Player> players;
        private List<FantasyTeam> teams;
        private FantasyTeam currentTeam;
        private int positionCounter = 0;
        private int teamCounter = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_pick_team);
            RetrieveElements();
            AddEventHandlers();
            ValidateTeams();
            SetTeamDetails();
            PositionSelector();
        }

        private FantasyTeam SelectInitialFantasyTeam()
        {
            int randomIndex = new Random().Next(1);
            return teams[randomIndex];
        }

        private void ValidateTeams()
        {
            if (teams == null || teams.Count != 2)
            {
                Toast.MakeText(this, "Requires 2 teams to be registered to select teams", ToastLength.Long).Show();
                StartActivity(typeof(MainActivity));
            }
        }

        private void SetTeamDetails()
        {
            if (currentTeam == null)
            {
                currentTeam = SelectInitialFantasyTeam();
            }
            else
            {
                int otherTeamIndex = teams.IndexOf(currentTeam) == 0 ? 1 : 0;
                currentTeam = teams[otherTeamIndex];
            }
            teamName.Text = currentTeam.FantasyTeamName;
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
            this.teams = sqlLiteRepository.GetFantasyTeams();
            this.positions = sqlLiteRepository.GetPositions();
        }

        private void AddEventHandlers()
        {
            positionSpinner.ItemSelected += PositionSpinner_ItemSelected;
            backBtn.Click += PickTeamActivity_Click;
            submitBtn.Click += SubmitBtn_Click;
        }

        private void PopulateListView()
        {
            if (listViewAdapter == null)
            {
                listViewAdapter = new PlayersListViewAdapter(this, this.players);
                playerSelectionLstView.Adapter = listViewAdapter;
            }
            else
            {
                listViewAdapter.UpdateData(this.players);
            }
        }

        private void RetrievePlayerData(int positionIndex)
        {
            Position position = positions[positionIndex];
            players = sqlLiteRepository.GetPlayers(position.PositionID);
        }

        private void PositionSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            RetrievePlayerData(e.Position); 
            PopulateListView();
         }

        private void SubmitBtn_Click(object sender, EventArgs e)
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