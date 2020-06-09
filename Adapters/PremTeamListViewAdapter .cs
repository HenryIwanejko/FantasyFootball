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

namespace FantasyFootball.Adapters
{
    public class PremierTeamListViewAdapter : BaseAdapter<PremierTeam>
    {
        private ISQLiteRepository sqlLiteRepository = new SQLiteRepository();

        Activity context;
        List<PremierTeam> premierTeams;

        public PremierTeamListViewAdapter(Activity context, List<PremierTeam> premierTeams)
        {
            this.context = context;
            this.premierTeams = premierTeams;
        }

        public override PremierTeam this[int position] 
        { 
            get
            {
                return premierTeams[position];
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            PremierTeamListViewAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as PremierTeamListViewAdapterViewHolder;

            if (holder == null)
            {
                holder = new PremierTeamListViewAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.spinner_item, parent, false);
                holder.PremierTeamName = view.FindViewById<TextView>(Resource.Id.spinnerItemTextField);
                view.Tag = holder;
            }

            PremierTeam premierTeam = premierTeams[position];
            holder.PremierTeamName.Text = premierTeam.PremierTeamName;

            return view;
        }

        public override int Count
        {
            get
            {
                return premierTeams.Count;
            }
        }
    }

    class PremierTeamListViewAdapterViewHolder : Java.Lang.Object
    {
        public TextView PremierTeamName { get; set; }
    }
}