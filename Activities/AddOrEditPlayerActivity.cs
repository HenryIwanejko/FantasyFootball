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
    [Activity(Label = "AddOrEditPlayerActivity")]
    public class AddOrEditPlayerActivity : Activity
    {
        private TextView title;
        private EditText firstnameEditView;
        private EditText surnameEditView;
        private EditText priceEditView;
        private Spinner positionSpinner;
        private Spinner premierTeamSpinner;
        private Button submitButton;
        private Button backBtn;

        private List<Position> positions;
        private List<PremierTeam> premierTeams;
        private PremierTeam selectedPremTeam;
        private Position selectedPosition;
        private string action;
        private Player editingPlayer;

        private readonly ISQLiteRepository sqlLiteRepository = new SQLiteRepository();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_or_edit_player);
            RetrieveElements();
            AddEventHandlers();
            PopulateElements();
            DetermineAction();
        }

        private void RetrieveElements()
        {
            title = FindViewById<TextView>(Resource.Id.addOrEditPlayerTitleTxtView);
            firstnameEditView = FindViewById<EditText>(Resource.Id.addOrEditPlayerFirstnameEditView);
            surnameEditView = FindViewById<EditText>(Resource.Id.addOrEditPlayerSurnameEditView);
            priceEditView = FindViewById<EditText>(Resource.Id.addOrEditPlayerPriceEditView);
            positionSpinner = FindViewById<Spinner>(Resource.Id.addOrEditPlayerPositionSpinner);
            premierTeamSpinner = FindViewById<Spinner>(Resource.Id.addOrEditPlayerPremierTeamSpinner);
            submitButton = FindViewById<Button>(Resource.Id.addOrEditPlayerSubmitBtn);
            backBtn = FindViewById<Button>(Resource.Id.addOrEditPlayerBackBtn);
        }

        private void AddEventHandlers()
        {
            backBtn.Click += BackBtn_Click;
            submitButton.Click += SubmitButton_Click;
            positionSpinner.ItemSelected += PositionSpinner_ItemSelected;
            premierTeamSpinner.ItemSelected += PremierTeamSpinner_ItemSelected;
        }

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
                SetPlayerData();
            }
            title.Text = $"{action} Player:";
        }

        private void SetPlayerData()
        {
            this.editingPlayer = JsonConvert.DeserializeObject<Player>(Intent.GetStringExtra("PlayerData"));
            firstnameEditView.Text = editingPlayer.Firstname;
            surnameEditView.Text = editingPlayer.Surname;
            priceEditView.Text = editingPlayer.Price.ToString();
            positionSpinner.SetSelection(editingPlayer.PositionID - 1);
            premierTeamSpinner.SetSelection(editingPlayer.PremierTeamID - 1);
        }

        private void PremierTeamSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            selectedPremTeam = premierTeams[e.Position];
        }

        private void PositionSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            selectedPosition = positions[e.Position];
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
            if (action == Util.Add)
            {
                AddPlayer();
            }
            else
            {
                EditPlayer();
            }
        }

        private void AddPlayer()
        {
            decimal price = 0;
            if (ValidateFields(ref price))
            {
                Player newPlayer = new Player(surnameEditView.Text, firstnameEditView.Text, selectedPremTeam.PremierTeamID, selectedPosition.PositionID, price);
                if (sqlLiteRepository.AddPlayer(newPlayer) == 1)
                {
                    Toast.MakeText(this, $"Player {surnameEditView.Text} has been added", ToastLength.Short).Show();
                    StartActivity(typeof(EditPlayerMainActivity));
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Error adding player to database", ToastLength.Short).Show();
                }
            }
        }

        private void EditPlayer()
        {
            decimal price = 0;
            if (ValidateFields(ref price) && editingPlayer != null)
            {
                UpdatePlayerInfo(price);
                if (sqlLiteRepository.UpdatePlayer(editingPlayer) == 1)
                {
                    Toast.MakeText(this, $"Player {surnameEditView.Text} has been edited", ToastLength.Short).Show();
                    StartActivity(typeof(EditPlayerMainActivity));
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Error editing player info in the database", ToastLength.Short).Show();
                }
            }
        }

        private void UpdatePlayerInfo(decimal price)
        {
            editingPlayer.Firstname = firstnameEditView.Text;
            editingPlayer.Surname = surnameEditView.Text;
            editingPlayer.Price = price;
            editingPlayer.PositionID = selectedPosition.PositionID;
            editingPlayer.PremierTeamID = selectedPremTeam.PremierTeamID;
        }

        private bool ValidateFields(ref decimal price)
        {
            if (Util.ValidateText(firstnameEditView.Text, surnameEditView.Text, priceEditView.Text) && Util.ValidateDecimal(priceEditView.Text, ref price))
            {
                return true;
            }
            Toast.MakeText(this, "Please enter valid fields", ToastLength.Short).Show();
            return false;
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(EditPlayerMainActivity));
            Finish();
        }
    }
}