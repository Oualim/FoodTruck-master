USE [FoodTruck_otmo]
GO

/****** Object:  Table [dbo].[Commande_Client]    Script Date: 16/10/2018 16:26:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Commande_Client](
	[ClientId] [int] NOT NULL,
	[CommandeId] [int] NOT NULL,
 CONSTRAINT [PK_Commande_Client] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC,
	[CommandeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Commande_Client]  WITH CHECK ADD  CONSTRAINT [FK_Commande_Client_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([Id])
GO

ALTER TABLE [dbo].[Commande_Client] CHECK CONSTRAINT [FK_Commande_Client_Client]
GO

ALTER TABLE [dbo].[Commande_Client]  WITH CHECK ADD  CONSTRAINT [FK_Commande_Client_Commande] FOREIGN KEY([CommandeId])
REFERENCES [dbo].[Commande] ([Id])
GO

ALTER TABLE [dbo].[Commande_Client] CHECK CONSTRAINT [FK_Commande_Client_Commande]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'table de liaison commandes par client' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Commande_Client'
GO

