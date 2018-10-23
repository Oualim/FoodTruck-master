using FoodTruck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodTruck.DAL;
using System.Net.Mail;

namespace FoodTruck.Controllers
{
    public class CommandeController : Controller
    {
        // GET: Commande
        public ActionResult Index()
        {

            ViewBag.PagePanier = false;
            ViewBag.pasDePanier = false;
            ViewBag.pasDeClient = false;

            if (this.Session["MonPanier"] == null)
            {
                ViewBag.pasDePanier = true;
                return View();
            }
            Panier lePanier = (Panier)this.Session["MonPanier"];

            if (this.Session["Utilisateur"] == null)
            {
                ViewBag.pasDeClient = true;
                return View();
            }
            Utilisateur lUtilisateur = (Utilisateur)this.Session["Utilisateur"];
            ViewBag.lUtilisateur = lUtilisateur;


            Commande laCommande = new Commande();
            
            laCommande.UtilisateurId = lUtilisateur.Id;
            laCommande.AdresseId = 1; //TODO
            laCommande.DateCommande = DateTime.Now;

            laCommande.DateCommande = laCommande.DateCommande.AddMinutes(120); //ajout 2 heures pour serveur Azure // DETTE TECHNIQUE

            laCommande.ModeLivraison = "à notre camion."; //TODO
            laCommande.DateLivraison = DateTime.Now;
            
            //calcul de l'heure de livraison fictive en fonction de l'heure actuelle TODO
            TimeSpan ts;
            if (5 <= laCommande.DateCommande.Hour && laCommande.DateCommande.Hour < 10)
                ts = new TimeSpan(10, 00, 0);
            else if (10 <= laCommande.DateCommande.Hour && laCommande.DateCommande.Hour < 12)
                ts = new TimeSpan(12, 30, 0);
            //else if (12 <= laCommande.DateCommande.Hour && laCommande.DateCommande.Hour < 16)
            //    ts = new TimeSpan(16, 30, 0);
            //else if (16 <= laCommande.DateCommande.Hour && laCommande.DateCommande.Hour < 20)
            else if (12 <= laCommande.DateCommande.Hour && laCommande.DateCommande.Hour < 20) //TODO CHOIX DINER POUR DEMO
                ts = new TimeSpan(20, 20, 0);
            else
                ts = new TimeSpan();
            laCommande.DateLivraison = laCommande.DateLivraison.Date + ts;
            ViewBag.DelaiLivraison = laCommande.DateLivraison - laCommande.DateCommande;


            laCommande.listeArticles = lePanier.listeArticles;

            laCommande.PrixTotal = 0;
            string lesArticles="";
            foreach (Article article in lePanier.listeArticles)
            {
                lesArticles += "\n" + article.Quantite + " x " + article.Nom;
                laCommande.PrixTotal += article.Prix * article.Quantite;
                ArticleDAL larticleDAL = new ArticleDAL();
                larticleDAL.AugmenterQuantite(article.Id, article.Quantite);
            }
                
            CommandeDAL laCommandeDal = new CommandeDAL();
            laCommandeDal.Ajouter(laCommande);


            //structuration du mail
            string nomClient = lUtilisateur.Nom;
            string prenomClient = lUtilisateur.Prenom;
            string emailClient = lUtilisateur.Email;


            string nomOk = Server.HtmlEncode("commande foodtruck");
            string commentsOk = Server.HtmlEncode("le détail de la commande");

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("commande@foodtruck.fr");
                message.To.Add("tvuille@gmail.com");
                message.Subject = "[OTMO][Commande] Nouvelle commande";
                message.Body = $"Nouvelle commande d'un client. Merci de lui préparer pour le {laCommande.DateLivraison}\n"+
                    $"Nom : {nomClient} Prénom : {prenomClient} Email : {emailClient}+ {lesArticles}";
           
                SmtpClient client = new SmtpClient();
                client.EnableSsl = true;
                client.Send(message);
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                ViewBag.Mail = "Erreur dans l'envoi de la commande veuillez rééssayer s'il vous plait";
            }
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("commande@foodtruck.fr");
                message.To.Add(emailClient);
                message.Subject = " Nouvelle commande OTMO prise en compte";
                message.Body = $"Bonjour {lUtilisateur.Prenom}\nVotre dernière commande a bien été prise en compte.\nMerci de votre confiance\n" +
                    "voici le récapitulatif : \n" + $"Nom : {nomClient} Prénom : {prenomClient} Email : {emailClient}  {lesArticles}";
                SmtpClient client = new SmtpClient();
                client.EnableSsl = true;
                client.Send(message);
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                ViewBag.Mail = "Erreur dans l'envoi de la commande veuillez rééssayer s'il vous plait";
            }

            Session["MaCommande"] = laCommande;
            ViewBag.laCommande = laCommande;
            lePanier=new Panier();
            Session["MonPanier"] = null;
            return View();
        }

        // GET: Commande/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
 
    }
}
