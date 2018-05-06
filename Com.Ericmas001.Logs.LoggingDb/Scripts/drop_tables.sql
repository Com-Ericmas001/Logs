
/****** Object:  View [dbo].[ViewLogs]    Script Date: 2016-02-28 10:54:06 ******/
DROP VIEW [dbo].[ViewLogs]
GO

ALTER TABLE [dbo].[ExecutedCommands] DROP CONSTRAINT [FK_ExecutedCommands_ServiceMethods]
GO
ALTER TABLE [dbo].[ExecutedCommands] DROP CONSTRAINT [FK_ExecutedCommands_Clients]
GO
/****** Object:  Table [dbo].[ServiceMethods]    Script Date: 2016-02-27 20:36:55 ******/
DROP TABLE [dbo].[ServiceMethods]
GO
/****** Object:  Table [dbo].[SentNotifications]    Script Date: 2016-02-27 20:36:55 ******/
DROP TABLE [dbo].[SentNotifications]
GO
/****** Object:  Table [dbo].[ExecutedCommands]    Script Date: 2016-02-27 20:36:55 ******/
DROP TABLE [dbo].[ExecutedCommands]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 2016-02-27 20:36:55 ******/
DROP TABLE [dbo].[Clients]
GO