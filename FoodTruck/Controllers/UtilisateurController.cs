using FoodTruck.DAL;
using FoodTruck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodTruck.Controllers
{
    public class UtilisateurController : Controller
    {
        public ActionResult Connexion()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Connexion(string Email, string Mdp)
        {
            ViewBag.PagePanier = false;

            Utilisateur lUtilisateur;
            UtilisateurDAL lUtilisateurDAL;
            if (this.Session["Utilisateur"] == null)
            {
                lUtilisateur = new Utilisateur();
                lUtilisateurDAL = new UtilisateurDAL();
                lUtilisateur = lUtilisateurDAL.Connexion(Email, Mdp);
            }
            else
            {
                lUtilisateur = (Utilisateur)this.Session["Utilisateur"];
            }
            this.Session["Utilisateur"] = lUtilisateur;
            ViewBag.lUtilisateur = lUtilisateur;
            if (lUtilisateur != null && lUtilisateur.Id != 0)
            {
                return RedirectToAction("../");
            }
            this.Session["Utilisateur"] = null;
            ViewBag.lUtilisateur = null;
            ViewBag.MauvaisEmailMdp = true;
            return View();
        }

        public ActionResult Deconnexion()
        {
            ViewBag.PagePanier = false;
            this.Session["Utilisateur"] = null;
            return View();
        }

        // GET: Utilisateur/Index/5
        public ActionResult Index(int id)
        {
            ViewBag.PagePanier = false;

            Utilisateur lUtilisateur;

            if (this.Session["Utilisateur"] == null)
                return View();
            else
            {
                lUtilisateur = (Utilisateur)this.Session["Utilisateur"];
            }
            this.Session["Utilisateur"] = lUtilisateur;
            ViewBag.lUtilisateur = lUtilisateur;

            return View();
        }

        public ActionResult Creation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Creation(string Email, string Mdp, string Nom, string Prenom, string Telephone)
        {
            ViewBag.PagePanier = false;

            Utilisateur lUtilisateur;
            UtilisateurDAL lUtilisateurDAL;
            if (this.Session["Utilisateur"] == null)
            {
                lUtilisateur = new Utilisateur();
                lUtilisateurDAL = new UtilisateurDAL();
                lUtilisateur = lUtilisateurDAL.Creation(Email, Mdp, Nom, Prenom, Telephone);
            }
            else
            {
                lUtilisateur = (Utilisateur)this.Session["Utilisateur"];
            }
            this.Session["Utilisateur"] = lUtilisateur;
            ViewBag.lUtilisateur = lUtilisateur;
            if (lUtilisateur != null && lUtilisateur.Id != 0)
            {
                return RedirectToAction("../");
            }
            this.Session["Utilisateur"] = null;
            ViewBag.lUtilisateur = null;
            ViewBag.MauvaisEmailMdp = true;
            return View();
        }

    }
}
