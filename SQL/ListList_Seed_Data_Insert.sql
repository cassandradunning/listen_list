
INSERT INTO [Episode] ([Title], [Description], [URL], [Image])
VALUES ('Episode 1', 'Description of Episode 1', 'http://example.com/episode1.mp3', 'http://example.com/episode1.jpg'),
       ('Episode 2', 'Description of Episode 2', 'http://example.com/episode2.mp3', 'http://example.com/episode2.jpg'),
       ('Episode 3', 'Description of Episode 3', 'http://example.com/episode3.mp3', 'http://example.com/episode3.jpg'),
       ('Episode 4', 'Description of Episode 4', 'http://example.com/episode4.mp3', 'http://example.com/episode4.jpg'),
       ('Episode 5', 'Description of Episode 5', 'http://example.com/episode5.mp3', 'http://example.com/episode5.jpg');


INSERT INTO [EpisodePlaylist] ([EpisodeId], [PlaylistId])
VALUES (1, 1),
       (2, 1),
       (3, 1),
       (4, 2),
       (5, 3);


INSERT INTO [Playlist] ([Name], [Image], [EpisodePlaylistId], [UserProfileId])
VALUES ('Favorites', 'http://example.com/favorites.jpg', 1, 1),
       ('To Listen', 'http://example.com/tolisten.jpg', 2, 2),
       ('Top Episodes', 'http://example.com/topepisodes.jpg', 5, 3);


INSERT INTO [UserProfile] ([Name], [FirebaseUserId], [Username], [Email], [About], [Image])
VALUES ('John Doe', '5Jjdp9QVLLQwgqJMrAejpe3Frmm2', 'johndoe', 'johndoe@example.com', 'I love listening to podcasts!', 'http://example.com/johndoe.jpg'),
       ('Jane Smith', 'NkIZ0HIzKOYwNsIgNOPk79lbKBk2', 'janesmith', 'janesmith@example.com', 'I enjoy discovering new podcasts.', 'http://example.com/janesmith.jpg'),
       ('Mike Johnson', '1zSRy0XS97UqDrhR9Rc6UHjZ6pE3', 'mikejohnson', 'mikejohnson@example.com', 'I am a podcast enthusiast.', 'http://example.com/mikejohnson.jpg'),
       ('Rocky Roll','uoP3JcPROygy71JMo6OJTYAMHF33', 'rockyroll', 'rocky@roll.com', 'lets rock', 'https://upload.wikimedia.org/wikipedia/en/thumb/3/37/Rocket_J._Squirrel.png/250px-Rocket_J._Squirrel.png');