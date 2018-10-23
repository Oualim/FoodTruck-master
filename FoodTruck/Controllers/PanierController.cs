using FoodTruck.DAL;
using FoodTruck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodTruck.Controllers
{
    public class PanierController : Controller
    {
        // GET: Panier
        public ActionResult Index()
        {
            ViewBag.PagePanier = true;

            Utilisateur lUtilisateur;
            if (this.Session["Utilisateur"] != null)
            {
                lUtilisateur = (Utilisateur)this.Session["Utilisateur"];
                ViewBag.lUtilisateur = lUtilisateur;
            }

            Panier lePanier;
            if (this.Session["MonPanier"] == null)
                lePanier = new Panier();
            else
                lePanier = (Panier)this.Session["MonPanier"];

            this.Session["MonPanier"] = lePanier;
            ViewBag.Panier = lePanier;

            

            return View();
        }

        // GET: Panier/Ajouter/5
        public ActionResult Ajouter(int id)
        {
            Utilisateur lUtilisateur;
            if (this.Session["Utilisateur"] != null)
            {
                lUtilisateur = (Utilisateur)this.Session["Utilisateur"];
                ViewBag.lUtilisateur = lUtilisateur;
            }

            Panier lePanier;
            if (this.Session["MonPanier"] == null)
                lePanier = new Panier();
            else
                lePanier = (Panier)this.Session["MonPanier"];

            ArticleDAL lArticleDAL = new ArticleDAL();
            Article lArticle = lArticleDAL.Details(id);

            var t = lePanier.listeArticles.Find(art => art.Id == lArticle.Id);
            if (t == null)
            {
                lArticle.Quantite = 1;
                lePanier.listeArticles.Add(lArticle);
            }
            else
                t.Quantite++;

            lePanier.PrixTotal += lArticle.Prix;
            this.Session["MonPanier"] = lePanier;
            ViewBag.Panier = lePanier;
            return RedirectToAction("../Article");
        }

        // GET: Panier/Retirer/0
        public ActionResult Retirer(int id)
        {
            Utilisateur lUtilisateur;
            if (this.Session["Utilisateur"] != null)
            {
                lUtilisateur = (Utilisateur)this.Session["Utilisateur"];
                ViewBag.lUtilisateur = lUtilisateur;
            }

            Panier lePanier;
            if (this.Session["MonPanier"] == null)
                lePanier = new Panier();
            else
                lePanier = (Panier)this.Session["MonPanier"];

            ArticleDAL lArticleDAL = new ArticleDAL();
            Article lArticle = lArticleDAL.Details(lePanier.listeArticles[id].Id);
            lePanier.PrixTotal -= lArticle.Prix;

            if (lePanier.listeArticles[id].Quantite > 1)
                lePanier.listeArticles[id].Quantite--;
            else
                lePanier.listeArticles.RemoveAt(id);
            this.Session["MonPanier"] = lePanier;
            ViewBag.Panier = lePanier;
            return RedirectToAction("../Article");
        }


        // POST: Panier/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Panier/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Panier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Panier/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Panier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
