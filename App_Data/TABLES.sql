USE [master]
GO
/****** Object:  Database [PatientMonitoringUsingIOT]    Script Date: 20-06-2018 12:29:42 ******/
CREATE DATABASE [PatientMonitoringUsingIOT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PatientMonitoringUsingIOT', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\PatientMonitoringUsingIOT.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PatientMonitoringUsingIOT_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\PatientMonitoringUsingIOT_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PatientMonitoringUsingIOT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET ARITHABORT OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET RECOVERY FULL 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET  MULTI_USER 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET DELAYED_DURABILITY = DISABLED 
GO
USE [PatientMonitoringUsingIOT]
GO
/****** Object:  Table [dbo].[Heartbeat]    Script Date: 20-06-2018 12:29:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Heartbeat](
	[Name] [nvarchar](50) NULL,
	[Heartbeat] [nvarchar](50) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PatientDetail]    Script Date: 20-06-2018 12:29:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Patientname] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Temperature]    Script Date: 20-06-2018 12:29:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Temperature](
	[Name] [nvarchar](50) NULL,
	[Temperature] [nvarchar](50) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Heartbeat] ON 

INSERT [dbo].[Heartbeat] ([Name], [Heartbeat], [Id]) VALUES (N'Divya', N'72', 1)
INSERT [dbo].[Heartbeat] ([Name], [Heartbeat], [Id]) VALUES (N'Divya', N'74', 2)
INSERT [dbo].[Heartbeat] ([Name], [Heartbeat], [Id]) VALUES (N'Divya', N'75', 3)
INSERT [dbo].[Heartbeat] ([Name], [Heartbeat], [Id]) VALUES (N'Divya', N'76', 4)
INSERT [dbo].[Heartbeat] ([Name], [Heartbeat], [Id]) VALUES (N'Divya', N'85', 5)
INSERT [dbo].[Heartbeat] ([Name], [Heartbeat], [Id]) VALUES (N'Karan', N'75', 6)
INSERT [dbo].[Heartbeat] ([Name], [Heartbeat], [Id]) VALUES (N'Karan', N'75', 7)
INSERT [dbo].[Heartbeat] ([Name], [Heartbeat], [Id]) VALUES (N'Karan', N'72', 8)
INSERT [dbo].[Heartbeat] ([Name], [Heartbeat], [Id]) VALUES (N'Karan', N'79', 9)
INSERT [dbo].[Heartbeat] ([Name], [Heartbeat], [Id]) VALUES (N'Karan', N'65', 10)
SET IDENTITY_INSERT [dbo].[Heartbeat] OFF
SET IDENTITY_INSERT [dbo].[PatientDetail] ON 

INSERT [dbo].[PatientDetail] ([Id], [Patientname]) VALUES (1, N'Karan')
INSERT [dbo].[PatientDetail] ([Id], [Patientname]) VALUES (2, N'Divya')
INSERT [dbo].[PatientDetail] ([Id], [Patientname]) VALUES (3, N'Rita')
SET IDENTITY_INSERT [dbo].[PatientDetail] OFF
SET IDENTITY_INSERT [dbo].[Temperature] ON 

INSERT [dbo].[Temperature] ([Name], [Temperature], [Id]) VALUES (N'Karan', N'96', 1)
INSERT [dbo].[Temperature] ([Name], [Temperature], [Id]) VALUES (N'Karan', N'95', 2)
INSERT [dbo].[Temperature] ([Name], [Temperature], [Id]) VALUES (N'Karan', N'102', 3)
INSERT [dbo].[Temperature] ([Name], [Temperature], [Id]) VALUES (N'Karan', N'95', 4)
INSERT [dbo].[Temperature] ([Name], [Temperature], [Id]) VALUES (N'Karan', N'94', 5)
INSERT [dbo].[Temperature] ([Name], [Temperature], [Id]) VALUES (N'Divya', N'96', 6)
INSERT [dbo].[Temperature] ([Name], [Temperature], [Id]) VALUES (N'Divya', N'97', 7)
INSERT [dbo].[Temperature] ([Name], [Temperature], [Id]) VALUES (N'Divya', N'99', 8)
INSERT [dbo].[Temperature] ([Name], [Temperature], [Id]) VALUES (N'Divya', N'96', 9)
INSERT [dbo].[Temperature] ([Name], [Temperature], [Id]) VALUES (N'Divya', N'95', 10)
SET IDENTITY_INSERT [dbo].[Temperature] OFF
USE [master]
GO
ALTER DATABASE [PatientMonitoringUsingIOT] SET  READ_WRITE 
GO
