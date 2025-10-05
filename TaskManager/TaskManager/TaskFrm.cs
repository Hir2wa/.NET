using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class TaskFrm : Form
    {
        // keep only the connection string here
        private readonly string connStr =
            @"Data Source=AlainF\NET_AU;Initial Catalog=PlanningDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

        public TaskFrm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            SetupComboBox();
        }

        private void TaskFrm_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void SetupComboBox()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "Pending", "In Progress", "Completed", "Cancelled" });
            comboBox1.SelectedIndex = 0;
        }

        private void registebtnTsk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(emailLogin.Text))
            {
                MessageBox.Show("Please enter a task name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    string query = "INSERT INTO Task (taskName, status) VALUES (@taskName, @sts)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@taskName", SqlDbType.NVarChar, 10).Value = emailLogin.Text;
                        cmd.Parameters.Add("@sts", SqlDbType.NVarChar, 50).Value = comboBox1.SelectedItem?.ToString() ?? "Pending";

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Task added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding task: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updTask_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(emailLogin.Text))
            {
                MessageBox.Show("Please enter a task name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var row = dataGridView1.SelectedRows[0];
                string taskIdValue = GetCellValue(row, new[] { "takId" });
                if (string.IsNullOrEmpty(taskIdValue))
                {
                    MessageBox.Show("Could not find Task ID column. Please check database structure.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int taskId = Convert.ToInt32(taskIdValue);
                
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    string query = "UPDATE Task SET taskName = @taskName, status = @sts WHERE takId = @taskId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@taskName", SqlDbType.NVarChar, 10).Value = emailLogin.Text;
                        cmd.Parameters.Add("@sts", SqlDbType.NVarChar, 50).Value = comboBox1.SelectedItem?.ToString() ?? "Pending";
                        cmd.Parameters.Add("@taskId", SqlDbType.Int).Value = taskId;

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Task updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating task: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dltTask_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this task?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var row = dataGridView1.SelectedRows[0];
                    string taskIdValue = GetCellValue(row, new[] { "takId" });
                    if (string.IsNullOrEmpty(taskIdValue))
                    {
                        MessageBox.Show("Could not find Task ID column. Please check database structure.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int taskId = Convert.ToInt32(taskIdValue);
                    
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        con.Open();

                        string query = "DELETE FROM Task WHERE takId = @taskId";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.Add("@taskId", SqlDbType.Int).Value = taskId;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Task deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    DisplayData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting task: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchTask.Text))
            {
                DisplayData();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    string query = "SELECT takId, taskName, status FROM Task WHERE taskName LIKE @searchTerm";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@searchTerm", SqlDbType.NVarChar, 10).Value = "%" + searchTask.Text + "%";
                        
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            sda.Fill(ds, "Task");
                            dataGridView1.DataSource = ds.Tables["Task"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching tasks: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                
                // Use your actual column names
                string taskName = GetCellValue(row, new[] { "taskName", "TaskName" });
                string status = GetCellValue(row, new[] { "status", "Status" });
                
                emailLogin.Text = taskName ?? "";
                comboBox1.Text = status ?? "Pending";
            }
        }

        private string GetCellValue(DataGridViewRow row, string[] possibleColumnNames)
        {
            foreach (string columnName in possibleColumnNames)
            {
                try
                {
                    if (row.Cells[columnName] != null)
                    {
                        return row.Cells[columnName].Value?.ToString();
                    }
                }
                catch
                {
                    // Column doesn't exist, try next one
                    continue;
                }
            }
            return null;
        }

        private void ClearFields()
        {
            emailLogin.Text = "";
            comboBox1.SelectedIndex = 0;
            dateTimePickerStart.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddDays(1);
            searchTask.Text = "";
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                FormManager.ShowForm<LoginForm>("Login");
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }


        private void DisplayData()
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
                        sda.Fill(ds, "Task");
                        dataGridView1.DataSource = ds.Tables["Task"];
                        
                        // Ensure column headers are properly set
                        if (dataGridView1.Columns.Count > 0)
                        {
                            dataGridView1.Columns[0].HeaderText = "Task ID";
                            if (dataGridView1.Columns.Count > 1)
                                dataGridView1.Columns[1].HeaderText = "Task Name";
                            if (dataGridView1.Columns.Count > 2)
                                dataGridView1.Columns[2].HeaderText = "Status";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}