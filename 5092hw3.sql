USE [master]
GO

/****** Object:  Database [MFM5092]    Script Date: 3/20/2020 3:11:31 PM ******/
CREATE DATABASE [MFM5092]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MFM5092', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MFM5092.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MFM5092_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MFM5092_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MFM5092].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [MFM5092] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [MFM5092] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [MFM5092] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [MFM5092] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [MFM5092] SET ARITHABORT OFF 
GO

ALTER DATABASE [MFM5092] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [MFM5092] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [MFM5092] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [MFM5092] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [MFM5092] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [MFM5092] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [MFM5092] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [MFM5092] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [MFM5092] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [MFM5092] SET  DISABLE_BROKER 
GO

ALTER DATABASE [MFM5092] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [MFM5092] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [MFM5092] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [MFM5092] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [MFM5092] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [MFM5092] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [MFM5092] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [MFM5092] SET RECOVERY FULL 
GO

ALTER DATABASE [MFM5092] SET  MULTI_USER 
GO

ALTER DATABASE [MFM5092] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [MFM5092] SET DB_CHAINING OFF 
GO

ALTER DATABASE [MFM5092] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [MFM5092] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [MFM5092] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [MFM5092] SET QUERY_STORE = OFF
GO

ALTER DATABASE [MFM5092] SET  READ_WRITE 
GO
USE [MFM5092]
GO

/****** Object:  Table [finance].[instrument]    Script Date: 3/20/2020 3:22:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [finance].[instrument](
	[InstrumentId] [int] IDENTITY(1,1) NOT NULL,
	[Companyname] [varchar](50) NULL,
	[Ticker] [varchar](10) NULL,
	[Exchange] [varchar](10) NOT NULL,
	[Underlying] [float] NULL,
	[Strike] [float] NULL,
	[Tenor] [float] NULL,
	[IsCall] [bit] NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_instrument] PRIMARY KEY CLUSTERED 
(
	[InstrumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [finance].[instrument]  WITH CHECK ADD  CONSTRAINT [FK_instrument_instype] FOREIGN KEY([Type])
REFERENCES [finance].[instype] ([typeId])
GO

ALTER TABLE [finance].[instrument] CHECK CONSTRAINT [FK_instrument_instype]
GO
USE [MFM5092]
GO

/****** Object:  Table [finance].[instype]    Script Date: 3/20/2020 3:23:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [finance].[instype](
	[typeId] [int] IDENTITY(1,1) NOT NULL,
	[typename] [varchar](20) NOT NULL,
 CONSTRAINT [PK_instype] PRIMARY KEY CLUSTERED 
(
	[typeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [finance].[instype]  WITH CHECK ADD  CONSTRAINT [FK_instype_instype] FOREIGN KEY([typeId])
REFERENCES [finance].[instype] ([typeId])
GO

ALTER TABLE [finance].[instype] CHECK CONSTRAINT [FK_instype_instype]
GO
USE [MFM5092]
GO

/****** Object:  Table [finance].[interestrate]    Script Date: 3/20/2020 3:23:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [finance].[interestrate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tenor] [float] NOT NULL,
	[Rate] [float] NOT NULL,
 CONSTRAINT [PK_interestrate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [MFM5092]
GO

/****** Object:  Table [finance].[stockprice]    Script Date: 3/20/2020 3:24:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [finance].[stockprice](
	[priceId] [int] IDENTITY(1,1) NOT NULL,
	[companyId] [int] NOT NULL,
	[date] [date] NOT NULL,
	[closeprice] [float] NOT NULL,
 CONSTRAINT [PK_stockprice] PRIMARY KEY CLUSTERED 
(
	[priceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [MFM5092]
GO

/****** Object:  Table [finance].[trade]    Script Date: 3/20/2020 3:26:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [finance].[trade](
	[TradeId] [int] IDENTITY(1,1) NOT NULL,
	[IsBuy] [bit] NOT NULL,
	[Quantity] [float] NOT NULL,
	[Instrument] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Timestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_trade] PRIMARY KEY CLUSTERED 
(
	[TradeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [finance].[trade]  WITH CHECK ADD  CONSTRAINT [FK_trade_instrument] FOREIGN KEY([Instrument])
REFERENCES [finance].[instrument] ([InstrumentId])
GO

ALTER TABLE [finance].[trade] CHECK CONSTRAINT [FK_trade_instrument]
GO

