INSERT INTO [Category] ([Name]) VALUES
('Comedy'),
('True Crime'),
('Sports'),
('Politics'),
('Education')

INSERT INTO [UserProfile] ([Name], [FirebaseUserId], [Username], [Email], [Bio], [Image]) VALUES
('John Doe', '5Jjdp9QVLLQwgqJMrAejpe3Frmm2', 'johndoe', 'johndoe@example.com', 'I love podcasts and sports!', 'https://example.com/profile_image.jpg'),
('Jane Smith', 'NkIZ0HIzKOYwNsIgNOPk79lbKBk2', 'jane_smith', 'janesmith@example.com', 'Aspiring journalist and podcast addict', 'https://example.com/profile_image.jpg'),
('Mike Johnson', '1zSRy0XS97UqDrhR9Rc6UHjZ6pE3', 'mikejohn', 'mikejohnson@example.com', 'Just a regular guy who likes podcasts', NULL)

INSERT INTO [Playlist] ([Name], [Image], [UserProfileId]) VALUES
('My Favorite Podcasts', 'https://example.com/playlist_image.jpg', 1),
('True Crime Obsessed', 'https://example.com/playlist_image.jpg', 2),
('Sports Talk', 'https://example.com/playlist_image.jpg', 3),
('News Roundup', 'https://example.com/playlist_image.jpg', 1)

INSERT INTO [Episode] ([Title], [Description], [Image], [URL], [CategoryId]) VALUES
('The Funniest Podcast Ever', 'Laugh out loud with this hilarious comedy podcast', 'https://example.com/episode_image.jpg', 'https://example.com/episode.mp3', 1),
('Murder Mystery', 'A thrilling true crime podcast about unsolved cases', 'https://example.com/episode_image.jpg', 'https://example.com/episode.mp3', 2),
('Sports Talk', 'Join our experts as they analyze the latest games and news in the world of sports', 'https://example.com/episode_image.jpg', 'https://example.com/episode.mp3', 3),
('Political Debate', 'A heated discussion between two experts on a controversial topic', 'https://example.com/episode_image.jpg', 'https://example.com/episode.mp3', 4),
('History Lesson', 'Learn something new with this educational podcast about history', 'https://example.com/episode_image.jpg', 'https://example.com/episode.mp3', 5)

INSERT INTO [EpisodePlaylist] ([EpisodeId], [PlaylistId]) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 1),
(2, 1),
(3, 2)

