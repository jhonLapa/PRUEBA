USE [POSPRUEBA]
GO
/****** Object:  UserDefinedFunction [dbo].[split_string]    Script Date: 05/11/2022 9:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[split_string]
(
    @string_value NVARCHAR(MAX),
    @delimiter_character CHAR(1)
)
RETURNS @result_set TABLE(splited_data NVARCHAR(MAX)
)
BEGIN
    DECLARE @start_position INT,
            @ending_position INT
    SELECT @start_position = 1,
            @ending_position = CHARINDEX(@delimiter_character, @string_value)
    WHILE @start_position < LEN(@string_value) + 1
            BEGIN
        IF @ending_position = 0 
           SET @ending_position = LEN(@string_value) + 1
        INSERT INTO @result_set (splited_data) 
        VALUES(SUBSTRING(@string_value, @start_position, @ending_position - @start_position))
        SET @start_position = @ending_position + 1
        SET @ending_position = CHARINDEX(@delimiter_character, @string_value, @start_position)
    END
    RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 05/11/2022 9:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE function [dbo].[SplitString] 
(
    @str nvarchar(max), 
    @separator char(1)
)
returns table
AS
return (
with tokens(p, a, b) AS (
    select 
        cast(1 as bigint), 
        cast(1 as bigint), 
        charindex(@separator, @str)
    union all
    select
        p + 1, 
        b + 1, 
        charindex(@separator, @str, b + 1)
    from tokens
    where b > 0
)
select
    p-1 ItemIndex,
    substring(
        @str, 
        a, 
        case when b > 0 then b-a ELSE LEN(@str) end) 
    AS s
from tokens
);
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 05/11/2022 9:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[AuditCreateUser] [int] NOT NULL,
	[AuditCreateDate] [datetime2](7) NOT NULL,
	[AuditUpdateUser] [int] NULL,
	[AuditUpdateDate] [datetime2](7) NULL,
	[AuditDeleteUser] [int] NULL,
	[AuditDeleteDate] [datetime2](7) NULL,
	[State] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 05/11/2022 9:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Name] [nvarchar](50) NULL,
	[Stock] [int] NOT NULL,
	[Image] [nvarchar](max) NULL,
	[SellPrice] [decimal](18, 2) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[State] [int] NOT NULL,
	[AuditCreateUser] [int] NULL,
	[AuditCreateDate] [datetime2](7) NULL,
	[AuditUpdateUser] [int] NULL,
	[AuditUpdateDate] [datetime2](7) NULL,
	[AuditDeleteUser] [int] NULL,
	[AuditDeleteDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleDetails]    Script Date: 05/11/2022 9:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDetails](
	[SaleDetailId] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [int] NULL,
	[SaleId] [int] NULL,
	[ProductId] [int] NULL,
	[PriceTotalProduct] [float] NULL,
	[AuditCreateUser] [int] NOT NULL,
	[AuditCreateDate] [datetime2](7) NOT NULL,
	[AuditUpdateUser] [int] NULL,
	[AuditUpdateDate] [datetime2](7) NULL,
	[AuditDeleteUser] [int] NULL,
	[AuditDeleteDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[SaleDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 05/11/2022 9:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[SaleId] [int] IDENTITY(1,1) NOT NULL,
	[Client] [varchar](200) NULL,
	[UserId] [int] NULL,
	[Total] [decimal](18, 2) NULL,
	[State] [int] NULL,
	[AuditCreateUser] [int] NULL,
	[AuditCreateDate] [datetime2](7) NULL,
	[AuditUpdateUser] [int] NULL,
	[AuditUpdateDate] [datetime2](7) NULL,
	[AuditDeleteUser] [int] NULL,
	[AuditDeleteDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[SaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 05/11/2022 9:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](max) NULL,
	[Email] [varchar](max) NULL,
	[Image] [varchar](max) NULL,
	[State] [int] NULL,
	[AuditCreateUser] [int] NOT NULL,
	[AuditCreateDate] [datetime2](7) NOT NULL,
	[AuditUpdateUser] [int] NULL,
	[AuditUpdateDate] [datetime2](7) NULL,
	[AuditDeleteUser] [int] NULL,
	[AuditDeleteDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [Name], [Description], [AuditCreateUser], [AuditCreateDate], [AuditUpdateUser], [AuditUpdateDate], [AuditDeleteUser], [AuditDeleteDate], [State]) VALUES (1, N'polo', N'grande', 1, CAST(N'2022-11-04T12:25:42.1034784' AS DateTime2), NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [Code], [Name], [Stock], [Image], [SellPrice], [CategoryId], [State], [AuditCreateUser], [AuditCreateDate], [AuditUpdateUser], [AuditUpdateDate], [AuditDeleteUser], [AuditDeleteDate]) VALUES (1, N'P001', N'inca', 23, N'', CAST(80.00 AS Decimal(18, 2)), 1, 1, 1, CAST(N'2022-11-04T12:26:53.9458811' AS DateTime2), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[SaleDetails] ON 

INSERT [dbo].[SaleDetails] ([SaleDetailId], [Amount], [SaleId], [ProductId], [PriceTotalProduct], [AuditCreateUser], [AuditCreateDate], [AuditUpdateUser], [AuditUpdateDate], [AuditDeleteUser], [AuditDeleteDate]) VALUES (2, 1, 2, 1, 80, 1, CAST(N'2022-11-04T14:05:32.6533333' AS DateTime2), 2, CAST(N'2022-11-05T09:03:36.1933333' AS DateTime2), NULL, NULL)
INSERT [dbo].[SaleDetails] ([SaleDetailId], [Amount], [SaleId], [ProductId], [PriceTotalProduct], [AuditCreateUser], [AuditCreateDate], [AuditUpdateUser], [AuditUpdateDate], [AuditDeleteUser], [AuditDeleteDate]) VALUES (3, 1, 3, 1, 80, 2, CAST(N'2022-11-05T08:48:35.3900000' AS DateTime2), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[SaleDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([SaleId], [Client], [UserId], [Total], [State], [AuditCreateUser], [AuditCreateDate], [AuditUpdateUser], [AuditUpdateDate], [AuditDeleteUser], [AuditDeleteDate]) VALUES (2, N'PRUEBA', 2, CAST(100.00 AS Decimal(18, 2)), 1, 1, CAST(N'2022-11-04T14:05:32.6333333' AS DateTime2), 2, CAST(N'2022-11-05T09:03:36.1600000' AS DateTime2), NULL, NULL)
INSERT [dbo].[Sales] ([SaleId], [Client], [UserId], [Total], [State], [AuditCreateUser], [AuditCreateDate], [AuditUpdateUser], [AuditUpdateDate], [AuditDeleteUser], [AuditDeleteDate]) VALUES (3, N'PRUEBA', 2, CAST(100.00 AS Decimal(18, 2)), 1, 2, CAST(N'2022-11-05T08:48:35.3500000' AS DateTime2), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Sales] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Email], [Image], [State], [AuditCreateUser], [AuditCreateDate], [AuditUpdateUser], [AuditUpdateDate], [AuditDeleteUser], [AuditDeleteDate]) VALUES (1, N'jhoncotos', N'$2b$10$XSarW73WP.h03APqBSJKoOkRzJ7dPzwV6kHBjPomWjnKVAX8vvKhm', N'cotos.jhon@gmail.com', N'https://pruebaapis123.blob.core.windows.net/users/2cee0bec-fe49-487a-9a0f-feb1381c4c74.jpg', 1, 1, CAST(N'2022-11-04T11:14:10.3562354' AS DateTime2), NULL, NULL, 1, CAST(N'2022-11-04T11:27:05.310' AS DateTime))
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Email], [Image], [State], [AuditCreateUser], [AuditCreateDate], [AuditUpdateUser], [AuditUpdateDate], [AuditDeleteUser], [AuditDeleteDate]) VALUES (2, N'prueba', N'$2b$10$.JaFEcNdbVArnlwn5lIOne72i4HuDy7DTVBUx1JFX8NtXqgtpGmuW', N'prueba', N'Microsoft.AspNetCore.Http.FormFile', 0, 1, CAST(N'2022-11-05T00:38:14.6720965' AS DateTime2), 1, CAST(N'2022-11-05T00:43:35.2200449' AS DateTime2), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[SaleDetails]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[SaleDetails]  WITH CHECK ADD FOREIGN KEY([SaleId])
REFERENCES [dbo].[Sales] ([SaleId])
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarVentaDetalle]    Script Date: 05/11/2022 9:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_ActualizarVentaDetalle](
@saleId	int,	
@Client	varchar(200),	
@UserId	int,	
@Total float,
@ProductId varchar (max)
)
as
begin


UPDATE [dbo].[Sales]
   SET [Client] =  @Client
      ,[UserId] =  @UserId
      ,[Total] =  @Total
      ,[AuditUpdateUser] = @UserId
     , [AuditUpdateDate] = getdate()
   WHERE SaleId = @saleId

	create table #temp (
		id int,
		value varchar(50)
	)

	create table #temp2 (
		id int,
		value varchar(50)
	)

	insert into #temp select * from  SplitString(@ProductId,',')

	select * from #temp
	declare @id int 

	while(select COUNT(*) from #temp ) >0 
		begin
			select top 1 @id=id  from #temp
			insert into #temp2 select * from SplitString((select value from #temp where id =@id),'-')
			select * from #temp2
			declare @id2 int 

			while(select COUNT(*) from #temp2 ) >0 
				begin
					---select top 1 @id2=id  from #temp2
					select @id2=id  from #temp2 where id=0
 
					select * from Products where ProductId=(select value from #temp2 where id=@id2)
					DECLARE @PriceTotalProduct As float = (select SellPrice * (select value from #temp2 where id=1) from Products where ProductId = (select value from #temp2 where id=@id2))					
				UPDATE [dbo].[SaleDetails]
				   SET [Amount] =  (select value from #temp2 where id=1)
					  ,[ProductId] = (select value from #temp2 where id=@id2)
					  ,[PriceTotalProduct] =   @PriceTotalProduct
					  ,[AuditUpdateUser] = @UserId
					  ,[AuditUpdateDate] = getdate()
				   WHERE SaleId = @saleId
					--select * from #temp2 where id=@id2  

					delete #temp2 
				end

			truncate table #temp2
			--select * from #temp where id=@id  
			--select * from Product where ProductId=@id
			delete #temp where id=@id
		end


	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_CrearVentaDetalle]    Script Date: 05/11/2022 9:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CrearVentaDetalle](
@Client	varchar(200),	
@UserId	int,	
@Total float,
@ProductId varchar (max)
)
as
begin

INSERT INTO [dbo].[Sales]
           ([Client]
           ,[UserId]
           ,[Total]
           ,[State]
           ,[AuditCreateUser]
           ,[AuditCreateDate] ) 
     VALUES
           (@Client
           ,@UserId
           ,@Total
           ,1
           ,@UserId
           ,getdate())



	DECLARE @SaleId int = SCOPE_IDENTITY() 
	if @SaleId > 0
     begin

	create table #temp (
		id int,
		value varchar(50)
	)

	create table #temp2 (
		id int,
		value varchar(50)
	)

	insert into #temp select * from  SplitString(@ProductId,',')

	select * from #temp
	declare @id int 

	while(select COUNT(*) from #temp ) >0 
		begin
			select top 1 @id=id  from #temp
			insert into #temp2 select * from SplitString((select value from #temp where id =@id),'-')
			select * from #temp2
			declare @id2 int 

			while(select COUNT(*) from #temp2 ) >0 
				begin
					---select top 1 @id2=id  from #temp2
					select @id2=id  from #temp2 where id=0
 
					select * from Products where ProductId=(select value from #temp2 where id=@id2)
					DECLARE @PriceTotalProduct As float = (select SellPrice * (select value from #temp2 where id=1) from Products where ProductId = (select value from #temp2 where id=@id2))
					insert into  SaleDetails(Amount, PriceTotalProduct ,SaleId,ProductId,[AuditCreateUser],[AuditCreateDate]) 
					values ((select value from #temp2 where id=1),@PriceTotalProduct, @SaleId,(select value from #temp2 where id=@id2) ,@UserId,getdate() ) 
				
					--select * from #temp2 where id=@id2  

					delete #temp2 
				end

			truncate table #temp2
			--select * from #temp where id=@id  
			--select * from Product where ProductId=@id
			delete #temp where id=@id
		end

	end
	else
	begin
	select  'no  se realizo  '  as mensaje ;
	end 
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarVentaDetalle]    Script Date: 05/11/2022 9:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_EliminarVentaDetalle](
@saleId	int,
@ProductId varchar (max)
)
as


begin

UPDATE Sales  SET State= 0
 WHERE saleId = @saleId

create table #temp (
id int,
value varchar(50)
)
create table #temp2 (
id int,
value varchar(50)
)
insert into #temp select * from  SplitString(@ProductId,',')
select * from #temp
declare @id int 
while(select COUNT(*) from #temp ) >0 
begin
select top 1 @id=id  from #temp
insert into #temp2 select * from SplitString((select value from #temp where id =@id),'-')
--select * from #temp2
declare @id2 int 
while(select COUNT(*) from #temp2 ) >0 
begin
select @id2=id  from #temp2 where id=0
select * from Products where ProductId=(select value from #temp2 where id=@id2)

update Products set Stock = Stock+(select value from #temp2 where id=1)   where ProductId = (select value from #temp2 where id=@id2) 
delete #temp2 
end
truncate table #temp2
delete #temp where id=@id
end
end

GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarVentaDetalleTotal]    Script Date: 05/11/2022 9:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_EliminarVentaDetalleTotal](
@saleId	int,
@ProductId varchar (max)
)
as


begin
DELETE FROM [dbo].[Sales]
      WHERE saleId = @saleId


create table #temp (
id int,
value varchar(50)
)
create table #temp2 (
id int,
value varchar(50)
)
insert into #temp select * from  SplitString(@ProductId,',')
select * from #temp
declare @id int 
while(select COUNT(*) from #temp ) >0 
begin
select top 1 @id=id  from #temp
insert into #temp2 select * from SplitString((select value from #temp where id =@id),'-')
--select * from #temp2
declare @id2 int 
while(select COUNT(*) from #temp2 ) >0 
begin
select @id2=id  from #temp2 where id=0
select * from Products where ProductId=(select value from #temp2 where id=@id2)
DECLARE @SaleDetalleId As float = (select SaleDetailId from SaleDetails where ProductId = (select value from #temp2 where id=@id2) and SaleId = @saleId)


update Products set Stock = Stock+(select value from #temp2 where id=1)   where ProductId = (select value from #temp2 where id=@id2) 

DELETE FROM [dbo].SaleDetails
      WHERE SaleDetailId = @SaleDetalleId

delete #temp2 


end
truncate table #temp2
delete #temp where id=@id
end
end

GO
/****** Object:  StoredProcedure [dbo].[sp_ListaComprasUsers]    Script Date: 05/11/2022 9:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_ListaComprasUsers]
@UserId int
as
begin
select 
*
from 
Sales 
where UserId = @UserId and State = 1

end
GO
