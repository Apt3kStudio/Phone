using Phone.ViewModels;
using Phone.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Android.Content;

namespace Phone.Models
{
    public class Account:IUser
    {
        public Account(Context context)
        {
            _context = context;
        }

        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public Context _context { get; set; }

        public Page IsUseregistered(bool isEnabaled)
        {
            bool isUserRegistered = (isEnabaled == true) ? isEnabaled : LoginUserViewModel.IsUseregisteredAsync().Result;
            if (isUserRegistered)
            {
                return new NavigationPage(new HomePage(_context));
            }
            else
            {
                return new Login(_context);
            }
        }


    }
}
