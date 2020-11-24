using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Factories;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class RoomDetailsController : Controller
    {
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room room = RoomFactory.Instance.Rooms.Where(room => room.RoomNumber == id).FirstOrDefault();
            return View(room);
        }
    }
}
