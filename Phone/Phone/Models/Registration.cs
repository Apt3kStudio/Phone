using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phone.Models
{
   public class Registration : IUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FBToken { get; internal set; }
        public string DeviceName { get; set; }
    }
}
