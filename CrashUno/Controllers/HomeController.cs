using CrashUno.Models;
using CrashUno.Models.ViewModels;
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

            return View();
        }

        public IActionResult Crash(int pageNum = 1)
        {
            int pageSize = 5;

            var x = new CrashViewModel
            {
                Crash = repo.Crash
                .OrderBy(c => c.crash_id)
                .Skip ((pageNum - 1) * pageSize)
                .Take(pageSize),


                PageInfo = new PageInfo
                {
                    TotalNumCrashes = repo.Crash.Count(),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
            
            var blah = repo.Crash
                .OrderBy(c => c.crash_id)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize);
            
            return View(x);
        }

        
    }
}
