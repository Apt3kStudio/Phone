using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Gms.Wearable;
using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using System.Text;
using System.Collections.Generic;
using Android.Runtime;
using Phone.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Phone.Models;
using System.Linq;
using Java.Lang;
using System.Runtime.CompilerServices;

namespace Phone.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.android.gms.wearable.BIND_LISTENER" })]
    public class ConnectionService : Java.Lang.Object, 
                                        MessageClient.IOnMessageReceivedListener, 
                                        DataClient.IOnDataChangedListener, 
                                        CapabilityClient.IOnCapabilityChangedListener,
                                        Android.Gms.Tasks.IOnSuccessListener
    {       
        const string path = "/my_capability";
        string capabilityName = "my_capability";
        Context context;
        private GoogleApiClient client;
        public ConnectionService()
        {
            context = UtilityHelper.GetContext();
            client = new GoogleApiClient.Builder(context)
                .AddApi(WearableClass.API).Build();
            
        }
        public void Resume()
        {
            if (client.IsConnected ==false)
            {
                client.Connect();                                         
            }          

            

            WearableClass.GetDataClient(context).AddListener(this);
            WearableClass.GetMessageClient(context).AddListener(this);
            WearableClass.GetCapabilityClient(context).AddListener(this, capabilityName);
            WearableClass.GetCapabilityClient(context).AddListener(this, Android.Net.Uri.Parse("wear://"), CapabilityClient.FilterReachable);
        }
        public void Pause()
        {
            client.Disconnect();
            WearableClass.GetDataClient(context).RemoveListener(this);
            WearableClass.GetMessageClient(context).RemoveListener(this);
            WearableClass.GetCapabilityClient(context).RemoveListener(this, capabilityName);
            

        }
        public void SendMessage(string message)
        {           if(!client.IsConnected)
                    client.Connect();       
            Task.Run(() => 
            {         
                foreach (var node in Nodes())
                {                  
                    var bytes = System.Text.Encoding.Default.GetBytes(message);
                    var result1 = WearableClass.GetMessageClient(context).SendMessage(node.Id, path, bytes);                 
                    var success = result1.JavaCast<IMessageApiSendMessageResult>().Status.IsSuccess ? "Ok." : "Failed!";                    
                    Log.Info("my_log", "Communicator: Sending message " + message + "... " + success);                 
                }
                client.Disconnect();
            });
        }
        public async Task StartTripAsync(string message)
        {  
                foreach (var node in Nodes())
                {
                    var bytes = System.Text.Encoding.Default.GetBytes(message);
                    var TripResult = await WearableClass.GetMessageClient(context).SendMessageAsync(node.Id, path, bytes);
                                          
                    Log.Info("MainLogic", "Trip Starts Now" + DateTime.Now.ToLocalTime() + "... " );
                }               
        }
        IList<INode> Nodes()
        {
            var result = WearableClass.NodeApi.GetConnectedNodes(client);           
            return result.JavaCast<INodeApiGetConnectedNodesResult>().Nodes;
        }
        public void SendData(DataMap dataMap)
        {
            Task.Run(() => 
            {
                try
                {
                    var request = PutDataMapRequest.Create(path);
                    request.DataMap.PutAll(dataMap);
                    var result = WearableClass.DataApi.PutDataItem(client, request.AsPutDataRequest()).Await();
                    var success = result.JavaCast<IDataApiDataItemResult>().Status.IsSuccess ? "Ok." : "Failed!";
                    Log.Info("my_log", "Communicator: Sending data map " + dataMap + "... " + success);
                }
                catch (System.Exception ex)
                {
                    throw;
                }
            });
        }
        public void OnMessageReceived(IMessageEvent messageEvent)
        {
            var message = System.Text.Encoding.Default.GetString(messageEvent.GetData());
            Log.Info("my_log", "Communicator: Message received \"" + message + "\"");
            EventViewModel eventModel = new EventViewModel();
            if (message.Contains("option") && message.ToString().ToCharArray().Length > ("option").Length)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    await eventModel.setOption(message);
                });
            }
            if (message.Contains("option"))
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () => {
                    await eventModel.TriggerFeatureAsync();
                });
            }           
        }
        public void OnDataChanged(DataEventBuffer p0)
        {
            Log.Info("my_log", "Communicator: Data changed (" + p0.Count + " data events)");
            for (var i = 0; i < p0.Count; i++)
            {
                var dataEvent = p0.Get(i).JavaCast<IDataEvent>();
                if (dataEvent.Type == DataEvent.TypeChanged && dataEvent.DataItem.Uri.Path == path)
                {

                }
            }
        }
        public void OnCapabilityChanged(ICapabilityInfo capabilityInfo)
        {
            var nodes = capabilityInfo.Nodes.ToList();
            GetNodeSubscriber(nodes);
        }
        public void DeviceDiscovery()
        {
            Task.Run(async() => 
            {
                var result2 = await WearableClass
                                        .GetCapabilityClient(context)
                                        .GetAllCapabilitiesAsync(CapabilityClient.FilterReachable);
                List<INode> nodes = result2.Values.First().Nodes.ToList();
                GetNodeSubscriber(nodes);
            });
        }
        public event Action<List<INode>> GetNodeSubscriber = delegate { };

        public void OnSuccess(Java.Lang.Object result)
        {
            var nodes = (ICapabilityInfo)result;
        }
        public void RegisterListeners()
        {
            RegisterDataListener();
            RegisterMessageListener();
            RegisterCapabilityListener();
            RegisterCapabilityWithURIListener();
        }
        private void RegisterCapabilityWithURIListener()
        {
            WearableClass.GetCapabilityClient(context).AddListener(this, Android.Net.Uri.Parse("wear://"), CapabilityClient.FilterReachable);
        }
        private void RegisterCapabilityListener()
        {
            WearableClass.GetCapabilityClient(context).AddListener(this, capabilityName);
        }
        private void RegisterMessageListener()
        {
            WearableClass.GetMessageClient(context).AddListener(this);
        }
        private void RegisterDataListener()
        {
            WearableClass.GetDataClient(context).AddListener(this);
        }

       
    }
}
