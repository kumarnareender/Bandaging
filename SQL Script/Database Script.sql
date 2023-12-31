USE [master]
GO
/****** Object:  Database [Bandaging]    Script Date: 10/28/2023 11:04:43 PM ******/
CREATE DATABASE [Bandaging]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Bandaging', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Bandaging.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Bandaging_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Bandaging_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Bandaging] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Bandaging].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Bandaging] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Bandaging] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Bandaging] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Bandaging] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Bandaging] SET ARITHABORT OFF 
GO
ALTER DATABASE [Bandaging] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Bandaging] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Bandaging] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Bandaging] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Bandaging] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Bandaging] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Bandaging] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Bandaging] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Bandaging] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Bandaging] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Bandaging] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Bandaging] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Bandaging] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Bandaging] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Bandaging] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Bandaging] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Bandaging] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Bandaging] SET RECOVERY FULL 
GO
ALTER DATABASE [Bandaging] SET  MULTI_USER 
GO
ALTER DATABASE [Bandaging] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Bandaging] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Bandaging] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Bandaging] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Bandaging] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Bandaging] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Bandaging', N'ON'
GO
ALTER DATABASE [Bandaging] SET QUERY_STORE = ON
GO
ALTER DATABASE [Bandaging] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Bandaging]
GO
/****** Object:  Table [dbo].[Blogs]    Script Date: 10/28/2023 11:04:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Headline] [nvarchar](max) NULL,
	[PublishedDate] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
	[ImageOne] [nvarchar](max) NULL,
	[ImageTwo] [nvarchar](max) NULL,
	[CategoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/28/2023 11:04:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 10/28/2023 11:04:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[CategoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/28/2023 11:04:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NULL,
	[password] [nvarchar](500) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Blogs]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[SubCategory] ([Id])
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
USE [master]
GO
ALTER DATABASE [Bandaging] SET  READ_WRITE 
GO
