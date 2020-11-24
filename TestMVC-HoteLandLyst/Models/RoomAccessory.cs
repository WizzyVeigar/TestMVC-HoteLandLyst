using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC_HoteLandLyst.Models
{
    public class RoomAccessory
    {
        public RoomAccessory(string accessoryName, decimal extraCharge)
        {
            AccessoryName = accessoryName;
            ExtraCharge = extraCharge;
        }

        public string AccessoryName { get; set; }
        public decimal ExtraCharge { get; set; }


    }
}
