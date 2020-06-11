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

        private List<PremierTeam> premierTeams;
        private PremierTeam selectedPremierTeam = null;

        private readonly ISQLiteRepository sqlLiteRepository = new SQLiteRepository();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_edit_premier_team_main);
            RetrieveElements();
            AddEventHandlers();
            PopulateElements();
        }

        // Retrieve the each element used in the UI xml file
        private void RetrieveElements()
        {
            premierTeamListView = FindViewById<ListView>(Resource.Id.adminEditPremierTeamMainPremierTeamsLstView);
            editPremierTeamBtn = FindViewById<Button>(Resource.Id.adminEditPremierTeamMainEditBtn);
            addPremierTeamBtn = FindViewById<Button>(Resource.Id.adminEditPremierTeamMainAddBtn);
            removePremierTeamBtn = FindViewById<Button>(Resource.Id.adminEditPremierTeamMainRemoveBtn);
        }

        // Map elements to event handlers
        private void AddEventHandlers()
        {
            FindViewById<Button>(Resource.Id.adminEditPremierTeamMainBackBtn).Click += BackBtn_Click;
            removePremierTeamBtn.Click += RemovePremierTeamBtn_Click;
            premierTeamListView.ItemClick += PremierTeamListView_ItemClick;
            addPremierTeamBtn.Click += AddPremierTeamBtn_Click;
            editPremierTeamBtn.Click += EditPremierTeamBtn_Click;
        }

        // On eidt button click package up premier team data and save to new activity to use.
        private void EditPremierTeamBtn_Click(object sender, EventArgs e)
        {
            if (selectedPremierTeam != null)
            {
                Intent addPlayerActivity = new Intent(this, typeof(AddOrEditPremierTeamActivity));
                addPlayerActivity.PutExtra("action", Util.Edit);
                addPlayerActivity.PutExtra("PremierTeamData", JsonConvert.SerializeObject(selectedPremierTeam));
                StartActivity(addPlayerActivity);
            }
            else
            {
                Toast.MakeText(this, "Please Select a Player", ToastLength.Short).Show();
            }
        }

        // On add button click save action and go to add premier team page.
        private void AddPremierTeamBtn_Click(object sender, EventArgs e)
        {
            Intent addPlayerActivity = new Intent(this, typeof(AddOrEditPremierTeamActivity));
            addPlayerActivity.PutExtra("action", Util.Add);
            StartActivity(addPlayerActivity);
        }

        // Save selected item to the selected premier team
        private void PremierTeamListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selectedPremierTeam = premierTeams[e.Position];
        }

        // On remove button click then remove selected player from the database.
        private void RemovePremierTeamBtn_Click(object sender, EventArgs e)
        {
            if (selectedPremierTeam != null)
            {
                int dbResponse = sqlLiteRepository.DeletePremierTeam(selectedPremierTeam.PremierTeamID);
                if (dbResponse == 1)
                {
                    Toast.MakeText(this, $"Deleted {selectedPremierTeam.PremierTeamName}", ToastLength.Short).Show();
                    UpdateListViewData();
                }
                else
                {
                    Toast.MakeText(this, "Error contacting database", ToastLength.Short).Show();
                }
                selectedPremierTeam = null;
            }
            else
            {
                Toast.MakeText(this, "Please Select a Player", ToastLength.Short).Show();
            }
        }

        // Retrieve data from database and update UI with data
        private void PopulateElements()
        {
            premierTeams = sqlLiteRepository.GetPremierTeams();
            UpdateListViewData();
        }

        // Retrieve data from database and update UI with data
        private void UpdateListViewData()
        {
            premierTeams = sqlLiteRepository.GetPremierTeams();
            premierTeamListView.Adapter = new PremierTeamListViewAdapter(this, premierTeams);
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AdminActivity));
        }
    }
}
