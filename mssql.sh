#!/bin/bash

# SQL 創建表
CREATE TABLE [Users] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Name] varchar(42) NOT NULL,
  [Address] varchar(42) NOT NULL,
  [Email] varchar(255) NOT NULL,
  [Nonce] varchar(42) NOT NULL,
  [Introduction] varchar(255),
  [BackgroundPhoto] varchar(255),
  [Picture] varchar(255),
  [Admin] bit NOT NULL,
  [CreatedAt] datetime NOT NULL DEFAULT (getdate()),
  [UpdatedAt] datetime NOT NULL DEFAULT (getdate())
)
GO

CREATE TABLE [Articles] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [UserId] int NOT NULL,
  [Title] varchar(42) NOT NULL,
  [SubStandard] varchar(100) NOT NULL,
  [Contents] nvarchar(max) NOT NULL,
  [State] bit NOT NULL,
  [CreatedAt] datetime NOT NULL DEFAULT (getdate()),
  [UpdatedAt] datetime NOT NULL DEFAULT (getdate())
)
GO

CREATE TABLE [Comments] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [UserId] int NOT NULL,
  [ArticleId] int NOT NULL,
  [Contents] varchar(255) NOT NULL,
  [Likes] int NOT NULL DEFAULT (0),
  [CreatedAt] datetime NOT NULL DEFAULT (getdate()),
  [UpdatedAt] datetime NOT NULL DEFAULT (getdate())
)
GO

CREATE TABLE [CommentLikes] (
  [UserId] int NOT NULL,
  [CommentId] int NOT NULL,
  [CreatedAt] datetime NOT NULL DEFAULT (getdate()),
  PRIMARY KEY ([UserId], [CommentId])
)
GO

CREATE TABLE [Flowers] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Name] varchar(42) NOT NULL,
  [Language] varchar(255) NOT NULL
)
GO

CREATE TABLE [FlowerGiver] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [FlowerId] int NOT NULL,
  [UserId] int NOT NULL,
  [ArticleId] int NOT NULL,
  [CreatedAt] datetime NOT NULL DEFAULT (getdate())
)
GO

CREATE TABLE [FlowerOwnership] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [UserId] int NOT NULL,
  [Flowerid] int NOT NULL,
  [FlowerCount] int NOT NULL
)
GO

CREATE TABLE [Mail] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Email] varchar(255) NOT NULL,
  [VerificationCode] int NOT NULL,
  [Verified] bit NOT NULL DEFAULT (0),
  [CreatedAt] datetime NOT NULL DEFAULT (getdate()),
  [UpdatedAt] datetime NOT NULL DEFAULT (getdate())
)
GO

ALTER TABLE [Articles] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [Comments] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [Comments] ADD FOREIGN KEY ([ArticleId]) REFERENCES [Articles] ([Id])
GO

ALTER TABLE [CommentLikes] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [CommentLikes] ADD FOREIGN KEY ([CommentId]) REFERENCES [Comments] ([Id])
GO

ALTER TABLE [FlowerGiver] ADD FOREIGN KEY ([FlowerId]) REFERENCES [Flowers] ([Id])
GO

ALTER TABLE [FlowerGiver] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [FlowerGiver] ADD FOREIGN KEY ([ArticleId]) REFERENCES [Articles] ([Id])
GO

ALTER TABLE [FlowerOwnership] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [FlowerOwnership] ADD FOREIGN KEY ([Flowerid]) REFERENCES [Flowers] ([Id])
GO

# SQL 新增預設
INSERT INTO Users
VALUES('Andy','0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089','andy910812@gmail.com','e36168f9-f39f-412e-b622-afb83f727616','Hello','https://localhost:3000/BackgroundPhoto/0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089/image.png','https://localhost:3000/Picture/0xEFa4Abac7FedB8F0514beE7212dc19D523DD3089/image.png','False','2023-04-25 23:41:35.347','2023-04-25 23:54:43.920');

INSERT INTO Flowers
VALUES('玫瑰花','愛情、浪漫、美麗、熱情、溫馨、關懷、感激、勇敢、誠實、溫柔');

INSERT INTO Flowers
VALUES('向日葵','忠誠、陽光、希望、堅強、自信、友善、積極、夢想、幸福、寬容。');

INSERT INTO Flowers
VALUES('康乃馨','真愛、友情、母愛、幸福、忍耐、感激、祝福、美麗、喜悅、寬容');

INSERT INTO Flowers
VALUES('百合花','純潔、寧靜、高雅、祝福、無暇、優雅、美好、真誠、祥和、善良');

INSERT INTO Flowers
VALUES('桃花','愛情、美麗、純真、浪漫、喜悅、甜蜜、吉祥、和諧、幸福、恩愛');

INSERT INTO FlowerOwnership
VALUES('0',0,0,0);

INSERT INTO FlowerOwnership
VALUES('1',0,1,0);

INSERT INTO FlowerOwnership
VALUES('2',0,2,0);

INSERT INTO FlowerOwnership
VALUES('3',0,3,0);

INSERT INTO FlowerOwnership
VALUES('4',0,4,0);