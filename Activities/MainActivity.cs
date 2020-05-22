using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.IO;
using FantasyFootball.Activities;

namespace FantasyFootball
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            AddEventHandlers();
            InitialiseDatabase();
        }

        private void InitialiseDatabase()
        {
            var docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var dbFile = Path.Combine(docFolder, "db.sqlite");
            if (!File.Exists(dbFile))
            {
                var dbRaw = Resources.OpenRawResource(Resource.Raw.FantasyTeams);
                FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);
                ReadWriteStream(dbRaw, writeStream);
            }
        }

        private void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int length = 256;
            byte[] buffer = new byte[length];
            int bytesRead = readStream.Read(buffer, 0, length);
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, length);
            }
            readStream.Close();
            writeStream.Close();
        }


        private void AddEventHandlers()
        {
            FindViewById<Button>(Resource.Id.mainInstructionsBtn).Click += InstructionsBtn_Click;
            FindViewById<Button>(Resource.Id.mainRegisterTeamBtn).Click += RegisterTeamBtn_Click;
            FindViewById<Button>(Resource.Id.mainPickTeamsBtn).Click += PickTeamBtn_Click;
            FindViewById<Button>(Resource.Id.mainAdminBtn).Click += AdminBtn_Click;
        }

        private void AdminBtn_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(AdminActivity));
        }

        private void PickTeamBtn_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(PickTeamActivity));
        }

        private void RegisterTeamBtn_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(RegisterTeamActivity));
        }

        private void InstructionsBtn_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(InstructionsActivity));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}