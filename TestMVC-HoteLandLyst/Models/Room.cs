using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMVC_HoteLandLyst.Models
{
    public enum RoomStatus
    {
        Available,
        Dirty,
        InUse
    }

    public class Room
    {
        public int RoomNumber { get; set; }
        public decimal DayPrice { get; set; }
        public RoomStatus RoomStatus { get; set; }
        public List<RoomAccessory> RoomAccessories { get; set; }

        public Room(int roomNumber, RoomStatus status)
        {
            RoomNumber = roomNumber;
            RoomStatus = status;
            DayPrice = 695;
            RoomAccessories = new List<RoomAccessory>();
        }

        public Room(int roomNumber, decimal price, RoomStatus status) : this(roomNumber, status)
        {
            DayPrice = price;
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
