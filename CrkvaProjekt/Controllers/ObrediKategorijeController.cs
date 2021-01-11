using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrkvaProjekt.Data;
using CrkvaProjekt.Models;
using CrkvaProjekt.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CrkvaProjekt.Controllers
{
    public class ObrediKategorijeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ObrediKategorijeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Prikazi()
        {
            List<ObrediKategorije> kategorije = _context.ObrediKategorije.ToList();
            ViewData["kategorije-kljuc"] = kategorije;
            return View();
        }

        public IActionResult Obrisi(int ObrediKategorijeID)
        {
            ObrediKategorije k = _context.ObrediKategorije.Find(ObrediKategorijeID);
            if (k == null)
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
        public IActionResult Uredi(int ObrediKategorijeID)
        {
            ObrediKategorije k = _context.ObrediKategorije.Find(ObrediKategorijeID);
            if (k == null)
            {
                TempData["error_poruka"] = "Kategorija ne postoji! ";
                return RedirectToAction("Prikazi");
            }
            ObrediKategorijeUrediVM model = new ObrediKategorijeUrediVM();
            model.Naziv = k.Naziv;

            return View("Uredi", model);
        }
        public IActionResult Dodaj()
        {
            ObrediKategorijeUrediVM model = new ObrediKategorijeUrediVM();
            return View("Uredi", model);
        }
        public IActionResult Snimi(ObrediKategorijeUrediVM input)
        {
            ObrediKategorije k;
            if (input.ObrediKategorijeID == 0)
            {
                k = new ObrediKategorije();
                _context.Add(k);
                TempData["success_poruka"] = "Uspješno ste dodali kategoriju.";
            }
            else
            {
                k = _context.ObrediKategorije.Find(input.ObrediKategorijeID);
            }
            k.ObrediKategorijeID = input.ObrediKategorijeID;
            k.Naziv = input.Naziv;

            _context.SaveChanges();

            if (input.ObrediKategorijeID == 0)
                TempData["success_poruka"] = "Uspješno ste dodali kategoriju.";
            else
                TempData["success_poruka"] = "Uspješno ste izmijenili podatke o kategoriji.";

            return RedirectToAction("Prikazi");
        }
    }
}
