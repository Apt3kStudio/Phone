using Android.Gms.Wearable;
using Phone.Droid;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phone.Services
{
    public class DeviceRegistrationService
    {
        public DeviceRegistrationService()
        {
            ConnectionService cServ = new ConnectionService();
            cServ.OnCapabilityChangedReceived += Deviceregistration;
            cServ.Resume();


        }

        private void Deviceregistration(List<INode> nodes)
        {
            
        }
    }
}
