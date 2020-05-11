
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/10/2020 20:18:46
-- Generated from EDMX file: C:\Users\90425\source\repos\portfolio manager\portfolio manager\MyEntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [entitymodel12];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Entity_rateEntity_instrument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entity_instrument] DROP CONSTRAINT [FK_Entity_rateEntity_instrument];
GO
IF OBJECT_ID(N'[dbo].[FK_Entity_instrumentEntity_Trade]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entity_Trade] DROP CONSTRAINT [FK_Entity_instrumentEntity_Trade];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Entity_rate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entity_rate];
GO
IF OBJECT_ID(N'[dbo].[Entity_Trade]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entity_Trade];
GO
IF OBJECT_ID(N'[dbo].[Entity_instrument]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entity_instrument];
GO
IF OBJECT_ID(N'[dbo].[Entity_Historyprice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entity_Historyprice];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Entity_rate'
CREATE TABLE [dbo].[Entity_rate] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Tenor] float  NOT NULL,
    [Interest_rate] float  NOT NULL
);
GO

-- Creating table 'Entity_Trade'
CREATE TABLE [dbo].[Entity_Trade] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Direction] smallint  NOT NULL,
    [Quantity] int  NOT NULL,
    [Tradeprice] float  NOT NULL,
    [Entity_instrumentId] int  NOT NULL
);
GO

-- Creating table 'Entity_instrument'
CREATE TABLE [dbo].[Entity_instrument] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ticker] nvarchar(max)  NULL,
    [Exchange] nvarchar(max)  NULL,
    [CompanyName] nvarchar(max)  NULL,
    [Underlying] float  NOT NULL,
    [Strike] nvarchar(max)  NULL,
    [Tenor] float  NULL,
    [Type] nvarchar(max)  NULL,
    [Entity_rateId] int  NOT NULL,
    [Instype] nvarchar(max)  NOT NULL,
    [Barrier] float  NULL,
    [Rebate] float  NULL
);
GO

-- Creating table 'Entity_Historyprice'
CREATE TABLE [dbo].[Entity_Historyprice] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ticker] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [ClosePrice] float  NOT NULL,
    [CompanyName] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Entity_rate'
ALTER TABLE [dbo].[Entity_rate]
ADD CONSTRAINT [PK_Entity_rate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Entity_Trade'
ALTER TABLE [dbo].[Entity_Trade]
ADD CONSTRAINT [PK_Entity_Trade]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Entity_instrument'
ALTER TABLE [dbo].[Entity_instrument]
ADD CONSTRAINT [PK_Entity_instrument]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Entity_Historyprice'
ALTER TABLE [dbo].[Entity_Historyprice]
ADD CONSTRAINT [PK_Entity_Historyprice]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Entity_rateId] in table 'Entity_instrument'
ALTER TABLE [dbo].[Entity_instrument]
ADD CONSTRAINT [FK_Entity_rateEntity_instrument]
    FOREIGN KEY ([Entity_rateId])
    REFERENCES [dbo].[Entity_rate]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Entity_rateEntity_instrument'
CREATE INDEX [IX_FK_Entity_rateEntity_instrument]
ON [dbo].[Entity_instrument]
    ([Entity_rateId]);
GO

-- Creating foreign key on [Entity_instrumentId] in table 'Entity_Trade'
ALTER TABLE [dbo].[Entity_Trade]
ADD CONSTRAINT [FK_Entity_instrumentEntity_Trade]
    FOREIGN KEY ([Entity_instrumentId])
    REFERENCES [dbo].[Entity_instrument]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Entity_instrumentEntity_Trade'
CREATE INDEX [IX_FK_Entity_instrumentEntity_Trade]
ON [dbo].[Entity_Trade]
    ([Entity_instrumentId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------