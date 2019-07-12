using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Runtime;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Wearable;

using Java.Util.Concurrent;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using Android.Gms.Gcm;
using Plugin.Vibrate;
using Xamarin.Essentials;

namespace SpideySenseWatch
{

    public class Communicator : Java.Lang.Object, IMessageApiMessageListener, IDataApiDataListener, ICapabilityApiCapabilityListener
    {
        readonly GoogleApiClient client;
        const string path = "/my_capability";
        string capabilityName = "my_capability";
        Context _context;
        public Communicator(Context context)
        {
            try
            {
                _context = context; 
                client = new GoogleApiClient.Builder(context)
                .AddApi(WearableClass.API)
                .Build();
                client.Connect();
            }
            catch (Exception)
            {
            
            }
            var result = WearableClass.CapabilityApi.GetCapability(client, capabilityName, CapabilityApi.FilterReachable);
        }        
        public void Resume()
        {
            if (!client.IsConnected)
            {
                client.Connect();
                WearableClass.MessageApi.AddListener(client, this);
                WearableClass.DataApi.AddListener(client, this);                
                WearableClass.CapabilityApi.AddCapabilityListener(client,this, capabilityName);
            }
        }      
       
        public void Pause()
        {
            if (client != null && client.IsConnected)
            {
                //client.Disconnect();
                //WearableClass.MessageApi.RemoveListener(client, this);
                //WearableClass.DataApi.RemoveListener(client, this);
            }
        }
        public void SendMessage(string message)
        {
            client.Connect();
            System.Threading.Tasks.Task.Run(() =>
            {
                foreach (var node in Nodes())
                {
                    // APP NOT GETS HERE, BECAUSE Nodes() RETURNS NOTHING
                    var bytes = Encoding.Default.GetBytes(message);
                    var result = WearableClass.MessageApi.SendMessage(client, node.Id, path, bytes).Await();
                    var success = result.JavaCast<IMessageApiSendMessageResult>().Status.IsSuccess ? "Ok." : "Failed!";
                    Log.Info("Spidey", "Communicator: Sending message " + message + "... " + success);
                   // client.Disconnect();
                }
            });
        }

        // Sending data via DataApi
        public void SendData(DataMap dataMap)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    var request = PutDataMapRequest.Create(path);
                    request.DataMap.PutAll(dataMap);
                    var result = WearableClass.DataApi.PutDataItem(client, request.AsPutDataRequest()).Await();
                    var success = result.JavaCast<IDataApiDataItemResult>().Status.IsSuccess ? "Ok." : "Failed!";
                    Log.Info("Spidey", "Communicator: Sending data map " + dataMap + "... " + success);
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }
        public void SendStamp(DataMap dataMap)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    var request = PutDataMapRequest.Create(path);
                    request.DataMap.PutAll(dataMap);
                    var result = WearableClass.DataApi.PutDataItem(client, request.AsPutDataRequest()).Await();
                    var success = result.JavaCast<IDataApiDataItemResult>().Status.IsSuccess ? "Ok." : "Failed!";
                    Log.Info("Spidey", "Communicator: Sending data map " + dataMap + "... " + success);
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }
        public void SetTriggerEvent(int index)
        {
            switch (index)
            {
                case 0:
                    //First Action
                    SendMessage("option1");
                    var v = CrossVibrate.Current;
                    v.Vibration(TimeSpan.FromSeconds(1)); // 1 second vibration
                    break;
                case 1:
                    //First Action
                    SendMessage("option2");
                    break;
                case 2:
                    //First Action
                    SendMessage("option3");
                    break;
                case 3:
                    //First Action
                    SendMessage("settings");
                    break;                   
            }
        }

        // Implementing IMessageApiMessageListener interface
        // On message received we want invoke event
        public void OnMessageReceived(IMessageEvent messageEvent)
        {
            var message = Encoding.Default.GetString(messageEvent.GetData());
            Log.Info("Spidey", "Communicator: Message received \"" + message + "\"");
            MessageReceived(message);
        }

        // Implementing IDataApiDataListener interface
        // On data changed we want invoke event
        public void OnDataChanged(DataEventBuffer p0)
        {
            Log.Info("Spidey", "Communicator: Data changed (" + p0.Count + " data events)");
            for (var i = 0; i < p0.Count; i++)
            {
                var dataEvent = p0.Get(i).JavaCast<IDataEvent>();
                if (dataEvent.Type == DataEvent.TypeChanged && dataEvent.DataItem.Uri.Path == path)
                    DataReceived(DataMapItem.FromDataItem(dataEvent.DataItem).DataMap);
            }
        }
        public void OnConnected(Bundle connectionHint)
        {

        }
        // Events for incoming message or update data
        public event Action<string> MessageReceived = delegate { };
        public event Action<DataMap> DataReceived = delegate { };
        IList<INode> Nodes()
        {

            var result = WearableClass.NodeApi.GetConnectedNodes(client).Await();
            return result.JavaCast<INodeApiGetConnectedNodesResult>().Nodes;
        }
        public void OnCapabilityChanged(ICapabilityInfo capabilityInfo)
        {
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

        //public void OnPeerConnected(INode peer)
        //{
        //    var sds = WearableClass.CapabilityApi.GetCapability(client, capabilityName, CapabilityApi.FilterReachable);

        //   // WearableClass.NodeApi.AddListener(client, capabilityName,);



        //    //  WearableClass.CapabilityApi.AddLocalCapability(client, capabilityName);
        //    Log.Info("Spidey", "Connected" + peer.DisplayName + "id:" + peer.Id);
        //}

        //public void OnPeerDisconnected(INode peer)
        //{
        //    Log.Info("Spidey", "Disconnected" + peer.DisplayName + "id:" + peer.Id);

        //}


    } }

