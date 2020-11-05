using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LibraryDay3.Controllers
{
    public class BorrowController :Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
