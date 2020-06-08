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
    [Activity(Label = "EditPremierTeamMainActivity")]
    public class EditPremierTeamMainActivity : Activity
    {
        private ListView premierTeamListView;
        private Button editPremierTeamBtn;
        private Button addPremierTeamBtn;
        private Button removePremierTeamBtn;

        private List<Player> premierTeam;
        private Player selectedPremierTeam = null;

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
            premierTeamListView = FindViewById<ListView>(Resource.Id.adminEditPremierTeamMainPremierTeamsLstView);
            editPremierTeamBtn = FindViewById<Button>(Resource.Id.adminEditPlayerMainEditBtn);
            addPremierTeamBtn = FindViewById<Button>(Resource.Id.adminEditPlayerMainAddBtn);
            removePremierTeamBtn = FindViewById<Button>(Resource.Id.adminEditPlayerMainRemoveBtn);
        }

        private void AddEventHandlers()
        {
            FindViewById<Button>(Resource.Id.adminEditPlayerMainBackBtn).Click += BackBtn_Click;
            removePremierTeamBtn.Click += RemovePlayerBtn_Click;
            premierTeamListView.ItemClick += PlayerListView_ItemClick;
            addPremierTeamBtn.Click += AddPlayerBtn_Click;
            editPremierTeamBtn.Click += EditPlayerBtn_Click;
        }

        private void EditPlayerBtn_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                Intent addPlayerActivity = new Intent(this, typeof(AddOrEditPlayerActivity));
                addPlayerActivity.PutExtra("action", Util.EditPlayer);
                addPlayerActivity.PutExtra("PlayerData", JsonConvert.SerializeObject(selectedPlayer));
                StartActivity(addPlayerActivity);
            }
            else
            {
                Toast.MakeText(this, "Please Select a Player", ToastLength.Short).Show();
            }
        }

        private void AddPlayerBtn_Click(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                Intent addPlayerActivity = new Intent(this, typeof(AddOrEditPlayerActivity));
                addPlayerActivity.PutExtra("action", Util.AddPlayer);
                StartActivity(addPlayerActivity);
            }
            else
            {
                Toast.MakeText(this, "Please Select a Player", ToastLength.Short).Show();
            }
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
