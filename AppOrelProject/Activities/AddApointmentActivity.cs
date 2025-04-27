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
using Java.Util;

namespace AppOrelProject.Activities
{
    [Activity(Label = "AddApointmentActivity")]
    public class AddApointmentActivity : Activity, IOnCompleteListener, Firebase.Firestore.IEventListener  
    {
        Appointment appointment=null;
        string id;
        FbData fbd;
        EditText etAppPhoneNumber;
        EditText etAppUserName;
        Button btnAddApointment;
        List<Appointment> lstApp;
        bool exist;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           id =Intent .GetStringExtra("Id");
            SetContentView(Resource.Layout.AddApointmentlayout);
            InitObject();
           InitViews();
            Toast.MakeText(this, id, ToastLength.Short).Show();
            CreateDateDialog();
           
           

            // Create your application here
        }

        private void InitViews()
        {
           etAppPhoneNumber=FindViewById<EditText>(Resource.Id.etAppPhoneNumber);   
            etAppUserName=FindViewById<EditText> (Resource.Id.etAppUserName);
            btnAddApointment=FindViewById<Button>(Resource.Id.btnAppSubmit);
            btnAddApointment.Click += BtnAddApointment_Click;
        }

        private void BtnAddApointment_Click(object sender, EventArgs e)
        {

           GetList();
           
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
                    Hour = double.Parse(item.Get(General.HOUR).ToString())

                };

                if (app.BarberId == appointment.BarberId && app.Day.ToString()==appointment.Day.ToString() && app.Hour==appointment.Hour)
                {
                    lstApp.Add(app);
                }

            }
          
            return lstApp;

        }
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                lstApp = GetDocuments((QuerySnapshot)task.Result);
                if (lstApp.Count != 0)
                {
                    Toast.MakeText(this, "Appointment already exist", ToastLength.Short).Show();

                }
                else
                {
                    SaveDocument();
                }
            }
        }
        private async void GetList()
        {
            Toast.MakeText(this, "GettingList", ToastLength.Short).Show();
            await fbd.GetCollection(General.FS_COLLECTIONAPP).AddOnCompleteListener(this);
        }
        public bool IsExistAppointment()
        {
            GetList();
            if (exist)
            {
                Toast.MakeText(this, "Appointment already exist", ToastLength.Short).Show();
                return true;
            }
            return false;   
        }

        private void InitObject()
        {
            appointment = new Appointment();
            appointment.BarberId = id;
            fbd=new FbData();


        }

        private void CreateTimeDialog()
        {
            DateTime dt = DateTime.Today;
            TimePickerDialog tpd = new TimePickerDialog(this, OnTimeSet, dt.Hour, dt.Minute, true);
            tpd.Show();
        }

        private void OnTimeSet(object? sender, TimePickerDialog.TimeSetEventArgs e)
        {
            string str = e.HourOfDay.ToString() + " " + e.Minute.ToString();
            appointment.Hour = e.HourOfDay;
            Toast.MakeText(this, str, ToastLength.Long).Show();
        }

        private void CreateDateDialog()
        {
            DateTime dt = DateTime.Today;
            DatePickerDialog d = new DatePickerDialog(this, OnDateSet, dt.Year, dt.Month - 1, dt.Day);
            d.Show();

        }

        private void OnDateSet(object? sender, DatePickerDialog.DateSetEventArgs e)
        {
            string str = e.Date.ToString();
            appointment.Day = e.Date;
            Toast.MakeText(this, str, ToastLength.Long).Show();
            CreateTimeDialog();


        }
        private async void SaveDocument()
        {
            if (await SetAppointment( etAppUserName.Text, etAppPhoneNumber.Text))

            {
                Toast.MakeText(this, "Add Appointment successfully ", ToastLength.Short).Show();
                etAppPhoneNumber.Text = "";
                etAppUserName.Text = "";
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);



            }
            else
            {
                Toast.MakeText(this, "Add Appointment Failed", ToastLength.Short).Show();
            }

        }

        private async Task<bool> SetAppointment( string UserName,  string PhoneNumber)
        {
            try
            {
               
                id = fbd.GetNewDocumentId(General.FS_COLLECTIONAPP);
                HashMap appMap = new HashMap();
                appMap.Put(General.DAY,appointment.Day.ToString());
               appMap.Put(General.HOUR,appointment.Hour);
                appMap.Put(General.KEY_USERNAME, UserName);
                appMap.Put(General.KEY_PHONENUMBER, PhoneNumber);
                appMap.Put(General.ID, id);
                appMap.Put(General.BARBERID,appointment.BarberId);
                DocumentReference userReference = fbd.firestore.Collection(General.FS_COLLECTIONAPP).Document(id);
                await userReference.Set(appMap);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;


        }

       

        public void OnEvent(Java.Lang.Object obj, FirebaseFirestoreException error)
        {
            Console.WriteLine(  );
        }
    }
}