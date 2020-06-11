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
using Newtonsoft.Json;
using FantasyFootballShared.Utilities;

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

        // Retrieve the each element used in the UI xml file
        private void RetrieveElements()
        {
            playerListView = FindViewById<ListView>(Resource.Id.adminEditPlayerMainPlayersLstView);
            editPlayerBtn = FindViewById<Button>(Resource.Id.adminEditPlayerMainEditBtn);
            addPlayerBtn = FindViewById<Button>(Resource.Id.adminEditPlayerMainAddBtn);
            removePlayerBtn = FindViewById<Button>(Resource.Id.adminEditPlayerMainRemoveBtn);
        }

        // Map elements to event handlers
        private void AddEventHandlers()
        {
            FindViewById<Button>(Resource.Id.adminEditPlayerMainBackBtn).Click += BackBtn_Click;
            removePlayerBtn.Click += RemovePlayerBtn_Click;
            playerListView.ItemClick += PlayerListView_ItemClick;
            addPlayerBtn.Click += AddPlayerBtn_Click;
            editPlayerBtn.Click += EditPlayerBtn_Click;
        }

        // On edit button click package player data up and save to activity for use.
        private void EditPlayerBtn_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                Intent addPlayerActivity = new Intent(this, typeof(AddOrEditPlayerActivity));
                addPlayerActivity.PutExtra("action", Util.Edit);
                addPlayerActivity.PutExtra("PlayerData", JsonConvert.SerializeObject(selectedPlayer));
                StartActivity(addPlayerActivity);
            }
            else
            {
                Toast.MakeText(this, "Please Select a Player", ToastLength.Short).Show();
            }
        }

        // On add button click save action and go to add player page.
        private void AddPlayerBtn_Click(object sender, EventArgs e)
        {
            Intent addPlayerActivity = new Intent(this, typeof(AddOrEditPlayerActivity));
            addPlayerActivity.PutExtra("action", Util.Add);
            StartActivity(addPlayerActivity);
        }

        // Save selected item to the selected player
        private void PlayerListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selectedPlayer = players[e.Position];
        }

        // if remove button clicked get selected item and remove from datbase and update view.
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

        // Retrieve players from database and update view with data.
        private void PopulateElements()
        {
            players = sqlLiteRepository.GetAllPlayers();
            UpdateListViewData();
        }

        // Retrieve players from database and update view with data.
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
