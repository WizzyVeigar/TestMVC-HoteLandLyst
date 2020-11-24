using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Models;
using TestMVC_HoteLandLyst.Factories;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult Index()
        {
            List<Room> rooms = RoomFactory.Instance.CreateRooms();

            return View(rooms);
        }
    }
}
