USE [LibraryDb]
GO

SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([user_id], [name], [surname], [image_url], [join_date], [status], [UserName], [NormalizedUserName], [email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [phone_number], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (1, NULL, NULL, NULL, CAST(N'2024-09-06' AS Date), N'Active', N'Admin@Admin.com', N'ADMIN@ADMIN.COM', N'Admin@Admin.com', N'ADMIN@ADMIN.COM', 1, N'AQAAAAIAAYagAAAAEHCyRX4wdZu6TYllSiqk6Fn/L7xOR8TpE4BYLxKugCZT7RUfhsP7AdmrGvWyBNktcQ==', N'7K6RS2U4JWQAUI3GIFFZO63ETDJHH6WP', N'67559e04-cf3f-4f5b-b5b5-45a4d9121920', NULL, 1, 0, NULL, 1, 0)
INSERT [dbo].[users] ([user_id], [name], [surname], [image_url], [join_date], [status], [UserName], [NormalizedUserName], [email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [phone_number], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (2, N'Mateusz', N'Grzybek', N'http://localhost:9090/api/images/userImage/gszp1@mail.com.jpg', CAST(N'2024-09-06' AS Date), N'Active', N'gszp1@mail.com', N'GSZP1@MAIL.COM', N'gszp1@mail.com', N'GSZP1@MAIL.COM', 1, N'AQAAAAIAAYagAAAAEGz9yW2vC2oQCKqbfQAhI6EZ8LELn0qaWC6X/ysLCWgXYqpuc6QzXPusByniJgBkTA==', N'EQBOJBSDJJAQOMS4JGRI7AERDF6WLFCJ', N'3e181cff-bc4f-4a43-bd9b-6f1de06492e9', N'111222333', 1, 0, NULL, 1, 0)
INSERT [dbo].[users] ([user_id], [name], [surname], [image_url], [join_date], [status], [UserName], [NormalizedUserName], [email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [phone_number], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (3, NULL, NULL, NULL, CAST(N'2024-09-06' AS Date), N'Active', N'gszp2@mail.com', N'GSZP2@MAIL.COM', N'gszp2@mail.com', N'GSZP2@MAIL.COM', 1, N'AQAAAAIAAYagAAAAEPFH1DPqKNxQljCwAS0h+Bgz0oBzlx5H/fB7HC+WZ1ZOsvORcnAkmbUKYaAhEar/Ww==', N'LA6R7XA5ONCJW4VFBVL23SHHZ42A7TRB', N'21c761ed-5c9e-4ba8-8a3d-36664d5e2c88', NULL, 1, 0, NULL, 1, 0)
INSERT [dbo].[users] ([user_id], [name], [surname], [image_url], [join_date], [status], [UserName], [NormalizedUserName], [email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [phone_number], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (4, NULL, NULL, NULL, CAST(N'2024-09-06' AS Date), N'Active', N'gszpo@mail.com', N'GSZPO@MAIL.COM', N'gszpo@mail.com', N'GSZPO@MAIL.COM', 1, N'AQAAAAIAAYagAAAAEI0bgddlQTANrMX1xgcVmtTVIsYVLu2GRS998SqE9LrFQRdZIin4E8os1flNtSNPXg==', N'PNXFXRT72PIVJKUHECWDZ3N4APCMFXVX', N'a7538fe1-eafa-4e98-8a2a-fb20d9038360', NULL, 1, 0, NULL, 1, 0)
INSERT [dbo].[users] ([user_id], [name], [surname], [image_url], [join_date], [status], [UserName], [NormalizedUserName], [email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [phone_number], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (5, NULL, NULL, NULL, CAST(N'2024-09-06' AS Date), N'Active', N'geralt@mail.com', N'GERALT@MAIL.COM', N'geralt@mail.com', N'GERALT@MAIL.COM', 1, N'AQAAAAIAAYagAAAAEGEd+pWHFGwfPEhJacswL9QXo2jT27Zslh1mLYBFAKUNYvQi7tj38+PIHzriyxS/hw==', N'YEOGTWV567VDE4JRHACPT4FLUFW7T2UN', N'1e3f7437-fc81-4672-9124-aaec826a7538', NULL, 1, 0, NULL, 1, 0)
INSERT [dbo].[users] ([user_id], [name], [surname], [image_url], [join_date], [status], [UserName], [NormalizedUserName], [email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [phone_number], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (6, NULL, NULL, NULL, CAST(N'2024-09-06' AS Date), N'Active', N'ok@mail.com', N'OK@MAIL.COM', N'ok@mail.com', N'OK@MAIL.COM', 1, N'AQAAAAIAAYagAAAAELJleog0zF6yueI7VeAqnjcO86PTgHWGKhY3/bTbrfoMJlZ5zqguGymsq6wjqKo7/w==', N'VZNVKG2SNHBUX2NDP2NJZINYS7LM6KBF', N'859c6250-3d04-44b1-9f02-694d3b783955', NULL, 1, 0, NULL, 1, 0)
INSERT [dbo].[users] ([user_id], [name], [surname], [image_url], [join_date], [status], [UserName], [NormalizedUserName], [email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [phone_number], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (7, NULL, NULL, NULL, CAST(N'2024-09-06' AS Date), N'Active', N'super@mail.com', N'SUPER@MAIL.COM', N'super@mail.com', N'SUPER@MAIL.COM', 1, N'AQAAAAIAAYagAAAAECNVYNPQ2g2WL6cdjLEVb9LAQ6UGq0KxcifgLUSeB0tkiCqo9P3D+tTZMd9CYe2sHQ==', N'3X5I3S52R5RDXEWTLGRIMND3PGVG6DSU', N'3bc7fbb4-8978-4118-9899-7a63164f4224', NULL, 1, 0, NULL, 1, 0)
INSERT [dbo].[users] ([user_id], [name], [surname], [image_url], [join_date], [status], [UserName], [NormalizedUserName], [email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [phone_number], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (8, NULL, NULL, NULL, CAST(N'2024-09-06' AS Date), N'Active', N'abs@mail.com', N'ABS@MAIL.COM', N'abs@mail.com', N'ABS@MAIL.COM', 1, N'AQAAAAIAAYagAAAAEIxYRFDQsDBRO10mpg8ZmYwd3ti4WTvU2yH56mkhGg8Te34LIVBUoMd/TeLGuBI3wA==', N'D2GQHOZON3UI2M3X3B7QWJJ4ZJ2QFUO7', N'aca9e0f5-63d1-4a2a-9eb7-e1224bfaa60c', NULL, 1, 0, NULL, 1, 0)
INSERT [dbo].[users] ([user_id], [name], [surname], [image_url], [join_date], [status], [UserName], [NormalizedUserName], [email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [phone_number], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (9, NULL, NULL, NULL, CAST(N'2024-09-06' AS Date), N'Active', N'tytus@mail.com', N'TYTUS@MAIL.COM', N'tytus@mail.com', N'TYTUS@MAIL.COM', 1, N'AQAAAAIAAYagAAAAEOVz7+vbzO1LnvO33h78xZewcKy4nX7Li5bItCz9xGrxe6aSuITzeaDeQZINpm8zbg==', N'KQSPVTXJYNBXZ4SCS2RFHDREYTK7SIK3', N'f842016a-4354-4980-b149-de27269fdfbf', NULL, 1, 0, NULL, 1, 0)
INSERT [dbo].[users] ([user_id], [name], [surname], [image_url], [join_date], [status], [UserName], [NormalizedUserName], [email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [phone_number], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (10, NULL, NULL, NULL, CAST(N'2024-09-06' AS Date), N'Active', N'romek@mail.com', N'ROMEK@MAIL.COM', N'romek@mail.com', N'ROMEK@MAIL.COM', 1, N'AQAAAAIAAYagAAAAEH/ukCJRhTkkTU0ikYemMAmlQLQ+5gjkIEh2GV0rH1ml39syo1jvgzPlY/ysJ93k1w==', N'AMFOQVW2IQTVVEUIUJIJ56NPLGE7D5AI', N'7a60f933-d691-4fbb-a87e-fe6bbaf98d5e', NULL, 1, 0, NULL, 1, 0)
INSERT [dbo].[users] ([user_id], [name], [surname], [image_url], [join_date], [status], [UserName], [NormalizedUserName], [email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [phone_number], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (11, NULL, NULL, NULL, CAST(N'2024-09-06' AS Date), N'Active', N'atomek@mail.com', N'ATOMEK@MAIL.COM', N'atomek@mail.com', N'ATOMEK@MAIL.COM', 1, N'AQAAAAIAAYagAAAAEFRvjGpXpaCNKKDI4KAjUpGq3kjzV67GVXQaQXGFaNQlyEXbtziM3TY9vLLeffYQcg==', N'5HKFA44N3B3GB47HHS6B7LYXBRTZ5Z4D', N'bca300d3-dd7e-4d89-afba-dd85c4ec0975', NULL, 1, 0, NULL, 1, 0)
INSERT [dbo].[users] ([user_id], [name], [surname], [image_url], [join_date], [status], [UserName], [NormalizedUserName], [email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [phone_number], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (12, NULL, NULL, NULL, CAST(N'2024-09-06' AS Date), N'Active', N'szymekCzarodziej@mail.com', N'SZYMEKCZARODZIEJ@MAIL.COM', N'szymekCzarodziej@mail.com', N'SZYMEKCZARODZIEJ@MAIL.COM', 1, N'AQAAAAIAAYagAAAAEI23Th+rRgOIFjKpu7qVBNi9d/4whO5tqrbbDB/+m1gUpPVG8I/YWd4uRTqh0yEpPQ==', N'PB7BHJ7E7TJG7PNWPUKHUZEJ5RIMYISP', N'079b4a60-78b9-4243-819c-8f291a1df10a', NULL, 1, 0, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[users] OFF
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (2, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (3, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (4, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (5, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (6, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (7, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (8, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (9, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (10, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (11, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (12, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (1, 2)
GO
SET IDENTITY_INSERT [dbo].[authors] ON 

INSERT [dbo].[authors] ([author_id], [first_name], [last_name], [email]) VALUES (1, N'Ben', N'Counter', N'B@mail.com')
INSERT [dbo].[authors] ([author_id], [first_name], [last_name], [email]) VALUES (2, N'Dan', N'Abnett', N'D@mail.com')
INSERT [dbo].[authors] ([author_id], [first_name], [last_name], [email]) VALUES (3, N'Nick', N'Kyme', N'N@mail.com')
INSERT [dbo].[authors] ([author_id], [first_name], [last_name], [email]) VALUES (4, N'Chris', N'Wraight', N'CW@mail.com')
INSERT [dbo].[authors] ([author_id], [first_name], [last_name], [email]) VALUES (5, N'Andrzej', N'Sapkowski', N'AS@mail.com')
INSERT [dbo].[authors] ([author_id], [first_name], [last_name], [email]) VALUES (6, N'Marcin', N'Zwierzchowski', N'MZ@mail.com')
SET IDENTITY_INSERT [dbo].[authors] OFF
GO
SET IDENTITY_INSERT [dbo].[publishers] ON 

INSERT [dbo].[publishers] ([publisher_id], [name], [address]) VALUES (1, N'copcorp', N'ul.sezamkowa')
INSERT [dbo].[publishers] ([publisher_id], [name], [address]) VALUES (2, N'superNowa', N'sa')
SET IDENTITY_INSERT [dbo].[publishers] OFF
GO
SET IDENTITY_INSERT [dbo].[resources] ON 

INSERT [dbo].[resources] ([resource_id], [title], [identifier], [description], [image_url], [FK_publisher]) VALUES (1, N'Galaktyka w płomieniach', N'HH-03', N'Po cudownym ozdrowieniu z ciężkich ran odniesionych na Davinie Mistrz Wojny wiedzie zwycięskie siły Imperium przeciw zbuntowanemu światu Isstvan III.', N'http://localhost:9090/api/images/GalaktykaWPłomieniach.png', 1)
INSERT [dbo].[resources] ([resource_id], [title], [identifier], [description], [image_url], [FK_publisher]) VALUES (2, N'Gwardia Honorowa', N'DG-04', N'Komisarz Gaunt i Duchy tym razem walczą na Hagii, świecie-świątyni, którego zdobycie ma istotne znaczenie taktyczne i duchowe. Gdy potężna flota Chaosu pojawia się w pobliżu planety, Gaunt i jego żołnierze zostają wysłani z misją uratowania najświętszych ', N'http://localhost:9090/api/images/GwardiaHonorowa.jpg', 1)
INSERT [dbo].[resources] ([resource_id], [title], [identifier], [description], [image_url], [FK_publisher]) VALUES (3, N'Vulkan Żyje', N'HH-26', N'Po masakrze na Isstvanie V Astartes z Legionu Salamander długo i wytrwale poszukują poległego Prymarchy, jednak ich wysiłki nie przynoszą rezultatu. Nie wiedzą, że chociaż Vulkan nadal żyje, to życzyłby sobie śmierci…', N'http://localhost:9090/api/images/VulkanŻyje.png', 1)
INSERT [dbo].[resources] ([resource_id], [title], [identifier], [description], [image_url], [FK_publisher]) VALUES (4, N'Prospero w płomieniach', N'HH-15', N'Imperator jest wściekły. Prymarcha Magnus Czerwony z Legionu Tysiąca Synów popełnił katastrofalny błąd i naraził Terrę na niebezpieczeństwo. Nie mając innego wyboru, Imperator powierza Kosmicznym Wilkom Lemana Russa zadanie pojmania jego brata z Prospero,', N'http://localhost:9090/api/images/ProsperoWPłomieniach.png', 1)
INSERT [dbo].[resources] ([resource_id], [title], [identifier], [description], [image_url], [FK_publisher]) VALUES (5, N'Jaghatai Khan: Sokół z Chogoris', N'PRYM-08', N'Od czasu odkrycia Chogoris przez Imperium pełna mistycyzmu kultura wojowników Białych Szram nigdy nie była w pełni zgodna z ideałami Unifikacji. Gdy Wielka Krucjata wypala sobie ścieżkę przez gwiazdy, ich enigmatyczny Prymarcha, Jaghatai Khan.', N'http://localhost:9090/api/images/JaghataiKhan.jpg', 1)
INSERT [dbo].[resources] ([resource_id], [title], [identifier], [description], [image_url], [FK_publisher]) VALUES (6, N'Wywyższenie Horusa', N'HH-01', N'Jest trzydzieste pierwsze tysiąclecie. Pod łaskawymi rządami nieśmiertelnego Imperatora, Imperium Człowieka rozciąga się na całą galaktykę. Nadeszła złota era odkryć i podboju.', N'http://localhost:9090/api/images/WywyższenieHorusa.png', 1)
INSERT [dbo].[resources] ([resource_id], [title], [identifier], [description], [image_url], [FK_publisher]) VALUES (7, N'Wiedźmin. Szpony i Kły.', N'W1', N'„No tak. Trzydzieści lat. Wiedźmin do wynajęcia za trzy tysiące orenów. Gdy pojawił się w Wyzimie, w karczmie „Pod Lisem” nie był młodzieniaszkiem. Pobielałe włosy, szrama na twarzy i ten charakterystyczny, paskudny uśmiech.', N'http://localhost:9090/api/images/WiedźminSzponyIKły.webp', 2)
INSERT [dbo].[resources] ([resource_id], [title], [identifier], [description], [image_url], [FK_publisher]) VALUES (8, N'Sezon burz. Wiedźmin. Tom 8', N'W8', N'Oto nowy Sapkowski i nowy wiedźmin. Mistrz polskiej fantastyki znowu zaskakuje. "Sezon burz” nie opowiada bowiem o młodzieńczych latach białowłosego zabójcy potworów ani o jego losach po śmierci/nieśmierci kończącej ostatni tom sagi.', N'http://localhost:9090/api/images/WiedźminSezonBurz.webp', 2)
INSERT [dbo].[resources] ([resource_id], [title], [identifier], [description], [image_url], [FK_publisher]) VALUES (9, N'Ostatnie życzenie. Wiedźmin. Tom 1', N'W2', N'Tom 1. sagi o Wiedźminie to zbiór opowiadań, które pozwolą Ci poznać Geralta. Co ciekawe, książka „Ostatnie życzenie” ukazała się rok po drugim w chronologii wewnętrznej cyklu opowiadań – „Miecz przeznaczenia”.', N'http://localhost:9090/api/images/WiedźminOstatnieŻyczenie.jpg', 2)
INSERT [dbo].[resources] ([resource_id], [title], [identifier], [description], [image_url], [FK_publisher]) VALUES (10, N'Miecz przeznaczenia. Wiedźmin. Tom 2', N'W3', N'Król Niedamir organizuje niebezpieczną wyprawę na smoka, który skrył się w jaskiniach Gór Pustulskich. Do śmiałków dołącza Geralt z Rivii i wraz z nimi rozpoczyna ryzykowną przygodę. Na swojej drodze spotyka trubadura Jaskra oraz czarodziejkę Yennefer.', N'http://localhost:9090/api/images/WiedźminMieczPrzeznaczenia.jpg', 2)
INSERT [dbo].[resources] ([resource_id], [title], [identifier], [description], [image_url], [FK_publisher]) VALUES (11, N'Krew elfów. Wiedźmin. Tom 3', N'W4', N'Zagłębiając się w karty tego dzieła, zaczniesz odkrywać magiczny, wykreowany przez autora w sposób bardzo realistyczny, świat wiedźminów. Poznasz również samego Geralta, który postanawia zaopiekować się dzieckiem‑niespodzianką, którym jest Ciri.', N'http://localhost:9090/api/images/WiedźminKrewElfow.jpg', 2)
INSERT [dbo].[resources] ([resource_id], [title], [identifier], [description], [image_url], [FK_publisher]) VALUES (12, N'Czas pogardy. Wiedźmin. Tom 4', N'W5', N'Świat Ciri i wiedźmina ogarniają płomienie. Nastał zapowiadany przez Ithlinne czas miecza i topora. Czas pogardy. A w czasach pogardy na powierzchnię wypełzają Szczury. Szczury atakujące po szczurzemu, cicho, zdradziecko i okrutnie. ', N'http://localhost:9090/api/images/WiedźminCzasPogardy.webp', 2)
SET IDENTITY_INSERT [dbo].[resources] OFF
GO
INSERT [dbo].[authors_resources] ([FK_author], [FK_resource]) VALUES (1, 1)
INSERT [dbo].[authors_resources] ([FK_author], [FK_resource]) VALUES (2, 2)
INSERT [dbo].[authors_resources] ([FK_author], [FK_resource]) VALUES (3, 3)
INSERT [dbo].[authors_resources] ([FK_author], [FK_resource]) VALUES (2, 4)
INSERT [dbo].[authors_resources] ([FK_author], [FK_resource]) VALUES (4, 5)
INSERT [dbo].[authors_resources] ([FK_author], [FK_resource]) VALUES (2, 6)
INSERT [dbo].[authors_resources] ([FK_author], [FK_resource]) VALUES (5, 7)
INSERT [dbo].[authors_resources] ([FK_author], [FK_resource]) VALUES (6, 7)
INSERT [dbo].[authors_resources] ([FK_author], [FK_resource]) VALUES (5, 8)
INSERT [dbo].[authors_resources] ([FK_author], [FK_resource]) VALUES (5, 9)
INSERT [dbo].[authors_resources] ([FK_author], [FK_resource]) VALUES (5, 10)
INSERT [dbo].[authors_resources] ([FK_author], [FK_resource]) VALUES (5, 11)
INSERT [dbo].[authors_resources] ([FK_author], [FK_resource]) VALUES (5, 12)
GO
SET IDENTITY_INSERT [dbo].[resource_instances] ON 

INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (1, 1, N'Active', 1)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (2, 1, N'Active', 1)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (3, 1, N'Active', 1)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (4, 1, N'Active', 2)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (5, 0, N'Active', 2)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (6, 1, N'Active', 3)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (7, 0, N'Active', 4)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (8, 0, N'Active', 4)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (9, 0, N'Active', 4)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (10, 0, N'Active', 4)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (11, 0, N'Active', 4)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (12, 1, N'Active', 5)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (13, 0, N'Active', 5)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (14, 1, N'Active', 6)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (15, 1, N'Active', 6)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (16, 0, N'Active', 6)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (17, 0, N'Active', 6)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (18, 1, N'Active', 7)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (19, 0, N'Active', 7)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (20, 0, N'Active', 7)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (21, 1, N'Active', 8)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (22, 1, N'Active', 9)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (23, 1, N'Active', 9)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (24, 1, N'Active', 10)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (25, 0, N'Active', 10)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (26, 0, N'Active', 10)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (27, 1, N'Active', 11)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (28, 0, N'Active', 11)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (29, 0, N'Active', 11)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (30, 0, N'Active', 11)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (31, 0, N'Active', 12)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (32, 0, N'Active', 12)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (33, 0, N'Active', 12)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (34, 0, N'Active', 12)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (35, 0, N'Active', 12)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (36, 0, N'Active', 12)
INSERT [dbo].[resource_instances] ([instance_id], [reserved], [status], [FK_resource]) VALUES (37, 0, N'Active', 12)
SET IDENTITY_INSERT [dbo].[resource_instances] OFF
GO
SET IDENTITY_INSERT [dbo].[reservations] ON 

INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (1, CAST(N'2024-09-06' AS Date), CAST(N'2024-09-20' AS Date), N'Active', 0, 11, 1)
INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (2, CAST(N'2024-09-06' AS Date), CAST(N'2024-09-20' AS Date), N'Active', 0, 11, 27)
INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (3, CAST(N'2024-09-06' AS Date), CAST(N'2024-09-20' AS Date), N'Active', 0, 10, 14)
INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (4, CAST(N'2024-09-06' AS Date), CAST(N'2024-09-20' AS Date), N'Borrowed', 0, 10, 22)
INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (5, CAST(N'2024-06-20' AS Date), CAST(N'2024-07-04' AS Date), N'Cancelled', 0, 9, 18)
INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (6, CAST(N'2024-09-28' AS Date), CAST(N'2024-12-28' AS Date), N'Active', 0, 9, 2)
INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (7, CAST(N'2024-06-20' AS Date), CAST(N'2024-07-04' AS Date), N'Borrowed', 0, 9, 24)
INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (8, CAST(N'2024-06-20' AS Date), CAST(N'2024-07-04' AS Date), N'Active', 0, 4, 21)
INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (9, CAST(N'2024-06-20' AS Date), CAST(N'2024-07-04' AS Date), N'Borrowed', 0, 4, 6)
INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (10, CAST(N'2024-06-20' AS Date), CAST(N'2024-06-20' AS Date), N'Active', 0, 4, 12)
INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (11, CAST(N'2024-08-21' AS Date), CAST(N'2024-08-27' AS Date), N'Active', 0, 2, 15)
INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (12, CAST(N'2024-06-20' AS Date), CAST(N'2024-07-04' AS Date), N'Cancelled', 0, 2, 23)
INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (13, CAST(N'2024-06-20' AS Date), CAST(N'2024-07-04' AS Date), N'Borrowed', 0, 2, 4)
INSERT [dbo].[reservations] ([reservation_id], [reservation_start], [reservation_end], [status], [extension_count], [FK_user], [FK_instance]) VALUES (14, CAST(N'2024-06-20' AS Date), CAST(N'2024-07-04' AS Date), N'Active', 0, 2, 3)
SET IDENTITY_INSERT [dbo].[reservations] OFF
GO
