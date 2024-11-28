-- Insert new playlists into the Playlists table if needed
INSERT INTO [dbo].[Playlists] ([PlaylistId], [PlaylistName])
VALUES (1, 'Rock Classics'),
       (2, 'Top Hits'),
       (3, 'Chill Vibes');


-- Insert data into PlaylistSongs
INSERT INTO [dbo].[PlaylistSongs] ([PlaylistId], [SongId])
VALUES (1, 3),  -- PlaylistId = 1, SongId = 3
       (2, 5),  -- PlaylistId = 2, SongId = 5
       (3, 2),  -- PlaylistId = 3, SongId = 2
       (1, 4),  -- PlaylistId = 1, SongId = 4
       (2, 1);  -- PlaylistId = 2, SongId = 1
