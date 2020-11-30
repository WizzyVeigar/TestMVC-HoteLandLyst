using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Managers;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.Factories
{
    public class RoomFactory : ICreateMultiple<Room>
    {
        private List<Room> rooms;
        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        private static RoomFactory instance;
        public static RoomFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomFactory();
                    instance.Rooms = new List<Room>();
                }
                return instance;
            }
        }


        /// <summary>
        /// Clears the <see cref="Instance.Rooms"/>
        /// </summary>
        private void ClearRooms()
        {
            if (Instance.Rooms.Count > 0)
            {
                Instance.Rooms.Clear();
            }
        }

        /// <summary>
        /// Create RoomAccessories and attach them to the correct room from <paramref name="rooms"/>
        /// </summary>
        /// <param name="rooms">The list of rooms without any accessories yet</param>
        private void CreateRoomAccessories(List<Room> rooms)
        {
            DataTable dt = MsSqlConnection.Instance.ExecuteSP("GetRoomAccessories");

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (Convert.ToInt32(row[0]) == rooms[i].RoomNumber)
                    {
                        rooms[i].RoomAccessories.Add(new RoomAccessoryModel(row[1].ToString(), (decimal)row[2]));
                    }
                }
            }
        }

        public IList<Room> CreateAll()
        {
            ClearRooms();
            DataTable dataTable = MsSqlConnection.Instance.ExecuteSP("GetAllRooms");
            foreach (DataRow row in dataTable.Rows)
            {
                Instance.Rooms.Add(new Room(Convert.ToInt32(row[0]), (decimal)row[1]));
            }

            CreateRoomAccessories(Instance.Rooms);
            return Instance.Rooms;
        }
    }
}
