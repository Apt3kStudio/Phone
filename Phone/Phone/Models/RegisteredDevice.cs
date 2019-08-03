using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Phone.Models
{
   public class RegisteredDevice: INotifyPropertyChanged
    {
        public string Id { get; set; }
        
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
        public string Flash { get; set; }
        public string Sound { get; set; }
        public string Vibration { get; set; }
       
        string _Distance = "";

        public RegisteredDevice()
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                await GetCount();
            });             
        }
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
                    for (var i = 0; i < 10000; i++)
                    {
                        await Task.Delay(1000);
                        Distance = i.ToString();
                    }
                return true;
            });
        }
    }
}
