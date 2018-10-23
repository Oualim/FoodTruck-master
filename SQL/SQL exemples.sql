use FoodTruck_otmo

--select Article.Nom, Article.Prix, Article.Description, FamilleArticle.Nom as typeDePlat
--from Article
--inner join FamilleArticle on FamilleArticle.Id=Article.FamilleId
--where FamilleArticle.Nom='Entree'

--select *
--from Commande
--inner join Commande_Article on Commande_Article.CommandeId=Commande.Id
--inner join Article on Article.Id = Commande_Article.ArticleId
--where ClientId=2


--select Client.Nom, Commande.Numero, DateLivraison, Article.Nom, Quantite, Commande_Article.PrixTotal as PrixArticles, Commande.PrixTotal
--from Commande
--inner join Client on Client.Id = Commande.ClientId
--inner join Commande_Article on Commande_Article.CommandeId=Commande.Id
--inner join Article on Article.Id = Commande_Article.ArticleId
--where ClientId=3


--SELECT Commande.Numero, SUM(Commande_Article.PrixTotal) as PrixTotalCommande
--FROM Commande
--inner join Commande_Article on Commande_Article.CommandeId=Commande.Id
--SELECT TOP 3 nom
--FROM Article
--ORDER BY NombreVendus




--INSERT INTO Commande(ClientId ,DateCommande,DateLivraison ,ModeLivraison,AdresseId ,PrixTotal)
-- VALUES ('2', '2018-10-10' ,'2018-10-10', 'Camion', '1', '15')

--insert into Commande_Article
--(CommandeId, ArticleId, Quantite, PrixTotal)
--values(7,1,1,4)


--select top 3 nom
--from Article
--where Id IN (select top 5 Id from Article where familleId<=3 order by NombreVendus desc)
--order by newid()


--Select Id, Nom, Prenom, Telephone, Email, Mdp
--From Client
--Where Id=2

--update Article
--set NombreVendus = NombreVendus+1
--where Id = 204

--article par dispo du lundi midi famille 3
--select Article.Id, Article.Nom, Image, Description, Prix, Grammage, Litrage
--from Article
--inner join Disponibilite on Disponibilite.ArticleId=Article.Id
--inner join Jour on Jour.Id = Disponibilite.JourId
--inner join TypeRepas on TypeRepas.Id = Disponibilite.TypeRepasId
--where Article.FamilleId=3 and Jour.Nom='Lundi' and TypeRepas.Nom='Dejeuner'


--where Article.FamilleId=3 and Jour.Nom='Lundi' and TypeRepas.Nom='Dejeuner'
---- articles non présent le dimanche au diner
--select Article.Id, Article.Nom, Image, Description, Prix, Grammage, Litrage
--from Article
--inner join Disponibilite on Disponibilite.ArticleId=Article.Id
--inner join Jour on Jour.Id = Disponibilite.JourId
--inner join TypeRepas on TypeRepas.Id = Disponibilite.TypeRepasId
--select Article.Id, Article.Nom, Image, Description, Prix, Grammage, Litrage
--from Article
--except
--select Article.Id, Article.Nom, Image, Description, Prix, Grammage, Litrage
--from Article
--inner join Disponibilite on Disponibilite.ArticleId=Article.Id
--inner join Jour on Jour.Id = Disponibilite.JourId
--inner join TypeRepas on TypeRepas.Id = Disponibilite.TypeRepasId
--where Article.FamilleId=3 and Jour.Nom='Lundi' and TypeRepas.Nom='Dejeuner'

insert into Utilisateur
(Email, Mdp)
values ('tototo', 'totototo')