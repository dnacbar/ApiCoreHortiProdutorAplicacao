﻿USE [DBHORTI]
GO

/****** OBJECT:  TABLE [DBO].[PRODUCER]    SCRIPT DATE: 25/09/2019 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

EXEC P_REMOVE_FK N'PRODUCER'
GO

IF OBJECT_ID('DBO.PRODUCER', 'U') IS NOT NULL
	DROP TABLE DBO.PRODUCER;

CREATE TABLE [DBO].[PRODUCER](
	[ID_PRODUCER] [UNIQUEIDENTIFIER] NOT NULL,
	[DS_NAME] [NVARCHAR](100) NOT NULL,
	[BO_ACTIVE] [BIT] NOT NULL,
	[DT_CREATION] [DATETIME2](3) NOT NULL,
	[DT_ATUALIZATION] [DATETIME2](3) NOT NULL,
	[DS_FANTASYNAME] [VARCHAR](100) NULL,
	[CD_CITY] [INT] NULL,
	[CD_DISTRICT] [UNIQUEIDENTIFIER] NULL,
	[DS_ZIP] [CHAR](8) NULL,
	[DS_ADRESS] [NVARCHAR](50) NULL,
	[DS_NUMBER] [VARCHAR](5) NULL,
	[DS_COMPLEMENT] [VARCHAR](15) NULL,
	[DS_FEDERALINSCRIPTION] [VARCHAR](16) NULL,
	[DS_STATEINSCRIPTION] [VARCHAR](16) NULL,
	[DS_MUNICIPALINSCRIPTION] [VARCHAR](25) NULL,
	[DS_DESCRIPTION] [NVARCHAR](MAX) NULL,
	[DT_BIRTH] [DATE] NULL,
	[DS_EMAIL] [NVARCHAR](50) NOT NULL,
	[DS_PHONE] [VARCHAR](11) NULL,
 CONSTRAINT [PK_PRODUCER] PRIMARY KEY CLUSTERED 
(
	[ID_PRODUCER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [DBO].[PRODUCER] ADD  CONSTRAINT [C_PRODUCER_ACTIVE]  DEFAULT ((1)) FOR [BO_ACTIVE]
GO

ALTER TABLE [DBO].[PRODUCER] ADD  CONSTRAINT [C_PRODUCER_DT_CREATION]  DEFAULT (GETDATE()) FOR [DT_CREATION]
GO

ALTER TABLE [DBO].[PRODUCER] ADD  CONSTRAINT [C_PRODUCER_DT_ATUALIZATION]  DEFAULT (GETDATE()) FOR [DT_ATUALIZATION]
GO

ALTER TABLE [DBO].[PRODUCER]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCER_USERHORTI] FOREIGN KEY([DS_EMAIL])
REFERENCES [DBO].[USERHORTI] ([DS_LOGIN])
GO

ALTER TABLE [DBO].[PRODUCER] CHECK CONSTRAINT [FK_PRODUCER_USERHORTI]
GO

ALTER TABLE [DBO].[PRODUCER]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCER_CITY] FOREIGN KEY([CD_CITY])
REFERENCES [DBO].[CITY] ([ID_CITY])
GO

ALTER TABLE [DBO].[PRODUCER] CHECK CONSTRAINT [FK_PRODUCER_CITY]
GO

ALTER TABLE [DBO].[PRODUCER]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCER_DISTRICT] FOREIGN KEY([CD_DISTRICT])
REFERENCES [DBO].[DISTRICT] ([ID_DISTRICT])
GO

ALTER TABLE [DBO].[PRODUCER] CHECK CONSTRAINT [FK_PRODUCER_DISTRICT]
GO

ALTER TABLE [DBO].[PRODUCER]  WITH CHECK ADD  CONSTRAINT [C_PRODUCER_DS_PHONE] CHECK  ((NOT [DS_PHONE] LIKE '%[^0-9]%') AND (DATALENGTH([DS_PHONE]) = 10 OR DATALENGTH([DS_PHONE]) = 11))
GO

ALTER TABLE [DBO].[PRODUCER] CHECK CONSTRAINT [C_PRODUCER_DS_PHONE]
GO

ALTER TABLE [DBO].[PRODUCER]  WITH CHECK ADD  CONSTRAINT [C_PRODUCER_DS_EMAIL] CHECK  (([DS_EMAIL] LIKE '%@%') AND ([DS_EMAIL] LIKE '%.com%'))
GO

ALTER TABLE [DBO].[PRODUCER] CHECK CONSTRAINT [C_PRODUCER_DS_EMAIL]
GO

ALTER TABLE [DBO].[PRODUCER] WITH CHECK ADD CONSTRAINT [C_PRODUCER_DS_ZIP] CHECK ((NOT [DS_ZIP] LIKE '%[^0-9]%') AND (DATALENGTH([DS_ZIP]) = 8))
GO

ALTER TABLE [DBO].[PRODUCER] CHECK CONSTRAINT [C_PRODUCER_DS_ZIP]
GO

ALTER TABLE [DBO].[PRODUCER] WITH CHECK ADD CONSTRAINT [C_PRODUCER_DT_BIRTH] CHECK (NOT [DT_BIRTH] >= GETDATE())
GO

ALTER TABLE [DBO].[PRODUCER] CHECK CONSTRAINT [C_PRODUCER_DT_BIRTH]
GO

ALTER TABLE [DBO].[PRODUCER]  WITH CHECK ADD  CONSTRAINT [C_PRODUCER_DS_FEDERALINSCRIPTION] CHECK  ((DATALENGTH([DS_FEDERALINSCRIPTION])=(11) OR DATALENGTH([DS_FEDERALINSCRIPTION])=(16)))
GO

ALTER TABLE [DBO].[PRODUCER] CHECK CONSTRAINT [C_PRODUCER_DS_FEDERALINSCRIPTION]
GO