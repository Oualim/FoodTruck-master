USE [FoodTruck_otmo]
GO

/****** Object:  Table [dbo].[Disponibilite]    Script Date: 16/10/2018 16:25:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Disponibilite](
	[ArticleId] [int] NOT NULL,
	[JourId] [int] NOT NULL,
	[TypeRepasId] [int] NOT NULL,
 CONSTRAINT [PK_Disponibilite_1] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC,
	[JourId] ASC,
	[TypeRepasId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Disponibilite]  WITH CHECK ADD  CONSTRAINT [FK_Disponibilite_Article1] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Article] ([Id])
GO

ALTER TABLE [dbo].[Disponibilite] CHECK CONSTRAINT [FK_Disponibilite_Article1]
GO

ALTER TABLE [dbo].[Disponibilite]  WITH CHECK ADD  CONSTRAINT [FK_Disponibilite_Jour] FOREIGN KEY([JourId])
REFERENCES [dbo].[Jour] ([Id])
GO

ALTER TABLE [dbo].[Disponibilite] CHECK CONSTRAINT [FK_Disponibilite_Jour]
GO

ALTER TABLE [dbo].[Disponibilite]  WITH CHECK ADD  CONSTRAINT [FK_Disponibilite_TypeRepas] FOREIGN KEY([TypeRepasId])
REFERENCES [dbo].[TypeRepas] ([Id])
GO

ALTER TABLE [dbo].[Disponibilite] CHECK CONSTRAINT [FK_Disponibilite_TypeRepas]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'disponibilité de l''article sur journée choisie et type de repas' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Disponibilite'
GO

