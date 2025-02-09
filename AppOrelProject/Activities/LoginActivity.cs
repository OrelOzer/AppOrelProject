using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppOrelProject.Helpers;

namespace AppOrelProject.Activities
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        EditText etMail, etPass;
        TextView TvDisplay;
        Button btnLogin;

        FbData fbd;

        string uid;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Loginlayout);
            InitViews();
            InitObject();


            // Create your application here
        }
        private void InitObject()
        {
            fbd = new FbData();
        }

        private void InitViews()
        {
            etMail = FindViewById<EditText>(Resource.Id.etMail);
            etPass = FindViewById<EditText>(Resource.Id.etPass);
            btnLogin = FindViewById<Button>(Resource.Id.btnSubmit);
            TvDisplay = FindViewById<TextView>(Resource.Id.tvDisplay);
            btnLogin.Click += BtnLogin_Click;
        }

        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            if (await LoginUser(etMail.Text, etPass.Text))
            {
                Toast.MakeText(this, "Logged In Successfully", ToastLength.Short).Show();
                etMail.Text = "";
                etPass.Text = "";
                Intent intent = new Intent(this, typeof(ProfileBarberActivity));
                intent.PutExtra("uid", uid);
                StartActivity(intent);

            }
            else
            {
                Toast.MakeText(this, "LoginFailed", ToastLength.Short).Show();
            }
        }

        public async Task<bool> LoginUser(string etMail, string etPass)
        {
            try
            {
                await fbd.auth.SignInWithEmailAndPassword(etMail, etPass);
                uid = fbd.auth.CurrentUser.Uid;
            }
            catch (System.Exception ex)
            {
                string s = ex.Message;
                return false;


            }
            return true;

        }

    }
}
