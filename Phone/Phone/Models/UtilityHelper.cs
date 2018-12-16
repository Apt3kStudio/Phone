using System;
using System.Collections.Generic;
using System.Text;

namespace Phone.Models
{
   public static class UtilityHelper
    {
        public static Guid getNewID()
        {
            Guid id = Guid.NewGuid();
            return id;
        }
    }
}
