using System;
using Newtonsoft.Json.Linq;
using PushNotification.Plugin;
using PushNotification.Plugin.Abstractions;

namespace Phone.Droid
{
    internal class CrossPushNotificationListener : IPushNotificationListener
    {
        public CrossPushNotificationListener()
        {
        }

        public void OnError(string message, DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public void OnMessage(JObject values, DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public void OnRegistered(string token, DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public void OnUnregistered(DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public bool ShouldShowNotification()
        {
            throw new NotImplementedException();
        }
    }
}