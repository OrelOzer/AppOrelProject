using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AppOrelProject.Activities;

namespace AppOrelProject
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //comment to check gitHub
        Button btnLogin, btnRegister,BtnList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            InitViews();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void InitViews()
        {
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += BtnLogin_Click1;
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
           btnRegister.Click += btnRegister_Click;
            BtnList=FindViewById<Button>(Resource.Id.BtnList);
            BtnList.Click += BtnList_Click;
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ListBarberActivity));
            StartActivity(intent);

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegisterActivity));
            StartActivity(intent);
        }

        private void BtnLogin_Click1(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }

        //private void btnRegister_Click(object? sender, EventArgs e)
        //{
        //    Intent intent = new Intent(this, typeof(Register));
        //    StartActivity(intent);
        //}


    }
}
