﻿using Phone.Droid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Phone.Models
{
    public class RegisteredDevice : INotifyPropertyChanged
    {
        public string nodeId { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Event { get; set; }
        public string device { get; set; }
        public string manufacturer { get; set; }
        public string deviceName { get; set; }
        public string version { get; set; }
        public string platform { get; set; }
        public string idiom { get; set; }
        public string deviceType { get; set; }
        public string ImageSource { set; get; }
        public string Flash { get; set; }
        public string Sound { get; set; }
        public bool isDeleted { get; set; }
        //public string public string Vibration { get; set; } { get; set; }



        public RegisteredDevice()
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(async () =>
                {
                    await Task.Delay(5000);
                    Distance = DateTime.Now.Second.ToString();
                    //for (int i = 0; i<2000; i++)
                    //{

                    //}

                });


            });
        }
        bool _Vibration = false;
        public bool Buzz
        {
            get => _Vibration;
            set
            {
                if (_Vibration == value)
                    return;
                _Vibration = value;
                BuzzWatch(value);
                NotifyPropertyChange(nameof(Buzz));
            }
        }

        private void BuzzWatch(bool enabled)
        {
            if (enabled)
            {           
                ConnectionService c = new ConnectionService();
                c.SendMessage("Buzz");
            }
        }

        string _Distance = "";
        public string Distance
        {
            get => _Distance;
            set
            {
                if (_Distance == value)
                    return;
                _Distance = value;
                NotifyPropertyChange(nameof(Distance));
            }
        }

        public string Measurement { get; internal set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChange(string propertyName)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<bool> GetCount()
        {
            return await Task.Run(async () =>
            {
                await Task.Delay(3);


                    //for (var i = 0; i < 10000; i++)
                    //{
                    //    await Task.Delay(1000);
                    //    Distance = i.ToString();
                    //}
                    return true;
            });
        }

        public bool HandShake(bool isHandShake)
        {
            bool retVAL = true;
            if (!isHandShake)
            {
                retVAL = false;
            }
            return retVAL;
        }

        public async Task StartMainLogicAsync(bool enableTestLogic = false)
        {
            if (enableTestLogic)
            {
                Random random = new Random();
                int mseconds = random.Next(3, 11) * 1000;
                System.Threading.Thread.Sleep(mseconds);
            }
            else
            {
                ConnectionService bluetoothConnection = new ConnectionService();
                await bluetoothConnection.StartTripAsync("LetGetATimeStamp", nodeId, false);
            }
        }
    }
}
