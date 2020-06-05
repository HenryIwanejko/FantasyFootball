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

        private ISQLiteRepository sqlLiteRepository = new SQLiteRepository();

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
        }

        private void PopulateElements()
        {
            List<Player> players = sqlLiteRepository.GetAllPlayers();
            playerListView.Adapter = new PlayersListViewAdapter(this, players);
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AdminActivity));
        }
    }
}