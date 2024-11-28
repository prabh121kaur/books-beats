-- Insert 5 rows of data into the Playlists table
INSERT INTO [dbo].[Playlists] ([Name], [Mood], [PrivacySetting])
VALUES 
    ('Summer Vibes', 'Happy', 'Public'),    -- PlaylistId will auto-increment
    ('Chill Beats', 'Relaxed', 'Private'),   -- PlaylistId will auto-increment
    ('Workout Hits', 'Energetic', 'Public'), -- PlaylistId will auto-increment
    ('Sad Songs', 'Sad', 'Private'),         -- PlaylistId will auto-increment
    ('Road Trip Tunes', 'Adventurous', 'Public'); -- PlaylistId will auto-increment
