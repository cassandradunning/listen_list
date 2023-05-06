CREATE TABLE [Episode] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Title] varchar(55) NOT NULL,
  [Description] varchar(255) NOT NULL,
  [URL] varchar(255) NOT NULL,
  [Image] varchar(255) NOT NULL
)
GO

CREATE TABLE [EpisodePlaylist] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [EpisodeId] int NOT NULL,
  [PlaylistId] int NOT NULL
)
GO

CREATE TABLE [Playlist] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(55) NOT NULL,
  [Image] varchar(255) NOT NULL,
  [UserProfileId] int NOT NULL
)
GO

CREATE TABLE [UserProfile] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(55) NOT NULL,
  [FirebaseUserId] varchar(28) NOT NULL,
  [Username] varchar(55) NOT NULL,
  [Email] varchar(55) NOT NULL,
  [About] varchar(255) NOT NULL,
  [Image] varchar(255)
)
GO

ALTER TABLE [EpisodePlaylist] ADD FOREIGN KEY ([EpisodeId]) REFERENCES [Episode] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [EpisodePlaylist] ADD FOREIGN KEY ([PlaylistId]) REFERENCES [Playlist] ([Id])
GO

ALTER TABLE [Playlist] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id]) ON DELETE CASCADE
GO
