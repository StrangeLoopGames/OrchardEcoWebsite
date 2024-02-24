// Copyright (c) Strange Loop Games. All rights reserved.

namespace PlayEco.Theme.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
