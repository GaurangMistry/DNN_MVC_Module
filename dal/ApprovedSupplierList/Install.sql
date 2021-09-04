/****** Object:  Table [dbo].[MMS_ASLAttachments]    Script Date: 18-Nov-19 10:13:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MMS_ASLAttachments](
	[ASLAttachmentId] [int] IDENTITY(1,1) NOT NULL,
	[ParentASLId] [int] NULL,
	[PortalId] [int] NULL,
	[FileName] [nvarchar](50) NULL,
	[FileBinary] [varbinary](max) NULL,
	[Extension] [nvarchar](50) NULL,
	[MimeType] [nvarchar](50) NULL,
 CONSTRAINT [PK_MMS_ASLAttachment] PRIMARY KEY CLUSTERED 
(
	[ASLAttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MMS_ASLCategories]    Script Date: 18-Nov-19 10:13:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MMS_ASLCategories](
	[ASLCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[PortalId] [int] NULL,
	[CategoryName] [nvarchar](50) NULL,
 CONSTRAINT [PK_MMS_ASLCategory] PRIMARY KEY CLUSTERED 
(
	[ASLCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MMS_ASLReEvaluationAttachments]    Script Date: 18-Nov-19 10:13:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MMS_ASLReEvaluationAttachments](
	[ASLReEvaluationAttachmentId] [int] IDENTITY(1,1) NOT NULL,
	[ParentASLReEvaluationId] [int] NULL,
	[PortalId] [int] NULL,
	[FileName] [nvarchar](50) NULL,
	[FileBinary] [varbinary](max) NULL,
	[Extension] [nvarchar](50) NULL,
	[MimeType] [nvarchar](50) NULL,
 CONSTRAINT [PK_MMS_ASLReEvaluationAttachment] PRIMARY KEY CLUSTERED 
(
	[ASLReEvaluationAttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MMS_ASLReEvaluations]    Script Date: 18-Nov-19 10:13:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MMS_ASLReEvaluations](
	[ASLReEvaluationId] [int] IDENTITY(1,1) NOT NULL,
	[PortalId] [int] NOT NULL,
	[ParentASLId] [int] NOT NULL,
	[EvaluationDate] [datetime] NOT NULL,
	[NextReEvaluationDate] [datetime] NULL,
	[EvaluationCriteria] [nvarchar](500) NULL,
	[InitialEvaluationResultId] [int] NULL,
	[EvaluationBy] [int] NULL,
	[EvaluationComments] [nvarchar](500) NULL,
 CONSTRAINT [PK_MMS_ASLReEvaluation] PRIMARY KEY CLUSTERED 
(
	[ASLReEvaluationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MMS_ASLs]    Script Date: 18-Nov-19 10:13:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MMS_ASLs](
	[ASLId] [int] IDENTITY(1,1) NOT NULL,
	[PortalId] [int] NULL,
	[SupplierName] [nvarchar](200) NULL,
	[ASLCategoryId] [int] NULL,
	[ScopeOfService] [nvarchar](500) NULL,
	[SuppliedCompanyLocations] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[CriticalProductService] [nvarchar](500) NULL,
	[NonCriticalProductService] [nvarchar](500) NULL,
	[MainLocationAddress] [nvarchar](250) NULL,
	[MainLocationCity] [nvarchar](250) NULL,
	[MainLocationState] [nvarchar](250) NULL,
	[MainLocationZip] [nvarchar](250) NULL,
	[MainLocationCountry] [nvarchar](250) NULL,
	[MainLocationCountryName] [nvarchar](250) NULL,
	[MainLocationStateName] [nvarchar](250) NULL,
	[SecondaryLocationAddress] [nvarchar](250) NULL,
	[SecondaryLocationCity] [nvarchar](250) NULL,
	[SecondaryLocationState] [nvarchar](250) NULL,
	[SecondaryLocationZip] [nvarchar](250) NULL,
	[SecondaryLocationCountry] [nvarchar](250) NULL,
	[SecondaryLocationCountryName] [nvarchar](250) NULL,
	[SecondaryLocationStateName] [nvarchar](250) NULL,
	[ContactName] [nvarchar](250) NULL,
	[ContactEmail] [nvarchar](250) NULL,
	[ContactPhone] [nvarchar](250) NULL,
	[InitialEvaluationDate] [datetime] NULL,
	[InitialEvaluationIsGrandfathered] [bit] NULL,
	[InitialEvaluationCriteria] [nvarchar](500) NULL,
	[InitialEvaluationResultId] [int] NULL,
	[InitialEvaluationBy] [int] NULL,
	[InitialEvaluationComments] [nvarchar](500) NULL,
	[ReevaluationInterval] [int] NULL,
 CONSTRAINT [PK_MMS_ASLs] PRIMARY KEY CLUSTERED 
(
	[ASLId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MMS_ASLSecondaryLocations]    Script Date: 18-Nov-19 10:13:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MMS_ASLSecondaryLocations](
	[ASLSecondaryLocationId] [int] IDENTITY(1,1) NOT NULL,
	[PortalId] [int] NULL,
	[ParentASLId] [int] NULL,
	[SecondaryLocationAddress] [nvarchar](250) NULL,
	[SecondaryLocationCity] [nvarchar](250) NULL,
	[SecondaryLocationState] [nvarchar](250) NULL,
	[SecondaryLocationZip] [nvarchar](250) NULL,
	[SecondaryLocationCountry] [nvarchar](250) NULL,
	[SecondaryLocationCountryName] [nvarchar](250) NULL,
	[SecondaryLocationStateName] [nvarchar](250) NULL,
 CONSTRAINT [PK_MMS_ASLSecondaryLocations] PRIMARY KEY CLUSTERED 
(
	[ASLSecondaryLocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MMS_CompanyLocations]    Script Date: 18-Nov-19 10:13:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MMS_CompanyLocations](
	[CompanyLocationId] [int] IDENTITY(1,1) NOT NULL,
	[Location] [nvarchar](100) NOT NULL,
	[PortalId] [int] NULL,
 CONSTRAINT [PK_MMS_CompanyLocation] PRIMARY KEY CLUSTERED 
(
	[CompanyLocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MMS_InitialEvaluationResults]    Script Date: 18-Nov-19 10:13:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MMS_InitialEvaluationResults](
	[InitialEvaluationResultId] [int] IDENTITY(1,1) NOT NULL,
	[PortalId] [int] NULL,
	[InitialEvaluationResultName] [nvarchar](50) NULL,
 CONSTRAINT [PK_MMS_InitialEvaluationResults] PRIMARY KEY CLUSTERED 
(
	[InitialEvaluationResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MMS_ASLAttachments]  WITH CHECK ADD  CONSTRAINT [FK_MMS_ASLAttachments_Portals] FOREIGN KEY([PortalId])
REFERENCES [dbo].[Portals] ([PortalID])
GO
ALTER TABLE [dbo].[MMS_ASLAttachments] CHECK CONSTRAINT [FK_MMS_ASLAttachments_Portals]
GO
ALTER TABLE [dbo].[MMS_ASLReEvaluationAttachments]  WITH CHECK ADD  CONSTRAINT [FK_MMS_ASLReEvaluationAttachments_MMS_ASLReEvaluations] FOREIGN KEY([ParentASLReEvaluationId])
REFERENCES [dbo].[MMS_ASLReEvaluations] ([ASLReEvaluationId])
GO
ALTER TABLE [dbo].[MMS_ASLReEvaluationAttachments] CHECK CONSTRAINT [FK_MMS_ASLReEvaluationAttachments_MMS_ASLReEvaluations]
GO
ALTER TABLE [dbo].[MMS_ASLReEvaluationAttachments]  WITH CHECK ADD  CONSTRAINT [FK_MMS_ASLReEvaluationAttachments_Portals] FOREIGN KEY([PortalId])
REFERENCES [dbo].[Portals] ([PortalID])
GO
ALTER TABLE [dbo].[MMS_ASLReEvaluationAttachments] CHECK CONSTRAINT [FK_MMS_ASLReEvaluationAttachments_Portals]
GO
ALTER TABLE [dbo].[MMS_ASLReEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_MMS_ASLReEvaluation_MMS_ASLs] FOREIGN KEY([ParentASLId])
REFERENCES [dbo].[MMS_ASLs] ([ASLId])
GO
ALTER TABLE [dbo].[MMS_ASLReEvaluations] CHECK CONSTRAINT [FK_MMS_ASLReEvaluation_MMS_ASLs]
GO
ALTER TABLE [dbo].[MMS_ASLs]  WITH CHECK ADD  CONSTRAINT [FK_MMS_ASLs_MMS_ASLCategories] FOREIGN KEY([PortalId])
REFERENCES [dbo].[Portals] ([PortalID])
GO
ALTER TABLE [dbo].[MMS_ASLs] CHECK CONSTRAINT [FK_MMS_ASLs_MMS_ASLCategories]
GO
ALTER TABLE [dbo].[MMS_ASLs]  WITH CHECK ADD  CONSTRAINT [FK_MMS_ASLs_Portals] FOREIGN KEY([PortalId])
REFERENCES [dbo].[Portals] ([PortalID])
GO
ALTER TABLE [dbo].[MMS_ASLs] CHECK CONSTRAINT [FK_MMS_ASLs_Portals]
GO
ALTER TABLE [dbo].[MMS_ASLs]  WITH CHECK ADD  CONSTRAINT [FK_MMS_ASLs_Users] FOREIGN KEY([InitialEvaluationBy])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[MMS_ASLs] CHECK CONSTRAINT [FK_MMS_ASLs_Users]
GO
ALTER TABLE [dbo].[MMS_ASLSecondaryLocations]  WITH CHECK ADD  CONSTRAINT [FK_MMS_ASLSecondaryLocations_MMS_ASLs] FOREIGN KEY([ParentASLId])
REFERENCES [dbo].[MMS_ASLs] ([ASLId])
GO
ALTER TABLE [dbo].[MMS_ASLSecondaryLocations] CHECK CONSTRAINT [FK_MMS_ASLSecondaryLocations_MMS_ASLs]
GO
ALTER TABLE [dbo].[MMS_ASLSecondaryLocations]  WITH CHECK ADD  CONSTRAINT [FK_MMS_ASLSecondaryLocations_Portals] FOREIGN KEY([PortalId])
REFERENCES [dbo].[Portals] ([PortalID])
GO
ALTER TABLE [dbo].[MMS_ASLSecondaryLocations] CHECK CONSTRAINT [FK_MMS_ASLSecondaryLocations_Portals]
GO
ALTER TABLE [dbo].[MMS_CompanyLocations]  WITH CHECK ADD  CONSTRAINT [FK_MMS_CompanyLocations_Portals] FOREIGN KEY([PortalId])
REFERENCES [dbo].[Portals] ([PortalID])
GO
ALTER TABLE [dbo].[MMS_CompanyLocations] CHECK CONSTRAINT [FK_MMS_CompanyLocations_Portals]
GO
ALTER TABLE [dbo].[MMS_InitialEvaluationResults]  WITH CHECK ADD  CONSTRAINT [FK_MMS_InitialEvaluationResults_Portals] FOREIGN KEY([PortalId])
REFERENCES [dbo].[Portals] ([PortalID])
GO
ALTER TABLE [dbo].[MMS_InitialEvaluationResults] CHECK CONSTRAINT [FK_MMS_InitialEvaluationResults_Portals]
GO
