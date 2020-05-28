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
    class PlayersListViewAdapter : BaseAdapter<Player>
    {
        private ISQLiteRepository sqLiteRepository = new SQLiteRepository();

        Activity context;
        List<Player> players;

        public PlayersListViewAdapter(Activity context, List<Player> players)
        {
            this.context = context;
            this.players = players;
        }

        public override Player this[int position] 
        { 
            get
            {
                return players[position];
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
            ListViewAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as ListViewAdapterViewHolder;

            if (holder == null)
            {
                holder = new ListViewAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.listview_item, parent, false);
                holder.PlayerName = view.FindViewById<TextView>(Resource.Id.listViewItemNameField);
                holder.Price = view.FindViewById<TextView>(Resource.Id.listViewItemPriceField);
                holder.PremierTeam = view.FindViewById<TextView>(Resource.Id.listViewItemPremierTeamField);
                view.Tag = holder;
            }

            Player player = players[position];
            holder.PlayerName.Text = player.Firstname != null ?  $"{player.Firstname} {player.Surname}" : player.Surname;
            holder.Price.Text = $"£{player.Price}m";
            holder.PremierTeam.Text = sqLiteRepository.GetPremierTeam(player.PremierTeamID).PremierTeamName;

            return view;
        }

        public override int Count
        {
            get
            {
                return players.Count;
            }
        }
    }

    class ListViewAdapterViewHolder : Java.Lang.Object
    {
        public TextView PlayerName { get; set; }
        
        public TextView Price { get; set; }

        public TextView PremierTeam { get; set; }
    }
}