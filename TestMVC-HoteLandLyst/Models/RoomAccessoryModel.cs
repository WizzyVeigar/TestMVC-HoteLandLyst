using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC_HoteLandLyst.Models
{
    //Model for a hotel room accessory
    public class RoomAccessoryModel
    {
        [JsonConstructor]
        public RoomAccessoryModel(string accessoryName, decimal extraCharge)
        {
            AccessoryName = accessoryName;
            ExtraCharge = extraCharge;
        }

        public string AccessoryName { get; set; }
        public decimal ExtraCharge { get; set; }


    }
}
