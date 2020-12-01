using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.DalClasses;
using TestMVC_HoteLandLyst.Models;
using TestMVC_HoteLandLyst.Interfaces;

namespace TestMVC_HoteLandLyst.Factories
{
    public class RoomFactory : ICreateMultiple<Room>, IGetSingle<Room>
    {
        private IDataAccess dataAccess;
        public RoomFactory(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
            Rooms = new List<Room>();
        }


        private List<Room> rooms;
        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }        

        /// <summary>
        /// Clears the <see cref="Instance.Rooms"/>
        /// </summary>
        private void ClearRooms()
        {
            if (Rooms.Count > 0)
            {
                Rooms.Clear();
            }
        }

        /// <summary>
        /// Create RoomAccessories and attach them to the correct room from <paramref name="rooms"/>
        /// </summary>
        /// <param name="rooms">The list of rooms without any accessories yet</param>
        private void CreateRoomAccessories(List<Room> rooms)
        {
            DataTable dt = dataAccess.ExecuteSP("GetRoomAccessories");

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
        private void CreateRoomAccessories(Room room)
        {
            DataTable dt = dataAccess.ExecuteSPParam("GetRoomAccessoriesById", room.RoomNumber);
            foreach (DataRow accessory in dt.Rows)
            {
                room.RoomAccessories.Add(new RoomAccessoryModel(accessory[0].ToString(), (decimal)accessory[1]));
            }
        }

        public IList<Room> CreateAll()
        {
            ClearRooms();
            DataTable dataTable = dataAccess.ExecuteSP("GetAllRooms");
            foreach (DataRow row in dataTable.Rows)
            {
                Rooms.Add(new Room(Convert.ToInt32(row[0]), (decimal)row[1]));
            }

            CreateRoomAccessories(Rooms);
            return Rooms;
        }

        public Room GetSingle(int id)
        {
            DataTable dt = dataAccess.ExecuteSPParam("GetRoomById", id);
            DataRow row = dt.Rows[0];
            Room room =  new Room((int)row[0],(decimal)row[1]);
            CreateRoomAccessories(room);
            return room;
        }
    }
}
