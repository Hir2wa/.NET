using System;
using System.Windows.Forms;

namespace E_LearningPlatform
{
    /// <summary>
    /// Simple test class to verify database connection
    /// </summary>
    public static class TestDatabaseConnection
    {
        /// <summary>
        /// Tests the database connection and displays results
        /// </summary>
        public static void TestConnection()
        {
            try
            {
                if (DatabaseHelper.TestConnection())
                {
                    MessageBox.Show("Database connection successful!\n\nYour E-Learning Platform is ready to use with SQL Server database.", 
                        "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Database connection failed!\n\nPlease check:\n" +
                        "1. SQL Server is running\n" +
                        "2. Database 'librarydb' exists\n" +
                        "3. Connection string in App.config is correct\n" +
                        "4. You have proper permissions", 
                        "Connection Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error testing database connection:\n{ex.Message}", 
                    "Connection Test Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
