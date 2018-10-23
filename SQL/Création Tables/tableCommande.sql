USE [FoodTruck_otmo]
GO

/****** Object:  Table [dbo].[Commande]    Script Date: 16/10/2018 16:27:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Commande](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Numero] [int] NOT NULL,
	[TypeRepasId] [int] NOT NULL,
	[ArticleId] [int] NOT NULL,
	[DateCommande] [datetime] NOT NULL,
	[DateLivraison] [datetime] NOT NULL,
	[ModeLivraison] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Commande] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Commande]  WITH CHECK ADD  CONSTRAINT [FK_Commande_TypeRepas] FOREIGN KEY([TypeRepasId])
REFERENCES [dbo].[TypeRepas] ([Id])
GO

ALTER TABLE [dbo].[Commande] CHECK CONSTRAINT [FK_Commande_TypeRepas]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date de la commande' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Commande', @level2type=N'COLUMN',@level2name=N'DateCommande'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'table des commandes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Commande'
GO

