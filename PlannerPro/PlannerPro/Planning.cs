using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace PlannerPro
{
    public partial class Planning : Form
    {
        public Planning()
        {
            InitializeComponent();
        }

        private void Planning_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadTasks();
            LoadPlanningData();
        }

        private void LoadUsers()
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["plannerCon"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT email FROM [User]", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    comboUser.Items.Clear();
                    while (reader.Read())
                    {
                        comboUser.Items.Add(reader["email"].ToString());
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
        }

        private void LoadTasks()
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["plannerCon"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT taskId, taskname, status FROM TaskEnhanced ORDER BY taskname", connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    combotask.Items.Clear();
                    while (reader.Read())
                    {
                        string taskName = reader["taskname"].ToString();
                        string status = reader["status"].ToString();
                        
                        // Show task name with status for clarity
                        string displayText = $"{taskName} ({status})";
                        combotask.Items.Add(displayText);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tasks: " + ex.Message);
            }
        }

        private void LoadPlanningData()
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["plannerCon"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT p.UUID, p.taskId, t.taskname, p.email, p.stardDate, p.endDate FROM Planning p INNER JOIN TaskEnhanced t ON p.taskId = t.taskId", connection);
                    
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "Planning");
                    taskviewplans.DataSource = ds.Tables["Planning"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading planning data: " + ex.Message);
            }
        }

        private void registerTask_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (combotask.SelectedItem == null)
                {
                    MessageBox.Show("Please select a task!");
                    return;
                }
                
                if (comboUser.SelectedItem == null)
                {
                    MessageBox.Show("Please select a user!");
                    return;
                }
                
                if (dateTimePicker1.Value >= dateend.Value)
                {
                    MessageBox.Show("Start date must be before end date!");
                    return;
                }

                // Get taskId from database
                string con = ConfigurationManager.ConnectionStrings["plannerCon"].ConnectionString;
                int taskId = 0;
                
                // Extract task name from the display text (remove status part)
                string selectedText = combotask.SelectedItem.ToString();
                string taskName = selectedText.Split('(')[0].Trim();
                
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT taskId FROM TaskEnhanced WHERE taskname = @taskname", connection);
                    cmd.Parameters.AddWithValue("@taskname", taskName);
                    taskId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Call the planning procedure
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("ps_createPlan", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    cmd.Parameters.AddWithValue("@email", comboUser.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@stardDate", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@endDate", dateend.Value);
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Task assigned successfully!");
                    
                    // Clear the form
                    combotask.SelectedIndex = -1;
                    comboUser.SelectedIndex = -1;
                    
                    // Refresh data
                    LoadTasks();
                    LoadPlanningData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

// Enhanced on 2025-10-19 - Commit 1
