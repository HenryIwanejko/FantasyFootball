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

namespace FantasyFootball.Activities
{
    [Activity(Label = "AddOrEditPlayerActivity")]
    public class AddOrEditPlayerActivity : Activity
    {
        private EditText firstnameEditView;
        private EditText surnameEditView;
        private EditText priceEditView;
        private Spinner positionSpinner;
        private Spinner premierTeamSpinner;
        private Button submitButton;
        private Button backBtn;

        private List<Position> positions;
        private List<PremierTeam> premierTeams;

        private readonly ISQLiteRepository sqlLiteRepository = new SQLiteRepository();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_or_edit_player);
            RetrieveElements();
            AddEventHandlers();
            PopulateElements();
        }

        private void RetrieveElements()
        {
            firstnameEditView = FindViewById<EditText>(Resource.Id.addOrEditPlayerFirstnameEditView);
            surnameEditView = FindViewById<EditText>(Resource.Id.addOrEditPlayerSurnameEditView);
            priceEditView = FindViewById<EditText>(Resource.Id.addOrEditPlayerSurnameEditView);
            positionSpinner = FindViewById<Spinner>(Resource.Id.addOrEditPlayerPositionSpinner);
            premierTeamSpinner = FindViewById<Spinner>(Resource.Id.addOrEditPlayerPremierTeamSpinner);
            submitButton = FindViewById<Button>(Resource.Id.addOrEditPlayerSubmitBtn);
            backBtn = FindViewById<Button>(Resource.Id.addOrEditPlayerBackBtn);
        }

        private void AddEventHandlers()
        {
            backBtn.Click += BackBtn_Click;
            submitButton.Click += SubmitButton_Click;
        }

        private void PopulateElements()
        {
            premierTeams = sqlLiteRepository.GetPremierTeams();
            positions = sqlLiteRepository.GetPositions();
            premierTeamSpinner.Adapter = new PremierTeamsSpinnerAdapter(this, premierTeams);
            positionSpinner.Adapter = new PositionsSpinnerAdapter(this, positions);
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (Util.ValidateText(firstnameEditView.Text, surnameEditView.Text, priceEditView.Text))
            {

            }
            else
            {
                Toast.MakeText(this, "Please enter valid fields", ToastLength.Short).Show();
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AdminActivity));
            Finish();
        }
    }
}