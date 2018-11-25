CREATE TABLE [dbo].[ExecutedCommands]
(
	[IdExecutedCommand] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [LogCreatedAt] DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(), 
	[ProcessName] NVARCHAR(200) NULL, 
	[ServiceName] NVARCHAR(200) NOT NULL,
	[ControllerName] NVARCHAR(200) NOT NULL,
	[MethodName] NVARCHAR(200) NOT NULL,
	[ClientIpAddress] NVARCHAR(200) NULL,
	[ClientUserAgent] NVARCHAR(2000) NULL,
	[Session] NVARCHAR(500) NULL,
	[RequestedAt] DATETIMEOFFSET NOT NULL,
	[RequestParameters] NVARCHAR(2000) NULL,
	[RequestContentType] NVARCHAR(200) NULL,
	[RequestBody] NVARCHAR(MAX) NULL,
	[RespondedAt] DATETIMEOFFSET NOT NULL,
	[ResponseCode] NVARCHAR(200) NOT NULL,
	[ResponseContentType] NVARCHAR(200) NULL,
	[ResponseData] NVARCHAR(max) NULL,
)

GO