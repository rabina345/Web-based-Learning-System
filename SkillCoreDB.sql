/* ============================================================================
   SkillCore Database
   CT050-3-2-WAPP — Group G

   Run this in SSMS against (localdb)\MSSQLLocalDB. Safe to re-run any time
   you want to reset — it drops and recreates the whole database.
   ========================================================================= */

USE master;
GO

IF DB_ID('SkillCoreDB') IS NOT NULL
BEGIN
    ALTER DATABASE SkillCoreDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SkillCoreDB;
END
GO

CREATE DATABASE SkillCoreDB;
GO

USE SkillCoreDB;
GO

/* ============================================================================
   1. ROLES  — just two: Admin and Member
   ========================================================================= */
CREATE TABLE Roles (
    RoleID      INT IDENTITY(1,1) NOT NULL,
    RoleName    NVARCHAR(30)  NOT NULL,
    CONSTRAINT PK_Roles PRIMARY KEY (RoleID),
    CONSTRAINT UQ_Roles_RoleName UNIQUE (RoleName)
);
GO

/* ============================================================================
   2. USERS
   ========================================================================= */
CREATE TABLE Users (
    UserID       INT IDENTITY(1,1) NOT NULL,
    FullName     NVARCHAR(100) NOT NULL,
    Email        NVARCHAR(150) NOT NULL,
    PasswordHash CHAR(64)      NOT NULL,   -- SHA-256 hex
    PasswordSalt CHAR(32)      NOT NULL,   -- random salt, hex
    RoleID       INT           NOT NULL,
    IsActive     BIT           NOT NULL DEFAULT 1,
    CreatedAt    DATETIME2(0)  NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_Users PRIMARY KEY (UserID),
    CONSTRAINT UQ_Users_Email UNIQUE (Email),
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);
GO

/* ============================================================================
   3. CATEGORIES  — programming topic areas
   ========================================================================= */
CREATE TABLE Categories (
    CategoryID   INT IDENTITY(1,1) NOT NULL,
    CategoryName NVARCHAR(60)  NOT NULL,
    Description  NVARCHAR(250) NULL,
    CONSTRAINT PK_Categories PRIMARY KEY (CategoryID),
    CONSTRAINT UQ_Categories_Name UNIQUE (CategoryName)
);
GO

/* ============================================================================
   4. COURSES
   ========================================================================= */
CREATE TABLE Courses (
    CourseID          INT IDENTITY(1,1) NOT NULL,
    Title             NVARCHAR(200)  NOT NULL,
    ShortDescription  NVARCHAR(300)  NULL,
    FullDescription   NVARCHAR(MAX)  NULL,
    CategoryID        INT            NOT NULL,
    DifficultyLevel   NVARCHAR(20)   NOT NULL DEFAULT 'Beginner',   -- Beginner / Intermediate / Advanced
    DurationMinutes   INT            NULL,
    IsPublished       BIT            NOT NULL DEFAULT 0,
    CreatedBy         INT            NOT NULL,
    CreatedAt         DATETIME2(0)   NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_Courses PRIMARY KEY (CourseID),
    CONSTRAINT FK_Courses_Categories FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    CONSTRAINT FK_Courses_Users FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
);
GO
CREATE INDEX IX_Courses_CategoryID ON Courses(CategoryID);
GO

/* ============================================================================
   5. LESSONS  — content within a course
   ========================================================================= */
CREATE TABLE Lessons (
    LessonID        INT IDENTITY(1,1) NOT NULL,
    CourseID        INT            NOT NULL,
    Title           NVARCHAR(200)  NOT NULL,
    ContentHtml     NVARCHAR(MAX)  NULL,      -- lesson body (code snippets, explanation)
    SortOrder       INT            NOT NULL DEFAULT 1,
    DurationMinutes INT            NULL,
    CONSTRAINT PK_Lessons PRIMARY KEY (LessonID),
    CONSTRAINT FK_Lessons_Courses FOREIGN KEY (CourseID) REFERENCES Courses(CourseID) ON DELETE CASCADE
);
GO
CREATE INDEX IX_Lessons_CourseID ON Lessons(CourseID);
GO

/* ============================================================================
   6. RESOURCES  — downloadable material attached to a lesson
   ========================================================================= */
CREATE TABLE Resources (
    ResourceID   INT IDENTITY(1,1) NOT NULL,
    LessonID     INT           NOT NULL,
    Title        NVARCHAR(150) NOT NULL,
    FilePath     NVARCHAR(260) NOT NULL,      -- e.g. ~/Uploads/Resources/xyz.pdf
    ResourceType NVARCHAR(20)  NOT NULL DEFAULT 'PDF',  -- PDF / ZIP / Code / Link
    CONSTRAINT PK_Resources PRIMARY KEY (ResourceID),
    CONSTRAINT FK_Resources_Lessons FOREIGN KEY (LessonID) REFERENCES Lessons(LessonID) ON DELETE CASCADE
);
GO

/* ============================================================================
   7. ENROLLMENTS  — which student is taking which course
   ========================================================================= */
CREATE TABLE Enrollments (
    EnrollmentID INT IDENTITY(1,1) NOT NULL,
    UserID       INT          NOT NULL,
    CourseID     INT          NOT NULL,
    EnrolledAt   DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CompletedAt  DATETIME2(0) NULL,
    CONSTRAINT PK_Enrollments PRIMARY KEY (EnrollmentID),
    CONSTRAINT FK_Enrollments_Users FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_Enrollments_Courses FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
    CONSTRAINT UQ_Enrollments_User_Course UNIQUE (UserID, CourseID)
);
GO

/* ============================================================================
   8. LESSONPROGRESS  — tracks which lessons a student finished
   ========================================================================= */
CREATE TABLE LessonProgress (
    ProgressID  INT IDENTITY(1,1) NOT NULL,
    UserID      INT          NOT NULL,
    LessonID    INT          NOT NULL,
    IsCompleted BIT          NOT NULL DEFAULT 0,
    CompletedAt DATETIME2(0) NULL,
    CONSTRAINT PK_LessonProgress PRIMARY KEY (ProgressID),
    CONSTRAINT FK_LessonProgress_Users FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_LessonProgress_Lessons FOREIGN KEY (LessonID) REFERENCES Lessons(LessonID),
    CONSTRAINT UQ_LessonProgress_User_Lesson UNIQUE (UserID, LessonID)
);
GO

/* ============================================================================
   9. QUIZZES / QUESTIONS / OPTIONS / ATTEMPTS
   ========================================================================= */
CREATE TABLE Quizzes (
    QuizID   INT IDENTITY(1,1) NOT NULL,
    CourseID INT           NOT NULL,
    Title    NVARCHAR(150) NOT NULL,
    PassMark INT           NOT NULL DEFAULT 50,   -- percentage
    CONSTRAINT PK_Quizzes PRIMARY KEY (QuizID),
    CONSTRAINT FK_Quizzes_Courses FOREIGN KEY (CourseID) REFERENCES Courses(CourseID) ON DELETE CASCADE
);
GO

CREATE TABLE Questions (
    QuestionID   INT IDENTITY(1,1) NOT NULL,
    QuizID       INT           NOT NULL,
    QuestionText NVARCHAR(400) NOT NULL,
    Marks        INT           NOT NULL DEFAULT 1,
    CONSTRAINT PK_Questions PRIMARY KEY (QuestionID),
    CONSTRAINT FK_Questions_Quizzes FOREIGN KEY (QuizID) REFERENCES Quizzes(QuizID) ON DELETE CASCADE
);
GO

CREATE TABLE QuestionOptions (
    OptionID   INT IDENTITY(1,1) NOT NULL,
    QuestionID INT           NOT NULL,
    OptionText NVARCHAR(250) NOT NULL,
    IsCorrect  BIT           NOT NULL DEFAULT 0,
    CONSTRAINT PK_QuestionOptions PRIMARY KEY (OptionID),
    CONSTRAINT FK_QuestionOptions_Questions FOREIGN KEY (QuestionID) REFERENCES Questions(QuestionID) ON DELETE CASCADE
);
GO

CREATE TABLE QuizAttempts (
    AttemptID   INT IDENTITY(1,1) NOT NULL,
    UserID      INT          NOT NULL,
    QuizID      INT          NOT NULL,
    Score       INT          NOT NULL,
    TotalMarks  INT          NOT NULL,
    IsPassed    BIT          NOT NULL,
    AttemptedAt DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_QuizAttempts PRIMARY KEY (AttemptID),
    CONSTRAINT FK_QuizAttempts_Users FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_QuizAttempts_Quizzes FOREIGN KEY (QuizID) REFERENCES Quizzes(QuizID)
);
GO

/* ============================================================================
   10. PORTFOLIOS  — students/professionals upload finished project work
   ========================================================================= */
CREATE TABLE Portfolios (
    PortfolioID INT IDENTITY(1,1) NOT NULL,
    UserID      INT           NOT NULL,
    CourseID    INT           NULL,          -- optional link to a course
    Title       NVARCHAR(150) NOT NULL,
    Description NVARCHAR(500) NULL,
    FilePath    NVARCHAR(260) NOT NULL,
    SubmittedAt DATETIME2(0)  NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_Portfolios PRIMARY KEY (PortfolioID),
    CONSTRAINT FK_Portfolios_Users FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_Portfolios_Courses FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);
GO

/* ============================================================================
   11. FEEDBACK  — contact form submissions
   ========================================================================= */
CREATE TABLE Feedback (
    FeedbackID  INT IDENTITY(1,1) NOT NULL,
    UserID      INT           NULL,
    Name        NVARCHAR(100) NOT NULL,
    Email       NVARCHAR(150) NOT NULL,
    Subject     NVARCHAR(150) NOT NULL,
    Message     NVARCHAR(MAX) NOT NULL,
    IsRead      BIT           NOT NULL DEFAULT 0,
    SubmittedAt DATETIME2(0)  NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_Feedback PRIMARY KEY (FeedbackID),
    CONSTRAINT FK_Feedback_Users FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

/* ============================================================================
   SEED DATA — real programming content
   ========================================================================= */

INSERT INTO Roles (RoleName) VALUES ('Admin'), ('Member');
GO

INSERT INTO Categories (CategoryName, Description) VALUES
    ('Web Development',   'HTML, CSS, JavaScript and building websites'),
    ('Python Programming', 'Python fundamentals through to practical scripting'),
    ('Data Structures & Algorithms', 'Core CS concepts every programmer needs');
GO

-- Placeholder admin row. DO NOT log in with this hash — it's not a real
-- password. Instead: register a normal account through the site, then run
-- the UPDATE statement at the bottom of this file to promote it to Admin.
INSERT INTO Users (FullName, Email, PasswordHash, PasswordSalt, RoleID, IsActive)
VALUES ('Placeholder Admin', 'placeholder@skillcore.local', REPLICATE('0', 64), REPLICATE('0', 32), 1, 0);
GO

INSERT INTO Courses (Title, ShortDescription, CategoryID, DifficultyLevel, DurationMinutes, IsPublished, CreatedBy)
VALUES
    ('HTML & CSS Fundamentals', 'Build your first web pages from scratch.', 1, 'Beginner', 90, 1, 1),
    ('Python for Beginners', 'Learn Python syntax, variables, and control flow.', 2, 'Beginner', 120, 1, 1),
    ('Introduction to Data Structures', 'Arrays, linked lists, stacks and queues explained.', 3, 'Intermediate', 150, 1, 1);
GO

INSERT INTO Lessons (CourseID, Title, ContentHtml, SortOrder, DurationMinutes) VALUES
    (1, 'What is HTML?', '<p>HTML gives a web page its structure...</p>', 1, 20),
    (1, 'Styling with CSS', '<p>CSS controls how HTML elements look...</p>', 2, 25),
    (2, 'Variables and Data Types', '<p>Python variables don''t need a declared type...</p>', 1, 20),
    (2, 'If Statements and Loops', '<p>Control flow lets your program make decisions...</p>', 2, 25),
    (3, 'Arrays vs Lists', '<p>An array is a fixed-size, contiguous block of memory...</p>', 1, 30);
GO

INSERT INTO Quizzes (CourseID, Title, PassMark) VALUES
    (1, 'HTML & CSS Basics Quiz', 60),
    (2, 'Python Fundamentals Quiz', 60);
GO

INSERT INTO Questions (QuizID, QuestionText, Marks) VALUES
    (1, 'Which tag is used to create a hyperlink in HTML?', 1),
    (2, 'Which keyword defines a function in Python?', 1);
GO

INSERT INTO QuestionOptions (QuestionID, OptionText, IsCorrect) VALUES
    (1, '<link>', 0), (1, '<a>', 1), (1, '<href>', 0), (1, '<nav>', 0),
    (2, 'func', 0), (2, 'def', 1), (2, 'function', 0), (2, 'lambda', 0);
GO

/* ============================================================================
   VERIFICATION — run after the script to confirm everything worked
   ========================================================================= */
SELECT * FROM Roles;
SELECT * FROM Categories;
SELECT c.Title, cat.CategoryName, c.DifficultyLevel FROM Courses c
    JOIN Categories cat ON c.CategoryID = cat.CategoryID;
SELECT l.Title AS Lesson, c.Title AS Course FROM Lessons l JOIN Courses c ON l.CourseID = c.CourseID;
GO

/* ============================================================================
   RUN THIS LATER, once you've registered your first real account through
   the site itself, to promote it to Admin instead of using the placeholder
   row above:

   UPDATE Users SET RoleID = 1 WHERE Email = 'your-real-email@example.com';
   ========================================================================= */


  SELECT UserID, FullName, Email, RoleID FROM Users;

  UPDATE Users SET RoleID = 1 WHERE Email = 'sijakhwo@gmail.com';
   
   SELECT UserID, FullName, Email, RoleID FROM Users WHERE Email = 'sijakhwo@gmail.com';

   SELECT UserID, Email, RoleID FROM Users;