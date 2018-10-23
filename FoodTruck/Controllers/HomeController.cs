using FoodTruck.DAL;
using FoodTruck.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace FoodTruck.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PagePanier = false;

            ArticlesDAL articles = new ArticlesDAL();
            articles.ListerRandom(3, 5);
            ViewBag.Articles = articles;

            Utilisateur lUtilisateur;
            if (this.Session["Utilisateur"] != null)
            {
                lUtilisateur = (Utilisateur)this.Session["Utilisateur"];
                ViewBag.lUtilisateur = lUtilisateur;
            }
            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.PagePanier = false;

            Utilisateur lUtilisateur;
            if (this.Session["Utilisateur"] != null)
            {
                lUtilisateur = (Utilisateur)this.Session["Utilisateur"];
                ViewBag.lUtilisateur = lUtilisateur;
            }

            ViewBag.Message = "Vous avez des questions sur nos produits ?" +
                " Vous souhaitez prendre contact avec nous ? Remplissez le formulaire ci-dessous " +
                "et un membre de notre équipe vous répondra dans les plus brefs délais.";
            ViewBag.Mail = "";

            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Contact(string nom, string email, string comments)
        {
            ViewBag.PagePanier = false;

            Utilisateur lUtilisateur;
            if (this.Session["Utilisateur"] != null)
            {
                lUtilisateur = (Utilisateur) this.Session["Utilisateur"];
                ViewBag.lUtilisateur = lUtilisateur;
            }

            ViewBag.Message = "Vous avez des questions sur nos produits ?" +
                              " Vous souhaitez prendre contact avec nous ? Remplissez le formulaire ci-dessous" +
                              " et un membre de notre équipe vous répondra dans les plus brefs délais.";

            string nomOk = Server.HtmlEncode(nom);
            string commentsOk = Server.HtmlEncode(comments);

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(email);
                message.To.Add("thomas@vuille.fr");
                message.Subject = "[OTMO] Message à partir du formulaire de contact";

                StringBuilder mastringbuilder = new StringBuilder();

                mastringbuilder.Append(
                    "<html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                mastringbuilder.Append(
                    "<meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\"><title>Mail du site</title></head><body><h3>De : ");
                mastringbuilder.Append(nomOk);
                mastringbuilder.Append("</h3><h3>Message : </h3><p>");
                mastringbuilder.Append(commentsOk);
                mastringbuilder.Append("<br/>");
                mastringbuilder.Append("<br/>Si vous désirez lui répondre, voici son adresse email : ");
                mastringbuilder.Append(email);
                mastringbuilder.Append("</p></body></html>");

                message.Body = mastringbuilder.ToString();
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.EnableSsl = true;
                client.Send(message);
                ViewBag.Mail = "Votre message a bien été envoyé.";
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                ViewBag.Mail = "Erreur dans l'envoi du mail, veuillez rééssayer s'il vous plait";
            }

            return View();
        }
    }
}