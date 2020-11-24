using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.ApiController;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.Factories
{
    public class RoomFactory : IFactory<Room>
    {
        private static RoomFactory instance;
        public static RoomFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomFactory();
                }
                return instance;
            }
        }

        private List<Room> rooms = new List<Room>();
        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        private void ClearRooms()
        {
            if (Rooms.Count > 0)
            {
                Rooms.Clear();
            }
        }

        //Should probably be in it's own constructor
        private void CreateRoomAccessories(List<Room> rooms)
        {
            DataTable dt = MsSqlManager.Instance.GetRoomAccessories();

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (Convert.ToInt32(row[0]) == rooms[i].RoomNumber)
                    {
                        rooms[i].RoomAccessories.Add(new RoomAccessory(row[1].ToString(), (decimal)row[2]));
                    }
                }
            }
        }

        public IList<Room> CreateAll()
        {
            ClearRooms();
            DataTable dataTable = MsSqlManager.Instance.GetAllRooms();
            foreach (DataRow row in dataTable.Rows)
            {
                Rooms.Add(new Room(Convert.ToInt32(row[0]), (decimal)row[1], ConvertToRoomStatus(row[2].ToString())));
            }

            CreateRoomAccessories(rooms);
            return Rooms;
        }
        private RoomStatus ConvertToRoomStatus(string statusString)
        {
            foreach (string name in Enum.GetNames(typeof(RoomStatus)))
            {
                if (name == statusString)
                {
                    return (RoomStatus)Enum.Parse(typeof(RoomStatus), statusString);
                }
            }
            //Log error
            return RoomStatus.Dirty;
        }
    }
}
