using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Phone.ViewModels
{
    public class BaseVM: INotifyPropertyChanged
    {
        bool _isBussy = false;
        public bool isBussy
        {
            get => _isBussy;
            set
            {
                if (_isBussy == value)
                    return;
                _isBussy = value;
                NotifyPropertyChange(nameof(isBussy));
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

    }
}
