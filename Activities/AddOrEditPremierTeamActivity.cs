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
using FantasyFootball.Adapters;
using FantasyFootballShared;
using FantasyFootballShared.Utilities;
using Newtonsoft.Json;

namespace FantasyFootball.Activities
{
    [Activity(Label = "AddOrEditPremierTeamActivity")]
    public class AddOrEditPremierTeamActivity : Activity
    {
        private TextView title;
        private EditText premierTeamName;
        private Button submitButton;
        private Button backBtn;

        private string action;
        private PremierTeam editingPremierTeam;

        private readonly ISQLiteRepository sqlLiteRepository = new SQLiteRepository();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_or_edit_premier_team);
            RetrieveElements();
            AddEventHandlers();
            DetermineAction();
        }

        // Retrieve the each element used in the UI xml file
        private void RetrieveElements()
        {
            title = FindViewById<TextView>(Resource.Id.addOrEditPremierTeamTitleTxtView);
            premierTeamName = FindViewById<EditText>(Resource.Id.addOrEditPremierTeamNameEditView);
            submitButton = FindViewById<Button>(Resource.Id.addOrEditPremierTeamSubmitBtn);
            backBtn = FindViewById<Button>(Resource.Id.addOrEditPremierTeamBackBtn);
        }

        // Map elements to event handlers
        private void AddEventHandlers()
        {
            backBtn.Click += BackBtn_Click;
            submitButton.Click += SubmitButton_Click;
        }

        // Retrieve from context to determine the user selected to add or edit a premier team
        private void DetermineAction()
        {
            this.action = Intent.GetStringExtra("action");
            if (!Util.ValidateText(action) || action == Util.Add)
            {
                this.action = Util.Add;
            }
            else
            {
                this.action = Util.Edit;
                SetPremierTeamData();
            }
            title.Text = $"{action} Premier Team:";
        }

        // if edit premier team selected then get prem team data and populate fields
        private void SetPremierTeamData()
        {
            this.editingPremierTeam = JsonConvert.DeserializeObject<PremierTeam>(Intent.GetStringExtra("PremierTeamData"));
            premierTeamName.Text = editingPremierTeam.PremierTeamName;
        }

        // On submit button click check action selected.
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (action == Util.Add)
            {
                AddPremierTeam();
            }
            else
            {
                EditPlayer();
            }
        }

        // if user selected to add then add premier team to the database
        private void AddPremierTeam()
        {
            if (Util.ValidateText(premierTeamName.Text))
            {
                PremierTeam newPremierTeam = new PremierTeam(premierTeamName.Text);
                if (sqlLiteRepository.AddPremierTeam(newPremierTeam) == 1)
                {
                    Toast.MakeText(this, $"Premier team {premierTeamName.Text} has been added", ToastLength.Short).Show();
                    StartActivity(typeof(EditPremierTeamMainActivity));
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Error adding premier team to database", ToastLength.Short).Show();
                }
            }
        }

        // if user selected to edit the premier team then update premier team to the database
        private void EditPlayer()
        {
            if (Util.ValidateText(premierTeamName.Text) && editingPremierTeam != null)
            {
                UpdatePremierTeamInfo();
                if (sqlLiteRepository.UpdatePremierTeam(editingPremierTeam) == 1)
                {
                    Toast.MakeText(this, $"Premier team {premierTeamName.Text} has been edited", ToastLength.Short).Show();
                    StartActivity(typeof(EditPremierTeamMainActivity));
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Error editing premier team info in the database", ToastLength.Short).Show();
                }
            }
        }

        // Get data from UI and update premier team object.
        private void UpdatePremierTeamInfo()
        {
            editingPremierTeam.PremierTeamName = premierTeamName.Text;
        }

        // On back button click go to edit premier team main page.
        private void BackBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(EditPremierTeamMainActivity));
            Finish();
        }
    }
}