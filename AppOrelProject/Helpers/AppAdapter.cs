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
using AppOrelProject.Activities;
using AppOrelProject.Models;

namespace AppOrelProject.Helpers
{
    internal class AppAdapter : BaseAdapter<Appointment>
    {
        Context context;
        private List<Appointment> lstApp;

        public AppAdapter(Context context)
        {
            this.context = context;

        }
        public AppAdapter(Context context, List<Appointment> lstApp)
        {
            this.lstApp = lstApp;
            this.context = context;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layoutInflater;
            layoutInflater = ((DeleteApointmentActivity)context).LayoutInflater;
            View view = layoutInflater.Inflate(Resource.Layout.AppRowlayout, parent, false);
            TextView AppHourRow = view.FindViewById<TextView>(Resource.Id.AppHourTextView);
            TextView AppDateRow = view.FindViewById<TextView>(Resource.Id.AppDateTextView);
            TextView AppBarberRow = view.FindViewById<TextView>(Resource.Id.AppBarberIdTextView);

            Appointment app = lstApp[position];
            if (app != null)
            {
                AppHourRow.Text = app.Hour.ToString();
                AppDateRow.Text = app.Day.ToString();
                AppBarberRow.Text= app.BarberId.ToString();

            }

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return lstApp.Count;
            }
        }

        public override Appointment this[int position]
        {
            get
            {
                return lstApp[position];
            }
        }

    }

    internal class AppAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}