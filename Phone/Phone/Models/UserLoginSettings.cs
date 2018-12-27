using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phone.Models
{
    public class UserLoginSettings 
    {   
        [PrimaryKey, AutoIncrement]
        public int ID { get; internal set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { set; get; }
    }
}
