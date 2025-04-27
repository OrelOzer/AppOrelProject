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

namespace AppOrelProject.Models
{
    internal class User
    {
        public string UserName { get; set; }    
        public string Id{ get; set; }    
        public string Fullname {  get; set; }   
        public string Email { get; set; }
        public string Password {  get; set; }   
        public string Phonenumber {  get; set; }
        public bool IsBarber {  get; set; }   


        public User()
        {
            UserName = string.Empty;
            Id = string.Empty;
            Fullname = string.Empty;
            Email = string.Empty;
            Phonenumber = string.Empty;
            IsBarber = false;
        }

        public User(string userName, string id, string fullname, string email, string phonenumber)
        {
            UserName = userName;
            Id = id;
            Fullname = fullname;
            Email = email;
            Phonenumber = phonenumber;
            IsBarber=false;
        }

        public User(string id, string userName, string phonenumber)
        {
            UserName = userName;
            Id = id;
            Phonenumber = phonenumber;
            IsBarber=false; 
        }

        public User(string id, string userName, bool barber, string phonenumber)
        {
            UserName = userName;
            Id = id;
            Phonenumber=phonenumber;    
          
            IsBarber = barber;
        }
    }
}