
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/15/2016 10:41:14
-- Generated from EDMX file: C:\MISC\visual studio 2015\Projects\HiQScoreboard\HiQScoreboard\ScoreboardDBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ScoreboardDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Officesscoreboardresults]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScoreboardResults] DROP CONSTRAINT [FK_Officesscoreboardresults];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Offices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Offices];
GO
IF OBJECT_ID(N'[dbo].[ScoreboardResults]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScoreboardResults];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ScoreboardResults'
CREATE TABLE [dbo].[ScoreboardResults] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Q1] decimal(18,0)  NULL,
    [Q2] decimal(18,0)  NULL,
    [Q3] decimal(18,0)  NULL,
    [Q4] decimal(18,0)  NULL,
    [Q5] decimal(18,0)  NULL,
    [OfficesId] int  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'Offices'
CREATE TABLE [dbo].[Offices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ScoreboardResults'
ALTER TABLE [dbo].[ScoreboardResults]
ADD CONSTRAINT [PK_ScoreboardResults]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Offices'
ALTER TABLE [dbo].[Offices]
ADD CONSTRAINT [PK_Offices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [OfficesId] in table 'ScoreboardResults'
ALTER TABLE [dbo].[ScoreboardResults]
ADD CONSTRAINT [FK_Officesscoreboardresults]
    FOREIGN KEY ([OfficesId])
    REFERENCES [dbo].[Offices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Officesscoreboardresults'
CREATE INDEX [IX_FK_Officesscoreboardresults]
ON [dbo].[ScoreboardResults]
    ([OfficesId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------