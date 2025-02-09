using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
using Google.Firestore.V1;

namespace AppOrelProject.Activities
{
    [Activity(Label = "ListBarberActivity")]
    public class ListBarberActivity : Activity,IOnCompleteListener,IEventListener
    {
        ListView listUserLv;
        List<User> lstbarber;
        BarberAdapter ba;
        FbData fbd;
        string uid;
        string result;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ListBarberlayout);
            InitObject();
            InitViews();
            GetList();
        }

        private async void GetList()
        {
          Toast.MakeText(this,"GettingList",ToastLength.Short).Show();
            await fbd.GetCollection(General.FS_COLLECTION).AddOnCompleteListener(this);
        }

        private void InitViews()
        {
            listUserLv=FindViewById<ListView>(Resource.Id.ListUserLv);
            listUserLv.ItemClick += ListUserLv_ItemClick;
        }

        private void ListUserLv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            User barber= lstbarber[e.Position];
            Intent intent = new Intent(this,typeof(AddApointmentActivity));
            intent.PutExtra("Id",barber.Id);
            StartActivity(intent);  
        }

        private void InitObject()
        {
            fbd = new FbData();
            fbd.AddCollectionSnapShotListener(this,General.FS_COLLECTION);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                lstbarber = GetDocuments((QuerySnapshot)task.Result);
                if (lstbarber.Count != 0)
                {
                    Toast.MakeText(this,"Ok",ToastLength.Short) .Show();

                }
                else
                {
                    Toast.MakeText(this,"Empty",ToastLength.Short) .Show();
                }
            }
        }

        private List<User> GetDocuments(QuerySnapshot result)
        {
            lstbarber= new List<User>();
            foreach(DocumentSnapshot item in result.Documents)
            {
                User user = new User()
                {
                    Id = item.Id,
                    Fullname=item.Get(General.KEY_FULLNAME).ToString(),
                    Email= item.Get(General.KEY_EMAIL).ToString(),
                    Phonenumber= item.Get(General.KEY_PHONENUMBER).ToString(),

                };
                lstbarber.Add(user); 
              
            }
            ba=new BarberAdapter(this,lstbarber);
            listUserLv.Adapter = ba;
            return lstbarber;
            
        }

        public void OnEvent(Java.Lang.Object obj, FirebaseFirestoreException error)
        {
            //do nothing
        }
    }
}