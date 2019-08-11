using System;
using Android.Gms.Wearable;
using Plugin.Vibrate;
using SpideySenseWatch.Services;

namespace SpideySenseWatch.Models
{
    public class Events
    {
        ConnectionService connSerive;

        public Events()
        {
            connSerive = new ConnectionService();
        }

        public void SetTriggerEvent(int index)
        {
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            switch (index)
            {
                case 0:
                    connSerive.SendMessage("option1");
                    var v = CrossVibrate.Current;
                    v.Vibration(TimeSpan.FromSeconds(1)); // 1 second vibration
                    break;
                case 1:
                    connSerive.SendMessage("option2");

                    break;
                case 2:
                    connSerive.SendMessage("option3");
                    break;
                case 3:
                    connSerive.SendMessage("settings");
                    break;
            }
        }
        public void SendStamp(DataMap dataMap)
        {
            connSerive.SendDataAsync(dataMap);
        }
    }
}