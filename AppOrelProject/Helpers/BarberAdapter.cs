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
    internal class BarberAdapter : BaseAdapter<User>
    {

        Context context;
        private List<User> lstUsers;

        public BarberAdapter(Context context)
        {
            this.context = context;
           
        }
        public BarberAdapter(Context context, List<User> lstUsers) 
        {
            this.lstUsers = lstUsers;
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
            layoutInflater = ((ListBarberActivity)context).LayoutInflater;
            View view = layoutInflater.Inflate(Resource.Layout.ListBarberRowlayout, parent, false);
            TextView UserListRowName = view.FindViewById<TextView>(Resource.Id.UserListRowNameTextView);
            TextView UserListRowPhoneNumber = view.FindViewById<TextView>(Resource.Id.UserListRowPhoneNumberTextView);
           
            User user = lstUsers[position];
            if (user != null)
            {
                UserListRowName.Text = user.Fullname;
                UserListRowPhoneNumber.Text =  user.Phonenumber;
              
            }

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return lstUsers.Count;
            }
        }

        public override User this[int position]
        {
            get
            {
                return lstUsers[position];    
            }
        }
       
    }

    internal class BarberAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}