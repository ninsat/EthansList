﻿using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using System.Linq;
using EthansList.Shared;

namespace ethanslist.android
{
    public class CityListAdapter : BaseAdapter<Location>
    {
        Activity context;
        IEnumerable<Location> cities;

        public CityListAdapter(Activity context, IEnumerable<Location> cities)
        {
            this.context = context;
            this.cities = cities;
        }

        public override Location this[int index]
        {
            get
            {
                return cities.ElementAt(index);
            }
        }

        public override int Count
        {
            get
            {
                return cities.Count();
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.CityRow, parent, false);
                var _city = view.FindViewById<TextView>(Resource.Id.cityListViewItem);

                view.Tag = new CityListViewHolder { City = _city };
            }

            var holder = (CityListViewHolder)view.Tag;

            holder.City.Text = cities.ElementAt(position).SiteName;
            return view;
        }
    }
}

