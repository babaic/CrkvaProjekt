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
    public class ObavjestenjaKategorijeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ObavjestenjaKategorijeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Prikazi()
        {
            List<ObavjestenjaKategorije> kategorije = _context.ObavjestenjaKategorije.ToList();
            ViewData["okategorije-kljuc"] = kategorije;
            return View();
        }
        public IActionResult Obrisi(int ObavjestenjaKategorijeID)
        {
            ObavjestenjaKategorije k = _context.ObavjestenjaKategorije.Find(ObavjestenjaKategorijeID);
            if (k == null)
            {
                TempData["error_poruka"] = "Kategorija ne postoji. ";
            }
            else
            {
                _context.Remove(k);
                try
                {
                    _context.SaveChanges();
                }
                catch
                {
                    TempData["error_poruka"] = "Nemoguće izbrisati kategoriju trenutno. Vezana je za nešto u bazi. ";
                }
                TempData["success_poruka"] = "Uspješno ste izbrisali kategoriju. ";
            }
            return RedirectToAction("Prikazi");
        }
        public IActionResult Dodaj()
        {
            ObavjestenjaKategorijeUrediVM model = new ObavjestenjaKategorijeUrediVM();
            return View("Uredi", model);
        }
        public IActionResult Uredi(int ObavjestenjaKategorijeID)
        {
            ObavjestenjaKategorije k = _context.ObavjestenjaKategorije.Find(ObavjestenjaKategorijeID);
            if (k == null)
            {
                ViewData["error_poruka"] = "Kategorija ne postoji. ";
                return RedirectToAction("Prikazi");
            }
            ObavjestenjaKategorijeUrediVM model = new ObavjestenjaKategorijeUrediVM();
            model.Naziv = k.Naziv;
            return View("Uredi", model);
        }
        public IActionResult Snimi(ObavjestenjaKategorijeUrediVM input)
        {
            ObavjestenjaKategorije k;
            if (input.ObavjestenjaKategorijeID == 0)
            {
                k = new ObavjestenjaKategorije();
                _context.Add(k);
                TempData["success_poruka"] = "Uspješno ste dodali kategoriju";
            }
            else
            {
                k = _context.ObavjestenjaKategorije.Find(input.ObavjestenjaKategorijeID);
            }
            k.ObavjestenjaKategorijeID = input.ObavjestenjaKategorijeID;
            k.Naziv = input.Naziv;

            _context.SaveChanges();

            if (input.ObavjestenjaKategorijeID == 0)
                TempData["success_poruka"] = "Uspješno ste dodali kategoriju. ";
            else
                TempData["success_poruka"] = "Uspješno ste izmijenili podatke o kategoriji. ";

            return RedirectToAction("Prikazi");
        }
    }
}
