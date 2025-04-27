using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Android.Views.View;

namespace AppOrelProject.Activities
{
    [Activity(Label = "ClientActivityMenu")]
    public class ClientActivityMenu : Activity,IOnClickListener
    {
        Button btnBarberList, btnClientProfile, btnClientApp;
        string uid, phoneNum;

        public void OnClick(View v)
        {
            if (v == btnBarberList)
            {
                Intent intent = new Intent(this, typeof(ListBarberActivity));
                StartActivity(intent);

            }
            else if (v == btnClientProfile)
            {
                Intent intent = new Intent(this, typeof(ProfileBarberActivity));
                intent.PutExtra("uid", uid);

                StartActivity(intent);

            }
            else if (v == btnClientApp)
            {

                Intent intent = new Intent(this, typeof(AppClientActivity));
                intent.PutExtra("uid", uid);
                intent.PutExtra("phone",phoneNum);  
                StartActivity(intent);
            }

            }

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ClientMenulayout);
            uid = Intent.GetStringExtra("uid");
            phoneNum = Intent.GetStringExtra("phone");
            InitViews();


        }

        private void InitViews()
        {
            btnBarberList = FindViewById<Button>(Resource.Id.btnBarberList);
           
            btnClientProfile = FindViewById<Button>(Resource.Id.btnClientProfile);
          
            btnClientApp = FindViewById<Button>(Resource.Id.btnClientApp);
            btnClientApp.SetOnClickListener(this);  
            btnBarberList.SetOnClickListener(this);
            btnClientProfile.SetOnClickListener(this);
            
        }
    }
}