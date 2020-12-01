using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Models;
using TestMVC_HoteLandLyst.Factories;
using System.Diagnostics;
using TestMVC_HoteLandLyst.Interfaces;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class RoomsController : Controller
    {
        private ICreateMultiple<Room> createMultiple;

        public RoomsController(ICreateMultiple<Room> createMultiple)
        {
            this.createMultiple = createMultiple;
        }

        public IActionResult Index()
        {
            try
            {
                List<Room> rooms = (List<Room>)(createMultiple).CreateAll();

                return View(rooms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
