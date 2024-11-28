-- Turn on IDENTITY_INSERT temporarily to allow auto-increment of SongId
SET IDENTITY_INSERT [dbo].[Songs] ON;

-- Insert 5 rows of data into the Songs table
INSERT INTO [dbo].[Songs] ([SongId], [Title], [Artist])
VALUES (1, 'Shape of You', 'Ed Sheeran'),
       (2, 'Blinding Lights', 'The Weeknd'),
       (3, 'Rolling in the Deep', 'Adele'),
       (4, 'Levitating', 'Dua Lipa'),
       (5, 'Bad Guy', 'Billie Eilish');

-- Turn off IDENTITY_INSERT to return it to normal behavior
SET IDENTITY_INSERT [dbo].[Songs] OFF;
