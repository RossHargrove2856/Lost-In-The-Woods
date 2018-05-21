using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LostInTheWoods.Models;
using LostInTheWoods.Factories;

namespace LostInTheWoods.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrailFactory trailFactory;
        public HomeController(TrailFactory trailConnect) {
            trailFactory = trailConnect;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Home()
        {
            return View("Index");
        }

        [HttpGet]
        [Route("/Trails")]
        public IActionResult Index()
        {
            ViewBag.Trails = trailFactory.Index();
            return View("AllTrails");
        }

        [HttpGet]
        [Route("/NewTrail")]
        public IActionResult NewTrail(){
            return View("AddTrail");
        }

        [HttpPost]
        [Route("/AddTrail")]
        public IActionResult AddTrail(Trail trail) {
            if(ModelState.IsValid) {
                trailFactory.Add(trail);
                return RedirectToAction("Index");
            }
            return View("AddTrail");
        }

        [HttpGet]
        [Route("/Trails/{id}")]
        public IActionResult FindTrail(int id) {
            ViewBag.Trail = trailFactory.FindByID(id);
            return View("TrailInfo");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
