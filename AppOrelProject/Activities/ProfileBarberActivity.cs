﻿using System;

using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.OS;
using Android.Widget;
using AppOrelProject.Helpers;
using AppOrelProject.Models;
using Firebase.Firestore;
using Java.Util;

namespace AppOrelProject.Activities
{
    [Activity(Label = "ProfileBarberActivity")]
    public class ProfileBarberActivity : Activity,IOnSuccessListener,Firebase.Firestore.IEventListener
    {
        EditText etProfileUserName, etProfilePassword, etProfileEmail, etProfileFullName, etProfilePhoneNumber;
       // Button btnRegister;
        FbData fbd;
        User user;
       
        string uid;
        public static string id;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          
            SetContentView(Resource.Layout.ProfileBarberlayout);
            uid = Intent.GetStringExtra("uid");
            InitObject();
            InitViews();
            GetProfileAsync();


        }

        private async void GetProfileAsync()
        {
            await fbd.GetCollection(General.FS_COLLECTION, uid).AddOnSuccessListener(this);
        }

        private void InitViews()
        {
            etProfileEmail = FindViewById<EditText>(Resource.Id.etProfileEmail);
            etProfileFullName = FindViewById<EditText>(Resource.Id.etProfileFullName);
            etProfilePassword = FindViewById<EditText>(Resource.Id.etProfilePassWord);
            etProfileUserName = FindViewById<EditText>(Resource.Id.etProfileUserName);
            etProfilePhoneNumber = FindViewById<EditText>(Resource.Id.etProfilePhoneNumber);
          
            
        }

        private void InitObject()
        {
            fbd = new FbData();
            user = new User();
            fbd.AddCollectionSnapShotListener(this, General.FS_COLLECTION);
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var snapshot = (DocumentSnapshot)result;
            user= new User(snapshot.Id,snapshot.Get("UserName").ToString(),snapshot.Get("Phonenumber").ToString());
            PrintUser(user);    
        }

        private void PrintUser(User user)
        {
            etProfileUserName.Text = user.UserName;
            etProfilePhoneNumber.Text = user.Phonenumber;

        }

        public void OnEvent(Java.Lang.Object obj, FirebaseFirestoreException error)
        {
            //nothing
        }
    }
}