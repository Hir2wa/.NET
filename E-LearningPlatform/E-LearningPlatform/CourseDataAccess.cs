using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace E_LearningPlatform
{
    /// <summary>
    /// Data access layer for Course operations
    /// </summary>
    public class CourseDataAccess
    {
        /// <summary>
        /// Gets all courses from the database
        /// </summary>
        /// <returns>List of Course objects</returns>
        public static List<Course> GetAllCourses()
        {
            var courses = new List<Course>();
            
            try
            {
                using (var command = new SqlCommand("sp_GetAllCourses", DatabaseHelper.GetConnection()))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    using (var reader = DatabaseHelper.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            courses.Add(CreateCourseFromReader(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving courses: {ex.Message}", "Database Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return courses;
        }

        /// <summary>
        /// Gets a course by its ID
        /// </summary>
        /// <param name="courseID">Course ID to search for</param>
        /// <returns>Course object or null if not found</returns>
        public static Course GetCourseByID(string courseID)
        {
            try
            {
                using (var command = new SqlCommand("sp_GetCourseByID", DatabaseHelper.GetConnection()))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourseID", courseID);
                    
                    using (var reader = DatabaseHelper.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            return CreateCourseFromReader(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving course: {ex.Message}", "Database Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return null;
        }

        /// <summary>
        /// Gets a course by its code
        /// </summary>
        /// <param name="courseCode">Course code to search for</param>
        /// <returns>Course object or null if not found</returns>
        public static Course GetCourseByCode(string courseCode)
        {
            try
            {
                using (var command = new SqlCommand("sp_GetCourseByCode", DatabaseHelper.GetConnection()))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourseCode", courseCode);
                    
                    using (var reader = DatabaseHelper.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            return CreateCourseFromReader(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving course: {ex.Message}", "Database Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return null;
        }

        /// <summary>
        /// Inserts a new course into the database
        /// </summary>
        /// <param name="course">Course object to insert</param>
        /// <returns>True if successful, false otherwise</returns>
        public static bool InsertCourse(Course course)
        {
            try
            {
                using (var command = new SqlCommand("sp_InsertCourse", DatabaseHelper.GetConnection()))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@CourseID", course.CourseID);
                    command.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                    command.Parameters.AddWithValue("@CourseName", course.CourseName);
                    command.Parameters.AddWithValue("@Semester", course.Semester);
                    command.Parameters.AddWithValue("@Credits", course.Credits);
                    command.Parameters.AddWithValue("@Instructor", course.Instructor);
                    command.Parameters.AddWithValue("@Prerequisites", string.IsNullOrEmpty(course.Prerequisites) ? (object)DBNull.Value : course.Prerequisites);
                    command.Parameters.AddWithValue("@Department", course.Department);
                    command.Parameters.AddWithValue("@Room", string.IsNullOrEmpty(course.Room) ? (object)DBNull.Value : course.Room);
                    command.Parameters.AddWithValue("@MaxStudents", course.MaxStudents);
                    command.Parameters.AddWithValue("@Status", course.Status);
                    
                    using (var reader = DatabaseHelper.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            string result = reader["Result"].ToString();
                            string message = reader["Message"].ToString();
                            
                            if (result == "SUCCESS")
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show($"Error inserting course: {message}", "Database Error", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting course: {ex.Message}", "Database Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return false;
        }

        /// <summary>
        /// Updates an existing course in the database
        /// </summary>
        /// <param name="course">Course object with updated information</param>
        /// <returns>True if successful, false otherwise</returns>
        public static bool UpdateCourse(Course course)
        {
            try
            {
                using (var command = new SqlCommand("sp_UpdateCourse", DatabaseHelper.GetConnection()))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@CourseID", course.CourseID);
                    command.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                    command.Parameters.AddWithValue("@CourseName", course.CourseName);
                    command.Parameters.AddWithValue("@Semester", course.Semester);
                    command.Parameters.AddWithValue("@Credits", course.Credits);
                    command.Parameters.AddWithValue("@Instructor", course.Instructor);
                    command.Parameters.AddWithValue("@Prerequisites", string.IsNullOrEmpty(course.Prerequisites) ? (object)DBNull.Value : course.Prerequisites);
                    command.Parameters.AddWithValue("@Department", course.Department);
                    command.Parameters.AddWithValue("@Room", string.IsNullOrEmpty(course.Room) ? (object)DBNull.Value : course.Room);
                    command.Parameters.AddWithValue("@MaxStudents", course.MaxStudents);
                    command.Parameters.AddWithValue("@Status", course.Status);
                    
                    using (var reader = DatabaseHelper.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            string result = reader["Result"].ToString();
                            string message = reader["Message"].ToString();
                            
                            if (result == "SUCCESS")
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show($"Error updating course: {message}", "Database Error", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating course: {ex.Message}", "Database Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return false;
        }

        /// <summary>
        /// Deletes a course from the database
        /// </summary>
        /// <param name="courseID">Course ID to delete</param>
        /// <returns>True if successful, false otherwise</returns>
        public static bool DeleteCourse(string courseID)
        {
            try
            {
                using (var command = new SqlCommand("sp_DeleteCourse", DatabaseHelper.GetConnection()))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourseID", courseID);
                    
                    using (var reader = DatabaseHelper.ExecuteReader(command))
                    {
                        if (reader.Read())
                        {
                            string result = reader["Result"].ToString();
                            string message = reader["Message"].ToString();
                            
                            if (result == "SUCCESS")
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show($"Error deleting course: {message}", "Database Error", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting course: {ex.Message}", "Database Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return false;
        }

        /// <summary>
        /// Searches courses with filters
        /// </summary>
        /// <param name="searchTerm">Search term for course code, name, or instructor</param>
        /// <param name="semester">Semester filter</param>
        /// <param name="department">Department filter</param>
        /// <param name="status">Status filter</param>
        /// <returns>List of filtered Course objects</returns>
        public static List<Course> SearchCourses(string searchTerm = null, string semester = null, 
            string department = null, string status = null)
        {
            var courses = new List<Course>();
            
            try
            {
                using (var command = new SqlCommand("sp_SearchCourses", DatabaseHelper.GetConnection()))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@SearchTerm", string.IsNullOrEmpty(searchTerm) ? (object)DBNull.Value : searchTerm);
                    command.Parameters.AddWithValue("@Semester", string.IsNullOrEmpty(semester) || semester == "All Semesters" ? (object)DBNull.Value : semester);
                    command.Parameters.AddWithValue("@Department", string.IsNullOrEmpty(department) || department == "All Departments" ? (object)DBNull.Value : department);
                    command.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(status) ? (object)DBNull.Value : status);
                    
                    using (var reader = DatabaseHelper.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            courses.Add(CreateCourseFromReader(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching courses: {ex.Message}", "Database Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return courses;
        }

        /// <summary>
        /// Checks if a course code already exists
        /// </summary>
        /// <param name="courseCode">Course code to check</param>
        /// <param name="excludeCourseID">Course ID to exclude from check (for updates)</param>
        /// <returns>True if course code exists, false otherwise</returns>
        public static bool CourseCodeExists(string courseCode, string excludeCourseID = null)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM Courses WHERE CourseCode = @CourseCode";
                if (!string.IsNullOrEmpty(excludeCourseID))
                {
                    sql += " AND CourseID != @ExcludeCourseID";
                }
                
                using (var command = new SqlCommand(sql, DatabaseHelper.GetConnection()))
                {
                    command.Parameters.AddWithValue("@CourseCode", courseCode);
                    if (!string.IsNullOrEmpty(excludeCourseID))
                    {
                        command.Parameters.AddWithValue("@ExcludeCourseID", excludeCourseID);
                    }
                    
                    int count = (int)DatabaseHelper.ExecuteScalar(command);
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking course code: {ex.Message}", "Database Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Gets the next available course ID
        /// </summary>
        /// <returns>Next course ID as string</returns>
        public static string GetNextCourseID()
        {
            try
            {
                using (var command = new SqlCommand("SELECT ISNULL(MAX(CAST(CourseID AS INT)), 0) + 1 FROM Courses", DatabaseHelper.GetConnection()))
                {
                    int nextID = (int)DatabaseHelper.ExecuteScalar(command);
                    return nextID.ToString("000");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting next course ID: {ex.Message}", "Database Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "001";
            }
        }

        /// <summary>
        /// Creates a Course object from a SqlDataReader
        /// </summary>
        /// <param name="reader">SqlDataReader object</param>
        /// <returns>Course object</returns>
        private static Course CreateCourseFromReader(SqlDataReader reader)
        {
            return new Course
            {
                CourseID = reader["CourseID"].ToString(),
                CourseCode = reader["CourseCode"].ToString(),
                CourseName = reader["CourseName"].ToString(),
                Semester = reader["Semester"].ToString(),
                Credits = Convert.ToInt32(reader["Credits"]),
                Instructor = reader["Instructor"].ToString(),
                Prerequisites = reader["Prerequisites"] == DBNull.Value ? "" : reader["Prerequisites"].ToString(),
                Department = reader["Department"].ToString(),
                Room = reader["Room"] == DBNull.Value ? "" : reader["Room"].ToString(),
                MaxStudents = Convert.ToInt32(reader["MaxStudents"]),
                Status = reader["Status"].ToString(),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
            };
        }
    }
}
