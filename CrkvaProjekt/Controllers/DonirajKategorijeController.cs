using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrkvaProjekt.Data;
using CrkvaProjekt.Models;
using CrkvaProjekt.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrkvaProjekt.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class DonirajKategorijeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DonirajKategorijeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Prikazi()
        {
            List<DonirajKategorije> kategorije = _context.DonirajKategorije.ToList();
            ViewData["kategorije-kljuc"] = kategorije;
            return View();
        }
        
        public IActionResult Obrisi(int DonirajKategorijeID)
        {
            DonirajKategorije k = _context.DonirajKategorije.Find(DonirajKategorijeID);
            if(k == null)
            {
                return Content("Kategorija ne postoji!");
            }
            _context.Remove(k);
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                TempData["error_poruka"] = "Kategoriju je nemoguće obrisati. ";
            }
            TempData["success_poruka"] = "Uspješno ste obrisali kategoriju. ";
            return RedirectToAction("Prikazi");
        }
        public IActionResult Uredi(int DonirajKategorijeID)
        {
            DonirajKategorije k = _context.DonirajKategorije.Find(DonirajKategorijeID);
            if (k == null)
            {
                TempData["error_poruka"] = "Kategorija ne postoji! ";
                return RedirectToAction("Prikazi");
            }
            DonirajKategorijeUrediVM model = new DonirajKategorijeUrediVM();
            model.Naziv = k.Naziv;

            return View("Uredi", model);
        }
        public IActionResult Dodaj()
        {
            DonirajKategorijeUrediVM model = new DonirajKategorijeUrediVM();
            return View("Uredi", model);
        }
        public IActionResult Snimi(DonirajKategorijeUrediVM input)
        {
            
                DonirajKategorije k;
                if (input.DonirajKategorijeID == 0)
                {
                    k = new DonirajKategorije();
                    _context.Add(k);
                    TempData["success_poruka"] = "Uspješno ste dodali kategoriju.";
                }
                else
                {
                    k = _context.DonirajKategorije.Find(input.DonirajKategorijeID);
                }
                k.DonirajKategorijeID = input.DonirajKategorijeID;
                k.Naziv = input.Naziv;

                _context.SaveChanges();

                if (input.DonirajKategorijeID == 0)
                    TempData["success_poruka"] = "Uspješno ste dodali kategoriju.";
                else
                    TempData["success_poruka"] = "Uspješno ste izmijenili podatke o kategoriji.";

            

            return RedirectToAction("Prikazi");
        }
    }
}
