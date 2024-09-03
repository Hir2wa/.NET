using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TaskManager
{
    public static class PDFGenerator
    {
        private static readonly string connStr =
            @"Data Source=AlainF\NET_AU;Initial Catalog=PlanningDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

        public static void GenerateUserReport(string userEmail)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    
                    // Get username from session manager or database
                    string username = SessionManager.GetDisplayName();
                    if (username == "User" || string.IsNullOrEmpty(username))
                    {
                        try
                        {
                            string userQuery = "SELECT Username FROM Users WHERE Email = @email";
                            using (SqlCommand userCmd = new SqlCommand(userQuery, con))
                            {
                                userCmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = userEmail;
                                var result = userCmd.ExecuteScalar();
                                if (result != null)
                                {
                                    username = result.ToString();
                                }
                            }
                        }
                        catch
                        {
                            // If Users table doesn't exist or user not found, use email
                            username = userEmail.Split('@')[0];
                        }
                    }

                    string query = "SELECT takId, taskName, status FROM Task ORDER BY takId DESC";
                    
                    using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                    {
                        DataSet ds = new DataSet();
                        sda.Fill(ds, "Task");
                        
                        if (ds.Tables["Task"].Rows.Count == 0)
                        {
                            MessageBox.Show("No tasks found to generate report.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // Create HTML report
                        StringBuilder html = new StringBuilder();
                        html.AppendLine("<!DOCTYPE html>");
                        html.AppendLine("<html><head><title>Task Management Report</title>");
                        html.AppendLine("<style>");
                        html.AppendLine("body { font-family: Arial, sans-serif; margin: 20px; }");
                        html.AppendLine("h1 { color: #333; text-align: center; }");
                        html.AppendLine("table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
                        html.AppendLine("th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }");
                        html.AppendLine("th { background-color: #f2f2f2; font-weight: bold; }");
                        html.AppendLine("</style></head><body>");
                        html.AppendLine("<h1>Task Management Report</h1>");
                        html.AppendLine($"<p><strong>Generated for:</strong> {username} ({userEmail})</p>");
                        html.AppendLine($"<p><strong>Generated on:</strong> {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>");
                        html.AppendLine("<table>");
                        html.AppendLine("<tr><th>Task ID</th><th>Task Name</th><th>Status</th></tr>");

                        foreach (DataRow row in ds.Tables["Task"].Rows)
                        {
                            html.AppendLine($"<tr><td>{row["takId"]}</td><td>{row["taskName"]}</td><td>{row["status"]}</td></tr>");
                        }

                        html.AppendLine("</table></body></html>");

                        string fileName = $"TaskReport_{username}_{DateTime.Now:yyyyMMdd_HHmmss}.html";
                        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
                        File.WriteAllText(filePath, html.ToString());

                        MessageBox.Show($"Report generated successfully for {username}!\nSaved to: {filePath}\n\nYou can open this file in your browser and print to PDF.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void GenerateAdminReport()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    string query = "SELECT takId, taskName, status FROM Task ORDER BY takId DESC";

                    using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                    {
                        DataSet ds = new DataSet();
                        sda.Fill(ds, "AdminReport");
                        
                        if (ds.Tables["AdminReport"].Rows.Count == 0)
                        {
                            MessageBox.Show("No data found to generate admin report.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // Create HTML report
                        StringBuilder html = new StringBuilder();
                        html.AppendLine("<!DOCTYPE html>");
                        html.AppendLine("<html><head><title>ADMIN - Task Management Report</title>");
                        html.AppendLine("<style>");
                        html.AppendLine("body { font-family: Arial, sans-serif; margin: 20px; }");
                        html.AppendLine("h1 { color: #8B0000; text-align: center; }");
                        html.AppendLine("table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
                        html.AppendLine("th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }");
                        html.AppendLine("th { background-color: #8B0000; color: white; font-weight: bold; }");
                        html.AppendLine("</style></head><body>");
                        html.AppendLine("<h1>ADMIN - Task Management Report</h1>");
                        html.AppendLine("<p><strong>Generated by:</strong> Administrator</p>");
                        html.AppendLine($"<p><strong>Generated on:</strong> {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>");
                        html.AppendLine("<table>");
                        html.AppendLine("<tr><th>Task ID</th><th>Task Name</th><th>Status</th></tr>");

                        foreach (DataRow row in ds.Tables["AdminReport"].Rows)
                        {
                            html.AppendLine($"<tr><td>{row["takId"]}</td><td>{row["taskName"]}</td><td>{row["status"]}</td></tr>");
                        }

                        html.AppendLine("</table></body></html>");

                        string fileName = $"AdminReport_{DateTime.Now:yyyyMMdd_HHmmss}.html";
                        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
                        File.WriteAllText(filePath, html.ToString());

                        MessageBox.Show($"Admin report generated successfully!\nSaved to: {filePath}\n\nYou can open this file in your browser and print to PDF.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating admin report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
