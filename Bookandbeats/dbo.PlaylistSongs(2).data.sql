-- Insert 5 rows into the PlaylistSongs table
INSERT INTO [dbo].[PlaylistSongs] ([PlaylistId], [SongId])
VALUES 
    (1, 1),    -- PlaylistId 1 contains SongId 1
    (1, 2),    -- PlaylistId 1 contains SongId 2
    (2, 3),    -- PlaylistId 2 contains SongId 3
    (3, 1),    -- PlaylistId 3 contains SongId 1
    (2, 4);    -- PlaylistId 2 contains SongId 4
