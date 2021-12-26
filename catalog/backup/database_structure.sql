USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_data_types]    Script Date: 05/07/2014 13:47:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[100_data_types]') AND type in (N'U'))
DROP TABLE [dbo].[100_data_types]
GO

USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_data_types]    Script Date: 05/07/2014 13:47:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[100_data_types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[data_type] [varchar](20) NOT NULL,
	[table_name] [varchar](100) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[creator] [varchar](20) NOT NULL,
	[parent_table] [varchar](100) NULL,
	[icon] [varbinary](max) NULL,
	[mime_type] [varchar](20) NULL,
	[system_function] [bit] NULL,
	[ckeditor_cs] [text] NULL,
	[ckeditor_en] [text] NULL,
	[ckeditor_de] [text] NULL,
	[ckeditor_ru] [text] NULL,
 CONSTRAINT [PK_100_data_types] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO





USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_dictionary]    Script Date: 05/07/2014 13:47:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[100_dictionary]') AND type in (N'U'))
DROP TABLE [dbo].[100_dictionary]
GO

USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_dictionary]    Script Date: 05/07/2014 13:47:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[100_dictionary](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[icon] [image] NULL,
	[mime_type] [varchar](20) NULL,
	[systemname] [varchar](50) NOT NULL,
	[lang_cs] [varchar](50) NOT NULL,
	[lang_en] [varchar](50) NOT NULL,
	[lang_de] [varchar](50) NULL,
	[lang_ru] [varchar](50) NULL,
 CONSTRAINT [PK_100_dictionary] PRIMARY KEY CLUSTERED 
(
	[systemname] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_icon_panel]    Script Date: 05/07/2014 13:48:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[100_icon_panel]') AND type in (N'U'))
DROP TABLE [dbo].[100_icon_panel]
GO

USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_icon_panel]    Script Date: 05/07/2014 13:48:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[100_icon_panel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[data_type] [varchar](20) NOT NULL,
	[systemname] [varchar](100) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[creator] [varchar](20) NOT NULL,
	[sequence] [int] NOT NULL,
	[parent_data_type] [varchar](20) NULL,
	[icon] [varbinary](max) NULL,
	[mime_type] [varchar](20) NULL,
	[system_function] [bit] NULL,
	[ckeditor_cs] [text] NULL,
	[ckeditor_en] [text] NULL,
	[ckeditor_de] [text] NULL,
	[ckeditor_ru] [text] NULL,
 CONSTRAINT [PK_100_icon_panel] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_login]    Script Date: 05/07/2014 13:48:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[100_login]') AND type in (N'U'))
DROP TABLE [dbo].[100_login]
GO

USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_login]    Script Date: 05/07/2014 13:48:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[100_login](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login_name] [varchar](20) NOT NULL,
	[login_pw] [varbinary](max) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[surname] [varchar](50) NOT NULL,
	[language] [varchar](20) NOT NULL,
	[last_login] [datetime] NOT NULL,
 CONSTRAINT [PK_100_login] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_main_setting]    Script Date: 05/07/2014 13:48:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[100_main_setting]') AND type in (N'U'))
DROP TABLE [dbo].[100_main_setting]
GO

USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_main_setting]    Script Date: 05/07/2014 13:48:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[100_main_setting](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[data_type] [varchar](20) NOT NULL,
	[systemname] [varchar](1024) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[creator] [varchar](20) NOT NULL,
	[sequence] [int] NULL,
	[parent_data_type] [varchar](20) NULL,
	[icon] [varbinary](max) NULL,
	[mime_type] [varchar](20) NULL,
	[system_function] [bit] NULL,
	[ckeditor_cs] [text] NULL,
	[ckeditor_en] [text] NULL,
	[ckeditor_de] [text] NULL,
	[ckeditor_ru] [text] NULL,
 CONSTRAINT [PK_100_main_setting] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_model]    Script Date: 05/07/2014 13:48:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[100_model]') AND type in (N'U'))
DROP TABLE [dbo].[100_model]
GO

USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_model]    Script Date: 05/07/2014 13:48:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[100_model](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[data_type] [varchar](20) NOT NULL,
	[systemname] [varchar](100) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[creator] [varchar](20) NOT NULL,
	[sequence] [int] NULL,
	[parent_data_type] [varchar](20) NULL,
	[icon] [varbinary](max) NULL,
	[mime_type] [varchar](20) NULL,
	[system_function] [bit] NULL,
	[ckeditor_cs] [text] NULL,
	[ckeditor_en] [text] NULL,
	[ckeditor_de] [text] NULL,
	[ckeditor_ru] [text] NULL,
 CONSTRAINT [PK_100_model] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_nomenclature_group]    Script Date: 05/07/2014 13:48:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[100_nomenclature_group]') AND type in (N'U'))
DROP TABLE [dbo].[100_nomenclature_group]
GO

USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_nomenclature_group]    Script Date: 05/07/2014 13:48:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[100_nomenclature_group](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[data_type] [varchar](20) NOT NULL,
	[systemname] [varchar](100) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[creator] [varchar](20) NOT NULL,
	[sequence] [int] NOT NULL,
	[parent_data_type] [varchar](20) NULL,
	[icon] [varbinary](max) NULL,
	[mime_type] [varchar](20) NULL,
	[system_function] [bit] NULL,
	[ckeditor_cs] [text] NULL,
	[ckeditor_en] [text] NULL,
	[ckeditor_de] [text] NULL,
	[ckeditor_ru] [text] NULL,
 CONSTRAINT [PK_100_nomenclature_group] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_pricelist]    Script Date: 05/07/2014 13:49:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[100_pricelist]') AND type in (N'U'))
DROP TABLE [dbo].[100_pricelist]
GO

USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_pricelist]    Script Date: 05/07/2014 13:49:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[100_pricelist](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[data_type] [varchar](20) NOT NULL,
	[systemname] [varchar](100) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[creator] [varchar](20) NOT NULL,
	[sequence] [int] NULL,
	[parent_data_type] [varchar](20) NULL,
	[icon] [varbinary](max) NULL,
	[mime_type] [varchar](20) NULL,
	[system_function] [bit] NULL,
	[ckeditor_cs] [text] NULL,
	[ckeditor_en] [text] NULL,
	[ckeditor_de] [text] NULL,
	[ckeditor_ru] [text] NULL,
 CONSTRAINT [PK_100_pricelist] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_product_group]    Script Date: 05/07/2014 13:49:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[100_product_group]') AND type in (N'U'))
DROP TABLE [dbo].[100_product_group]
GO

USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_product_group]    Script Date: 05/07/2014 13:49:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[100_product_group](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[data_type] [varchar](20) NOT NULL,
	[systemname] [varchar](100) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[creator] [varchar](20) NOT NULL,
	[sequence] [int] NULL,
	[parent_data_type] [varchar](20) NULL,
	[icon] [varbinary](max) NULL,
	[mime_type] [varchar](20) NULL,
	[system_function] [bit] NULL,
	[ckeditor_cs] [text] NULL,
	[ckeditor_en] [text] NULL,
	[ckeditor_de] [text] NULL,
	[ckeditor_ru] [text] NULL,
 CONSTRAINT [PK_100_product_group] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_std_prod_size]    Script Date: 05/07/2014 13:49:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[100_std_prod_size]') AND type in (N'U'))
DROP TABLE [dbo].[100_std_prod_size]
GO

USE [DNBAME]
GO

/****** Object:  Table [dbo].[100_std_prod_size]    Script Date: 05/07/2014 13:49:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[100_std_prod_size](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[data_type] [varchar](20) NOT NULL,
	[systemname] [varchar](100) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[creator] [varchar](20) NOT NULL,
	[sequence] [int] NULL,
	[parent_data_type] [varchar](20) NULL,
	[icon] [varbinary](max) NULL,
	[mime_type] [varchar](20) NULL,
	[system_function] [bit] NULL,
	[ckeditor_cs] [text] NULL,
	[ckeditor_en] [text] NULL,
	[ckeditor_de] [text] NULL,
	[ckeditor_ru] [text] NULL,
 CONSTRAINT [PK_100_std_prod_size] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


