USE Locations

SET IDENTITY_INSERT [dbo].[Location] ON 

INSERT [dbo].[Location] ([ID], [Street], [City], [Zip], [MorningPriority], [NoonPriority], [NightPriority], [Latitude], [Longitude], [Number]) VALUES (1, N'Vase Jagazovica', N'Veternik', N'21000', 1, 2, 2, 45.245054739266195, 19.779118277456949, 30)
INSERT [dbo].[Location] ([ID], [Street], [City], [Zip], [MorningPriority], [NoonPriority], [NightPriority], [Latitude], [Longitude], [Number]) VALUES (2, N'Bulevar Evrope', N'Novi Sad', N'21000', 2, 3, 4, 45.24489466200648, 19.819271061114858, 28)
INSERT [dbo].[Location] ([ID], [Street], [City], [Zip], [MorningPriority], [NoonPriority], [NightPriority], [Latitude], [Longitude], [Number]) VALUES (3, N'Bulevar Cara Lazara', N'Novi Sad', N'21000', 3, 1, 3, 45.244324702807631, 19.843701390634706, 19)
INSERT [dbo].[Location] ([ID], [Street], [City], [Zip], [MorningPriority], [NoonPriority], [NightPriority], [Latitude], [Longitude], [Number]) VALUES (4, N'Vojvode Stepe ', N'Indjija', N'22320', 3, 1, 1, 45.048539007282038, 20.081929176984737, 6)
INSERT [dbo].[Location] ([ID], [Street], [City], [Zip], [MorningPriority], [NoonPriority], [NightPriority], [Latitude], [Longitude], [Number]) VALUES (6, N'Nikole Bursaca', N'Indjija', N'22320', 1, 1, 2, 45.040740653738048, 20.094191843793059, 37)
INSERT [dbo].[Location] ([ID], [Street], [City], [Zip], [MorningPriority], [NoonPriority], [NightPriority], [Latitude], [Longitude], [Number]) VALUES (7, N'Maksima Gorkog', N'Beograd', N'11000', 3, 3, 3, 44.797304828084727, 20.477165749765437, 39)
INSERT [dbo].[Location] ([ID], [Street], [City], [Zip], [MorningPriority], [NoonPriority], [NightPriority], [Latitude], [Longitude], [Number]) VALUES (8, N'Mikenska', N'Beograd', N'11000', 3, 4, 3, 44.777006844405669, 20.515251937738249, 24)
SET IDENTITY_INSERT [dbo].[Location] OFF
GO