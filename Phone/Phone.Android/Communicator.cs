using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Net;
using Android.Util;
using Android.Gms.Wearable;
using System.Threading.Tasks;
using Android.Gms.Common.Apis;
using System.Text;
using System.Collections.Generic;
using Android.Runtime;
using Phone.Views;
using Phone.ViewModels;
using Xamarin.Forms;
using Java.Lang;

namespace Phone.Droid
{
    public class Communicator : Java.Lang.Object, IMessageApiMessageListener, IDataApiDataListener, IChannelApiChannelListener,ICapabilityApiCapabilityListener,INodeApiNodeListener
    {
        readonly GoogleApiClient client;
        const string path = "/my_capability";

        // Initializing GoogleApiClient
        public Communicator(Context context)
        {
            client = new GoogleApiClient.Builder(context)                
                .AddApi(WearableClass.API).Build();
                WearableClass.NodeApi.AddListener(client, this);
         //   WearableClass.CapabilityApi.AddCapabilityListener(client,ICapabilityApiCapabilityListener, "");
        }

        // Connecting client when we want it (usually on Activity.OnResume)
        public void Resume()
        {
            if (!client.IsConnected)
            {
                client.Connect();
                WearableClass.MessageApi.AddListener(client, this);
                WearableClass.DataApi.AddListener(client, this);
                //WearableClass.NodeApi.AddListenerAsync(client,this);
            }
        }

        // Disconnecting client when we want it (usually on Activity.OnPause)
        public void Pause()
        {
            if (client != null && client.IsConnected)
            {
                //client.Disconnect();
                //WearableClass.MessageApi.RemoveListener(client, this);
                //WearableClass.DataApi.RemoveListener(client, this);
            }
        }
       
        // Sending message via MessageApi
        public void SendMessage(string message)
        {
            client.Connect();
            Task.Run(() => {
                foreach (var node in Nodes())
                {
                    // APP NOT GETS HERE, BECAUSE Nodes() RETURNS NOTHING
                    var bytes = System.Text.Encoding.Default.GetBytes(message);
                    var result = WearableClass.MessageApi.SendMessage(client, node.Id, path, bytes).Await();
                    var success = result.JavaCast<IMessageApiSendMessageResult>().Status.IsSuccess ? "Ok." : "Failed!";
                    Log.Info("my_log", "Communicator: Sending message " + message + "... " + success);
                    //client.Disconnect();
                }
            });
        }

        // Sending data via DataApi
        public void SendData(DataMap dataMap)
        {
            Task.Run(() => {
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

        // Implementing IMessageApiMessageListener interface
        // On message received we want invoke event
        public void OnMessageReceived(IMessageEvent messageEvent)
        {
            var message = System.Text.Encoding.Default.GetString(messageEvent.GetData());
            Log.Info("my_log", "Communicator: Message received \"" + message + "\"");
            EventViewModel eventModel = new EventViewModel();
            if (message.Contains("option") && message.ToString().ToCharArray().Length > ("option").Length)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await eventModel.setOption(message);
                });
            }
            if (message.Contains("option"))
            {
                Device.BeginInvokeOnMainThread(async () => {
                    await eventModel.TriggerFeatureAsync();
                });
            }
            MessageReceived(message);
        }

       

        // Implementing IDataApiDataListener interface
        // On data changed we want invoke event
        public void OnDataChanged(DataEventBuffer p0)
        {
            Log.Info("my_log", "Communicator: Data changed (" + p0.Count + " data events)");
            for (var i = 0; i < p0.Count; i++)
            {
                var dataEvent = p0.Get(i).JavaCast<IDataEvent>();
                if (dataEvent.Type == DataEvent.TypeChanged && dataEvent.DataItem.Uri.Path == path)
                    DataReceived(DataMapItem.FromDataItem(dataEvent.DataItem).DataMap);
            }
        }

        // Events for incoming message or update data
        public event Action<string> MessageReceived = delegate 
        {

        };
        public event Action<DataMap> DataReceived = delegate 
        {

        };

        IList<INode> Nodes()
        {

            var result = WearableClass.NodeApi.GetConnectedNodes(client).Await();
            return result.JavaCast<INodeApiGetConnectedNodesResult>().Nodes;
        }

        public void OnResult(Java.Lang.Object result)
        {
            throw new NotImplementedException();
        }

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

        public void OnChannelClosed(IChannel channel, int closeReason, int appSpecificErrorCode)
        {
            throw new NotImplementedException();
        }

        public void OnChannelOpened(IChannel channel)
        {
            throw new NotImplementedException();
        }

        public void OnInputClosed(IChannel channel, int closeReason, int appSpecificErrorCode)
        {
            throw new NotImplementedException();
        }

        public void OnOutputClosed(IChannel channel, int closeReason, int appSpecificErrorCode)
        {
            throw new NotImplementedException();
        }

        public void OnPeerDisconnected(INode peer)
        {
            EventViewModel eventModel = new EventViewModel();
            Device.BeginInvokeOnMainThread(async () => {
             //   await TriggerFeatureAsync("vib", eventModel);
                await eventModel.TriggerFeatureAsync();
            });
        }

        public void OnCapabilityChanged(ICapabilityInfo capabilityInfo)
        {
           
        }
        public void OnPeerConnected(INode peer)
        {
            EventViewModel eventModel = new EventViewModel();
            Device.BeginInvokeOnMainThread(async () => {
              //  await TriggerFeatureAsync("vib", eventModel);
                await eventModel.TriggerFeatureAsync();
            });
        }
}
}
