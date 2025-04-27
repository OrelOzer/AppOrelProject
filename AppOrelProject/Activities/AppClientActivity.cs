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

namespace AppOrelProject.Activities
{
    [Activity(Label = "AppClientActivity")]
    public class AppClientActivity : Activity,IOnCompleteListener,IEventListener
    {
        ListView listApplv;
        List<Appointment> lstApp;
        AppAdapter apa;
        FbData fbd;
        string uid;
        string result;
        string phone;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DeleteAppointmentlayout);
           phone= Intent.GetStringExtra("phone");
            InitObject();
            InitViews();
            GetList();
        }

        private async void GetList()
        {
            Toast.MakeText(this, "GettingList", ToastLength.Short).Show();
            await fbd.GetCollection(General.FS_COLLECTIONAPP).AddOnCompleteListener(this);
        }

        private void InitViews()
        {
            listApplv = FindViewById<ListView>(Resource.Id.ListApplv);
            listApplv.ItemClick += ListAppLv_ItemClick;
        }

        private async void ListAppLv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Appointment app = lstApp[e.Position];
            if (await DeleteAppAsync(app.Id))
            {
                Toast.MakeText(this, "Delete successfully", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Delete Failed", ToastLength.Short).Show();
            }
        }

        private async System.Threading.Tasks.Task<bool> DeleteAppAsync(string id)
        {
            try
            {
                await fbd.DeleteFsDocument(General.FS_COLLECTIONAPP, id);
            }
            catch (System.Exception ex)
            {
                string s = ex.Message;
                return false;
            }
            return true;
        }

        private void InitObject()
        {
            fbd = new FbData();
            fbd.AddCollectionSnapShotListener(this, General.FS_COLLECTION);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                lstApp = GetDocuments((QuerySnapshot)task.Result);
                if (lstApp.Count != 0)
                {
                    Toast.MakeText(this, "Ok", ToastLength.Short).Show();

                }
                else
                {
                    Toast.MakeText(this, "Empty", ToastLength.Short).Show();
                }
            }
        }

        private List<Appointment> GetDocuments(QuerySnapshot result)
        {
            lstApp = new List<Appointment>();
            foreach (DocumentSnapshot item in result.Documents)
            {
                Appointment app = new Appointment()
                {
                    Id = item.Id,
                    BarberId = item.Get(General.BARBERID).ToString(),
                    Day = DateTime.Parse(item.Get(General.DAY).ToString()),
                    Hour = double.Parse(item.Get(General.HOUR).ToString()),
                    Phonenumber=item.Get(General.KEY_PHONENUMBER).ToString()

                };
                if (app.Phonenumber==phone)
                {
                    lstApp.Add(app);

                }
               

            }
            apa = new AppAdapter(this, lstApp);
            listApplv.Adapter = apa;
            return lstApp;

        }

        public void OnEvent(Java.Lang.Object obj, FirebaseFirestoreException error)
        {
            //do nothing
        }
    }
}
