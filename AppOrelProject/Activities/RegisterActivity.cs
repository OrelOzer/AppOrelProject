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
using AppOrelProject.Models;
using Firebase.Firestore;
using Java.Util;


namespace AppOrelProject.Activities
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        EditText etRegUserName, etRegPassword, etRegEmail, etRegFullName,etRegPhoneNumber;
        Button btnRegister;
        FbData fbd;
        User user;
        HashMap ha;
        string uid;
        public static string id;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Title=string.Empty;
            SetContentView(Resource.Layout.Registerlayout);
            InitObject();
            InitViews();

            
        }

        private void InitViews()
        {
            etRegEmail =FindViewById<EditText>(Resource.Id.etRegEmail);
            etRegFullName= FindViewById<EditText>(Resource.Id.etRegFullName);
            etRegPassword = FindViewById<EditText>(Resource.Id.etRegPassWord);
            etRegUserName= FindViewById<EditText>(Resource.Id.etRegUserName);
            etRegPhoneNumber= FindViewById<EditText>(Resource.Id.etRegPhoneNumber);
            btnRegister = FindViewById<Button>(Resource.Id.btnSubmit);
            btnRegister.Click += BtnRegister_Click;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            bool check = CheckClass.ChackInputData(etRegFullName.Text, etRegPhoneNumber.Text);
            if (check)
            {
                SaveDocument();
            }
            else
            {
                Toast.MakeText(this, "invalid name or phone number ", ToastLength.Short).Show();

            }
           
        }

        private async void SaveDocument()
        {
            if (await Register(etRegFullName.Text,etRegUserName.Text ,etRegEmail.Text, etRegPassword.Text, etRegPhoneNumber.Text))
           
            {
                Toast.MakeText(this,"Register successfully ",ToastLength.Short).Show();
                etRegFullName.Text = "";
                etRegEmail.Text = "";
                etRegPassword.Text = "";
                etRegPhoneNumber.Text = "";
                etRegUserName.Text = "";
                Intent intent = new Intent(this,typeof(MainActivity));
                StartActivity(intent);



            }
            else
            {
                Toast.MakeText(this, "Register Failed", ToastLength.Short).Show();
            }

        }

        private async Task<bool> Register(string FullName, string UserName, string Email, string Password, string PhoneNumber)
        {
            try
            {
                await fbd.auth.CreateUserWithEmailAndPassword(Email, Password);
                id = fbd.auth.CurrentUser.Uid;
                HashMap userMap = new HashMap();
                userMap.Put(General.KEY_FULLNAME, FullName);
                userMap.Put(General.KEY_EMAIL, Email);
                userMap.Put(General.KEY_USERNAME, UserName);
                userMap.Put(General.KEY_PASSWORD, Password);
                userMap.Put(General.KEY_PHONENUMBER, PhoneNumber);  
                userMap.Put(General.KEY_ISBARBER,false);
                userMap.Put(General.KEY_ID, fbd.auth.CurrentUser.Uid);
                DocumentReference userReference = fbd.firestore.Collection(General.FS_COLLECTION).Document(fbd.auth.CurrentUser.Uid);
                await userReference.Set(userMap);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;


        }

        private void InitObject()
        {
            fbd = new FbData();
            user = new User();
        }
    }
}