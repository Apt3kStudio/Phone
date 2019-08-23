using Android.Gms.Wearable;
using Phone.Droid;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phone.Services
{
    public class DeviceService
    {
        public Func<object> ReceiveConnectedNodes { get; internal set; }
        ConnectionService cServ;
        public DeviceService()
        {
            cServ  = new ConnectionService();
            SubscribeToGetDiscoveredNodes(cServ);
        }

        public void InitiateDiscovery()
        {
            cServ.DeviceDiscovery();           
        }

        private void SubscribeToGetDiscoveredNodes(ConnectionService cServ)
        {
            cServ.GetNodeSubscriber += ActionReceiveConnectedNodes;
        }
        private void ActionReceiveConnectedNodes(List<INode> nodes)
 {
            WebPortalApiServices wapi = new WebPortalApiServices();
            Task.Run(()=>wapi.GetRegisteredDevices());
            SubscribeToUnregisteredDevicesDiscovered(nodes);
        }
        public event Action<List<INode>> SubscribeToUnregisteredDevicesDiscovered= delegate { };
    }
}
