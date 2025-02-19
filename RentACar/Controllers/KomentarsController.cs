﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class KomentarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Komentars
        public ActionResult Index()
        {
            var komentari = db.Komentari.Include(k => k.Korisnik).Include(k => k.Vozilo);
            return View(komentari.ToList());
        }

        // GET: Komentars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentar komentar = db.Komentari.Find(id);
            if (komentar == null)
            {
                return HttpNotFound();
            }
            return View(komentar);
        }

        // GET: Komentars/Create
        public ActionResult Create()
        {
            ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name");
            ViewBag.VoziloId = new SelectList(db.Vozila, "VoziloId", "ModelName");
            return View();
        }

        // POST: Komentars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SopstvenikId,Description,Rating,VoziloId,KorisnikId")] Komentar komentar)
        {
            if (ModelState.IsValid)
            {
                db.Komentari.Add(komentar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name", komentar.KorisnikId);
            ViewBag.VoziloId = new SelectList(db.Vozila, "VoziloId", "ModelName", komentar.VoziloId);
            return View(komentar);
        }

        // GET: Komentars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentar komentar = db.Komentari.Find(id);
            if (komentar == null)
            {
                return HttpNotFound();
            }
            ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name", komentar.KorisnikId);
            ViewBag.VoziloId = new SelectList(db.Vozila, "VoziloId", "ModelName", komentar.VoziloId);
            return View(komentar);
        }

        // POST: Komentars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SopstvenikId,Description,Rating,VoziloId,KorisnikId")] Komentar komentar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(komentar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KorisnikId = new SelectList(db.Korisnici, "KorisnikId", "Name", komentar.KorisnikId);
            ViewBag.VoziloId = new SelectList(db.Vozila, "VoziloId", "ModelName", komentar.VoziloId);
            return View(komentar);
        }

        // GET: Komentars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentar komentar = db.Komentari.Find(id);
            if (komentar == null)
            {
                return HttpNotFound();
            }
            return View(komentar);
        }

        // POST: Komentars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Komentar komentar = db.Komentari.Find(id);
            db.Komentari.Remove(komentar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
