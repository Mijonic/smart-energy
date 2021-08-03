USE [DeviceUsage]

SET IDENTITY_INSERT [dbo].[DeviceUsage] ON 

INSERT [dbo].[DeviceUsage] ([ID], [IncidentID], [WorkRequestID], [WorkPlanID], [SafetyDocumentID], [DeviceID]) VALUES (1, 1, 1, 2, 2, 2)
INSERT [dbo].[DeviceUsage] ([ID], [IncidentID], [WorkRequestID], [WorkPlanID], [SafetyDocumentID], [DeviceID]) VALUES (3, 1, 1, 2, 2, 6)
INSERT [dbo].[DeviceUsage] ([ID], [IncidentID], [WorkRequestID], [WorkPlanID], [SafetyDocumentID], [DeviceID]) VALUES (4, 3, 2, 3, NULL, 1)
INSERT [dbo].[DeviceUsage] ([ID], [IncidentID], [WorkRequestID], [WorkPlanID], [SafetyDocumentID], [DeviceID]) VALUES (5, 2, 3, NULL, NULL, 4)
INSERT [dbo].[DeviceUsage] ([ID], [IncidentID], [WorkRequestID], [WorkPlanID], [SafetyDocumentID], [DeviceID]) VALUES (6, 5, 4, 4, 3, 2)
INSERT [dbo].[DeviceUsage] ([ID], [IncidentID], [WorkRequestID], [WorkPlanID], [SafetyDocumentID], [DeviceID]) VALUES (7, 6, 5, 5, 4, 6)
INSERT [dbo].[DeviceUsage] ([ID], [IncidentID], [WorkRequestID], [WorkPlanID], [SafetyDocumentID], [DeviceID]) VALUES (8, 7, 6, 7, 5, 2)
SET IDENTITY_INSERT [dbo].[DeviceUsage] OFF
GO
