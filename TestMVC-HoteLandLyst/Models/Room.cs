using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC_HoteLandLyst.Models
{
    //Model for a hotel room
    public class Room
    {
        public int RoomNumber { get; set; }
        public decimal DayPrice { get; set; }
        public List<RoomAccessoryModel> RoomAccessories { get; set; }

        public Room(int roomNumber)
        {
            RoomNumber = roomNumber;
            DayPrice = 695;
            RoomAccessories = new List<RoomAccessoryModel>();
        }

        public Room(int roomNumber, decimal price) : this(roomNumber)
        {
            DayPrice = price;
        }

        [JsonConstructor]
        public Room(int roomNumber,decimal price, List<RoomAccessoryModel> roomAccessoryModels) : this(roomNumber, price)
        {
            RoomAccessories = roomAccessoryModels;
        }

        public override string ToString()
        {
            decimal total = DayPrice;
            for (int i = 0; i < RoomAccessories.Count; i++)
            {
                total += RoomAccessories[i].ExtraCharge;
            }
            return total.ToString();
        }
    }

}
