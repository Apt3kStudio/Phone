using System;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Runtime;
using Android.Gms.Common.Apis;
using Android.Gms.Wearable;
using System.Text;
using System.Collections.Generic;
using Xamarin.Essentials;
using SpideySenseWatch.Models;

namespace SpideySenseWatch.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.android.gms.wearable.BIND_LISTENER" })]
    public class ConnectionService : Java.Lang.Object, MessageClient.IOnMessageReceivedListener, DataClient.IOnDataChangedListener, CapabilityClient.IOnCapabilityChangedListener// IMessageApiMessageListener, IDataApiDataListener, ICapabilityApiCapabilityListener
    {
        readonly GoogleApiClient client;
        const string path = "/my_capability";
        string capabilityName = "my_capability";
        Context _context;
        public ConnectionService()
        {            
            _context = UtilityHelper.GetContext();
            client = new GoogleApiClient.Builder(_context)
            .AddApi(WearableClass.API)
            .Build();                                           
        }        
        public void Resume()
        {
            if (!client.IsConnected)
            {
                client.Connect();
            }
                WearableClass.GetDataClient(_context).AddListener(this);
                WearableClass.GetMessageClient(_context).AddListener(this);
                WearableClass.GetCapabilityClient(_context).AddListener(this, capabilityName);
                WearableClass.GetCapabilityClient(_context).AddListener(this, Android.Net.Uri.Parse("wear://"), CapabilityClient.FilterReachable);
            
        }             
        public void Pause()
        {
            if (client.IsConnected)
            {
                client.Disconnect();
            }
            WearableClass.GetDataClient(_context).RemoveListener(this);
            WearableClass.GetMessageClient(_context).RemoveListener(this);
            WearableClass.GetCapabilityClient(_context).RemoveListener(this, capabilityName);
            WearableClass.GetCapabilityClient(_context).RemoveListener(this);

        }
        public void SendMessage(string message)
        {
           
            if (!client.IsConnected)
            {
                client.Connect();
            }
                System.Threading.Tasks.Task.Run(() =>
                {
                    foreach (var node in Nodes())
                    {
                        // APP NOT GETS HERE, BECAUSE Nodes() RETURNS NOTHING
                        var bytes = Encoding.Default.GetBytes(message);
                        var result = WearableClass.GetMessageClient(_context).SendMessage(node.Id, path, bytes);
                        var success = result.JavaCast<IMessageApiSendMessageResult>().Status.IsSuccess ? "Ok." : "Failed!";
                        Log.Info("Spidey", "Communicator: Sending message " + message + "... " + success);
                       
                    }
                });
            
        }
        IList<INode> Nodes()
        {
            var result = WearableClass.NodeApi.GetConnectedNodes(client).Await();
            return result.JavaCast<INodeApiGetConnectedNodesResult>().Nodes;
        }
        public  void SendDataAsync(DataMap dataMap)
        {            
                try
                {
                    var request = PutDataMapRequest.Create(path);
                    request.DataMap.PutAll(dataMap);
                    var result = WearableClass.GetDataClient(_context).PutDataItem(request.AsPutDataRequest());
                    var success = result.JavaCast<IDataApiDataItemResult>().Status.IsSuccess ? "Ok." : "Failed!";
                    Log.Info("Spidey", "Communicator: Sending data map " + dataMap + "... " + success);
                }
                catch (Exception ex)
                {
                    throw;
                }            
        }
        public void OnMessageReceived(IMessageEvent messageEvent)
        {
            var message = Encoding.Default.GetString(messageEvent.GetData());
            Log.Info("Spidey", "Communicator: Message received \"" + message + "\"");            
        }
        public void OnDataChanged(DataEventBuffer p0)
        {
            Log.Info("Spidey", "Communicator: Data changed (" + p0.Count + " data events)");
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
            var nodes = capabilityInfo.Nodes;
            string isVibrate = "";
            try
            {
                Vibration.Vibrate();
                var duration = TimeSpan.FromSeconds(20);
                Vibration.Vibrate(duration);
                isVibrate = "Success";
            }
            catch (FeatureNotSupportedException ex)
            {
                isVibrate = "Not Supported";
            }
            catch (System.Exception ex)
            {
                isVibrate = "Error";
            }
        }      
    }
}

