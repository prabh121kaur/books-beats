-- Insert 5 rows of data into the Books table
INSERT INTO [dbo].[Books] ([Title], [GenreId])
VALUES 
    ('The Great Gatsby', 1),   -- GenreId = 1 (assuming this corresponds to a valid Genre)
    ('1984', 2),               -- GenreId = 2 (assuming this corresponds to a valid Genre)
    ('To Kill a Mockingbird', 3),  -- GenreId = 3 (assuming this corresponds to a valid Genre)
    ('Moby Dick', 4),          -- GenreId = 4 (assuming this corresponds to a valid Genre)
    ('War and Peace', 5);      -- GenreId = 5 (assuming this corresponds to a valid Genre)
