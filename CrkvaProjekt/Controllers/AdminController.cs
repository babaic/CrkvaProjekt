using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrkvaProjekt.Data;
using CrkvaProjekt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CrkvaProjekt.Controllers
{
   [Authorize(Roles ="Administrator")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        public AdminController(UserManager<User> userManager,ApplicationDbContext context)
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
