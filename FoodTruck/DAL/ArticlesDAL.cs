using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FoodTruck.Models;

namespace FoodTruck.DAL
{
    public class ArticlesDAL
    {
        public List<Article> listeArticles { get; set; }

        //Random de 3 des 5 articles les plus vendus
        public void ListerRandom(int nombreRetour, int nombreTop)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                ConnectionStringSettings connex = ConfigurationManager.ConnectionStrings["ServeurTestUser"];
                connection.ConnectionString = connex.ConnectionString;
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                       $"select top {nombreRetour} Id, Nom, Image, Description, Prix, Grammage, Litrage " +
                        "from Article " +
                       $"where Id in(select top { nombreTop} Id from Article where familleId<=3 order by NombreVendus desc) " +
                        "order by newid()";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        listeArticles = new List<Article>();
                        while (reader.Read())
                        {
                            Article articleEnCours = new Article
                            {
                                Id = (int)reader["Id"],
                                Nom = reader["Nom"].ToString(),
                                Image = reader["Image"].ToString(),
                                Description = reader["Description"].ToString(),
                                Prix = (double)reader["Prix"],
                                Grammage = (int)reader["Grammage"],
                                Litrage = (int)reader["Litrage"]
                            };
                            listeArticles.Add(articleEnCours);
                        }
                    }
                }
            }
        }



        //Lister les nombreMax articles et ordonner par Nombre Vendus
        public void Lister(int nombreMax, int familleId)
        {
            Lister(nombreMax, familleId, Jour.Aucun, TypeRepas.Aucun);
        }


        //Lister les nombreMax articles de la familleId, du Jour et du typeRepas et ordonner par Nombre Vendus
        public void Lister(int nombreMax, int familleId, Jour jour, TypeRepas typeRepas)
        {
            if (nombreMax == 0) nombreMax = 200;

            using (SqlConnection connection = new SqlConnection())
            {
                ConnectionStringSettings connex = ConfigurationManager.ConnectionStrings["ServeurTestUser"];
                connection.ConnectionString = connex.ConnectionString;
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    if (jour == Jour.Aucun && typeRepas == TypeRepas.Aucun)
                    {
                        if (familleId == 0)
                            command.CommandText =
                                $" SELECT TOP {nombreMax} Id, Nom, Image, Description, Prix, Grammage, Litrage" +
                                " FROM Article" +
                                " ORDER BY NombreVendus DESC";
                        else
                            command.CommandText =
                                $" SELECT TOP {nombreMax} Id, Nom, Image, Description, Prix, Grammage, Litrage" +
                                " FROM Article" +
                                $" WHERE FamilleId = {familleId}" +
                                " ORDER BY NombreVendus DESC";
                    }
                    else
                    {
                        if (familleId == 0)
                            command.CommandText =
                               $" SELECT TOP {nombreMax} Article.Id, Article.Nom, Image, Description, Prix, Grammage, Litrage" +
                                " FROM Article" +
                                " inner join Disponibilite on Disponibilite.ArticleId=Article.Id"+
                                " inner join Jour on Jour.Id = Disponibilite.JourId"+
                                " inner join TypeRepas on TypeRepas.Id = Disponibilite.TypeRepasId"+
                               $" where Jour.Nom = '{jour}' and TypeRepas.Nom = '{typeRepas}'"+
                                " ORDER BY NombreVendus DESC";
                        else
                            command.CommandText =
                               $" SELECT TOP {nombreMax} Article.Id, Article.Nom, Image, Description, Prix, Grammage, Litrage" +
                                " FROM Article" +
                                " inner join Disponibilite on Disponibilite.ArticleId=Article.Id" +
                                " inner join Jour on Jour.Id = Disponibilite.JourId" +
                                " inner join TypeRepas on TypeRepas.Id = Disponibilite.TypeRepasId" +
                               $" WHERE FamilleId = {familleId} and Jour.Nom = '{jour}' and TypeRepas.Nom = '{typeRepas}'" +
                                " ORDER BY NombreVendus DESC";
                    }



                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        listeArticles = new List<Article>();
                        while (reader.Read())
                        {
                            Article articleEnCours = new Article
                            {
                                Id = (int)reader["Id"],
                                Nom = reader["Nom"].ToString(),
                                Image = reader["Image"].ToString(),
                                Description = reader["Description"].ToString(),
                                Prix = (double)reader["Prix"],
                                Grammage = (int)reader["Grammage"],
                                Litrage = (int)reader["Litrage"]
                            };
                            listeArticles.Add(articleEnCours);
                        }
                    }
                }
            }
        }

        public void Lister(int nombreMax)
        {
            Lister(nombreMax, 0);
        }
        public void Lister()
        {
            Lister(0, 0);
        }

    }
}