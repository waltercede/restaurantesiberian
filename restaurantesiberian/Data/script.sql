USE [master]
GO
/****** Object:  Database [SiberianDB]    Script Date: 10/10/2021 22:48:04 ******/
CREATE DATABASE [SiberianDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SiberianDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SiberianDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SiberianDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SiberianDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SiberianDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SiberianDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SiberianDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SiberianDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SiberianDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SiberianDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SiberianDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SiberianDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SiberianDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SiberianDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SiberianDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SiberianDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SiberianDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SiberianDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SiberianDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SiberianDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SiberianDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SiberianDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SiberianDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SiberianDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SiberianDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SiberianDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SiberianDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SiberianDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SiberianDB] SET RECOVERY FULL 
GO
ALTER DATABASE [SiberianDB] SET  MULTI_USER 
GO
ALTER DATABASE [SiberianDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SiberianDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SiberianDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SiberianDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SiberianDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [SiberianDB]
GO
/****** Object:  User [TESIS]    Script Date: 10/10/2021 22:48:05 ******/
CREATE USER [TESIS] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[TESIS]
GO
/****** Object:  Schema [TESIS]    Script Date: 10/10/2021 22:48:05 ******/
CREATE SCHEMA [TESIS]
GO
/****** Object:  Table [dbo].[Ciudad]    Script Date: 10/10/2021 22:48:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ciudad](
	[IDCiudad] [int] IDENTITY(1,1) NOT NULL,
	[Nombre Ciudad] [varchar](150) NULL,
	[Fecha Creacion] [date] NULL,
 CONSTRAINT [PK_Ciudad] PRIMARY KEY CLUSTERED 
(
	[IDCiudad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Restaurantes]    Script Date: 10/10/2021 22:48:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Restaurantes](
	[NombreRestaurante] [varchar](250) NULL,
	[IDCiudad] [int] NULL,
	[NumeroAforo] [int] NULL,
	[Telefono] [nchar](10) NULL,
	[Fecha Creacion] [date] NULL,
	[IDRestaurante] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [Pk_Restaurantes_IDRestaurante] PRIMARY KEY CLUSTERED 
(
	[IDRestaurante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Restaurantes]  WITH CHECK ADD  CONSTRAINT [FK_Restaurantes_Ciudad] FOREIGN KEY([IDCiudad])
REFERENCES [dbo].[Ciudad] ([IDCiudad])
GO
ALTER TABLE [dbo].[Restaurantes] CHECK CONSTRAINT [FK_Restaurantes_Ciudad]
GO
/****** Object:  StoredProcedure [dbo].[Sp_Restaurantes]    Script Date: 10/10/2021 22:48:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_Restaurantes]
@tipofuncion int,
@ciudad varchar(100)='',
@idciudad int=1,
@idrestaurante int=1,
@nombrerestaurante varchar(250)='',
@numeroAforo int=0,
@telefono VARCHAR(10)=''
AS

BEGIN
--CONSULTA LISTA DE RESTAURANTES POR EL NOMBRE DE LA CIUDAD
if (@tipofuncion =1)
begin
SELECT 
		c.IDCiudad,c.[Nombre Ciudad],r.IDRestaurante,r.NombreRestaurante,r.NumeroAforo,r.Telefono,r.[Fecha Creacion]
FROM 
	    Ciudad c INNER JOIN Restaurantes r on c.IDCiudad=r.IDCiudad 
WHERE  
        c.[Nombre Ciudad] like '%'+@ciudad+'%'
end
--CONSULTA SOLO UN RESTAURANTE ATRAVEZ DEL ID
else if(@tipofuncion=2)
begin
SELECT IDCiudad, IDRestaurante,NombreRestaurante,NumeroAforo,Telefono ,[Fecha Creacion]
FROM Restaurantes WHERE IDRestaurante=@idrestaurante
end
--INSERTA RESTAURANTE
else if(@tipofuncion=3)
begin
INSERT INTO Restaurantes(NombreRestaurante,IDCiudad,NumeroAforo,Telefono,[Fecha Creacion]) 
VALUES(@nombrerestaurante,@idciudad,@numeroAforo,@telefono,GETDATE())
end
--ACTUALIZA RETAURANTE
else if(@tipofuncion=4)
begin
update Restaurantes SET NombreRestaurante=@nombrerestaurante,
                        IDCiudad=@idciudad,NumeroAforo=@numeroAforo,
						Telefono=@telefono
					WHERE  IDRestaurante=@idrestaurante
end
--ELIMINA RESTAURANTE
else if(@tipofuncion=5)
begin 
DELETE FROM Restaurantes WHERE IDRestaurante=@idrestaurante
end
END


GO
USE [master]
GO
ALTER DATABASE [SiberianDB] SET  READ_WRITE 
GO
