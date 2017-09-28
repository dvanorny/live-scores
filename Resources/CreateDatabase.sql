/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2012 (11.0.3128)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2012
    Target Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Target Database Engine Type : Standalone SQL Server
*/


/****** Object:  Table [dbo].[AuditLog]    Script Date: 9/27/2017 6:42:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AuditLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [varchar](50) NOT NULL,
	[PrimaryKeyId] [int] NOT NULL,
	[FieldName] [varchar](50) NOT NULL,
	[OldValue] [varchar](max) NOT NULL,
	[NewValue] [varchar](max) NOT NULL,
	[ChangeTime] [datetime] NOT NULL,
	[ChangedBy] [varchar](200) NULL,
 CONSTRAINT [PK_AuditLog] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[PageViewLog]    Script Date: 9/27/2017 6:42:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PageViewLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[LogTimestamp] [datetime] NOT NULL,
	[Username] [varchar](255) NOT NULL,
	[PageView] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Roadrunner_PageViewLog] PRIMARY KEY CLUSTERED 
(
	[LogID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Settings]    Script Date: 9/27/2017 6:42:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Settings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SettingValue] [varchar](max) NOT NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_Roadrunner_Settings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[UserBannerProperties]    Script Date: 9/27/2017 6:42:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserBannerProperties](
	[Username] [varchar](100) NOT NULL,
	[BannerText] [varchar](max) NULL,
	[ShowBanner] [bit] NOT NULL,
 CONSTRAINT [PK_UserBannerProperties] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[UserProfileGridColumns]    Script Date: 9/27/2017 6:42:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserProfileGridColumns](
	[Username] [varchar](100) NOT NULL,
	[GridName] [varchar](20) NOT NULL,
	[ActiveColumn] [varchar](50) NOT NULL,
	[SortOrder] [smallint] NULL,
 CONSTRAINT [PK_UserProfileGridColumns] PRIMARY KEY CLUSTERED 
(
	[Username] ASC,
	[GridName] ASC,
	[ActiveColumn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[UserStyles]    Script Date: 9/27/2017 6:42:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserStyles](
	[Username] [varchar](100) NOT NULL,
	[IsSavedStyle] [bit] NOT NULL,
	[BackgroundColor] [varchar](20) NULL,
	[FontFamily] [varchar](50) NULL,
	[TitleColor] [varchar](20) NULL,
	[MainLinksColor] [varchar](20) NULL,
	[TopFrameColor] [varchar](20) NULL,
	[TopFrameBorderColor] [varchar](20) NULL,
	[TableHeaderRowBackground] [varchar](20) NULL,
	[TableRowAlternateBackground] [varchar](20) NULL,
	[TableRowBackground] [varchar](20) NULL,
	[TableCellBorder] [varchar](20) NULL,
	[TablePadding] [smallint] NULL,
	[FontSize] [smallint] NULL,
 CONSTRAINT [PK_UserStyles] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AuditLog] ADD  CONSTRAINT [DF_Table_1_AuditTime]  DEFAULT (getdate()) FOR [ChangeTime]
GO

ALTER TABLE [dbo].[PageViewLog] ADD  CONSTRAINT [DF_Roadrunner_PageViewLog_LogTimestamp]  DEFAULT (getdate()) FOR [LogTimestamp]
GO

ALTER TABLE [dbo].[UserBannerProperties] ADD  CONSTRAINT [DF_UserBannerProperties_BannerDisplay]  DEFAULT ((1)) FOR [ShowBanner]
GO

ALTER TABLE [dbo].[UserStyles] ADD  CONSTRAINT [DF_UserStyles_IsSavedStyle]  DEFAULT ((0)) FOR [IsSavedStyle]
GO


