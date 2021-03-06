USE [master]
GO
/****** Object:  Database [HotelLandLyst]    Script Date: 11-12-2020 08:41:23 ******/
CREATE DATABASE [HotelLandLyst]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HotelLandLyst', FILENAME = N'C:\Users\Kenn5073\HotelLandLyst.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HotelLandLyst_log', FILENAME = N'C:\Users\Kenn5073\HotelLandLyst_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [HotelLandLyst] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HotelLandLyst].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HotelLandLyst] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HotelLandLyst] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HotelLandLyst] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HotelLandLyst] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HotelLandLyst] SET ARITHABORT OFF 
GO
ALTER DATABASE [HotelLandLyst] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [HotelLandLyst] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HotelLandLyst] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HotelLandLyst] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HotelLandLyst] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HotelLandLyst] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HotelLandLyst] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HotelLandLyst] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HotelLandLyst] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HotelLandLyst] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HotelLandLyst] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HotelLandLyst] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HotelLandLyst] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HotelLandLyst] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HotelLandLyst] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HotelLandLyst] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HotelLandLyst] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HotelLandLyst] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HotelLandLyst] SET  MULTI_USER 
GO
ALTER DATABASE [HotelLandLyst] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HotelLandLyst] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HotelLandLyst] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HotelLandLyst] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HotelLandLyst] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HotelLandLyst] SET QUERY_STORE = OFF
GO
USE [HotelLandLyst]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [HotelLandLyst]
GO
/****** Object:  User [Cleaning-HL]    Script Date: 11-12-2020 08:41:23 ******/
CREATE USER [Cleaning-HL] FOR LOGIN [Cleaning-HL] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RoomNumber] [int] IDENTITY(100,1) NOT NULL,
	[DayPrice] [decimal](18, 2) NOT NULL,
	[RoomStatusValue] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CleaningView]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CleaningView]
AS
SELECT        RoomNumber, RoomStatusValue
FROM            dbo.Room
WHERE        (RoomStatusValue = 'Dirty')
GO
/****** Object:  Table [dbo].[City]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[CityAreaCode] [int] NOT NULL,
	[AreaName] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[CityAreaCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[customerPhone] [varchar](20) NOT NULL,
	[customerFName] [varchar](100) NOT NULL,
	[customerLName] [varchar](100) NOT NULL,
	[customerAddress] [varchar](500) NULL,
	[CityAreaCode] [int] NOT NULL,
	[customerEmail] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[customerPhone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FinishedReservationLog]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinishedReservationLog](
	[DeletionDate] [datetime] NOT NULL,
	[RoomNumber] [int] NOT NULL,
	[customerPhone] [varchar](20) NOT NULL,
	[ReservationPrice] [decimal](18, 2) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DeletionDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[ReservationId] [int] IDENTITY(1000,1) NOT NULL,
	[RoomNumber] [int] NOT NULL,
	[customerPhone] [varchar](20) NOT NULL,
	[ReservationPrice] [decimal](18, 2) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomAccessory]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomAccessory](
	[RaId] [int] IDENTITY(1,1) NOT NULL,
	[AccName] [varchar](200) NOT NULL,
	[ExtraCharge] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomAccessoryList]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomAccessoryList](
	[RoomNumber] [int] NOT NULL,
	[RAId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomNumber] ASC,
	[RAId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomStatus]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomStatus](
	[RoomStatusValue] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomStatusValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([CityAreaCode])
REFERENCES [dbo].[City] ([CityAreaCode])
GO
ALTER TABLE [dbo].[FinishedReservationLog]  WITH CHECK ADD FOREIGN KEY([customerPhone])
REFERENCES [dbo].[Customer] ([customerPhone])
GO
ALTER TABLE [dbo].[FinishedReservationLog]  WITH CHECK ADD FOREIGN KEY([RoomNumber])
REFERENCES [dbo].[Room] ([RoomNumber])
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD FOREIGN KEY([customerPhone])
REFERENCES [dbo].[Customer] ([customerPhone])
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD FOREIGN KEY([RoomNumber])
REFERENCES [dbo].[Room] ([RoomNumber])
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD FOREIGN KEY([RoomStatusValue])
REFERENCES [dbo].[RoomStatus] ([RoomStatusValue])
GO
ALTER TABLE [dbo].[RoomAccessoryList]  WITH CHECK ADD FOREIGN KEY([RoomNumber])
REFERENCES [dbo].[Room] ([RoomNumber])
GO
ALTER TABLE [dbo].[RoomAccessoryList]  WITH CHECK ADD FOREIGN KEY([RAId])
REFERENCES [dbo].[RoomAccessory] ([RaId])
GO
/****** Object:  StoredProcedure [dbo].[CheckForOldReservations]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[CheckForOldReservations]
as
delete from Reservation
where endDate < GetDate()
GO
/****** Object:  StoredProcedure [dbo].[GetAllRooms]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetAllRooms]
as
select * from Room
GO
/****** Object:  StoredProcedure [dbo].[GetCustomer]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetCustomer] @customerPhone varchar(20)
as
select * FROM Customer WHERE customerPhone=@customerPhone
GO
/****** Object:  StoredProcedure [dbo].[GetReservationDates]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetReservationDates] @Id int
as
select StartDate, EndDate
from Reservation
where RoomNumber = @Id
GO
/****** Object:  StoredProcedure [dbo].[GetRoomAccessories]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetRoomAccessories] 
as

select Room.RoomNumber, AccName, ExtraCharge
from Room
left join RoomAccessoryList on RoomAccessoryList.RoomNumber = Room.RoomNumber
left join RoomAccessory on RoomAccessory.RaId = RoomAccessoryList.RAId
GO
/****** Object:  StoredProcedure [dbo].[GetRoomAccessoriesById]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[GetRoomAccessoriesById] @Id int
as
select RoomAccessory.AccName, RoomAccessory.ExtraCharge
from RoomAccessory
inner join RoomAccessoryList
on 
RoomAccessory.RaId = RoomAccessoryList.RAId
inner join Room
on Room.RoomNumber = RoomAccessoryList.RoomNumber
where Room.RoomNumber = @Id;
GO
/****** Object:  StoredProcedure [dbo].[GetRoomById]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetRoomById] @id int
as

select * 
from Room
where RoomNumber = @id
GO
/****** Object:  StoredProcedure [dbo].[GetRoomTotalPrice]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetRoomTotalPrice] 
	@RoomId int
	AS
	begin
		declare @TotalPrice int
		select @TotalPrice = Sum(DayPrice) + Sum(ExtraCharge)
		from Room
		inner join RoomAccessoryList
		on RoomAccessoryList.RoomNumber = Room.RoomNumber
		inner join RoomAccessory
		on RoomAccessory.RaId = RoomAccessoryList.RAId
		where Room.RoomNumber = @RoomId
		return @TotalPrice
	end
GO
/****** Object:  StoredProcedure [dbo].[MakeCustomer]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[MakeCustomer] @customerFName varchar(100), @customerLName varchar(100), 
@customerAddress varchar(500), @customerPhone varchar(20), 
@customerEmail varchar(200), @customerAreaCode int

AS

insert into Customer(customerFName, customerLName, customerAddress, CityAreaCode, customerEmail, customerPhone)
values
(@customerFName, @customerLName, @customerAddress, @customerAreaCode, @customerEmail, @customerPhone)
GO
/****** Object:  StoredProcedure [dbo].[MakeReservation]    Script Date: 11-12-2020 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[MakeReservation]
@RoomNumber int, @customerPhone varchar(20), @StartDate DateTime, 
@EndDate DateTime
as

declare @dayAmount int = (Select DATEDIFF(day, @StartDate, @EndDate));
declare @TotalAmount int;

exec @TotalAmount = GetRoomTotalPrice @RoomNumber;

insert into Reservation(customerPhone, RoomNumber, StartDate, EndDate, ReservationPrice)
values(
	@customerPhone,
	@RoomNumber, 
	@StartDate,
	@EndDate, 
	case when @dayAmount >= 7 then @TotalAmount * 0.9
	else @TotalAmount
	end	
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Room"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 2295
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 990
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CleaningView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CleaningView'
GO
USE [master]
GO
ALTER DATABASE [HotelLandLyst] SET  READ_WRITE 
GO
