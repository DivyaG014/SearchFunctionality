

Create Database [airline_db]

USE [airline_db]
GO

/****** Object:  Table [dbo].[RegisteredUsers]   ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RegisteredUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[LoginPassword] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO




/****** Object:  Table [dbo].[Flight]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Flight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromAirportId] [int] NULL,
	[ToAirportId] [int] NULL,
	[FlightNumber] [nvarchar](max) NULL,
	[DepartureTime] [datetime] NULL,
	[ArrivalTime] [datetime] NULL,
	[Price] [bigint] NULL,
 CONSTRAINT [flight_id_pi] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [flight_airportidairport_fk] FOREIGN KEY([FromAirportId])
REFERENCES [dbo].[Airport] ([Id])
GO

ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [flight_airportidairport_fk]
GO

ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [flight_toairportidairport_fk] FOREIGN KEY([ToAirportId])
REFERENCES [dbo].[Airport] ([Id])
GO

ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [flight_toairportidairport_fk]
GO


/****** Object:  Table [dbo].[Airport]   ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Airport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AirportName] [nvarchar](max) NULL,
	[Code] [nvarchar](max) NULL,
 CONSTRAINT [airport_id_pi] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO




