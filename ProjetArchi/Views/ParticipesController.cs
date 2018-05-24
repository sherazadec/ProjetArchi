﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetArchi.Models;

namespace ProjetArchi.Views
{
    public class ParticipesController : Controller
    {
        private ProjetArchi3DEntities db = new ProjetArchi3DEntities();

        // GET: Participes
        public ActionResult Index()
        {
            var participe = db.Participe.Include(p => p.Architectes).Include(p => p.Clients).Include(p => p.Modélisateurs).Include(p => p.Projets);
            return View(participe.ToList());
        }

        // GET: Participes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participe participe = db.Participe.Find(id);
            if (participe == null)
            {
                return HttpNotFound();
            }
            return View(participe);
        }

        // GET: Participes/Create
        public ActionResult Create()
        {
            ViewBag.xid_archi = new SelectList(db.Architectes, "id_archi", "id_archi");
            ViewBag.xid_client = new SelectList(db.Clients, "id_client", "admin");
            ViewBag.xid_mod = new SelectList(db.Modélisateurs, "id_model", "id_model");
            ViewBag.xid_projet = new SelectList(db.Projets, "id_proj", "id_proj");
            return View();
        }

        // POST: Participes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,xid_client,xid_projet,xid_archi,xid_mod")] Participe participe)
        {
            if (ModelState.IsValid)
            {
                db.Participe.Add(participe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.xid_archi = new SelectList(db.Architectes, "id_archi", "id_archi", participe.xid_archi);
            ViewBag.xid_client = new SelectList(db.Clients, "id_client", "admin", participe.xid_client);
            ViewBag.xid_mod = new SelectList(db.Modélisateurs, "id_model", "id_model", participe.xid_mod);
            ViewBag.xid_projet = new SelectList(db.Projets, "id_proj", "id_proj", participe.xid_projet);
            return View(participe);
        }

        // GET: Participes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participe participe = db.Participe.Find(id);
            if (participe == null)
            {
                return HttpNotFound();
            }
            ViewBag.xid_archi = new SelectList(db.Architectes, "id_archi", "id_archi", participe.xid_archi);
            ViewBag.xid_client = new SelectList(db.Clients, "id_client", "admin", participe.xid_client);
            ViewBag.xid_mod = new SelectList(db.Modélisateurs, "id_model", "id_model", participe.xid_mod);
            ViewBag.xid_projet = new SelectList(db.Projets, "id_proj", "id_proj", participe.xid_projet);
            return View(participe);
        }

        // POST: Participes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,xid_client,xid_projet,xid_archi,xid_mod")] Participe participe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.xid_archi = new SelectList(db.Architectes, "id_archi", "id_archi", participe.xid_archi);
            ViewBag.xid_client = new SelectList(db.Clients, "id_client", "admin", participe.xid_client);
            ViewBag.xid_mod = new SelectList(db.Modélisateurs, "id_model", "id_model", participe.xid_mod);
            ViewBag.xid_projet = new SelectList(db.Projets, "id_proj", "id_proj", participe.xid_projet);
            return View(participe);
        }

        // GET: Participes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participe participe = db.Participe.Find(id);
            if (participe == null)
            {
                return HttpNotFound();
            }
            return View(participe);
        }

        // POST: Participes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Participe participe = db.Participe.Find(id);
            db.Participe.Remove(participe);
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
