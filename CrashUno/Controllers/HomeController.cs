using CrashUno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CrashUno.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repo;
        // does this line need to go?? private Context context { get; set; }

        public HomeController (IRepository temp)
        {
            repo = temp;
        }
        
        public IActionResult Index()
        {
            var blah = repo.Crash.ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
