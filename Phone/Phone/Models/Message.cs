using System;
using System.Collections.Generic;
using System.Text;

namespace Phone.Models
{
    public class Message
    {
        public string id { get; set; }
        public string MessageName {set;get;}
        public string MessageText { get; set; }
        public bool isSuccess { set; get; }
        public Message(string title, string text, bool _isSuccess = false)
        {
            MessageName = title;
            MessageText = text;
            id = UtilityHelper.getNewID().ToString();
            isSuccess = _isSuccess;
        }

    }
}
