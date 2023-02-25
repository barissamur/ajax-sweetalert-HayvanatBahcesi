using Microsoft.AspNetCore.Mvc;
using Odev27.Models;
using System.Diagnostics;

namespace Odev27.Controllers
{
    public class HomeController : Controller
    {
        private readonly UygulamaDbContext _db;
        private readonly IWebHostEnvironment _env;

        public HomeController(UygulamaDbContext db, IWebHostEnvironment env)
        {
            _db=db;
            _env=env;
        }

        public IActionResult Index()
        {
            var vm = new HayvanViewModel()
            {
                Hayvanlar = _db.Hayvanlar.ToList()
            };
            return View(vm);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(HayvanViewModel vm)
        {
            if (vm.Resim == null)
                return View(vm);


            else if (!vm.Resim.ContentType.StartsWith("image/"))
                ModelState.AddModelError("resim", "Geçersiz bir resim dosyası seçtiniz");

            if (ModelState.IsValid)
            {
                string dosyaAd = vm.Resim.FileName;
                string kayitYolu = Path.Combine(_env.WebRootPath, "img", dosyaAd);
                using (var stream = new FileStream(kayitYolu, FileMode.OpenOrCreate))
                {
                    vm.Resim.CopyTo(stream);
                }
                _db.Hayvanlar.Add(new Hayvan()
                {
                    Ad = vm.Ad,
                    Resim = dosyaAd
                });
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            vm.Hayvanlar = _db.Hayvanlar.ToList();
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Sil(int id)
        {
            var hayvan = _db.Hayvanlar.Find(id);

            string dosyaYolu = Path.Combine(_env.WebRootPath, "img", hayvan!.Resim);
            System.IO.File.Delete(dosyaYolu);

            _db.Hayvanlar.Remove(hayvan);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }


        [Route("Home/Duzenle/{id}/{girilenAd?}")]
        public IActionResult Duzenle(int id, string girilenAd)
         {
            if (girilenAd == null)
                ModelState.AddModelError("isim", "İsim boş bırakılamaz");

            if (ModelState.IsValid)
            {
                var hayvan = _db.Hayvanlar.Find(id);
                hayvan!.Ad = girilenAd;
            }
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public string Ad(int id)
        {
            var hayvan = _db.Hayvanlar.Find(id);
            return hayvan!.Ad;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}