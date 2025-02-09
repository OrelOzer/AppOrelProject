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

namespace AppOrelProject.Helpers
{
    internal class General
    {
        public const string FS_COLLECTION = "UsersCollection";
        public const string KEY_ID = "Id";
        public const string KEY_FULLNAME = "Fullname";
        public const string KEY_USERNAME = "UserName";
        public const string KEY_EMAIL = "Email";
        public const string KEY_PASSWORD = "Password";
        public const string KEY_PHONENUMBER = "Phonenumber";
        public const string KEY_URL = "ImageURL";

        public const string FS_COLLECTIONAPP = "AppointmentCollection";
        public const string ID = "Id";
        public const string FULLNAME = "Fullname";
        public const string DAY = "Day";
        public const string HOUR = "Hour";
        public const string BARBERID = "BarberId";
    }
}