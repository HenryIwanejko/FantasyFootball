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

namespace FantasyFootball.Adapters
{
    class PositionsSpinnerAdapter : BaseAdapter<Position>
    {
        private readonly Activity context;

        private readonly List<Position> positions;

        public PositionsSpinnerAdapter(Activity context, List<Position> positions)
        {
            this.context = context;
            this.positions = positions;
        }

        public override Position this[int position] 
        { 
            get
            {
                return positions[position];
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

            holder.TextField.Text = positions[position].PositionName;

            return view;
        }

        public override int Count
        {
            get
            {
                return positions.Count;
            }
        }
    }

    class PositionsSpinnerAdapterViewHolder : Java.Lang.Object
    {
        public TextView TextField { get; set; }
    }
}