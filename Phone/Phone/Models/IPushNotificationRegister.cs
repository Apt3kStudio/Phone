using System;
using System.Collections.Generic;
using System.Text;

namespace Phone.Models
{
    public interface IPushNotificationRegister
    {
        void ExtractTokenAndRegister();
    }
}
