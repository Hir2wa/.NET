using System;
using System.Configuration;
using System.Data.SqlClient;

namespace E_LearningPlatform
{
    /// <summary>
    /// Database connection helper class for E-Learning Platform
    /// </summary>
    public static class DatabaseHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["E-LearningPlatformDB"]?.ConnectionString;

        /// <summary>
        /// Gets a new SQL Server connection
        /// </summary>
        /// <returns>SqlConnection object</returns>
        public static SqlConnection GetConnection()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Database connection string not found in configuration file.");
            }

            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Tests the database connection
        /// </summary>
        /// <returns>True if connection is successful, false otherwise</returns>
        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Database connection failed: {ex.Message}", 
                    "Connection Error", System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Executes a command and returns the result
        /// </summary>
        /// <param name="command">SQL command to execute</param>
        /// <returns>Number of rows affected</returns>
        public static int ExecuteNonQuery(SqlCommand command)
        {
            using (var connection = GetConnection())
            {
                command.Connection = connection;
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes a command and returns a data reader
        /// </summary>
        /// <param name="command">SQL command to execute</param>
        /// <returns>SqlDataReader object</returns>
        public static SqlDataReader ExecuteReader(SqlCommand command)
        {
            var connection = GetConnection();
            command.Connection = connection;
            connection.Open();
            return command.ExecuteReader();
        }

        /// <summary>
        /// Executes a command and returns a single value
        /// </summary>
        /// <param name="command">SQL command to execute</param>
        /// <returns>Object result</returns>
        public static object ExecuteScalar(SqlCommand command)
        {
            using (var connection = GetConnection())
            {
                command.Connection = connection;
                connection.Open();
                return command.ExecuteScalar();
            }
        }
    }
}
