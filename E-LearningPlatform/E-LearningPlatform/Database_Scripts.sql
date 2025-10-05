-- =============================================
-- E-Learning Platform Database Setup Scripts
-- =============================================

-- Use the librarydb database (or create a new one if needed)
USE librarydb;
GO

-- Create Courses table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Courses' AND xtype='U')
BEGIN
    CREATE TABLE Courses (
        CourseID NVARCHAR(10) PRIMARY KEY,
        CourseCode NVARCHAR(20) NOT NULL UNIQUE,
        CourseName NVARCHAR(100) NOT NULL,
        Semester NVARCHAR(50) NOT NULL,
        Credits INT NOT NULL CHECK (Credits >= 1 AND Credits <= 10),
        Instructor NVARCHAR(100) NOT NULL,
        Prerequisites NVARCHAR(500),
        Department NVARCHAR(50) NOT NULL,
        Room NVARCHAR(20),
        MaxStudents INT DEFAULT 0,
        Status NVARCHAR(20) DEFAULT 'Active' CHECK (Status IN ('Active', 'Inactive')),
        CreatedDate DATETIME2 DEFAULT GETDATE(),
        ModifiedDate DATETIME2 DEFAULT GETDATE()
    );
    
    PRINT 'Courses table created successfully.';
END
ELSE
BEGIN
    PRINT 'Courses table already exists.';
END
GO

-- Create indexes for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Courses_CourseCode')
BEGIN
    CREATE INDEX IX_Courses_CourseCode ON Courses(CourseCode);
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Courses_Semester')
BEGIN
    CREATE INDEX IX_Courses_Semester ON Courses(Semester);
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Courses_Department')
BEGIN
    CREATE INDEX IX_Courses_Department ON Courses(Department);
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Courses_Status')
BEGIN
    CREATE INDEX IX_Courses_Status ON Courses(Status);
END
GO


-- Create stored procedures for common operations

-- Stored procedure to get all courses
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetAllCourses')
    DROP PROCEDURE sp_GetAllCourses;
GO

CREATE PROCEDURE sp_GetAllCourses
AS
BEGIN
    SELECT CourseID, CourseCode, CourseName, Semester, Credits, Instructor, 
           Prerequisites, Department, Room, MaxStudents, Status, CreatedDate, ModifiedDate
    FROM Courses
    ORDER BY CourseCode;
END
GO

-- Stored procedure to get course by ID
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetCourseByID')
    DROP PROCEDURE sp_GetCourseByID;
GO

CREATE PROCEDURE sp_GetCourseByID
    @CourseID NVARCHAR(10)
AS
BEGIN
    SELECT CourseID, CourseCode, CourseName, Semester, Credits, Instructor, 
           Prerequisites, Department, Room, MaxStudents, Status, CreatedDate, ModifiedDate
    FROM Courses
    WHERE CourseID = @CourseID;
END
GO

-- Stored procedure to get course by code
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetCourseByCode')
    DROP PROCEDURE sp_GetCourseByCode;
GO

CREATE PROCEDURE sp_GetCourseByCode
    @CourseCode NVARCHAR(20)
AS
BEGIN
    SELECT CourseID, CourseCode, CourseName, Semester, Credits, Instructor, 
           Prerequisites, Department, Room, MaxStudents, Status, CreatedDate, ModifiedDate
    FROM Courses
    WHERE CourseCode = @CourseCode;
END
GO

-- Stored procedure to insert new course
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_InsertCourse')
    DROP PROCEDURE sp_InsertCourse;
GO

CREATE PROCEDURE sp_InsertCourse
    @CourseID NVARCHAR(10),
    @CourseCode NVARCHAR(20),
    @CourseName NVARCHAR(100),
    @Semester NVARCHAR(50),
    @Credits INT,
    @Instructor NVARCHAR(100),
    @Prerequisites NVARCHAR(500) = NULL,
    @Department NVARCHAR(50),
    @Room NVARCHAR(20) = NULL,
    @MaxStudents INT = 0,
    @Status NVARCHAR(20) = 'Active'
AS
BEGIN
    BEGIN TRY
        INSERT INTO Courses (CourseID, CourseCode, CourseName, Semester, Credits, Instructor, 
                           Prerequisites, Department, Room, MaxStudents, Status)
        VALUES (@CourseID, @CourseCode, @CourseName, @Semester, @Credits, @Instructor, 
                @Prerequisites, @Department, @Room, @MaxStudents, @Status);
        
        SELECT 'SUCCESS' AS Result, 'Course inserted successfully.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT 'ERROR' AS Result, ERROR_MESSAGE() AS Message;
    END CATCH
END
GO

-- Stored procedure to update course
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_UpdateCourse')
    DROP PROCEDURE sp_UpdateCourse;
GO

CREATE PROCEDURE sp_UpdateCourse
    @CourseID NVARCHAR(10),
    @CourseCode NVARCHAR(20),
    @CourseName NVARCHAR(100),
    @Semester NVARCHAR(50),
    @Credits INT,
    @Instructor NVARCHAR(100),
    @Prerequisites NVARCHAR(500) = NULL,
    @Department NVARCHAR(50),
    @Room NVARCHAR(20) = NULL,
    @MaxStudents INT = 0,
    @Status NVARCHAR(20) = 'Active'
AS
BEGIN
    BEGIN TRY
        UPDATE Courses 
        SET CourseCode = @CourseCode,
            CourseName = @CourseName,
            Semester = @Semester,
            Credits = @Credits,
            Instructor = @Instructor,
            Prerequisites = @Prerequisites,
            Department = @Department,
            Room = @Room,
            MaxStudents = @MaxStudents,
            Status = @Status,
            ModifiedDate = GETDATE()
        WHERE CourseID = @CourseID;
        
        IF @@ROWCOUNT > 0
            SELECT 'SUCCESS' AS Result, 'Course updated successfully.' AS Message;
        ELSE
            SELECT 'ERROR' AS Result, 'Course not found.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT 'ERROR' AS Result, ERROR_MESSAGE() AS Message;
    END CATCH
END
GO

-- Stored procedure to delete course
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_DeleteCourse')
    DROP PROCEDURE sp_DeleteCourse;
GO

CREATE PROCEDURE sp_DeleteCourse
    @CourseID NVARCHAR(10)
AS
BEGIN
    BEGIN TRY
        DELETE FROM Courses WHERE CourseID = @CourseID;
        
        IF @@ROWCOUNT > 0
            SELECT 'SUCCESS' AS Result, 'Course deleted successfully.' AS Message;
        ELSE
            SELECT 'ERROR' AS Result, 'Course not found.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT 'ERROR' AS Result, ERROR_MESSAGE() AS Message;
    END CATCH
END
GO

-- Stored procedure to search courses with filters
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_SearchCourses')
    DROP PROCEDURE sp_SearchCourses;
GO

CREATE PROCEDURE sp_SearchCourses
    @SearchTerm NVARCHAR(100) = NULL,
    @Semester NVARCHAR(50) = NULL,
    @Department NVARCHAR(50) = NULL,
    @Status NVARCHAR(20) = NULL
AS
BEGIN
    SELECT CourseID, CourseCode, CourseName, Semester, Credits, Instructor, 
           Prerequisites, Department, Room, MaxStudents, Status, CreatedDate, ModifiedDate
    FROM Courses
    WHERE (@SearchTerm IS NULL OR 
           CourseCode LIKE '%' + @SearchTerm + '%' OR 
           CourseName LIKE '%' + @SearchTerm + '%' OR 
           Instructor LIKE '%' + @SearchTerm + '%')
    AND (@Semester IS NULL OR Semester = @Semester)
    AND (@Department IS NULL OR Department = @Department)
    AND (@Status IS NULL OR Status = @Status)
    ORDER BY CourseCode;
END
GO

-- Create trigger to update ModifiedDate automatically
IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'tr_Courses_UpdateModifiedDate')
    DROP TRIGGER tr_Courses_UpdateModifiedDate;
GO

CREATE TRIGGER tr_Courses_UpdateModifiedDate
ON Courses
AFTER UPDATE
AS
BEGIN
    UPDATE Courses 
    SET ModifiedDate = GETDATE()
    WHERE CourseID IN (SELECT CourseID FROM inserted);
END
GO

PRINT 'Database setup completed successfully!';
PRINT 'Tables, indexes, stored procedures, and triggers have been created.';
PRINT 'You can now run your C# application with database integration.';
