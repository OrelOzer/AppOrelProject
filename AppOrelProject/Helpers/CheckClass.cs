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
    internal class CheckClass
    {
        private bool ChackInputData(string name,string password,string mail,string phone)
        {
            bool allOk = true;
            
            allOk = NameCheck(name) && MailChack(mail) && PassCheck(password) && PhoneCheck(phone);
            return allOk;

        }

        private bool PhoneCheck(string phone)
        {
            return true;
        }

        private bool PassCheck(string password)
        {
            bool ok = true;
            if (password.Length < 6 || password.Length > 16)
            {
                ok = false;
            }
            if (CheckingCapitalLeters(password) == false)
            {
                ok = false;
            }
            return ok;
        }

        private bool CheckingCapitalLeters(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] >= 'A' && password[i] <= 'Z')
                {
                    return true;
                }


            }
            return false;
        }

        private bool MailChack(string mail)
        {
            int placeShtru = mail.IndexOf('@');
            if (placeShtru <= 0)
            {
                return false;
            }
            if (mail.IndexOf('.', placeShtru) == -1)
                return false;
            return true;
        }

        private bool NameCheck(string name)
        {
            bool ok = true;
            if (name.Length <= 1)
                ok = false;
            if (CheckingNumbers(name) != true)
                ok = false;
            return ok;
        }

        private bool CheckingNumbers(string name)
        {
            char[] charArr = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < charArr.Length; i++)
            {
                if (name.IndexOf(charArr[i]) != -1)
                    return false;
            }
            return true;
        }
    }
}