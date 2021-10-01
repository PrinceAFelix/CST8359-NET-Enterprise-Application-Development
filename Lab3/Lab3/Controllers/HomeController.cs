using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Lab3.Models;


namespace Lab3.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Razor() => View();

        [HttpPost]
        public IActionResult Count()
        {
          
            HttpContext.Session.SetString("numberOfBottles", Request.Form["numberOfBottles"]);
            return View();
        }

        public IActionResult CreatePerson() => View();

        [HttpPost]
        public IActionResult DisplayPerson(Person person)
        {

            return View(person);
        }
        public IActionResult Error()
        {
            return View();
        }

    }
}
