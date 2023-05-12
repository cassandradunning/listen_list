CREATE TABLE [Episode] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Title] varchar(55) NOT NULL,
  [Description] varchar(255) NOT NULL,
  [Image] varchar(255) NOT NULL,
  [URL] varchar(255) NOT NULL,
  [CategoryId] int NOT NULL
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
  [Bio] varchar(255) NOT NULL,
  [Image] varchar(255)
)
GO

CREATE TABLE [Category] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(55) NOT NULL
)
GO

ALTER TABLE [EpisodePlaylist] ADD FOREIGN KEY ([EpisodeId]) REFERENCES [Episode] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [EpisodePlaylist] ADD FOREIGN KEY ([PlaylistId]) REFERENCES [Playlist] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [Playlist] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [Episode] ADD FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE
GO



1	John Doe	5Jjdp9QVLLQwgqJMrAejpe3Frmm2	johndoe	johndoe@example.com	I love listening to podcasts!	http://example.com/johndoe.jpg
2	Jane Smith	NkIZ0HIzKOYwNsIgNOPk79lbKBk2	janesmith	janesmith@example.com	I enjoy discovering new podcasts.	http://example.com/janesmith.jpg
3	Mike Johnson	1zSRy0XS97UqDrhR9Rc6UHjZ6pE3	mikejohnson	mikejohnson@example.com	I am a podcast enthusiast.	http://example.com/mikejohnson.jpg
4	Rocky Roll	uoP3JcPROygy71JMo6OJTYAMHF33	rockyroll	rocky@roll.com	lets rock	https://upload.wikimedia.org/wikipedia/en/thumb/3/37/Rocket_J._Squirrel.png/250px-Rocket_J._Squirrel.png
5	jerry	uoP3JcPROygy71JMo6OJTYAMHF33	string	string	string	string
6	Kandy Korner	G7knBGLofYSaIijtKBGfSYISmxO2	kandy	kady@korner.com	i like kandy	https://img.freepik.com/free-vector/lollipop-candy-cartoon-icon-illustration_138676-2675.jpg
7	Brandon Foltz	5dHVbtX5P7cZmY9nsqwhhg5Ufcn2	bfoltz	bfoltz@gmail.com	hi	https://img.freepik.com/free-vector/lollipop-candy-cartoon-icon-illustration_138676-2675.jpg
NULL	NULL	NULL	NULL	NULL	NULL	NULL