USE [FoodTruck_otmo]
GO

/****** Object:  Table [dbo].[Client]    Script Date: 16/10/2018 16:27:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Client](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nchar](20) NOT NULL,
	[Mdp] [nchar](64) NOT NULL,
	[Nom] [nchar](20) NOT NULL,
	[Prenom] [nchar](20) NOT NULL,
	[Societe] [nchar](20) NULL,
	[AdresseVille] [nchar](20) NULL,
	[AdresseRue] [nchar](30) NULL,
	[AdresseNumero] [nchar](4) NULL,
	[Telephone] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mot de passe chiffré en SHA256 sur 64 caractères' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Client', @level2type=N'COLUMN',@level2name=N'Mdp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'table des clients' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Client'
GO

