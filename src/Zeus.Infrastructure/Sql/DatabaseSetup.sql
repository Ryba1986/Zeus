-- V1
IF((SELECT COUNT(*) FROM [INFORMATION_SCHEMA].[TABLES] WHERE TABLE_NAME = '_DB') = 0)

BEGIN TRY
   BEGIN TRANSACTION;

   CREATE TABLE [_DB]
   (
      [Id] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
      [Version] INT UNIQUE NOT NULL,
      [Date] DATETIME2 NOT NULL,
   );

   INSERT INTO [_DB] ([Version], [Date])
   VALUES (1, CAST(GETDATE() AS datetime2(0)));

   CREATE TABLE [User]
   (
      [Id] INT IDENTITY(1, 1) NOT NULL,
      [Name] NVARCHAR(50) NOT NULL,
      [Email] NVARCHAR(50) NOT NULL,
      [Password] NCHAR(64) NOT NULL,
      [Role] TINYINT NOT NULL,
      [IsActive] BIT NOT NULL,
      [Version] ROWVERSION NOT NULL,
      CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED ([Id]),
      CONSTRAINT [UK_User_Name] UNIQUE ([Name]),
      CONSTRAINT [UK_User_Email] UNIQUE ([Email]),
   );
   -- // TODO: move to other logic
   INSERT INTO [User] ([Name], [Email], [Password], [Role], [IsActive])
   VALUES (N'Administrator', N'admin@admin.com', N'E7D3E769F3F593DADCB8634CC5B09FC90DD3A61C4A06A79CB0923662FE6FAE6B', 255, 1);

   CREATE TABLE [UserHistory]
   (
      [Id] INT IDENTITY(1, 1) NOT NULL,
      [UserId] INT NOT NULL,
      [Name] NVARCHAR(50) NOT NULL,
      [Email] NVARCHAR(50) NOT NULL,
      [Role] NVARCHAR(30) NOT NULL,
      [IsActive] BIT NOT NULL,
      [CreatedById] INT NOT NULL,
      [CreateDate] DATETIME2 NOT NULL,
      CONSTRAINT [PK_UserHistory_Id] PRIMARY KEY CLUSTERED ([Id]),
      CONSTRAINT [FK_UserHistory_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
      CONSTRAINT [FK_UserHistory_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [User]([Id]),
      INDEX [IX_UserHistory_UserId] NONCLUSTERED ([UserId]),
   );

   CREATE TABLE [Location]
   (
      [Id] INT IDENTITY(1, 1) NOT NULL,
      [Name] NVARCHAR(30) NOT NULL,
      [MacAddress] NCHAR(12) NOT NULL,
      [Hostname] NCHAR(62) NOT NULL,
      [ClientVersion] NVARCHAR(20) NOT NULL,
      [IncludeReport] BIT NOT NULL,
      [IsActive] BIT NOT NULL,
      [Version] ROWVERSION NOT NULL,
      CONSTRAINT [PK_Location_Id] PRIMARY KEY CLUSTERED ([Id]),
      CONSTRAINT [UK_Location_Name] UNIQUE ([Name]),
      CONSTRAINT [UK_Location_MacAddress] UNIQUE ([MacAddress]),
   );

   CREATE TABLE [LocationHistory]
   (
      [Id] INT IDENTITY(1, 1) NOT NULL,
      [LocationId] INT NOT NULL,
      [Name] NVARCHAR(30) NOT NULL,
      [MacAddress] NCHAR(12) NOT NULL,
      [IncludeReport] BIT NOT NULL,
      [IsActive] BIT NOT NULL,
      [CreatedById] INT NOT NULL,
      [CreateDate] DATETIME2 NOT NULL,
      CONSTRAINT [PK_LocationHistory_Id] PRIMARY KEY CLUSTERED ([Id]),
      CONSTRAINT [FK_LocationHistory_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Location]([Id]),
      CONSTRAINT [FK_LocationHistory_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [User]([Id]),
      INDEX [IX_LocationHistory_LocationId] NONCLUSTERED ([LocationId]),
   );

CREATE TABLE [Device]
   (
      [Id] INT IDENTITY(1, 1) NOT NULL,
      [LocationId] INT NOT NULL,
      [Name] NVARCHAR(30) NOT NULL,
      [SerialNumber] NVARCHAR(30) NOT NULL,
      [Type] SMALLINT NOT NULL,
      [ModbusId] TINYINT NOT NULL,
      [RsBoundRate] SMALLINT NOT NULL,
      [RsDataBits] TINYINT NOT NULL,
      [RsParity] TINYINT NOT NULL,
      [RsStopBits] TINYINT NOT NULL,
      [IncludeReport] BIT NOT NULL,
      [IsActive] BIT NOT NULL,
      [Version] ROWVERSION NOT NULL,
      CONSTRAINT [PK_Device_Id] PRIMARY KEY CLUSTERED ([Id]),
      CONSTRAINT [FK_Device_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Location]([Id]),
      CONSTRAINT [UK_Device_LocationId_Name] UNIQUE ([LocationId], [Name]),
      CONSTRAINT [UK_Device_LocationId_ModbusId] UNIQUE ([LocationId], [ModbusId]),
   );

   CREATE TABLE [DeviceHistory]
   (
      [Id] INT IDENTITY(1, 1) NOT NULL,
      [DeviceId] INT NOT NULL,
      [Name] NVARCHAR(30) NOT NULL,
      [LocationName] NVARCHAR(30) NOT NULL,
      [Type] NVARCHAR(30) NOT NULL,
      [ModbusId] NVARCHAR(3) NOT NULL,
      [RsBoundRate] NVARCHAR(10) NOT NULL,
      [RsDataBits] NVARCHAR(5) NOT NULL,
      [RsParity] NVARCHAR(10) NOT NULL,
      [RsStopBits] NVARCHAR(10) NOT NULL,
      [IncludeReport] BIT NOT NULL,
      [IsActive] BIT NOT NULL,
      [CreatedById] INT NOT NULL,
      [CreateDate] DATETIME2 NOT NULL,
      CONSTRAINT [PK_DeviceHistory_Id] PRIMARY KEY CLUSTERED ([Id]),
      CONSTRAINT [FK_DeviceHistory_DeviceId] FOREIGN KEY ([DeviceId]) REFERENCES [Device]([Id]),
      CONSTRAINT [FK_DeviceHistory_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [User]([Id]),
      INDEX [IX_DeviceHistory_DeviceId] NONCLUSTERED ([DeviceId]),
   );

   CREATE TABLE [Meter]
   (
      [Date] DATETIME2 NOT NULL,
      [DeviceId] INT NOT NULL,
      [InletTemp] REAL NOT NULL,
      [OutletTemp] REAL NOT NULL,
      [Power] REAL NOT NULL,
      [Volume] REAL NOT NULL,
      [VolumeSummary] INT NOT NULL,
      [EnergySummary] INT NOT NULL,
      [HourCount] INT NOT NULL,
      [ErrorCode] SMALLINT NOT NULL,
      CONSTRAINT [PK_Meter_Date_DeviceId] PRIMARY KEY CLUSTERED ([Date], [DeviceId]),
      CONSTRAINT [FK_Meter_DeviceId] FOREIGN KEY ([DeviceId]) REFERENCES [Device]([Id]),
   );

  CREATE TABLE [Rvd145]
   (
      [Date] DATETIME2 NOT NULL,
      [DeviceId] INT NOT NULL,
      [OutsideTemp] REAL NOT NULL,
      [CoHighInletPresure] REAL NOT NULL,
      [Alarm] SMALLINT NOT NULL,
      [CoHighOutletTemp] REAL NOT NULL,
      [CoLowInletTemp] REAL NOT NULL,
      [CoLowOutletPresure] REAL NOT NULL,
      [CoHeatCurveTemp] REAL NOT NULL,
      [CoPumpStatus] BIT NOT NULL,
      [CoStatus] BIT NOT NULL,
      [CwuTemp] REAL NOT NULL,
      [CwuTempSet] REAL NOT NULL,
      [CwuCirculationTemp] REAL NOT NULL,
      [CwuPumpStatus] BIT NOT NULL,
      [CwuStatus] BIT NOT NULL,
      CONSTRAINT [PK_Rvd145_Date_DeviceId] PRIMARY KEY CLUSTERED ([Date], [DeviceId]),
      CONSTRAINT [FK_Rvd145_DeviceId] FOREIGN KEY ([DeviceId]) REFERENCES [Device]([Id]),
   );

   COMMIT TRANSACTION;
END TRY
BEGIN CATCH
   ROLLBACK TRANSACTION;
END CATCH;   
