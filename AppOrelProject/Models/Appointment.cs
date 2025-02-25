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


using Java.Util;

namespace AppOrelProject.Models
{
    internal class Appointment
    {
      
        public string Id { get; set; }
        public string Fullname { get; set; }
       
        public string Phonenumber { get; set; }
        public System.DateTime Day { get; set; }
        public double Hour { get; set; }
        public string BarberId { get; set; }
        public Appointment()
        {
           
            Id = string.Empty;
            Fullname = string.Empty;
            Phonenumber = string.Empty;
            BarberId = string.Empty;
        }
        public Appointment( string id, string fullname,  string phonenumber,string bid,DateTime d,double hour)
        {
          
            Id = id;
            Fullname = fullname;
            Phonenumber = phonenumber;
            BarberId=bid;   
            Day = d;
            Hour = hour;
        }


    }
}