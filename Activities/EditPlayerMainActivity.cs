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

namespace FantasyFootball.Activities
{
    [Activity(Label = "EditPlayerMainActivity")]
    public class EditPlayerMainActivity : Activity
    {
        private ListView playerListView;
        private Button editPlayerBtn;
        private Button addPlayerBtn;
        private Button removePlayerBtn;

        private List<Player> players;
        private Player selectedPlayer = null;

        private readonly ISQLiteRepository sqlLiteRepository = new SQLiteRepository();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_edit_player_main);
            RetrieveElements();
            AddEventHandlers();
            PopulateElements();
        }

        private void RetrieveElements()
        {
            playerListView = FindViewById<ListView>(Resource.Id.adminEditPlayerMainPlayersLstView);
            editPlayerBtn = FindViewById<Button>(Resource.Id.adminEditPlayerMainEditBtn);
            addPlayerBtn = FindViewById<Button>(Resource.Id.adminEditPlayerMainAddBtn);
            removePlayerBtn = FindViewById<Button>(Resource.Id.adminEditPlayerMainRemoveBtn);
        }

        private void AddEventHandlers()
        {
            FindViewById<Button>(Resource.Id.adminEditPlayerMainBackBtn).Click += BackBtn_Click;
            removePlayerBtn.Click += RemovePlayerBtn_Click;
            playerListView.ItemClick += PlayerListView_ItemClick;
            addPlayerBtn.Click += AddPlayerBtn_Click;
            editPlayerBtn.Click += EditPlayerBtn_Click;
        }

        private void EditPlayerBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AddOrEditPlayerActivity));
        }

        private void AddPlayerBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AddOrEditPlayerActivity));
        }

        private void PlayerListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selectedPlayer = players[e.Position];
        }

        private void RemovePlayerBtn_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                int dbResponse = sqlLiteRepository.DeletePlayer(selectedPlayer.PlayerID);
                if (dbResponse == 1)
                {
                    Toast.MakeText(this, $"Deleted {selectedPlayer.Surname}", ToastLength.Short).Show();
                    UpdateListViewData();
                }
                else
                {
                    Toast.MakeText(this, "Error contacting database", ToastLength.Short).Show();
                }
                selectedPlayer = null;
            }
            else
            {
                Toast.MakeText(this, "Please Select a Player", ToastLength.Short).Show();
            }
        }

        private void PopulateElements()
        {
            players = sqlLiteRepository.GetAllPlayers();
            UpdateListViewData();
        }

        private void UpdateListViewData()
        {
            players = sqlLiteRepository.GetAllPlayers();
            playerListView.Adapter = new PlayersListViewAdapter(this, players);
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AdminActivity));
        }
    }
}
