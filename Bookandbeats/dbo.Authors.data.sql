a-- Turn on IDENTITY_INSERT temporarily to allow auto-increment of AuthorId
SET IDENTITY_INSERT [dbo].[Authors] ON;

-- Insert 5 rows of data into the Authors table
INSERT INTO [dbo].[Authors] ([AuthorId], [FirstName], [LastName])
VALUES (1, 'John', 'Doe'),
       (2, 'Jane', 'Smith'),
       (3, 'Robert', 'Johnson'),
       (4, 'Emily', 'Davis'),
       (5, 'Michael', 'Brown');

-- Turn off IDENTITY_INSERT to return it to normal behavior
SET IDENTITY_INSERT [dbo].[Authors] OFF;
