using System;
using System.Collections.Generic;
using System.Text;

namespace Phone.Models
{
    public interface IEventModel
    {
        string EventID { get; set; }
        string EventName { get; set; }
        string EventMessage { get; set; }
        string EventDuration { get; set; }
    }
}
