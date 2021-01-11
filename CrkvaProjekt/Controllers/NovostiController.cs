using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using CrkvaProjekt.Data;
using CrkvaProjekt.Models;
using CrkvaProjekt.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Helpers;

namespace CrkvaProjekt.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class NovostiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NovostiController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Prikazi()
        {
            List<Novosti> novosti = _context.Novosti.ToList();
            ViewData["novosti-kljuc"] = novosti;
            return View();
        }
        public IActionResult Dodaj()
        {
            NovostiUrediVM model = new NovostiUrediVM();
            return View("Uredi", model);
        }
        public IActionResult Uredi(int NovostiID)
        {
            Novosti n = _context.Novosti.Find(NovostiID);
            if (n == null)
            {
                TempData["error_poruka"] = "Novost ne postoji. ";
            }
            NovostiUrediVM model = new NovostiUrediVM();
            model.Naslov = n.Naslov;
            model.Text = n.Text;
            model.DatumObjavljivanja = n.DatumObjavljivanja;
           // model.Slika = n.Slika;

            return View("Uredi", model);
        }
        public IActionResult Obrisi(int NovostiID)
        {
            Novosti n = _context.Novosti.Find(NovostiID);
            List<NovostiSlike> slike = _context.NovostiSlike.Where(x => x.NovostiID == NovostiID).ToList();
            if (n == null)
            {
                TempData["error_poruka"] = "Novost ne postoji.";
            }
            else
            {
                if (slike != null)
                {
                    foreach(NovostiSlike x in slike)
                    {
                        _context.Remove(x);
                    }
                }
                _context.Remove(n);
                try
                {
                    _context.SaveChanges();
                }
                catch
                {
                    TempData["error_poruka"] = "Nemoguće izbrisati novost. Vezana je za nešto u bazi. ";
                }
                TempData["success_poruka"] = "Uspješno ste izbrisali novost";
            }
            return RedirectToAction("Prikazi");
        }
        public IActionResult Snimi(NovostiUrediVM input)
        {
            Novosti n;
            if(input.NovostiID == 0)
            {
                n = new Novosti();
                _context.Add(n);
                TempData["success_poruka"] = "Uspješno ste dodali novost. ";
            }
            else
            {
                n = _context.Novosti.Find(input.NovostiID);
            }
            n.NovostiID = input.NovostiID;
            n.Naslov = input.Naslov;
            n.Text = input.Text;
            n.DatumObjavljivanja = input.DatumObjavljivanja;
            if (input.UploadSlike != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    input.UploadSlike.Slika.CopyTo(memoryStream);
                    if (memoryStream.Length < 2097152)
                    {
                        n.Slika = memoryStream.ToArray();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "File je prevelik.");
                    }
                }
            }
            _context.SaveChanges();
            if (input.NovostiID == 0)
                TempData["success_poruka"] = "Uspješno ste dodali novost. ";
            else
                TempData["success_poruka"] = "Uspješno ste izmijenili podatke o novosti. ";
            return RedirectToAction("Prikazi");
            
        }
        
    }
}
