﻿using FoodTruck.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace FoodTruck.DAL
{
    public class UtilisateurDAL
    {
        public Utilisateur Connexion(int id)
        {
            Utilisateur lUtilisateur = new Utilisateur();
            return lUtilisateur;
        }

        public Utilisateur Connexion(string Email, string Mdp)
        {
            string mdpHash;

            using (SHA256 Hash = SHA256.Create())
                mdpHash = GetHash(Hash, Mdp);

            Utilisateur lUtilisateur = new Utilisateur();
            using (SqlConnection connection = new SqlConnection())
            {
                ConnectionStringSettings connex = ConfigurationManager.ConnectionStrings["ServeurTestUser"];
                connection.ConnectionString = connex.ConnectionString;
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        " Select Id, Nom, Prenom, Telephone, Email, Mdp" +
                        " From Utilisateur" +
                       $" WHERE Email = '{Email}' and Mdp = '{mdpHash}'";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lUtilisateur.Id = (int)reader["Id"];
                            lUtilisateur.Nom = reader["Nom"].ToString();
                            lUtilisateur.Prenom = reader["Prenom"].ToString();
                            lUtilisateur.Telephone = reader["Telephone"].ToString();
                            lUtilisateur.Email = reader["Email"].ToString();
                            lUtilisateur.Mdp = reader["Mdp"].ToString();
                        }
                    }
                }
            }

            return lUtilisateur;
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public Utilisateur Creation(string Email, string Mdp, string Nom, string Prenom, string Telephone)
        {
            string mdpHash;

            using (SHA256 Hash = SHA256.Create())
                mdpHash = GetHash(Hash, Mdp);

            Utilisateur lUtilisateur = new Utilisateur();
            using (SqlConnection connection = new SqlConnection())
            {
                ConnectionStringSettings connex = ConfigurationManager.ConnectionStrings["ServeurTestUser"];
                connection.ConnectionString = connex.ConnectionString;
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        " insert into Utilisateur" +
                        " (Email, Mdp, Nom, Prenom, Telephone) " +
                       $" values('{Email}', '{mdpHash}', '{Nom}', '{Prenom}', '{Telephone}')";

                    command.ExecuteNonQuery();
                    
                }
            }

            return lUtilisateur;
        }


    }
}