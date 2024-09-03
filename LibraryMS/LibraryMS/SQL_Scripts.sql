-- Create database
IF DB_ID('HospitalMS') IS NULL
BEGIN
    CREATE DATABASE HospitalMS;
END
GO

USE HospitalMS;
GO

-- Roles table (optional enumerations)
IF OBJECT_ID('dbo.Roles','U') IS NULL
BEGIN
    CREATE TABLE dbo.Roles (
        RoleId INT IDENTITY(1,1) PRIMARY KEY,
        RoleName NVARCHAR(50) NOT NULL UNIQUE -- Admin, Doctor, Patient
    );
END
GO

-- Users table (Patients, Doctors, Admins)
IF OBJECT_ID('dbo.Users','U') IS NULL
BEGIN
    CREATE TABLE dbo.Users (
        UserId INT IDENTITY(1,1) PRIMARY KEY,
        FullName NVARCHAR(100) NOT NULL,
        Username NVARCHAR(50) NOT NULL UNIQUE,
        Email NVARCHAR(100) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(200) NOT NULL, -- store hashed password in production
        RoleName NVARCHAR(50) NOT NULL CHECK (RoleName IN ('Admin','Doctor','Patient')),
        Phone NVARCHAR(20) NULL,
        Address NVARCHAR(200) NULL,
        DateOfBirth DATE NULL,
        Specialization NVARCHAR(100) NULL, -- For doctors
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Users_CreatedAt DEFAULT (SYSUTCDATETIME())
    );
END
GO

-- Medical Records table (replaces Books)
IF OBJECT_ID('dbo.MedicalRecords','U') IS NULL
BEGIN
    CREATE TABLE dbo.MedicalRecords (
        RecordId INT IDENTITY(1,1) PRIMARY KEY,
        PatientId INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(UserId),
        DoctorId INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(UserId),
        Diagnosis NVARCHAR(200) NOT NULL,
        Treatment NVARCHAR(300) NULL,
        Prescription NVARCHAR(300) NULL,
        Notes NVARCHAR(500) NULL,
        RecordDate DATETIME2 NOT NULL CONSTRAINT DF_MedicalRecords_RecordDate DEFAULT (SYSUTCDATETIME()),
        Status NVARCHAR(20) NOT NULL DEFAULT 'Active' -- Active, Archived, Deleted
    );
END
GO

-- Appointments table (replaces BorrowRecords)
IF OBJECT_ID('dbo.Appointments','U') IS NULL
BEGIN
    CREATE TABLE dbo.Appointments (
        AppointmentId INT IDENTITY(1,1) PRIMARY KEY,
        PatientId INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(UserId),
        DoctorId INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(UserId),
        AppointmentDate DATETIME2 NOT NULL,
        AppointmentTime TIME NOT NULL,
        Duration INT NOT NULL DEFAULT 30, -- Duration in minutes
        Status NVARCHAR(20) NOT NULL DEFAULT 'Scheduled', -- Scheduled, Completed, Cancelled, NoShow
        Notes NVARCHAR(300) NULL,
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Appointments_CreatedAt DEFAULT (SYSUTCDATETIME())
    );
END
GO

-- Seed roles
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE RoleName='Admin') INSERT dbo.Roles(RoleName) VALUES('Admin');
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE RoleName='Doctor') INSERT dbo.Roles(RoleName) VALUES('Doctor');
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE RoleName='Patient') INSERT dbo.Roles(RoleName) VALUES('Patient');
GO

-- Seed main admin Alain
IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Username='Alain')
BEGIN
    INSERT dbo.Users(FullName, Username, Email, PasswordHash, RoleName)
    VALUES('Alain','Alain','alain@example.com','Alain2003','Admin'); -- In production store a hash
END
GO

-- Authentication: Login
IF OBJECT_ID('dbo.sp_Auth_Login','P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Auth_Login;
GO
CREATE PROCEDURE dbo.sp_Auth_Login
    @Username NVARCHAR(50),
    @Password NVARCHAR(200),
    @DesiredRole NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @FoundRole NVARCHAR(50);
    DECLARE @Success BIT = 0;
    DECLARE @Message NVARCHAR(200) = NULL;

    SELECT TOP 1 @FoundRole = RoleName
    FROM dbo.Users
    WHERE Username = @Username AND PasswordHash = @Password;

    IF @FoundRole IS NULL
    BEGIN
        SET @Success = 0;
        SET @Message = N'Invalid username or password.';
    END
    ELSE IF @DesiredRole IS NOT NULL AND @DesiredRole <> '' AND @DesiredRole <> 'User' AND @FoundRole <> @DesiredRole
    BEGIN
        SET @Success = 0;
        SET @Message = N'You do not have the required staff role.';
    END
    ELSE
    BEGIN
        SET @Success = 1;
        SET @Message = N'OK';
    END

    SELECT @Success AS Success, ISNULL(@FoundRole,'') AS Role, @Message AS [Message];
END
GO

-- Authentication: Create User
IF OBJECT_ID('dbo.sp_Auth_CreateUser','P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Auth_CreateUser;
GO
CREATE PROCEDURE dbo.sp_Auth_CreateUser
    @FullName NVARCHAR(100),
    @Username NVARCHAR(50),
    @Email NVARCHAR(100),
    @Password NVARCHAR(200),
    @Role NVARCHAR(50),
    @Ok BIT OUTPUT,
    @Message NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @Ok = 0;
    SET @Message = NULL;

    IF @Role NOT IN ('Patient','Doctor')
    BEGIN
        SET @Message = N'Role must be Patient or Doctor.';
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM dbo.Users WHERE Username=@Username OR Email=@Email)
    BEGIN
        SET @Message = N'Username or Email already exists.';
        RETURN;
    END

    INSERT dbo.Users(FullName, Username, Email, PasswordHash, RoleName)
    VALUES(@FullName, @Username, @Email, @Password, @Role);

    SET @Ok = 1;
    SET @Message = N'Created';
END
GO

-- Hospital Management: Get appointments by doctor
IF OBJECT_ID('dbo.sp_GetAppointmentByDoctor','P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetAppointmentByDoctor;
GO
CREATE PROCEDURE dbo.sp_GetAppointmentByDoctor
    @DoctorUsername NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT a.AppointmentId, p.FullName AS PatientName, a.AppointmentDate, a.AppointmentTime, 
           a.Duration, a.Status, a.Notes
    FROM dbo.Appointments a
    JOIN dbo.Users d ON d.UserId = a.DoctorId
    JOIN dbo.Users p ON p.UserId = a.PatientId
    WHERE d.Username = @DoctorUsername
    ORDER BY a.AppointmentDate DESC, a.AppointmentTime DESC;
END
GO

-- Hospital Management: Get detailed appointment information
IF OBJECT_ID('dbo.sp_DetailAppointment','P') IS NOT NULL
    DROP PROCEDURE dbo.sp_DetailAppointment;
GO
CREATE PROCEDURE dbo.sp_DetailAppointment
    @AppointmentId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT a.AppointmentId, 
           p.FullName AS PatientName, p.Email AS PatientEmail, p.Phone AS PatientPhone,
           d.FullName AS DoctorName, d.Specialization,
           a.AppointmentDate, a.AppointmentTime, a.Duration, a.Status, a.Notes,
           mr.Diagnosis, mr.Treatment, mr.Prescription
    FROM dbo.Appointments a
    JOIN dbo.Users p ON p.UserId = a.PatientId
    JOIN dbo.Users d ON d.UserId = a.DoctorId
    LEFT JOIN dbo.MedicalRecords mr ON mr.PatientId = a.PatientId AND mr.DoctorId = a.DoctorId
    WHERE a.AppointmentId = @AppointmentId;
END
GO

-- Hospital Management: Get patient history
IF OBJECT_ID('dbo.sp_GetPatientHistory','P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetPatientHistory;
GO
CREATE PROCEDURE dbo.sp_GetPatientHistory
    @PatientUsername NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT mr.RecordId, d.FullName AS DoctorName, d.Specialization,
           mr.Diagnosis, mr.Treatment, mr.Prescription, mr.Notes, mr.RecordDate, mr.Status
    FROM dbo.MedicalRecords mr
    JOIN dbo.Users p ON p.UserId = mr.PatientId
    JOIN dbo.Users d ON d.UserId = mr.DoctorId
    WHERE p.Username = @PatientUsername
    ORDER BY mr.RecordDate DESC;
END
GO

-- Hospital Management: Get overdue appointments (missed appointments)
IF OBJECT_ID('dbo.sp_GetOverdueAppointments','P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetOverdueAppointments;
GO
CREATE PROCEDURE dbo.sp_GetOverdueAppointments
AS
BEGIN
    SET NOCOUNT ON;
    SELECT a.AppointmentId, p.FullName AS PatientName, d.FullName AS DoctorName, 
           a.AppointmentDate, a.AppointmentTime, a.Status
    FROM dbo.Appointments a
    JOIN dbo.Users p ON p.UserId = a.PatientId
    JOIN dbo.Users d ON d.UserId = a.DoctorId
    WHERE a.Status = 'NoShow' OR (a.Status = 'Scheduled' AND a.AppointmentDate < CAST(GETDATE() AS DATE));
END
GO

-- Hospital Management: Get most active patients
IF OBJECT_ID('dbo.sp_GetMostActivePatients','P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetMostActivePatients;
GO
CREATE PROCEDURE dbo.sp_GetMostActivePatients
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 10 p.UserId, p.FullName AS PatientName, COUNT(*) AS AppointmentCount
    FROM dbo.Appointments a
    JOIN dbo.Users p ON p.UserId = a.PatientId
    GROUP BY p.UserId, p.FullName
    ORDER BY AppointmentCount DESC;
END
GO

-- Summary counts for Home panel
IF OBJECT_ID('dbo.sp_GetDashboardSummary','P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetDashboardSummary;
GO
CREATE PROCEDURE dbo.sp_GetDashboardSummary
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        (SELECT COUNT(*) FROM dbo.Users WHERE RoleName = 'Patient') AS TotalPatients,
        (SELECT COUNT(*) FROM dbo.Users WHERE RoleName = 'Doctor') AS TotalDoctors,
        (SELECT COUNT(*) FROM dbo.Appointments WHERE Status = 'Scheduled') AS ScheduledAppointments;
END
GO

-- Schedule an appointment
IF OBJECT_ID('dbo.sp_ScheduleAppointment','P') IS NOT NULL
    DROP PROCEDURE dbo.sp_ScheduleAppointment;
GO
CREATE PROCEDURE dbo.sp_ScheduleAppointment
    @PatientUsername NVARCHAR(50),
    @DoctorUsername NVARCHAR(50),
    @AppointmentDate DATETIME2,
    @AppointmentTime TIME,
    @Duration INT = 30,
    @Notes NVARCHAR(300) = NULL,
    @Ok BIT OUTPUT,
    @Message NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @Ok = 0; SET @Message = NULL;

    DECLARE @PatientId INT, @DoctorId INT;
    SELECT @PatientId = UserId FROM dbo.Users WHERE Username=@PatientUsername AND RoleName='Patient';
    IF @PatientId IS NULL BEGIN SET @Message=N'Patient not found.'; RETURN; END

    SELECT @DoctorId = UserId FROM dbo.Users WHERE Username=@DoctorUsername AND RoleName='Doctor';
    IF @DoctorId IS NULL BEGIN SET @Message=N'Doctor not found.'; RETURN; END

    -- Check for conflicts
    IF EXISTS (SELECT 1 FROM dbo.Appointments 
               WHERE DoctorId=@DoctorId AND AppointmentDate=@AppointmentDate 
               AND AppointmentTime=@AppointmentTime AND Status='Scheduled')
    BEGIN
        SET @Message=N'Doctor has a conflicting appointment at this time.'; RETURN;
    END

    INSERT dbo.Appointments(PatientId, DoctorId, AppointmentDate, AppointmentTime, Duration, Notes)
    VALUES(@PatientId, @DoctorId, @AppointmentDate, @AppointmentTime, @Duration, @Notes);

    SET @Ok = 1; SET @Message=N'Appointment scheduled';
END
GO

-- Complete an appointment
IF OBJECT_ID('dbo.sp_CompleteAppointment','P') IS NOT NULL
    DROP PROCEDURE dbo.sp_CompleteAppointment;
GO
CREATE PROCEDURE dbo.sp_CompleteAppointment
    @AppointmentId INT,
    @Diagnosis NVARCHAR(200),
    @Treatment NVARCHAR(300) = NULL,
    @Prescription NVARCHAR(300) = NULL,
    @Notes NVARCHAR(500) = NULL,
    @Ok BIT OUTPUT,
    @Message NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @Ok = 0; SET @Message = NULL;

    DECLARE @PatientId INT, @DoctorId INT;
    SELECT @PatientId = PatientId, @DoctorId = DoctorId 
    FROM dbo.Appointments 
    WHERE AppointmentId=@AppointmentId AND Status='Scheduled';
    
    IF @PatientId IS NULL BEGIN SET @Message=N'Appointment not found or already completed.'; RETURN; END

    -- Update appointment status
    UPDATE dbo.Appointments SET Status='Completed' WHERE AppointmentId=@AppointmentId;

    -- Create medical record
    INSERT dbo.MedicalRecords(PatientId, DoctorId, Diagnosis, Treatment, Prescription, Notes)
    VALUES(@PatientId, @DoctorId, @Diagnosis, @Treatment, @Prescription, @Notes);

    SET @Ok = 1; SET @Message=N'Appointment completed and medical record created';
END
GO

-- Appointment Requests (replaces BorrowRequests)
IF OBJECT_ID('dbo.AppointmentRequests','U') IS NULL
BEGIN
    CREATE TABLE dbo.AppointmentRequests (
        RequestId INT IDENTITY(1,1) PRIMARY KEY,
        PatientId INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(UserId),
        DoctorId INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(UserId),
        RequestedDate DATETIME2 NOT NULL,
        RequestedTime TIME NOT NULL,
        Duration INT NOT NULL DEFAULT 30,
        Notes NVARCHAR(300) NULL,
        RequestedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        Status NVARCHAR(20) NOT NULL DEFAULT 'Pending' -- Pending, Approved, Rejected
    );
END
GO

-- Request an appointment
IF OBJECT_ID('dbo.sp_RequestAppointment','P') IS NOT NULL DROP PROCEDURE dbo.sp_RequestAppointment;
GO
CREATE PROCEDURE dbo.sp_RequestAppointment
    @PatientUsername NVARCHAR(50),
    @DoctorUsername NVARCHAR(50),
    @RequestedDate DATETIME2,
    @RequestedTime TIME,
    @Duration INT = 30,
    @Notes NVARCHAR(300) = NULL,
    @Ok BIT OUTPUT,
    @Message NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET @Ok=0; SET @Message=NULL;
    
    DECLARE @PatientId INT, @DoctorId INT;
    SELECT @PatientId = UserId FROM dbo.Users WHERE Username=@PatientUsername AND RoleName='Patient';
    IF @PatientId IS NULL BEGIN SET @Message=N'Patient not found'; RETURN; END
    
    SELECT @DoctorId = UserId FROM dbo.Users WHERE Username=@DoctorUsername AND RoleName='Doctor';
    IF @DoctorId IS NULL BEGIN SET @Message=N'Doctor not found'; RETURN; END
    
    INSERT dbo.AppointmentRequests(PatientId, DoctorId, RequestedDate, RequestedTime, Duration, Notes) 
    VALUES(@PatientId, @DoctorId, @RequestedDate, @RequestedTime, @Duration, @Notes);
    SET @Ok=1; SET @Message=N'Appointment request submitted';
END
GO

-- List appointment requests
IF OBJECT_ID('dbo.sp_ListAppointmentRequests','P') IS NOT NULL DROP PROCEDURE dbo.sp_ListAppointmentRequests;
GO
CREATE PROCEDURE dbo.sp_ListAppointmentRequests
AS
BEGIN
    SET NOCOUNT ON;
    SELECT r.RequestId, p.FullName AS PatientName, d.FullName AS DoctorName, 
           r.RequestedDate, r.RequestedTime, r.Duration, r.Notes, r.RequestedAt, r.Status
    FROM dbo.AppointmentRequests r
    JOIN dbo.Users p ON p.UserId=r.PatientId
    JOIN dbo.Users d ON d.UserId=r.DoctorId
    WHERE r.Status='Pending'
    ORDER BY r.RequestedAt DESC;
END
GO

-- Approve appointment request
IF OBJECT_ID('dbo.sp_ApproveAppointment','P') IS NOT NULL DROP PROCEDURE dbo.sp_ApproveAppointment;
GO
CREATE PROCEDURE dbo.sp_ApproveAppointment
    @RequestId INT,
    @Ok BIT OUTPUT,
    @Message NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET @Ok=0; SET @Message=NULL;
    DECLARE @PatientId INT, @DoctorId INT, @RequestedDate DATETIME2, @RequestedTime TIME, @Duration INT, @Notes NVARCHAR(300);
    SELECT @PatientId=PatientId, @DoctorId=DoctorId, @RequestedDate=RequestedDate, @RequestedTime=RequestedTime, @Duration=Duration, @Notes=Notes 
    FROM dbo.AppointmentRequests WHERE RequestId=@RequestId AND Status='Pending';
    IF @PatientId IS NULL BEGIN SET @Message=N'Request not found'; RETURN; END

    -- Check for conflicts
    IF EXISTS (SELECT 1 FROM dbo.Appointments 
               WHERE DoctorId=@DoctorId AND AppointmentDate=@RequestedDate 
               AND AppointmentTime=@RequestedTime AND Status='Scheduled')
    BEGIN
        SET @Message = N'Doctor has a conflicting appointment at this time.'; RETURN;
    END

    -- Create the appointment
    INSERT dbo.Appointments(PatientId, DoctorId, AppointmentDate, AppointmentTime, Duration, Notes) 
    VALUES(@PatientId, @DoctorId, @RequestedDate, @RequestedTime, @Duration, @Notes);
    UPDATE dbo.AppointmentRequests SET Status='Approved' WHERE RequestId=@RequestId;
    SET @Ok=1; SET @Message=N'Appointment approved';
END
GO

-- Reject appointment request
IF OBJECT_ID('dbo.sp_RejectAppointment','P') IS NOT NULL DROP PROCEDURE dbo.sp_RejectAppointment;
GO
CREATE PROCEDURE dbo.sp_RejectAppointment
    @RequestId INT,
    @Ok BIT OUTPUT,
    @Message NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET @Ok=0; SET @Message=NULL;
    SELECT @RequestId FROM dbo.AppointmentRequests WHERE RequestId=@RequestId AND Status='Pending';
    IF @RequestId IS NULL BEGIN SET @Message=N'Request not found or already processed'; RETURN; END

    UPDATE dbo.AppointmentRequests SET Status='Rejected' WHERE RequestId=@RequestId;
    SET @Ok=1; SET @Message=N'Appointment request rejected';
END
GO

-- Patient cancels own pending appointment request
IF OBJECT_ID('dbo.sp_CancelAppointmentRequest','P') IS NOT NULL DROP PROCEDURE dbo.sp_CancelAppointmentRequest;
GO
CREATE PROCEDURE dbo.sp_CancelAppointmentRequest
    @PatientUsername NVARCHAR(50),
    @RequestId INT,
    @Ok BIT OUTPUT,
    @Message NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET @Ok=0; SET @Message=NULL;
    DECLARE @PatientId INT;
    SELECT @PatientId = UserId FROM dbo.Users WHERE Username=@PatientUsername AND RoleName='Patient';
    IF @PatientId IS NULL BEGIN SET @Message=N'Patient not found'; RETURN; END
    SELECT @RequestId FROM dbo.AppointmentRequests WHERE RequestId=@RequestId AND PatientId=@PatientId AND Status='Pending';
    IF @RequestId IS NULL BEGIN SET @Message=N'Request not found or already processed'; RETURN; END

    UPDATE dbo.AppointmentRequests SET Status='Rejected' WHERE RequestId=@RequestId;
    SET @Ok=1; SET @Message=N'Appointment request canceled';
END
GO

-- Medical Records CRUD
IF OBJECT_ID('dbo.sp_MedicalRecords_List','P') IS NOT NULL DROP PROCEDURE dbo.sp_MedicalRecords_List;
GO
CREATE PROCEDURE dbo.sp_MedicalRecords_List
AS
BEGIN
    SET NOCOUNT ON;
    SELECT mr.RecordId, p.FullName AS PatientName, d.FullName AS DoctorName, 
           mr.Diagnosis, mr.Treatment, mr.Prescription, mr.RecordDate, mr.Status
    FROM dbo.MedicalRecords mr
    JOIN dbo.Users p ON p.UserId = mr.PatientId
    JOIN dbo.Users d ON d.UserId = mr.DoctorId
    ORDER BY mr.RecordDate DESC;
END
GO

-- Users CRUD (Admin)
IF OBJECT_ID('dbo.sp_Users_List','P') IS NOT NULL DROP PROCEDURE dbo.sp_Users_List;
GO
CREATE PROCEDURE dbo.sp_Users_List
AS
BEGIN
    SET NOCOUNT ON;
    SELECT UserId, FullName, Username, Email, RoleName, Phone, Address, DateOfBirth, Specialization, CreatedAt 
    FROM dbo.Users ORDER BY Username;
END
GO

IF OBJECT_ID('dbo.sp_Users_Add','P') IS NOT NULL DROP PROCEDURE dbo.sp_Users_Add;
GO
CREATE PROCEDURE dbo.sp_Users_Add
    @FullName NVARCHAR(100),
    @Username NVARCHAR(50),
    @Email NVARCHAR(100),
    @Password NVARCHAR(200),
    @Role NVARCHAR(50),
    @Phone NVARCHAR(20) = NULL,
    @Address NVARCHAR(200) = NULL,
    @DateOfBirth DATE = NULL,
    @Specialization NVARCHAR(100) = NULL,
    @Ok BIT OUTPUT,
    @Message NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET @Ok=0; SET @Message=NULL;
    IF @Role NOT IN('Admin','Doctor','Patient') BEGIN SET @Message=N'Invalid role'; RETURN; END
    IF EXISTS (SELECT 1 FROM dbo.Users WHERE Username=@Username OR Email=@Email) BEGIN SET @Message=N'Username or Email exists'; RETURN; END
    INSERT dbo.Users(FullName, Username, Email, PasswordHash, RoleName, Phone, Address, DateOfBirth, Specialization)
    VALUES(@FullName,@Username,@Email,@Password,@Role,@Phone,@Address,@DateOfBirth,@Specialization);
    SET @Ok=1; SET @Message=N'Created';
END
GO

IF OBJECT_ID('dbo.sp_Users_Update','P') IS NOT NULL DROP PROCEDURE dbo.sp_Users_Update;
GO
CREATE PROCEDURE dbo.sp_Users_Update
    @UserId INT,
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100),
    @Role NVARCHAR(50),
    @Phone NVARCHAR(20) = NULL,
    @Address NVARCHAR(200) = NULL,
    @DateOfBirth DATE = NULL,
    @Specialization NVARCHAR(100) = NULL,
    @Ok BIT OUTPUT,
    @Message NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET @Ok=0; SET @Message=NULL;
    IF @Role NOT IN('Admin','Doctor','Patient') BEGIN SET @Message=N'Invalid role'; RETURN; END
    -- Prevent demotion of main admin Alain
    IF EXISTS (SELECT 1 FROM dbo.Users WHERE UserId=@UserId AND Username='Alain' AND @Role<>'Admin')
    BEGIN
        SET @Message = N'Cannot change main admin role.'; RETURN;
    END

    UPDATE dbo.Users SET FullName=@FullName, Email=@Email, RoleName=@Role, Phone=@Phone, Address=@Address, DateOfBirth=@DateOfBirth, Specialization=@Specialization WHERE UserId=@UserId;
    IF @@ROWCOUNT=0 BEGIN SET @Message=N'User not found'; RETURN; END
    SET @Ok=1; SET @Message=N'Updated';
END
GO

IF OBJECT_ID('dbo.sp_Users_Delete','P') IS NOT NULL DROP PROCEDURE dbo.sp_Users_Delete;
GO
CREATE PROCEDURE dbo.sp_Users_Delete
    @UserId INT,
    @Ok BIT OUTPUT,
    @Message NVARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON; SET @Ok=0; SET @Message=NULL;
    DELETE FROM dbo.Users WHERE UserId=@UserId;
    IF @@ROWCOUNT=0 BEGIN SET @Message=N'User not found'; RETURN; END
    SET @Ok=1; SET @Message=N'Deleted';
END
GO

IF OBJECT_ID('dbo.sp_MedicalRecords_Add','P') IS NOT NULL DROP PROCEDURE dbo.sp_MedicalRecords_Add;
GO
CREATE PROCEDURE dbo.sp_MedicalRecords_Add
    @PatientId INT,
    @DoctorId INT,
    @Diagnosis NVARCHAR(200),
    @Treatment NVARCHAR(300) = NULL,
    @Prescription NVARCHAR(300) = NULL,
    @Notes NVARCHAR(500) = NULL,
    @NewId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.MedicalRecords(PatientId, DoctorId, Diagnosis, Treatment, Prescription, Notes)
    VALUES(@PatientId, @DoctorId, @Diagnosis, @Treatment, @Prescription, @Notes);
    SET @NewId = SCOPE_IDENTITY();
END
GO

IF OBJECT_ID('dbo.sp_MedicalRecords_Update','P') IS NOT NULL DROP PROCEDURE dbo.sp_MedicalRecords_Update;
GO
CREATE PROCEDURE dbo.sp_MedicalRecords_Update
    @RecordId INT,
    @Diagnosis NVARCHAR(200),
    @Treatment NVARCHAR(300),
    @Prescription NVARCHAR(300),
    @Notes NVARCHAR(500),
    @Status NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.MedicalRecords
    SET Diagnosis=@Diagnosis, Treatment=@Treatment, Prescription=@Prescription, Notes=@Notes, Status=@Status
    WHERE RecordId=@RecordId;
END
GO

IF OBJECT_ID('dbo.sp_MedicalRecords_Delete','P') IS NOT NULL DROP PROCEDURE dbo.sp_MedicalRecords_Delete;
GO
CREATE PROCEDURE dbo.sp_MedicalRecords_Delete
    @RecordId INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.MedicalRecords WHERE RecordId=@RecordId;
END
GO


