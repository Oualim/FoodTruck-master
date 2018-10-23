USE [FoodTruck_otmo]
GO

/****** Object:  Table [dbo].[Article]    Script Date: 16/10/2018 16:26:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Article](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [nchar](30) NOT NULL,
	[Image] [nchar](30) NULL,
	[Prix] [float] NOT NULL,
	[FamilleId] [int] NOT NULL,
	[NombreVendus] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Allergenes] [nchar](30) NULL,
	[Grammage] [int] NULL,
	[Litrage] [int] NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_FamilleArticle] FOREIGN KEY([FamilleId])
REFERENCES [dbo].[FamilleArticle] ([Id])
GO

ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_FamilleArticle]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Litrage de l''artricle en ml' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Article', @level2type=N'COLUMN',@level2name=N'Litrage'
GO

