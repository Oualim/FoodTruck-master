using FoodTruck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodTruck.DAL;
using System.Globalization;

namespace FoodTruck.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            ViewBag.PagePanier = false;

            Jour leJour;
            DayOfWeek jourDuJour = DateTime.Now.DayOfWeek;
            switch (jourDuJour)
            {
                case DayOfWeek.Sunday:
                    leJour = Jour.Dimanche;
                    ViewBag.DimancheSelected = true;
                    break;
                case DayOfWeek.Monday:
                    leJour = Jour.Lundi;
                    ViewBag.LundiSelected = true;
                    break;
                case DayOfWeek.Tuesday:
                    leJour = Jour.Mardi;
                    ViewBag.MardiSelected = true;
                    break;
                case DayOfWeek.Wednesday:
                    leJour = Jour.Mercredi;
                    ViewBag.MercrediSelected = true;
                    break;
                case DayOfWeek.Thursday:
                    leJour = Jour.Jeudi;
                    ViewBag.JeudiSelected = true;
                    break;
                case DayOfWeek.Friday:
                    leJour = Jour.Vendredi;
                    ViewBag.VendrediSelected = true;
                    break;
                case DayOfWeek.Saturday:
                    leJour = Jour.Samedi;
                    ViewBag.SamediSelected = true;
                    break;
                default:
                    leJour = Jour.Samedi;
                    break;
            }

            TypeRepas leTypeRepas = TypeRepas.Diner;

            int lHeure = DateTime.Now.Hour;
            if (lHeure>=21)
            {
                ViewBag.DejeunerSelected = true;
                if (leJour == Jour.Dimanche)
                    leJour = Jour.Lundi;
                else
                    leJour++;
                switch (leJour)
                {
                    case Jour.Dimanche:
                        ViewBag.DimancheSelected = true;
                        break;
                    case Jour.Lundi:
                        ViewBag.LundiSelected = true;
                        break;
                    case Jour.Mardi:
                        ViewBag.MardiSelected = true;
                        break;
                    case Jour.Mercredi:
                        ViewBag.MercrediSelected = true;
                        break;
                    case Jour.Jeudi:
                        ViewBag.JeudiSelected = true;
                        break;
                    case Jour.Vendredi:
                        ViewBag.VendrediSelected = true;
                        break;
                    case Jour.Samedi:
                        ViewBag.SamediSelected = true;
                        break;
                    default:
                        leJour = Jour.Samedi;
                        break;
                }
                leTypeRepas = TypeRepas.Dejeuner;
            }
            else if(lHeure < 11)
            {
                ViewBag.DejeunerSelected = true;
                leTypeRepas = TypeRepas.Dejeuner;
            }
            else
            {
                ViewBag.DinerSelected = true;
                leTypeRepas = TypeRepas.Diner;
            }

            Utilisateur lUtilisateur;
            if (this.Session["Utilisateur"] != null)
            {
                lUtilisateur = (Utilisateur)this.Session["Utilisateur"];
                ViewBag.lUtilisateur = lUtilisateur;
            }

            ArticlesDAL articlesEntree = new ArticlesDAL();
            articlesEntree.Lister(0, 1, leJour, leTypeRepas);
            ViewBag.articlesEntree = articlesEntree;

            ArticlesDAL articlesPlat = new ArticlesDAL();
            articlesPlat.Lister(0, 2, leJour, leTypeRepas);
            ViewBag.articlesPlat = articlesPlat;

            ArticlesDAL articlesDessert = new ArticlesDAL();
            articlesDessert.Lister(0, 3, leJour, leTypeRepas);
            ViewBag.articlesDessert = articlesDessert;

            ArticlesDAL articlesBoissonFraiche = new ArticlesDAL();
            articlesBoissonFraiche.Lister(0, 4, leJour, leTypeRepas);
            ViewBag.articlesBoissonFraiche = articlesBoissonFraiche;

            ArticlesDAL articlesBoissonChaude = new ArticlesDAL();
            articlesBoissonChaude.Lister(0, 5, leJour, leTypeRepas);
            ViewBag.articlesBoissonChaude = articlesBoissonChaude;

            ArticlesDAL articlesLivraisonZone = new ArticlesDAL();
            articlesLivraisonZone.Lister(0, 8);
            ViewBag.articlesLivraisonCamion = articlesLivraisonZone;

            ArticlesDAL articlesLivraisonCamion = new ArticlesDAL();
            articlesLivraisonCamion.Lister(0, 12);
            ViewBag.articlesLivraisonCamion = articlesLivraisonCamion;

            Panier lePanier;
            if (this.Session["MonPanier"] == null) lePanier = new Panier();
            else lePanier = (Panier)this.Session["MonPanier"];
            this.Session["MonPanier"] = lePanier;
            ViewBag.Panier = lePanier;

            return View();
        }

        [HttpPost]
        public ActionResult Index(string strJour, string strTypeRepas)
        {
            ViewBag.PagePanier = false;

            Jour leJour = (Jour)int.Parse(strJour);
            switch (leJour)
            {
                case Jour.Dimanche:
                    ViewBag.DimancheSelected = true;
                    break;
                case Jour.Lundi:
                    ViewBag.LundiSelected = true;
                    break;
                case Jour.Mardi:
                    ViewBag.MardiSelected = true;
                    break;
                case Jour.Mercredi:
                    ViewBag.MercrediSelected = true;
                    break;
                case Jour.Jeudi:
                    ViewBag.JeudiSelected = true;
                    break;
                case Jour.Vendredi:
                    ViewBag.VendrediSelected = true;
                    break;
                case Jour.Samedi:
                    ViewBag.SamediSelected = true;
                    break;
                default:
                    leJour = Jour.Samedi;
                    break;
            }
            TypeRepas leTypeRepas = (TypeRepas)int.Parse(strTypeRepas);
            switch (leTypeRepas)
            {
                case TypeRepas.PetitDejeuner:
                    ViewBag.PdjSelected = true;
                    break;
                case TypeRepas.Dejeuner:
                    ViewBag.DejeunerSelected = true;
                    break;
                case TypeRepas.Gouter:
                    ViewBag.GouterSelected = true;
                    break;
                case TypeRepas.Diner:
                    ViewBag.DinerSelected = true;
                    break;
            }

            Utilisateur lUtilisateur;
            if (this.Session["Utilisateur"] != null)
            {
                lUtilisateur = (Utilisateur)this.Session["Utilisateur"];
                ViewBag.lUtilisateur = lUtilisateur;
            }

            ArticlesDAL articlesEntree = new ArticlesDAL();
            articlesEntree.Lister(0, 1, leJour, leTypeRepas);
            ViewBag.articlesEntree = articlesEntree;

            ArticlesDAL articlesPlat = new ArticlesDAL();
            articlesPlat.Lister(0, 2, leJour, leTypeRepas);
            ViewBag.articlesPlat = articlesPlat;

            ArticlesDAL articlesDessert = new ArticlesDAL();
            articlesDessert.Lister(0, 3, leJour, leTypeRepas);
            ViewBag.articlesDessert = articlesDessert;

            ArticlesDAL articlesBoissonFraiche = new ArticlesDAL();
            articlesBoissonFraiche.Lister(0, 4, leJour, leTypeRepas);
            ViewBag.articlesBoissonFraiche = articlesBoissonFraiche;

            ArticlesDAL articlesBoissonChaude = new ArticlesDAL();
            articlesBoissonChaude.Lister(0, 5, leJour, leTypeRepas);
            ViewBag.articlesBoissonChaude = articlesBoissonChaude;

            ArticlesDAL articlesLivraisonZone = new ArticlesDAL();
            articlesLivraisonZone.Lister(0, 8);
            ViewBag.articlesLivraisonCamion = articlesLivraisonZone;

            ArticlesDAL articlesLivraisonCamion = new ArticlesDAL();
            articlesLivraisonCamion.Lister(0, 12);
            ViewBag.articlesLivraisonCamion = articlesLivraisonCamion;

            Panier lePanier;
            if (this.Session["MonPanier"] == null) lePanier = new Panier();
            else lePanier = (Panier)this.Session["MonPanier"];
            this.Session["MonPanier"] = lePanier;
            ViewBag.Panier = lePanier;

            return View();
        }


        // GET: Article/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.PagePanier = false;

            Utilisateur lUtilisateur;
            if (this.Session["Utilisateur"] != null)
            {
                lUtilisateur = (Utilisateur)this.Session["Utilisateur"];
                ViewBag.lUtilisateur = lUtilisateur;
            }

            Article articleCourant = new Article();
            ArticleDAL lArticleDAL = new ArticleDAL();
            articleCourant = lArticleDAL.Details(id);
            ViewBag.articleCourant = articleCourant;

            Panier lePanier;
            if (this.Session["MonPanier"] == null) lePanier = new Panier();
            else lePanier = (Panier)this.Session["MonPanier"];
            this.Session["MonPanier"] = lePanier;
            ViewBag.Panier = lePanier;

            return View();
        }
    }
}
