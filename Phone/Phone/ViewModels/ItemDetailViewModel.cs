using System;

using Phone.Models;

namespace Phone.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public AlertModel Item { get; set; }
        public ItemDetailViewModel(AlertModel item = null)
        {
            Title = item?.Name;
            Item = item;
        }
    }
}
