﻿using BasketballAppSoftuni.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BasketballAppSoftuni.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}