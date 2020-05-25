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

namespace FantasyFootball
{
    [Activity(Label = "PickTeamActivity")]
    public class PickTeamActivity : Activity
    {
        private readonly SQLiteRepository sqlLiteRepository = new SQLiteRepository();

        private Button backBtn;
        private Button submitBtn;
        private ListView playerSelectionLstView;
        private Spinner positionSpinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_pick_team);
            RetrieveElements();
            AddEventHandlers();
            PopulateElements();
        }

        private void PopulateElements()
        {

            List<Position> positions = sqlLiteRepository.GetPositions();
            List<String> positionNames = new List<String>();
            foreach (var position in positions)
            {
                positionNames.Add(position.PositionName);
            }
            ArrayAdapter arrayAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, positionNames);
            positionSpinner.Adapter = arrayAdapter;
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
            backBtn.Click += PickTeamActivity_Click;
            submitBtn.Click += SubmitBtn_Click;
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