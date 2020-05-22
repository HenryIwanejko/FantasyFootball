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
using FantasyFootballSQLDB;

namespace FantasyFootball
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterTeamActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register_team);
            AddEventHandlers();
        }

        private void AddEventHandlers()
        {
            FindViewById<Button>(Resource.Id.registerTeamBackBtn).Click += RegisterTeamActivity_Click;
            FindViewById<Button>(Resource.Id.registerTeamSubmitBtn).Click += SubmitButton_Click;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            List<Player> players = new SQLiteRepository().GetPlayers();
            Console.WriteLine(players.ToString());
            StartActivity(typeof(MainActivity));
        }

        private void RegisterTeamActivity_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}