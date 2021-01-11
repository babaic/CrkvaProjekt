using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrkvaProjekt.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CrkvaProjekt.Controllers
{
   //[Authorize(Roles ="Administrator")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly ApplicationDbContext _context;
        public AdminController(UserManager<IdentityUser<int>> userManager,ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View("Prikazi");
        }
        public IActionResult Prikazi()
        {
            return View();
        }
    }
}
