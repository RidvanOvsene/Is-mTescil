using IsimTescil.Contex;
using IsimTescil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsimTescil.Controllers
{
    public class HomeController : Controller
    {
        public DayOfWeek DayOfWeek { get; }
        IsimTescilContex db = new IsimTescilContex();
        public ActionResult Index()
        {
            DateTime dt = new DateTime(2003, 5, 1);
            
            var Urunler = db.Urunler.ToList();
            var Sepet = db.Sepet.ToList();
            ViewBag.sepet = Sepet;
            return View(Urunler);
        }
        public ActionResult UrunList()
        {
            ViewBag.IsWekeend = false;
            DayOfWeek today = DateTime.Today.DayOfWeek;
            DateTime date = DateTime.Now;
            if (today==DayOfWeek.Saturday || today == DayOfWeek.Sunday)
            {
                ViewBag.IsWekeend = true;
            }
                var Urunler = db.Urunler.ToList();
            var Sepet = db.Sepet.ToList();
            ViewBag.sepet = Sepet;
            return View(Urunler);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Urunler urunler)
        {
            db.Urunler.Add(urunler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            var Urun = db.Urunler.FirstOrDefault(Urunler => Urunler.Id == Id);
            return View(Urun);
        }
        [HttpPost]
        public ActionResult Edit(Urunler urunler)
        {
            var Urun = db.Urunler.FirstOrDefault(Urunler => Urunler.Id == urunler.Id);
            Urun.UrunAdi = urunler.UrunAdi;
            Urun.Fiyat = urunler.Fiyat;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            var Urun = db.Urunler.FirstOrDefault(Urunler => Urunler.Id == Id);
            db.Urunler.Remove(Urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SepeteEkle(int? Id)
        {
            var _Urun = db.Urunler.FirstOrDefault(x => x.Id == Id);

            if (_Urun != null)
            {
                var Sepet = new Sepet();
                Sepet.Id = _Urun.Id;
                Sepet.UrunAdi = _Urun.UrunAdi;
                Sepet.Date = DateTime.Now;
                Sepet.Adet = 1;
                Sepet.Fiyat = _Urun.Fiyat;
                db.Sepet.Add(Sepet);
                db.SaveChanges();
            }
            return RedirectToAction("UrunList");
        }
        public ActionResult Eksilt(int? Id)
        {
            var sepet = db.Sepet.FirstOrDefault(x => x.SepetId == Id);
            int Adet = 0;
            using (var contex = new IsimTescilContex())
            {
                var entity = contex.Sepet.Where(x => x.Id == Id && x.Adet > 0).FirstOrDefault();
                if (entity != null)
                {
                    Adet = (int)entity.Adet - 1;
                    entity.Adet = entity.Adet - 1;
                    contex.SaveChanges();

                }
            }
            var result = new { Adet = Adet };
            return RedirectToAction("UrunList");

        }

        public ActionResult Artir(int? Id)
        {
            var sepet = db.Sepet.FirstOrDefault(x => x.SepetId == Id);
            int Adet = 0;
            using (var contex = new IsimTescilContex())
            {
                var entity = contex.Sepet.Where(x => x.Id == Id && x.Adet > 0).FirstOrDefault();
                if (entity != null)
                {
                    Adet = (int)entity.Adet + 1;
                    entity.Adet = entity.Adet + 1;
                    contex.SaveChanges();

                }
            }
            var result = new { Adet = Adet };
            return RedirectToAction("UrunList");
        }
        public ActionResult SepetiBosalt(int? Id)
        {
            var sepet = db.Sepet.ToList();
            foreach (var item in sepet)
            {
                item.Adet = 0;
                db.SaveChanges();
            }
            return RedirectToAction("UrunList");
        }
    }
}