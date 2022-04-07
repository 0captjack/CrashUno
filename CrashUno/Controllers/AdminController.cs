using CrashUno.Models;
using CrashUno.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrashUno.Controllers
{
    public class AdminController : Controller
    {
        private IRepository repo;
        // does this line need to go?? private Context context { get; set; }

        public AdminController(IRepository temp)
        {
            repo = temp;
        }

        [Authorize]
        public IActionResult Index(int searchString = 0, int pageNum = 1)
        {
            int pageSize = 13;

            var x = new CrashViewModel
            {
                Crash = repo.Crash
                .OrderBy(c => c.crash_id)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),


                PageInfo = new PageInfo
                {
                    TotalNumCrashes =
                    repo.Crash.Count(),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            if(searchString != 0)
            {
                x.Crash = repo.Crash.Where(x => x.crash_id == searchString);
            };

            ViewBag.SelectedType = RouteData?.Values["crashseverityid"];

            ViewBag.Types = repo.Crash
            .Select(x => x.crash_severity_id)
            .Distinct()
            .OrderBy(x => x);

            return View(x);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int crashid)
        {
            ViewBag.Title = "Edit Crash Information";
            var c = repo.Crash.FirstOrDefault(x => x.crash_id == crashid);
            return View("Form", c);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(Crash c)
        {
            repo.SaveCrashRecord(c);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Title = "Add New Crash Record";
            var c = new Crash();
            return View("Form", c);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Crash c)
        {
            if (ModelState.IsValid)
            {
                repo.CreateCrashRecord(c);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Add");
            }

        }

        [Authorize]
        public IActionResult Delete(int crashid)
        {
            var c = repo.Crash.FirstOrDefault(x => x.crash_id == crashid);
            repo.DeleteCrashRecord(c);

            return RedirectToAction("Index");
        }
    }
}
