using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppOrelProject.Helpers;
using AppOrelProject.Models;
using Firebase.Firestore;

namespace AppOrelProject.Activities
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity,IOnSuccessListener
    {
        EditText etMail, etPass;
        TextView TvDisplay;
        Button btnLogin;
        User user;

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
                //etMail.Text = "";
                //etPass.Text = "";
                CheckIfBarberAsync();
                

            }
            else
            {
                Toast.MakeText(this, "LoginFailed", ToastLength.Short).Show();
            }
        }

        private async void CheckIfBarberAsync()
        {
         
        
            await fbd.GetCollection(General.FS_COLLECTION, uid).AddOnSuccessListener(this);
        
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

        public void OnSuccess(Java.Lang.Object result)
        {
            var snapshot = (DocumentSnapshot)result;
            user = new User(snapshot.Id, snapshot.Get("UserName").ToString(), bool.Parse(snapshot.Get("IsBarber").ToString()), snapshot.Get("Phonenumber").ToString());
            if (user.IsBarber == false)// אם לקוח תפריט לקוח
            {
                Toast.MakeText(this, user.IsBarber.ToString(), ToastLength.Short).Show();
                Intent intent = new Intent(this, typeof(ClientActivityMenu));
                intent.PutExtra("uid", user.Id);
                intent.PutExtra("phone", user.Phonenumber);
                StartActivity(intent);
            }
            else if (etMail.Text == General.ADMINMAIL && etPass.Text == General.ADMINPASSWORD)//אם מנהל תפריט מנהל  
            {
                Toast.MakeText(this, user.IsBarber.ToString(), ToastLength.Short).Show();
                Intent intent = new Intent(this, typeof(AdminActivity));
                intent.PutExtra("uid", user.Id);
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(this, user.IsBarber.ToString(), ToastLength.Short).Show();// אם ברבר תפריט ברבר  
                Intent intent = new Intent(this, typeof(DeleteApointmentActivity));
                intent.PutExtra("uid", user.Id);
                StartActivity(intent);
            }
        }
    }
}
