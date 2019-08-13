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
            OnCapabilityChangedReceived(nodes);
        }
        public event Action<List<INode>> OnCapabilityChangedReceived = delegate { };

        public ICollection<string> NodeIds
        {
            get
            {
                var results = new HashSet<string>();
                var nodes =
                    Android.Gms.Wearable.WearableClass.NodeApi.GetConnectedNodes(client)
                        .Await()
                        .JavaCast<Android.Gms.Wearable.INodeApiGetConnectedNodesResult>();

                foreach (var node in nodes.Nodes)
                {
                    results.Add(node.Id);
                }
                return results;
            }
        }

        public void DeviceDiscovery()
        {

            WearableClass.GetCapabilityClient(context)
                .GetAllCapabilities(CapabilityClient.FilterReachable)
                .AddOnSuccessListener(this);
            //Task.Run(() => {
            //    Android.Gms.Tasks.IOnSuccessListener result = null;
            //    //var nodes =
            //    //    Android.Gms.Wearable.WearableClass.NodeApi.GetConnectedNodes(client)
            //    //        .Await()
            //    //        .JavaCast<Android.Gms.Wearable.INodeApiGetConnectedNodesResult>();

            //    var nodess =  WearableClass.GetNodeClient(context).GetConnectedNodes();
            //    var sss =   WearableClass.GetCapabilityClient(context).GetAllCapabilities(CapabilityClient.FilterReachable);
            //    // .AddOnSuccessListener(result);
            //});

        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var nodes = Android.Runtime.Extensions.JavaCast<ICapabilityInfo>(result);
        }

        private void showNodes(string capabilityNames)
        {
            
            WearableClass.GetCapabilityClient(context).GetAllCapabilities(CapabilityClient.FilterReachable)
                .AddOnSuccessListener(this);

 //           Wearable.getCapabilityClient(this)
 //                   .getAllCapabilities(CapabilityClient.FILTER_REACHABLE).apply {
 //               addOnSuccessListener {
 //                   capabilityInfoMap->
 //val nodes: Set < Node > = capabilityInfoMap
 //        .filter { capabilityNames.contains(it.key) }
 //                           .flatMap { it.value.nodes }
 //                           .toSet()
 //                   showDiscoveredNodes(nodes)
 //               }
 //           }
        }

       

        //       private fun showDiscoveredNodes(nodes: Set<Node>)
        //       {
        //           val nodesList: Set < String > = nodes.map { it.displayName }.toSet()
        //           val msg: String = if (nodesList.isEmpty())
        //           {
        //               Log.d(TAG, "Connected Nodes: No connected device was found for the given capabilities")
        //       getString(R.string.no_device)
        //   }
        //           else
        //           {
        //               Log.d(TAG, "Connected Nodes: ${nodesList.joinToString(separator = ", ")}")
        //             getString(R.string.connected_nodes, nodesList)
        //         }
        //           Toast.makeText(this@MainActivity, msg, Toast.LENGTH_LONG).show()
        //       }

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
