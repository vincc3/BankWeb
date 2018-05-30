# BankWeb

Instructions:
1. Load BankWeb.sln by Visual Studio 2017
2. Press F5 to start IIS Express Debugger
3. It will run Index.html (a simple HTML page using jQuery AJAX)
4. The screenshot is /document/capture.png

==========================================

Web API GET method:
http://localhost:49786/api/Customers
http://localhost:49786/api/Customers/{id}

http://localhost:49786/api/Transactions
http://localhost:49786/api/Transactions/{id}


Visual Studio 2017
WEB API 2
Entity Framework 6.x
SQL Server 2017
.NET Framework 4.5


SQL Database [MyDatabase] tables:

CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[AccountNumber] [varchar](50) NOT NULL,
	[AccountName] [varchar](50) NOT NULL,
	[AccountBalance] [varchar](50) NOT NULL,
	[ContactInformation] [varchar](100) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Transaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [decimal](18, 2) NOT NULL,
	[Date] [datetime] NOT NULL,
	[DebitCredit] [varchar](10) NOT NULL,
	[CustomerId] [int] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO

ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Customer]
GO

