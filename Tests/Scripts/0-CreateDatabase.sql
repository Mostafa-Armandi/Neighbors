USE [master]
GO

/****** Object:  Database [Roamler]    Script Date: 11/14/2021 7:58:48 PM ******/
CREATE DATABASE [Roamler]
    CONTAINMENT = NONE
    ON  PRIMARY
    ( NAME = N'Roamler', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Roamler.mdf' , SIZE = 139264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
    LOG ON
    ( NAME = N'Roamler_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Roamler_log.ldf' , SIZE = 270336KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO