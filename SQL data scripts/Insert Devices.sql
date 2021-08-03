USE Devices



SET IDENTITY_INSERT [dbo].[Devices] ON 

INSERT [dbo].[Devices] ([ID], [DeviceType], [Name], [LocationID], [DeviceCounter], [Timestamp]) VALUES (1, N'DISCONNECTOR', N'DIS1', 1, 1, CAST(N'2021-06-06T23:42:45.2759950' AS DateTime2))
INSERT [dbo].[Devices] ([ID], [DeviceType], [Name], [LocationID], [DeviceCounter], [Timestamp]) VALUES (2, N'FUSE', N'FUS2', 4, 2, CAST(N'2021-06-06T23:42:07.4420726' AS DateTime2))
INSERT [dbo].[Devices] ([ID], [DeviceType], [Name], [LocationID], [DeviceCounter], [Timestamp]) VALUES (4, N'POWER_SWITCH', N'POW4', 7, 4, CAST(N'2021-06-06T23:42:22.7247712' AS DateTime2))
INSERT [dbo].[Devices] ([ID], [DeviceType], [Name], [LocationID], [DeviceCounter], [Timestamp]) VALUES (5, N'FUSE', N'FUS5', 6, 5, CAST(N'2021-06-06T23:42:30.9578908' AS DateTime2))
INSERT [dbo].[Devices] ([ID], [DeviceType], [Name], [LocationID], [DeviceCounter], [Timestamp]) VALUES (6, N'TRANSFORMER', N'TRA6', 4, 6, CAST(N'2021-06-06T23:42:38.4842247' AS DateTime2))
INSERT [dbo].[Devices] ([ID], [DeviceType], [Name], [LocationID], [DeviceCounter], [Timestamp]) VALUES (7, N'DISCONNECTOR', N'DIS7', 6, 7, CAST(N'2021-06-07T11:19:16.2521278' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Devices] OFF
GO