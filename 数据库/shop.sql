USE [master]
GO
/****** Object:  Database [Shop]    Script Date: 2020/7/6 11:11:55 ******/
CREATE DATABASE [Shop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Shop', FILENAME = N'C:\sq3gen\MSSQL11.LIU\MSSQL\DATA\Shop.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Shop_log', FILENAME = N'C:\sq3gen\MSSQL11.LIU\MSSQL\DATA\Shop_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Shop] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Shop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Shop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Shop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Shop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Shop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Shop] SET ARITHABORT OFF 
GO
ALTER DATABASE [Shop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Shop] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Shop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Shop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Shop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Shop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Shop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Shop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Shop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Shop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Shop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Shop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Shop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Shop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Shop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Shop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Shop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Shop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Shop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Shop] SET  MULTI_USER 
GO
ALTER DATABASE [Shop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Shop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Shop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Shop] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Shop]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2020/7/6 11:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductTitle] [nvarchar](500) NULL,
	[ProductMainImg] [varchar](500) NULL,
	[ProductSlideImgs] [varchar](1000) NULL,
	[ProductDetail] [nvarchar](max) NULL,
	[ProductSku] [varchar](3000) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[ProductCategoryID] [int] NULL,
	[Price] [decimal](18, 2) NULL,
	[Stock] [decimal](18, 2) NULL,
	[ProductCategoryPath] [varchar](100) NULL,
	[ProductSkuValues] [varchar](5000) NULL,
	[ProductBrandID] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductAttr]    Script Date: 2020/7/6 11:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductAttr](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductAttrs] [varchar](2000) NULL,
	[ProductID] [int] NULL,
 CONSTRAINT [PK_ProductAttr] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductAttrKey]    Script Date: 2020/7/6 11:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttrKey](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AttrName] [nvarchar](50) NULL,
	[ProductCategoryID] [int] NULL,
	[IsSku] [int] NULL,
	[EnterType] [int] NULL,
	[OrderNum] [int] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductAttrValue]    Script Date: 2020/7/6 11:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttrValue](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AttrValue] [nvarchar](50) NULL,
	[ProductAttrKeyID] [int] NULL,
 CONSTRAINT [PK_ProductAttrKey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductBrand]    Script Date: 2020/7/6 11:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductBrand](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](50) NULL,
	[LogoInitials] [nvarchar](50) NULL,
	[BrandLogo] [nvarchar](200) NULL,
	[BrandImg] [nvarchar](200) NULL,
	[BrandStory] [nvarchar](500) NULL,
	[OrderNum] [int] NULL,
	[Display] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_ProductBrand] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 2020/7/6 11:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Img] [nvarchar](200) NULL,
	[PID] [int] NULL,
	[OrderNum] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[keyWords] [nvarchar](200) NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductSku]    Script Date: 2020/7/6 11:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductSku](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductSku] [varchar](1000) NULL,
	[Price] [decimal](18, 2) NULL,
	[Stock] [int] NULL,
	[ProductID] [int] NULL,
	[SkuNum] [varchar](50) NULL,
 CONSTRAINT [PK_ProductSku] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ProductCategory] ADD  CONSTRAINT [DF_ProductCategory_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0表示普通属性,1 表示规格(sku)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductAttrKey', @level2type=N'COLUMN',@level2name=N'IsSku'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入方式:0 表示手动录入,1 表示下拉选择' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductAttrKey', @level2type=N'COLUMN',@level2name=N'EnterType'
GO
USE [master]
GO
ALTER DATABASE [Shop] SET  READ_WRITE 
GO
