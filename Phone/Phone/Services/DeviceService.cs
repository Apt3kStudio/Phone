using Android.Gms.Wearable;
using Phone.Droid;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phone.Services
{
    public class DeviceService
    {
        public DeviceService()
        {
            ConnectionService cServ = new ConnectionService();
            cServ.DeviceDiscovery();
            
        }

        private void RegisterOnCapabilityChanged(ConnectionService cServ)
        {
            cServ.OnCapabilityChangedReceived += Deviceregistration;
            cServ.Resume();
        }

        private void Deviceregistration(List<INode> nodes)
        {
            
        }
    }
}
