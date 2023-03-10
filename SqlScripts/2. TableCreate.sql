USE [Consulting]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customers](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[Name] [NVARCHAR](70) NOT NULL,
	[PhoneNumber] [NVARCHAR](10) NOT NULL,
	[AddressLineOne] NVARCHAR(35) NULL,
	[AddressLineTwo] NVARCHAR(35) NULL,
	[City] NVARCHAR(35) NULL,
	[State] NVARCHAR(35) NULL,	
	[ZipCode] NVARCHAR(5) NULL,
	[ZipCodeAdditional] NVARCHAR(5) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerOpenHours](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[CustomerId] [INT] NOT NULL,
	[DayOfWeek] [INT] NOT NULL,
	[Opening] [TIME] NOT NULL,
	[Closing] [TIME] NOT NULL,
 CONSTRAINT [PK_CustomerOpenHours] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CustomerOpenHours]  WITH CHECK ADD  CONSTRAINT [FK_CustomerOpenHours_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO

ALTER TABLE [dbo].[CustomerOpenHours] CHECK CONSTRAINT [FK_CustomerOpenHours_Customers]
GO

