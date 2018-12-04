using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kodi_Youtube_Generator.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Kodi_Youtube_Generator.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}