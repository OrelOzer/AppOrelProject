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

namespace AppOrelProject.Activities
{
    [Activity(Label = "AdminActivity")]
    public class AdminActivity : Activity
    {
        Button LAB, BarberList, ClientList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AdminMenulayout);
            InitViews();
           // InitObject();
        }

        //private void InitObject()
        //{
        //    throw new NotImplementedException();
        //}

        private void InitViews()
        {
            LAB = FindViewById<Button>(Resource.Id.LAB);
            BarberList=FindViewById<Button>(Resource.Id.BarberList);
            ClientList = FindViewById<Button>(Resource.Id.ClientList);
            LAB.Click += LAB_Click;
            BarberList.Click += BarberList_Click;
            ClientList.Click += ClientList_Click;
        }

        private void ClientList_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ClientListActivity));
            StartActivity(intent);
        }

        private void BarberList_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ListBarberActivity));
            StartActivity(intent);
        }

        private void LAB_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(BarberOptionActivity));
            StartActivity(intent);
        }
    }
}