using System;
using System.Collections.Generic;
using System.Text;

namespace Phone.Models
{
    public class Distance
    {
        public int ID { get; set; }
        public int DistanceValue { get; set; }
        public Distance()
        {
            #region Default distance
                DistanceValue = 1252;
            #endregion
        }
        internal void updateDistance(int newDistanceValue)
        {
            DistanceValue = newDistanceValue;
        }
      

    }
}
