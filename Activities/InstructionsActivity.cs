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

namespace FantasyFootball
{
    [Activity(Label = "InstructionsActivity")]
    public class InstructionsActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_instructions);
            AddEventHandlers();
        }

        // Map elements to event handlers
        private void AddEventHandlers()
        {
            FindViewById<Button>(Resource.Id.instructionsBackBtn).Click += BackBtn_Click; ;
        }

        // On back button click go back to main page
        private void BackBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }
    }
}