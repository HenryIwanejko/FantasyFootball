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

namespace FantasyFootball
{
    [Activity(Label = "PickTeamActivity")]
    public class PickTeamActivity : Activity
    {
        private readonly ISQLiteRepository sqlLiteRepository = new SQLiteRepository();

        private Button backBtn;
        private Button submitBtn;
        private ListView playerSelectionLstView;
        private Spinner positionSpinner;

        private List<Position> positions;
        private List<Player> players;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_pick_team);
            RetrieveElements();
            AddEventHandlers();
            PopulateSpinner();
        }

        private void PopulateSpinner()
        {
            this.positions = sqlLiteRepository.GetPositions();
            positionSpinner.Adapter = new PositionsSpinnerAdapter(this, positions);
        }

        private void RetrieveElements()
        {
            backBtn = FindViewById<Button>(Resource.Id.pickTeamBackBtn);
            submitBtn = FindViewById<Button>(Resource.Id.pickTeamSubmitBtn);
            playerSelectionLstView = FindViewById<ListView>(Resource.Id.pickTeamPlayerSelectionLstView);
            positionSpinner = FindViewById<Spinner>(Resource.Id.pickTeamPositionSpinner);
        }

        private void AddEventHandlers()
        {
            positionSpinner.ItemSelected += PositionSpinner_ItemSelected;
            backBtn.Click += PickTeamActivity_Click;
            submitBtn.Click += SubmitBtn_Click;
        }

        private void PopulateListView(List<Player> players)
        {
            playerSelectionLstView.Adapter = new PlayersListViewAdapter(this, players);
        }

        private void PositionSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Position position = positions[e.Position];
            players = sqlLiteRepository.GetPlayers(position.PositionID);
            PopulateListView(players);
         }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PickTeamActivity_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}