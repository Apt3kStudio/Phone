using System;
using Android.App;
using Android.Gms.Common.Apis;
using Android.Gms.Wearable;
using Android.Content;
using Android.Runtime;
using System.Linq;
using Android.Util;

namespace SpideySenseWatch
{
    [Service]
    [IntentFilter(new string[] { "com.google.android.gms.wearable.BIND_LISTENER" })]
    //[IntentFilter(new string[] { "com.google.android.gms.wearable.CAPABILITY_CHANGED" })]
    public class DisconnectListenerService : WearableListenerService, IResultCallback
    {
        readonly GoogleApiClient client;
        const string path = "/my_capability";
        string capabilityName = "my_capability";
        public INode phoneNode;
        const string TAG = "ExampleFindPhoneApp";
        const int FORGOT_PHONE_NOTIFICATION_ID = 1;
        Context _context;
        public DisconnectListenerService()
        {
        }
        public DisconnectListenerService(Context context)
        {
            _context = context;
            client = new GoogleApiClient.Builder(context)
                .AddApi(WearableClass.API)
                .Build();
            client.Connect();
            var capabilitiesTask = WearableClass.CapabilityApi.GetAllCapabilities(client, CapabilityApi.FilterReachable);
            var result = WearableClass.CapabilityApi.GetCapability(client, capabilityName, CapabilityApi.FilterReachable);
        }
        public override void OnPeerDisconnected(INode p0)
        {               
        }
        public override void OnCapabilityChanged(ICapabilityInfo capabilityInfo)
        {
        }
        public override void OnPeerConnected(INode p0)
        {
                
        }
        public override void OnDataChanged(DataEventBuffer dataEvents)
        {

        }
        private void updateStatus()
        {
            WearableClass.CapabilityApi.GetCapability(
                    client, capabilityName,
                    CapabilityApi.FilterReachable).SetResultCallback(this);
        }
        private void updateConnectionCapability(ICapabilityInfo capabilityInfo)
        {
          var connectedNodes = capabilityInfo.Nodes;
            if (connectedNodes!=null)
            {
                // The connection is lost !
            }
            else
            {
                foreach(INode node in connectedNodes)
                {
                    if (node.IsNearby)
                    {
                        // The connection is OK !
                    }
                }
            }
        }
        public void OnResult(Java.Lang.Object result)
        {
            var apiResult = result.JavaCast<ICapabilityApiGetCapabilityResult>();
            var nodes = apiResult.Capability.Nodes;
            phoneNode = nodes.FirstOrDefault();
            if (phoneNode == null)
            {
               // DisplayError();
                return;
            }

            if (apiResult.Status.IsSuccess)
            {
                updateConnectionCapability(apiResult.Capability);
            }
            else
            {
                Log.Info("Spidey", "Communicator: Sending message ");
            }          
        }      
    }
}

