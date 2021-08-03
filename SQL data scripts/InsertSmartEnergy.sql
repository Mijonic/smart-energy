USE [SmartEnergy]
GO
SET IDENTITY_INSERT [dbo].[Crews] ON 

INSERT [dbo].[Crews] ([ID], [CrewName]) VALUES (2, N'Ekipa2')
INSERT [dbo].[Crews] ([ID], [CrewName]) VALUES (3, N'Ekipica')
SET IDENTITY_INSERT [dbo].[Crews] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (1, N'nidza98', N'nekibezvezni@gmail.com', N'Nikola', N'Mijonic', N'$2a$04$16Pf3sY7eXgeVZGoRmRbAOe4RsIxut.XCwT9vbIjtQJrcQShytfRW', CAST(N'1998-06-03T00:00:00.0000000' AS DateTime2), N'ADMIN', NULL, NULL, 2, N'APPROVED')
INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (4, N'sveto98', N'svetoizvorac@yahoo.com', N'Svetozar', N'Izvoric', N'$2a$04$16Pf3sY7eXgeVZGoRmRbAOe4RsIxut.XCwT9vbIjtQJrcQShytfRW', CAST(N'1998-05-03T00:00:00.0000000' AS DateTime2), N'ADMIN', NULL, NULL, 7, N'APPROVED')
INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (5, N'stele98', N'vuci@gmail.com', N'Stefan', N'Besovic', N'$2a$11$L.fb./NAUzUTNLGFJiv8quleGSjDb.30RCG2BKYjxp6GNtGIT5/ji', CAST(N'1999-06-08T00:00:00.0000000' AS DateTime2), N'DISPATCHER', NULL, NULL, 4, N'APPROVED')
INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (6, N'rokajlo', N'roki98@gmail.com', N'Vladimir', N'Rodusek', N'$2a$11$1rVTVYnxDjlBnX.sbgQA0eSAwJOHT0tCQumkXtkGWVfuGYmGXWhKy', CAST(N'1993-05-03T00:00:00.0000000' AS DateTime2), N'WORKER', NULL, NULL, 1, N'DENIED')
INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (7, N'badrlja', N'badrlja@gmail.com', N'DRagisa', N'Badrljica', N'$2a$11$Mx/mrBwRgNWCDRvATfWxu.4gv4lTye19IrmVavhUveQhBRVFc90wi', CAST(N'1996-08-09T00:00:00.0000000' AS DateTime2), N'CONSUMER', NULL, NULL, 2, N'APPROVED')
INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (8, N'p.glavas', N'p.glavas@yandex.com', N'Предраг', N'Главаш', N'2857725861144145', CAST(N'2021-06-06T23:19:34.9514760' AS DateTime2), N'CONSUMER', NULL, NULL, 1, N'APPROVED')
INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (10, N'crew1', N'ekipar@hotmail.com', N'Ekipar', N'Prvi', N'$2a$04$16Pf3sY7eXgeVZGoRmRbAOe4RsIxut.XCwT9vbIjtQJrcQShytfRW', CAST(N'1996-05-03T00:00:00.0000000' AS DateTime2), N'CREW_MEMBER', NULL, NULL, 3, N'APPROVED')
INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (14, N'crew2', N'ekipar2@gmaul.com', N'Ekipar', N'Drugi', N'$2a$04$16Pf3sY7eXgeVZGoRmRbAOe4RsIxut.XCwT9vbIjtQJrcQShytfRW', CAST(N'1970-02-11T00:00:00.0000000' AS DateTime2), N'CREW_MEMBER', NULL, 2, 6, N'APPROVED')
INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (15, N'CRE4', N'ekipar4@hotmail.com', N'Ekipar', N'Cetvrti', N'$2a$04$16Pf3sY7eXgeVZGoRmRbAOe4RsIxut.XCwT9vbIjtQJrcQShytfRW', CAST(N'1987-03-24T00:00:00.0000000' AS DateTime2), N'CREW_MEMBER', NULL, 3, 2, N'APPROVED')
INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (16, N'crew3', N'ekipa@gmail.com', N'EKIPAR', N'PETI', N'$2a$04$16Pf3sY7eXgeVZGoRmRbAOe4RsIxut.XCwT9vbIjtQJrcQShytfRW', CAST(N'2001-01-31T00:00:00.0000000' AS DateTime2), N'CREW_MEMBER', NULL, 3, 3, N'APPROVED')
INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (17, N'smart.energy.pusgs', N'smart.energy.pusgs@gmail.com', N'PUSGS', N'2021', N'55f458b6726fde658ad7b01b3112cdca67d7e2cd', CAST(N'2021-06-07T00:09:35.4654987' AS DateTime2), N'DISPATCHER', NULL, NULL, 1, N'APPROVED')
INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (20, N'KONZUMENT', N'nikolamijonic@gmail.com', N'Kunz', N'Umnet', N'$2a$04$16Pf3sY7eXgeVZGoRmRbAOe4RsIxut.XCwT9vbIjtQJrcQShytfRW', CAST(N'1990-08-05T00:00:00.0000000' AS DateTime2), N'CONSUMER', NULL, NULL, 4, N'APPROVED')
INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (21, N'pedjaglavas98', N'pedjaglavas98@gmail.com', N'Predrag', N'Glavas', N'b5b9351f0c85c299eb5d90f869ec3c50480ebac1', CAST(N'2021-06-07T21:01:11.3258879' AS DateTime2), N'CONSUMER', NULL, NULL, 1, N'PENDING')
INSERT [dbo].[Users] ([ID], [Username], [Email], [Name], [Lastname], [Password], [BirthDay], [UserType], [ImageURL], [CrewID], [LocationID], [UserStatus]) VALUES (22, N'petrovicp537', N'petrovicp537@gmail.com', N'Petar', N'Petrovic', N'2c98eb2bc4bd7933441210838e48cb2a95c89e3c', CAST(N'2021-08-02T15:33:32.0872352' AS DateTime2), N'DISPATCHER', NULL, NULL, 1, N'APPROVED')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[MultimediaAnchors] ON 

INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (1)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (2)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (3)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (4)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (5)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (6)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (7)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (8)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (9)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (10)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (11)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (12)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (13)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (14)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (15)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (16)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (17)
INSERT [dbo].[MultimediaAnchors] ([ID]) VALUES (18)
SET IDENTITY_INSERT [dbo].[MultimediaAnchors] OFF
GO
SET IDENTITY_INSERT [dbo].[NotificationAnchors] ON 

INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (1)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (2)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (3)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (4)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (5)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (6)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (7)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (8)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (9)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (10)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (11)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (12)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (13)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (14)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (15)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (16)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (17)
INSERT [dbo].[NotificationAnchors] ([ID]) VALUES (18)
SET IDENTITY_INSERT [dbo].[NotificationAnchors] OFF
GO
SET IDENTITY_INSERT [dbo].[Incidents] ON 

INSERT [dbo].[Incidents] ([ID], [Priority], [Confirmed], [ETA], [ATA], [ETR], [IncidentDateTime], [WorkBeginDate], [VoltageLevel], [Description], [UserID], [CrewID], [MultimediaAnchorID], [NotificationAnchorID], [WorkType], [IncidentStatus], [Timestamp]) VALUES (1, 1, 0, CAST(N'2021-06-15T10:29:00.0000000' AS DateTime2), CAST(N'2021-06-17T11:06:00.0000000' AS DateTime2), NULL, CAST(N'2021-06-01T19:26:00.0000000' AS DateTime2), CAST(N'2021-06-08T10:32:00.0000000' AS DateTime2), 220, N'', 14, NULL, 1, 1, N'PLANNED', N'UNRESOLVED', CAST(N'2021-06-06T23:28:30.2937366' AS DateTime2))
INSERT [dbo].[Incidents] ([ID], [Priority], [Confirmed], [ETA], [ATA], [ETR], [IncidentDateTime], [WorkBeginDate], [VoltageLevel], [Description], [UserID], [CrewID], [MultimediaAnchorID], [NotificationAnchorID], [WorkType], [IncidentStatus], [Timestamp]) VALUES (2, 3, 1, CAST(N'2021-07-29T11:33:00.0000000' AS DateTime2), CAST(N'2021-08-28T11:34:00.0000000' AS DateTime2), NULL, CAST(N'2021-06-15T12:34:00.0000000' AS DateTime2), CAST(N'2021-06-28T01:34:00.0000000' AS DateTime2), 380, N'', 5, NULL, 2, 2, N'UNPLANNED', N'UNRESOLVED', CAST(N'2021-06-06T23:29:45.6040200' AS DateTime2))
INSERT [dbo].[Incidents] ([ID], [Priority], [Confirmed], [ETA], [ATA], [ETR], [IncidentDateTime], [WorkBeginDate], [VoltageLevel], [Description], [UserID], [CrewID], [MultimediaAnchorID], [NotificationAnchorID], [WorkType], [IncidentStatus], [Timestamp]) VALUES (3, 2, 1, CAST(N'2021-06-29T12:34:00.0000000' AS DateTime2), CAST(N'2021-06-30T01:35:00.0000000' AS DateTime2), CAST(N'2021-06-06T07:31:04.0260000' AS DateTime2), CAST(N'2021-02-16T00:31:00.0000000' AS DateTime2), CAST(N'2021-04-14T23:32:00.0000000' AS DateTime2), 500, N'opis neki', 5, NULL, 3, 3, N'UNPLANNED', N'UNRESOLVED', CAST(N'2021-06-06T23:31:04.1956688' AS DateTime2))
INSERT [dbo].[Incidents] ([ID], [Priority], [Confirmed], [ETA], [ATA], [ETR], [IncidentDateTime], [WorkBeginDate], [VoltageLevel], [Description], [UserID], [CrewID], [MultimediaAnchorID], [NotificationAnchorID], [WorkType], [IncidentStatus], [Timestamp]) VALUES (4, 0, 0, CAST(N'2021-06-23T00:34:00.0000000' AS DateTime2), CAST(N'2021-08-27T00:33:00.0000000' AS DateTime2), CAST(N'2021-06-06T02:34:00.5970000' AS DateTime2), CAST(N'2021-06-17T23:34:00.0000000' AS DateTime2), CAST(N'2021-06-25T12:34:00.0000000' AS DateTime2), 30, N'', 5, NULL, 4, 4, N'UNPLANNED', N'INITIAL', CAST(N'2021-06-06T23:32:00.7544271' AS DateTime2))
INSERT [dbo].[Incidents] ([ID], [Priority], [Confirmed], [ETA], [ATA], [ETR], [IncidentDateTime], [WorkBeginDate], [VoltageLevel], [Description], [UserID], [CrewID], [MultimediaAnchorID], [NotificationAnchorID], [WorkType], [IncidentStatus], [Timestamp]) VALUES (5, 1, 0, CAST(N'2021-08-24T15:35:00.0000000' AS DateTime2), CAST(N'2021-08-26T15:35:00.0000000' AS DateTime2), CAST(N'2021-08-02T20:35:27.6020000' AS DateTime2), CAST(N'2021-08-10T15:35:00.0000000' AS DateTime2), CAST(N'2021-08-11T15:35:00.0000000' AS DateTime2), 123, N'opis neki', 22, NULL, 10, 10, N'UNPLANNED', N'UNRESOLVED', CAST(N'2021-08-02T15:35:26.6426576' AS DateTime2))
INSERT [dbo].[Incidents] ([ID], [Priority], [Confirmed], [ETA], [ATA], [ETR], [IncidentDateTime], [WorkBeginDate], [VoltageLevel], [Description], [UserID], [CrewID], [MultimediaAnchorID], [NotificationAnchorID], [WorkType], [IncidentStatus], [Timestamp]) VALUES (6, 1, 0, CAST(N'2021-09-04T15:35:00.0000000' AS DateTime2), CAST(N'2021-09-05T15:36:00.0000000' AS DateTime2), CAST(N'2021-08-02T22:35:02.8550000' AS DateTime2), CAST(N'2021-08-16T15:35:00.0000000' AS DateTime2), CAST(N'2021-08-24T15:35:00.0000000' AS DateTime2), 225, N'test', 22, NULL, 11, 11, N'UNPLANNED', N'UNRESOLVED', CAST(N'2021-08-02T15:36:01.8542483' AS DateTime2))
INSERT [dbo].[Incidents] ([ID], [Priority], [Confirmed], [ETA], [ATA], [ETR], [IncidentDateTime], [WorkBeginDate], [VoltageLevel], [Description], [UserID], [CrewID], [MultimediaAnchorID], [NotificationAnchorID], [WorkType], [IncidentStatus], [Timestamp]) VALUES (7, 1, 1, CAST(N'2021-08-19T15:42:00.0000000' AS DateTime2), CAST(N'2021-08-28T15:42:00.0000000' AS DateTime2), CAST(N'2021-08-02T17:45:58.2930000' AS DateTime2), CAST(N'2021-08-04T15:42:00.0000000' AS DateTime2), CAST(N'2021-08-13T15:42:00.0000000' AS DateTime2), 123, N'aaa', 22, NULL, 14, 14, N'UNPLANNED', N'UNRESOLVED', CAST(N'2021-08-02T15:42:57.2475375' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Incidents] OFF
GO
SET IDENTITY_INSERT [dbo].[MultimediaAttachments] ON 

INSERT [dbo].[MultimediaAttachments] ([ID], [Url], [MultimediaAnchorID]) VALUES (5, N'cv-glavas.pdf', 5)
INSERT [dbo].[MultimediaAttachments] ([ID], [Url], [MultimediaAnchorID]) VALUES (6, N'5024f4ae-1709-49d6-9b8c-263b6dba6b7d.jpg', 7)
INSERT [dbo].[MultimediaAttachments] ([ID], [Url], [MultimediaAnchorID]) VALUES (7, N'device.png', 5)
SET IDENTITY_INSERT [dbo].[MultimediaAttachments] OFF
GO
SET IDENTITY_INSERT [dbo].[StateChangeAnchors] ON 

INSERT [dbo].[StateChangeAnchors] ([ID]) VALUES (1)
INSERT [dbo].[StateChangeAnchors] ([ID]) VALUES (2)
INSERT [dbo].[StateChangeAnchors] ([ID]) VALUES (3)
INSERT [dbo].[StateChangeAnchors] ([ID]) VALUES (4)
INSERT [dbo].[StateChangeAnchors] ([ID]) VALUES (5)
INSERT [dbo].[StateChangeAnchors] ([ID]) VALUES (6)
INSERT [dbo].[StateChangeAnchors] ([ID]) VALUES (7)
INSERT [dbo].[StateChangeAnchors] ([ID]) VALUES (8)
INSERT [dbo].[StateChangeAnchors] ([ID]) VALUES (9)
INSERT [dbo].[StateChangeAnchors] ([ID]) VALUES (10)
INSERT [dbo].[StateChangeAnchors] ([ID]) VALUES (11)
SET IDENTITY_INSERT [dbo].[StateChangeAnchors] OFF
GO
SET IDENTITY_INSERT [dbo].[WorkRequests] ON 

INSERT [dbo].[WorkRequests] ([ID], [StartDate], [EndDate], [CreatedOn], [Purpose], [Note], [Details], [CompanyName], [Phone], [Street], [DocumentType], [DocumentStatus], [IsEmergency], [UserID], [IncidentID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID]) VALUES (1, CAST(N'2021-06-15T20:00:00.0000000' AS DateTime2), CAST(N'2021-06-29T20:00:00.0000000' AS DateTime2), CAST(N'2021-06-06T22:04:37.6920000' AS DateTime2), N'razlog neki nesot tako', N'', N'', N'', N'3092432904', N'Vojvode Stepe , Indjija', N'UNPLANNED', N'DRAFT', 0, 5, 1, 5, 1, 5)
INSERT [dbo].[WorkRequests] ([ID], [StartDate], [EndDate], [CreatedOn], [Purpose], [Note], [Details], [CompanyName], [Phone], [Street], [DocumentType], [DocumentStatus], [IsEmergency], [UserID], [IncidentID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID]) VALUES (2, CAST(N'2021-06-14T22:00:00.0000000' AS DateTime2), CAST(N'2021-06-15T22:00:00.0000000' AS DateTime2), CAST(N'2021-06-06T22:08:07.7420000' AS DateTime2), N'sssdsd', N'', N'', N'', N'', N'Vase Jagazovica, Veternik', N'PLANNED', N'DRAFT', 0, 5, 3, 6, 2, 6)
INSERT [dbo].[WorkRequests] ([ID], [StartDate], [EndDate], [CreatedOn], [Purpose], [Note], [Details], [CompanyName], [Phone], [Street], [DocumentType], [DocumentStatus], [IsEmergency], [UserID], [IncidentID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID]) VALUES (3, CAST(N'2021-06-15T22:00:00.0000000' AS DateTime2), CAST(N'2021-06-24T22:00:00.0000000' AS DateTime2), CAST(N'2021-06-07T09:15:12.1560000' AS DateTime2), N'SDASDAS', N'', N'', N'', N'', N'Maksima Gorkog, Beograd', N'UNPLANNED', N'DRAFT', 0, 5, 2, 9, 5, 9)
INSERT [dbo].[WorkRequests] ([ID], [StartDate], [EndDate], [CreatedOn], [Purpose], [Note], [Details], [CompanyName], [Phone], [Street], [DocumentType], [DocumentStatus], [IsEmergency], [UserID], [IncidentID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID]) VALUES (4, CAST(N'2021-08-02T22:00:00.0000000' AS DateTime2), CAST(N'2021-08-03T22:00:00.0000000' AS DateTime2), CAST(N'2021-08-02T15:36:12.5660000' AS DateTime2), N'test', N'test', N'test', N'kompanija neka', N'021212121', N'Vojvode Stepe , Indjija', N'UNPLANNED', N'DRAFT', 0, 22, 5, 12, 6, 12)
INSERT [dbo].[WorkRequests] ([ID], [StartDate], [EndDate], [CreatedOn], [Purpose], [Note], [Details], [CompanyName], [Phone], [Street], [DocumentType], [DocumentStatus], [IsEmergency], [UserID], [IncidentID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID]) VALUES (5, CAST(N'2021-08-22T22:00:00.0000000' AS DateTime2), CAST(N'2021-08-27T22:00:00.0000000' AS DateTime2), CAST(N'2021-08-02T15:36:55.6540000' AS DateTime2), N'bbb', N'bbb', N'bbb', N'aaaa', N'025123123', N'Vojvode Stepe , Indjija', N'UNPLANNED', N'DRAFT', 0, 22, 6, 13, 7, 13)
INSERT [dbo].[WorkRequests] ([ID], [StartDate], [EndDate], [CreatedOn], [Purpose], [Note], [Details], [CompanyName], [Phone], [Street], [DocumentType], [DocumentStatus], [IsEmergency], [UserID], [IncidentID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID]) VALUES (6, CAST(N'2021-08-23T22:00:00.0000000' AS DateTime2), CAST(N'2021-08-25T22:00:00.0000000' AS DateTime2), CAST(N'2021-08-02T15:43:06.6310000' AS DateTime2), N'sadas', N'dasd', N'asdas', N'aaaa', N'021213123', N'Vojvode Stepe , Indjija', N'UNPLANNED', N'DRAFT', 0, 22, 7, 15, 8, 15)
SET IDENTITY_INSERT [dbo].[WorkRequests] OFF
GO
SET IDENTITY_INSERT [dbo].[WorkPlans] ON 

INSERT [dbo].[WorkPlans] ([ID], [Purpose], [Notes], [Phone], [Street], [CompanyName], [CreatedOn], [StartDate], [EndDate], [DocumentType], [DocumentStatus], [UserID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID], [WorkRequestID]) VALUES (2, N'DFDSF', N'Neka beleska', N'021984393', N'Vasa Jagzovica', N'ddsd', CAST(N'2021-06-07T00:13:19.4700000' AS DateTime2), CAST(N'2021-06-20T00:00:00.0000000' AS DateTime2), CAST(N'2021-06-25T00:00:00.0000000' AS DateTime2), N'PLANNED', N'DRAFT', 5, NULL, NULL, NULL, 1)
INSERT [dbo].[WorkPlans] ([ID], [Purpose], [Notes], [Phone], [Street], [CompanyName], [CreatedOn], [StartDate], [EndDate], [DocumentType], [DocumentStatus], [UserID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID], [WorkRequestID]) VALUES (3, N'VBV', N'BVCBVC', N'435435435', N'ULICAAA', N'dfdfs', CAST(N'2021-06-07T00:14:17.9900000' AS DateTime2), CAST(N'2021-05-31T00:00:00.0000000' AS DateTime2), CAST(N'2021-07-05T00:00:00.0000000' AS DateTime2), N'UNPLANNED', N'DRAFT', 5, NULL, NULL, NULL, 2)
INSERT [dbo].[WorkPlans] ([ID], [Purpose], [Notes], [Phone], [Street], [CompanyName], [CreatedOn], [StartDate], [EndDate], [DocumentType], [DocumentStatus], [UserID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID], [WorkRequestID]) VALUES (4, N'neka svrha', N'notes', N'021222333', N'Ulica neka', N'kompanija neka', CAST(N'2021-08-25T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-28T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-07T00:00:00.0000000' AS DateTime2), N'PLANNED', N'DRAFT', 5, NULL, NULL, NULL, 4)
INSERT [dbo].[WorkPlans] ([ID], [Purpose], [Notes], [Phone], [Street], [CompanyName], [CreatedOn], [StartDate], [EndDate], [DocumentType], [DocumentStatus], [UserID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID], [WorkRequestID]) VALUES (5, N'aaaa', N'bbbbbb', N'021777755', N'Ulicica', N'kompanijica', CAST(N'2021-09-04T00:00:00.0000000' AS DateTime2), CAST(N'2021-09-07T00:00:00.0000000' AS DateTime2), CAST(N'2021-10-05T00:00:00.0000000' AS DateTime2), N'PLANNED', N'DRAFT', 5, NULL, NULL, NULL, 5)
INSERT [dbo].[WorkPlans] ([ID], [Purpose], [Notes], [Phone], [Street], [CompanyName], [CreatedOn], [StartDate], [EndDate], [DocumentType], [DocumentStatus], [UserID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID], [WorkRequestID]) VALUES (7, N'bbbbbb', N'dddddd', N'025223311', N'neka tamo', N'neka tamo', CAST(N'2021-10-11T00:00:00.0000000' AS DateTime2), CAST(N'2021-10-12T00:00:00.0000000' AS DateTime2), CAST(N'2021-11-12T00:00:00.0000000' AS DateTime2), N'PLANNED', N'DRAFT', 5, NULL, NULL, NULL, 6)
SET IDENTITY_INSERT [dbo].[WorkPlans] OFF
GO
SET IDENTITY_INSERT [dbo].[SafetyDocuments] ON 

INSERT [dbo].[SafetyDocuments] ([ID], [Details], [Notes], [Phone], [OperationCompleted], [TagsRemoved], [GroundingRemoved], [Ready], [CreatedOn], [DocumentType], [DocumentStatus], [UserID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID], [WorkPlanID]) VALUES (2, N'', N'', N'5435', 1, 0, 0, 1, CAST(N'2021-06-06T22:26:41.4880000' AS DateTime2), N'UNPLANNED', N'APPROVED', 17, 8, 4, 8, 2)
INSERT [dbo].[SafetyDocuments] ([ID], [Details], [Notes], [Phone], [OperationCompleted], [TagsRemoved], [GroundingRemoved], [Ready], [CreatedOn], [DocumentType], [DocumentStatus], [UserID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID], [WorkPlanID]) VALUES (3, N'4', N'4', N'4', 0, 0, 0, 0, CAST(N'2021-08-02T16:48:12.7760000' AS DateTime2), N'PLANNED', N'DRAFT', 22, 16, 9, 16, 4)
INSERT [dbo].[SafetyDocuments] ([ID], [Details], [Notes], [Phone], [OperationCompleted], [TagsRemoved], [GroundingRemoved], [Ready], [CreatedOn], [DocumentType], [DocumentStatus], [UserID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID], [WorkPlanID]) VALUES (4, N'555555', N'555555', N'55555555555', 0, 0, 0, 0, CAST(N'2021-08-02T16:55:44.5020000' AS DateTime2), N'UNPLANNED', N'DRAFT', 22, 17, 10, 17, 5)
INSERT [dbo].[SafetyDocuments] ([ID], [Details], [Notes], [Phone], [OperationCompleted], [TagsRemoved], [GroundingRemoved], [Ready], [CreatedOn], [DocumentType], [DocumentStatus], [UserID], [MultimediaAnchorID], [StateChangeAnchorID], [NotificationAnchorID], [WorkPlanID]) VALUES (5, N'777777777', N'777777777', N'7777777', 0, 0, 0, 0, CAST(N'2021-08-02T16:56:43.6140000' AS DateTime2), N'UNPLANNED', N'DRAFT', 22, 18, 11, 18, 7)
SET IDENTITY_INSERT [dbo].[SafetyDocuments] OFF
GO
SET IDENTITY_INSERT [dbo].[StateChangeHistories] ON 

INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (1, CAST(N'2021-06-07T00:05:12.4400000' AS DateTime2), N'DRAFT', 1, 5)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (2, CAST(N'2021-06-07T00:08:24.2100000' AS DateTime2), N'DRAFT', 2, 5)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (3, CAST(N'2021-06-07T00:10:48.4100000' AS DateTime2), N'APPROVED', 2, 17)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (4, CAST(N'2021-06-07T00:15:01.3066667' AS DateTime2), N'DRAFT', 3, 17)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (5, CAST(N'2021-06-07T00:16:31.5466667' AS DateTime2), N'APPROVED', 1, 17)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (6, CAST(N'2021-06-07T00:18:38.7700000' AS DateTime2), N'APPROVED', 3, 17)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (7, CAST(N'2021-06-07T00:26:55.8000000' AS DateTime2), N'DRAFT', 4, 17)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (8, CAST(N'2021-06-07T00:27:30.1700000' AS DateTime2), N'DENIED', 4, 17)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (9, CAST(N'2021-06-07T00:27:32.0233333' AS DateTime2), N'APPROVED', 4, 17)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (10, CAST(N'2021-06-07T11:15:37.0633333' AS DateTime2), N'DRAFT', 5, 5)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (11, CAST(N'2021-08-02T15:36:43.1833333' AS DateTime2), N'DRAFT', 6, 22)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (12, CAST(N'2021-08-02T15:37:17.2266667' AS DateTime2), N'DRAFT', 7, 22)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (13, CAST(N'2021-08-02T15:43:22.8300000' AS DateTime2), N'DRAFT', 8, 22)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (14, CAST(N'2021-08-02T16:49:24.7500000' AS DateTime2), N'DRAFT', 9, 22)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (15, CAST(N'2021-08-02T16:56:03.2800000' AS DateTime2), N'DRAFT', 10, 22)
INSERT [dbo].[StateChangeHistories] ([ID], [ChangeDate], [DocumentStatus], [StateChangeAnchorID], [UserID]) VALUES (16, CAST(N'2021-08-02T16:56:54.6133333' AS DateTime2), N'DRAFT', 11, 22)
SET IDENTITY_INSERT [dbo].[StateChangeHistories] OFF
GO
SET IDENTITY_INSERT [dbo].[Consumers] ON 

INSERT [dbo].[Consumers] ([ID], [Name], [Lastname], [Phone], [AccountID], [AccountType], [UserID], [LocationID]) VALUES (1, N'Konzumer', N'Neki', N'022462484', N'1223213', 0, NULL, 1)
INSERT [dbo].[Consumers] ([ID], [Name], [Lastname], [Phone], [AccountID], [AccountType], [UserID], [LocationID]) VALUES (3, N'Petar', N'Petrovic', N'021454895', N'3454345', 1, NULL, 3)
INSERT [dbo].[Consumers] ([ID], [Name], [Lastname], [Phone], [AccountID], [AccountType], [UserID], [LocationID]) VALUES (5, N'Jovica', N'Bulut', N'324234234', N'4564255', 0, 20, 4)
INSERT [dbo].[Consumers] ([ID], [Name], [Lastname], [Phone], [AccountID], [AccountType], [UserID], [LocationID]) VALUES (6, N'Zdravko', N'Rogan', N'402342904', N'2349023', 0, 8, 4)
INSERT [dbo].[Consumers] ([ID], [Name], [Lastname], [Phone], [AccountID], [AccountType], [UserID], [LocationID]) VALUES (7, N'Nadja', N'Gutova', N'324324234', N'2343244', 1, NULL, 2)
SET IDENTITY_INSERT [dbo].[Consumers] OFF
GO
SET IDENTITY_INSERT [dbo].[Calls] ON 

INSERT [dbo].[Calls] ([ID], [CallReason], [Comment], [Hazard], [LocationID], [ConsumerID], [IncidentID]) VALUES (1, N'NO_POWER', N'komeatrcic', N'safjdofjs', 4, NULL, 1)
INSERT [dbo].[Calls] ([ID], [CallReason], [Comment], [Hazard], [LocationID], [ConsumerID], [IncidentID]) VALUES (2, N'FLICKERING_LIGHT', N'SADSD', N'ADSADA', 4, 5, 1)
INSERT [dbo].[Calls] ([ID], [CallReason], [Comment], [Hazard], [LocationID], [ConsumerID], [IncidentID]) VALUES (3, N'VOLTAGE_PROBLEM', N'SFDF', N'FDSF', 4, 1, 1)
INSERT [dbo].[Calls] ([ID], [CallReason], [Comment], [Hazard], [LocationID], [ConsumerID], [IncidentID]) VALUES (5, N'POWER_RESTORED', N'DSFDSF', N'SFDFDS', 3, 1, NULL)
INSERT [dbo].[Calls] ([ID], [CallReason], [Comment], [Hazard], [LocationID], [ConsumerID], [IncidentID]) VALUES (6, N'MALFUNCTION', N'komentiram nestoo', N'Lupam nesto batee', 6, NULL, 1)
INSERT [dbo].[Calls] ([ID], [CallReason], [Comment], [Hazard], [LocationID], [ConsumerID], [IncidentID]) VALUES (7, N'FLICKERING_LIGHT', N'', N'neki hazard bakisa', 4, 6, 1)
SET IDENTITY_INSERT [dbo].[Calls] OFF
GO
SET IDENTITY_INSERT [dbo].[Resolutions] ON 

INSERT [dbo].[Resolutions] ([ID], [Cause], [Subcause], [Construction], [Material], [IncidentID]) VALUES (1, N'WEATHER', N'SNOW', N'SURFACE', N'PLASTICS', 1)
SET IDENTITY_INSERT [dbo].[Resolutions] OFF
GO
