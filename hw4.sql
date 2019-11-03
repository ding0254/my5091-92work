USE [MFM_Financial]
GO

/****** Object:  Table [FinData].[HistPrices]    Script Date: 10/30/2019 9:16:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [FinData].[Instrument](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](250) NULL,
	[StockTicker] [varchar](10) NULL,
	[Market] [varchar](10) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [FinData].[Instrument]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_Table_1] FOREIGN KEY([ID])
REFERENCES [FinData].[Instrument] ([ID])
GO

ALTER TABLE [FinData].[Instrument] CHECK CONSTRAINT [FK_Table_1_Table_1]
GO


CREATE TABLE [FinData].[HistPrices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InstID] [int] NOT NULL,
	[Date] [date] NULL,
	[OpenPrice] [float] NULL,
	[HighPrice] [float] NULL,
	[LowPrice] [float] NULL,
	[ClosePrice] [float] NULL,
	[Volume] [int] NULL,
 CONSTRAINT [PK_HistPrice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [FinData].[HistPrices]  WITH CHECK ADD  CONSTRAINT [FK_HistPrice_Table_1] FOREIGN KEY([InstID])
REFERENCES [FinData].[Instrument] ([ID])
GO

ALTER TABLE [FinData].[HistPrices] CHECK CONSTRAINT [FK_HistPrice_Table_1]
GO


