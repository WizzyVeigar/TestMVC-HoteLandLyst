using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Models;
using TestMVC_HoteLandLyst.Factories;
using System.Diagnostics;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                List<Room> rooms = (List<Room>)RoomFactory.Instance.CreateAll();

                return View(rooms);
            }
            catch (Exception)
            {
                return View(new ErrorViewModel() { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}
