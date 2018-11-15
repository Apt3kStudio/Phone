using System;
using System.Collections.Generic;
using System.Text;

namespace Phone.Models
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName1 { get; set; }

        public string FullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
