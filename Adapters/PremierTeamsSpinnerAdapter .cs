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
    public class PremierTeamsSpinnerAdapter : BaseAdapter<PremierTeam>
    {
        private readonly Activity context;

        private readonly List<PremierTeam> premierTeams;

        public PremierTeamsSpinnerAdapter(Activity context, List<PremierTeam> positions)
        {
            this.context = context;
            this.premierTeams = positions;
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
            PositionsSpinnerAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as PositionsSpinnerAdapterViewHolder;

            if (holder == null)
            {
                holder = new PositionsSpinnerAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.spinner_item, parent, false);
                holder.TextField = view.FindViewById<TextView>(Resource.Id.spinnerItemTextField);
                view.Tag = holder;
            }

            holder.TextField.Text = premierTeams[position].PremierTeamName;

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

    class PremierTeamsSpinnerAdapterViewHolder : Java.Lang.Object
    {
        public TextView TextField { get; set; }
    }
}