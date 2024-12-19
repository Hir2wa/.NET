using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace TaskManager
{
    public partial class 
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        ModernTaskForm : Form
    {
        private Panel headerPanel;
        private Panel sidebarPanel;
        private Panel mainPanel;
        private Label welcomeLabel;
        private Label userInfoLabel;
        private Button logoutButton;
        private Button generateReportButton;
        private Button adminPanelButton;
        private Panel taskInputPanel;
        private TextBox taskNameTextBox;
        private ComboBox statusComboBox;
        private Button addTaskButton;
        private Button updateTaskButton;
        private Button deleteTaskButton;
        private Button clearButton;
        private DataGridView tasksDataGridView;
        private TextBox searchTextBox;
        private Button searchButton;
        private Button refreshButton;
        private Label statsLabel;
        private Label messageLabel;

        private readonly string connStr = @"Data Source=AlainF\NET_AU;Initial Catalog=PlanningDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

        public ModernTaskForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 600);
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.Text = "Alain's Task Manager App - Dashboard";
        }

        private void InitializeComponent()
        {
            // Header Panel
            headerPanel = new Panel();
            headerPanel.Size = new Size(1000, 70);
            headerPanel.Location = new Point(0, 0);
            headerPanel.BackColor = Color.FromArgb(30, 41, 59);
            headerPanel.Padding = new Padding(16, 10, 16, 10);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Paint += HeaderPanel_Paint;

            welcomeLabel = new Label();
            welcomeLabel.Text = "";
            welcomeLabel.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            welcomeLabel.ForeColor = Color.White;
            welcomeLabel.Location = new Point(20, 20);
            welcomeLabel.AutoSize = true;
            welcomeLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            welcomeLabel.Visible = false;
            headerPanel.Controls.Add(welcomeLabel);

            userInfoLabel = new Label();
            userInfoLabel.Text = "";
            userInfoLabel.Font = new Font("Segoe UI", 12);
            userInfoLabel.ForeColor = Color.FromArgb(156, 163, 175);
            userInfoLabel.Location = new Point(20, 42);
            userInfoLabel.AutoSize = true;
            userInfoLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            userInfoLabel.Visible = false;
            headerPanel.Controls.Add(userInfoLabel);

            logoutButton = new Button();
            logoutButton.Text = "Logout";
            logoutButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            logoutButton.ForeColor = Color.White;
            logoutButton.BackColor = Color.FromArgb(239, 68, 68);
            logoutButton.FlatStyle = FlatStyle.Flat;
            logoutButton.FlatAppearance.BorderSize = 0;
            logoutButton.Size = new Size(80, 35);
            logoutButton.Location = new Point(900, 18);
            logoutButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            logoutButton.Cursor = Cursors.Hand;
            logoutButton.Click += LogoutButton_Click;
            headerPanel.Controls.Add(logoutButton);

            this.Controls.Add(headerPanel);

            // Sidebar Panel
            sidebarPanel = new Panel();
            sidebarPanel.Size = new Size(250, 540);
            sidebarPanel.Location = new Point(0, 60);
            sidebarPanel.BackColor = Color.FromArgb(30, 41, 59);
            sidebarPanel.Paint += SidebarPanel_Paint;

            // Task Input Section
            taskInputPanel = new Panel();
            taskInputPanel.Size = new Size(230, 250);
            taskInputPanel.Location = new Point(10, 20);
            taskInputPanel.BackColor = Color.FromArgb(51, 65, 85);
            taskInputPanel.Paint += TaskInputPanel_Paint;

            Label taskInputTitle = new Label();
            taskInputTitle.Text = "Add New Task";
            taskInputTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            taskInputTitle.ForeColor = Color.White;
            taskInputTitle.Location = new Point(15, 15);
            taskInputPanel.Controls.Add(taskInputTitle);

            Label taskNameLabel = new Label();
            taskNameLabel.Text = "Task Name";
            taskNameLabel.Font = new Font("Segoe UI", 10);
            taskNameLabel.ForeColor = Color.FromArgb(156, 163, 175);
            taskNameLabel.Location = new Point(15, 50);
            taskInputPanel.Controls.Add(taskNameLabel);

            taskNameTextBox = new TextBox();
            taskNameTextBox.Font = new Font("Segoe UI", 11);
            taskNameTextBox.ForeColor = Color.White;
            taskNameTextBox.BackColor = Color.FromArgb(75, 85, 99);
            taskNameTextBox.BorderStyle = BorderStyle.FixedSingle;
            taskNameTextBox.Size = new Size(200, 25);
            taskNameTextBox.Location = new Point(15, 75);
            taskInputPanel.Controls.Add(taskNameTextBox);

            Label statusLabel = new Label();
            statusLabel.Text = "Status";
            statusLabel.Font = new Font("Segoe UI", 10);
            statusLabel.ForeColor = Color.FromArgb(156, 163, 175);
            statusLabel.Location = new Point(15, 110);
            taskInputPanel.Controls.Add(statusLabel);

            statusComboBox = new ComboBox();
            statusComboBox.Font = new Font("Segoe UI", 11);
            statusComboBox.ForeColor = Color.White;
            statusComboBox.BackColor = Color.FromArgb(75, 85, 99);
            statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            statusComboBox.Size = new Size(200, 25);
            statusComboBox.Location = new Point(15, 135);
            statusComboBox.Items.AddRange(new string[] { "Pending", "In Progress", "Completed", "Cancelled" });
            statusComboBox.SelectedIndex = 0;
            taskInputPanel.Controls.Add(statusComboBox);

            addTaskButton = new Button();
            addTaskButton.Text = "Add Task";
            addTaskButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            addTaskButton.ForeColor = Color.White;
            addTaskButton.BackColor = Color.FromArgb(34, 197, 94);
            addTaskButton.FlatStyle = FlatStyle.Flat;
            addTaskButton.FlatAppearance.BorderSize = 0;
            addTaskButton.Size = new Size(100, 30);
            addTaskButton.Location = new Point(15, 180);
            addTaskButton.Cursor = Cursors.Hand;
            addTaskButton.Click += AddTaskButton_Click;
            taskInputPanel.Controls.Add(addTaskButton);

            updateTaskButton = new Button();
            updateTaskButton.Text = "Update";
            updateTaskButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            updateTaskButton.ForeColor = Color.White;
            updateTaskButton.BackColor = Color.FromArgb(59, 130, 246);
            updateTaskButton.FlatStyle = FlatStyle.Flat;
            updateTaskButton.FlatAppearance.BorderSize = 0;
            updateTaskButton.Size = new Size(100, 30);
            updateTaskButton.Location = new Point(125, 180);
            updateTaskButton.Cursor = Cursors.Hand;
            updateTaskButton.Click += UpdateTaskButton_Click;
            taskInputPanel.Controls.Add(updateTaskButton);

            deleteTaskButton = new Button();
            deleteTaskButton.Text = "Delete";
            deleteTaskButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            deleteTaskButton.ForeColor = Color.White;
            deleteTaskButton.BackColor = Color.FromArgb(239, 68, 68);
            deleteTaskButton.FlatStyle = FlatStyle.Flat;
            deleteTaskButton.FlatAppearance.BorderSize = 0;
            deleteTaskButton.Size = new Size(100, 30);
            deleteTaskButton.Location = new Point(15, 220);
            deleteTaskButton.Cursor = Cursors.Hand;
            deleteTaskButton.Click += DeleteTaskButton_Click;
            taskInputPanel.Controls.Add(deleteTaskButton);

            clearButton = new Button();
            clearButton.Text = "Clear";
            clearButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            clearButton.ForeColor = Color.White;
            clearButton.BackColor = Color.FromArgb(107, 114, 128);
            clearButton.FlatStyle = FlatStyle.Flat;
            clearButton.FlatAppearance.BorderSize = 0;
            clearButton.Size = new Size(100, 30);
            clearButton.Location = new Point(125, 220);
            clearButton.Cursor = Cursors.Hand;
            clearButton.Click += ClearButton_Click;
            taskInputPanel.Controls.Add(clearButton);

            sidebarPanel.Controls.Add(taskInputPanel);

            // Generate Report Button
            generateReportButton = new Button();
            generateReportButton.Text = "Generate Report";
            generateReportButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            generateReportButton.ForeColor = Color.White;
            generateReportButton.BackColor = Color.FromArgb(147, 51, 234);
            generateReportButton.FlatStyle = FlatStyle.Flat;
            generateReportButton.FlatAppearance.BorderSize = 0;
            generateReportButton.Size = new Size(230, 40);
            generateReportButton.Location = new Point(10, 280);
            generateReportButton.Cursor = Cursors.Hand;
            generateReportButton.Click += GenerateReportButton_Click;
            sidebarPanel.Controls.Add(generateReportButton);

            // Admin Panel Button (only show if user is admin)
            adminPanelButton = new Button();
            adminPanelButton.Text = "Admin Panel";
            adminPanelButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            adminPanelButton.ForeColor = Color.White;
            adminPanelButton.BackColor = Color.FromArgb(239, 68, 68);
            adminPanelButton.FlatStyle = FlatStyle.Flat;
            adminPanelButton.FlatAppearance.BorderSize = 0;
            adminPanelButton.Size = new Size(230, 40);
            adminPanelButton.Location = new Point(10, 330);
            adminPanelButton.Cursor = Cursors.Hand;
            adminPanelButton.Click += AdminPanelButton_Click;
            adminPanelButton.Visible = false; // Hidden by default
            sidebarPanel.Controls.Add(adminPanelButton);

            this.Controls.Add(sidebarPanel);

            // Main Panel
            mainPanel = new Panel();
            mainPanel.Size = new Size(750, 540);
            mainPanel.Location = new Point(250, 60);
            mainPanel.BackColor = Color.FromArgb(15, 23, 42);

            // Search Panel
            Panel searchPanel = new Panel();
            searchPanel.Size = new Size(730, 50);
            searchPanel.Location = new Point(10, 10);
            searchPanel.BackColor = Color.FromArgb(30, 41, 59);
            searchPanel.Paint += SearchPanel_Paint;

            searchTextBox = new TextBox();
            searchTextBox.Font = new Font("Segoe UI", 12);
            searchTextBox.ForeColor = Color.White;
            searchTextBox.BackColor = Color.FromArgb(51, 65, 85);
            searchTextBox.BorderStyle = BorderStyle.FixedSingle;
            searchTextBox.Size = new Size(250, 25);
            searchTextBox.Location = new Point(20, 12);
            // PlaceholderText not available in .NET Framework 4.7.2
            searchPanel.Controls.Add(searchTextBox);

            searchButton = new Button();
            searchButton.Text = "Search";
            searchButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            searchButton.ForeColor = Color.White;
            searchButton.BackColor = Color.FromArgb(59, 130, 246);
            searchButton.FlatStyle = FlatStyle.Flat;
            searchButton.FlatAppearance.BorderSize = 0;
            searchButton.Size = new Size(80, 25);
            searchButton.Location = new Point(280, 12);
            searchButton.Cursor = Cursors.Hand;
            searchButton.Click += SearchButton_Click;
            searchPanel.Controls.Add(searchButton);

            refreshButton = new Button();
            refreshButton.Text = "Refresh";
            refreshButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            refreshButton.ForeColor = Color.White;
            refreshButton.BackColor = Color.FromArgb(34, 197, 94);
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.FlatAppearance.BorderSize = 0;
            refreshButton.Size = new Size(80, 25);
            refreshButton.Location = new Point(370, 12);
            refreshButton.Cursor = Cursors.Hand;
            refreshButton.Click += RefreshButton_Click;
            searchPanel.Controls.Add(refreshButton);

            statsLabel = new Label();
            statsLabel.Text = "Total Tasks: 0";
            statsLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            statsLabel.ForeColor = Color.FromArgb(34, 197, 94);
            statsLabel.Location = new Point(460, 15);
            statsLabel.AutoSize = true;
            searchPanel.Controls.Add(statsLabel);

            // Message Label
            messageLabel = new Label();
            messageLabel.Text = "";
            messageLabel.Font = new Font("Segoe UI", 10);
            messageLabel.ForeColor = Color.FromArgb(34, 197, 94);
            messageLabel.AutoSize = true;
            messageLabel.Location = new Point(20, 35);
            searchPanel.Controls.Add(messageLabel);

            mainPanel.Controls.Add(searchPanel);

            // Tasks DataGridView
            tasksDataGridView = new DataGridView();
            tasksDataGridView.Size = new Size(730, 460);
            tasksDataGridView.Location = new Point(10, 70);
            tasksDataGridView.BackgroundColor = Color.FromArgb(30, 41, 59);
            tasksDataGridView.BorderStyle = BorderStyle.None;
            tasksDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 65, 85);
            tasksDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tasksDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tasksDataGridView.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            tasksDataGridView.DefaultCellStyle.ForeColor = Color.White;
            tasksDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(59, 130, 246);
            tasksDataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            tasksDataGridView.EnableHeadersVisualStyles = false;
            tasksDataGridView.GridColor = Color.FromArgb(75, 85, 99);
            tasksDataGridView.RowHeadersVisible = false;
            tasksDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tasksDataGridView.SelectionChanged += TasksDataGridView_SelectionChanged;
            mainPanel.Controls.Add(tasksDataGridView);

            this.Controls.Add(mainPanel);

            // Load data
            LoadTasks();
            UpdateUserInfo();
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {
            // Add subtle gradient
            using (LinearGradientBrush brush = new LinearGradientBrush(
                headerPanel.ClientRectangle,
                Color.FromArgb(30, 41, 59),
                Color.FromArgb(51, 65, 85),
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
            }
        }

        private void SidebarPanel_Paint(object sender, PaintEventArgs e)
        {
            // Add subtle border
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawLine(pen, sidebarPanel.Width - 1, 0, sidebarPanel.Width - 1, sidebarPanel.Height);
            }
        }

        private void TaskInputPanel_Paint(object sender, PaintEventArgs e)
        {
            // Add rounded corners effect
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, taskInputPanel.Width - 1, taskInputPanel.Height - 1);
            }
        }

        private void SearchPanel_Paint(object sender, PaintEventArgs e)
        {
            // Add subtle border
            Panel panel = sender as Panel;
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private void UpdateUserInfo()
        {
            if (SessionManager.IsLoggedIn)
            {
                userInfoLabel.Text = $"Welcome, {SessionManager.GetDisplayName()}";
                userInfoLabel.Visible = true;
                this.Text = $"Alain's Task Manager App - {SessionManager.GetDisplayName()}";
                
                // Show admin panel button if user is admin
                if (SessionManager.IsAdmin)
                {
                    adminPanelButton.Visible = true;
                }
                else
                {
                    adminPanelButton.Visible = false;
                }
            }
        }

        private void LoadTasks()
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
                        tasksDataGridView.DataSource = ds.Tables["Task"];
                        
                        if (tasksDataGridView.Columns.Count > 0)
                        {
                            tasksDataGridView.Columns[0].HeaderText = "ID";
                            tasksDataGridView.Columns[0].Width = 50;
                            if (tasksDataGridView.Columns.Count > 1)
                            {
                                tasksDataGridView.Columns[1].HeaderText = "Task Name";
                                tasksDataGridView.Columns[1].Width = 400;
                            }
                            if (tasksDataGridView.Columns.Count > 2)
                            {
                                tasksDataGridView.Columns[2].HeaderText = "Status";
                                tasksDataGridView.Columns[2].Width = 150;
                            }
                        }

                        statsLabel.Text = $"Total Tasks: {ds.Tables["Task"].Rows.Count}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(taskNameTextBox.Text))
            {
                messageLabel.Text = "Please enter a task name.";
                messageLabel.ForeColor = Color.FromArgb(239, 68, 68);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    string query = "INSERT INTO Task (taskName, status) VALUES (@taskName, @status)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@taskName", SqlDbType.NVarChar, 10).Value = taskNameTextBox.Text;
                        cmd.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = statusComboBox.SelectedItem?.ToString() ?? "Pending";
                        cmd.ExecuteNonQuery();
                    }
                }

                messageLabel.Text = "Task added successfully!";
                messageLabel.ForeColor = Color.FromArgb(34, 197, 94);
                ClearFields();
                LoadTasks();
            }
            catch (Exception ex)
            {
                messageLabel.Text = $"Error adding task: {ex.Message}";
                messageLabel.ForeColor = Color.FromArgb(239, 68, 68);
            }
        }

        private void UpdateTaskButton_Click(object sender, EventArgs e)
        {
            if (tasksDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(taskNameTextBox.Text))
            {
                MessageBox.Show("Please enter a task name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var row = tasksDataGridView.SelectedRows[0];
                int taskId = Convert.ToInt32(row.Cells["takId"].Value);
                
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    string query = "UPDATE Task SET taskName = @taskName, status = @status WHERE takId = @taskId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@taskName", SqlDbType.NVarChar, 10).Value = taskNameTextBox.Text;
                        cmd.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = statusComboBox.SelectedItem?.ToString() ?? "Pending";
                        cmd.Parameters.Add("@taskId", SqlDbType.Int).Value = taskId;
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Task updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                LoadTasks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating task: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteTaskButton_Click(object sender, EventArgs e)
        {
            if (tasksDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this task?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var row = tasksDataGridView.SelectedRows[0];
                    int taskId = Convert.ToInt32(row.Cells["takId"].Value);
                    
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
                    LoadTasks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting task: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchTextBox.Text))
            {
                LoadTasks();
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
                        cmd.Parameters.Add("@searchTerm", SqlDbType.NVarChar, 10).Value = "%" + searchTextBox.Text + "%";
                        
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            sda.Fill(ds, "Task");
                            tasksDataGridView.DataSource = ds.Tables["Task"];
                            
                            if (tasksDataGridView.Columns.Count > 0)
                            {
                                tasksDataGridView.Columns[0].HeaderText = "ID";
                                tasksDataGridView.Columns[0].Width = 50;
                                if (tasksDataGridView.Columns.Count > 1)
                                {
                                    tasksDataGridView.Columns[1].HeaderText = "Task Name";
                                    tasksDataGridView.Columns[1].Width = 400;
                                }
                                if (tasksDataGridView.Columns.Count > 2)
                                {
                                    tasksDataGridView.Columns[2].HeaderText = "Status";
                                    tasksDataGridView.Columns[2].Width = 150;
                                }
                            }

                            statsLabel.Text = $"Search Results: {ds.Tables["Task"].Rows.Count}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching tasks: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateReportButton_Click(object sender, EventArgs e)
        {
            if (SessionManager.IsLoggedIn)
            {
                PDFGenerator.GenerateUserReport(SessionManager.CurrentUserEmail);
            }
            else
            {
                MessageBox.Show("Please login first to generate reports.", "Session Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SessionManager.Logout();
                FormManager.ShowForm<ModernLoginForm>("Login");
            }
        }

        private void AdminPanelButton_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm<ModernAdminForm>("Admin");
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Text = "";
            messageLabel.Text = "Refreshed successfully!";
            messageLabel.ForeColor = Color.FromArgb(34, 197, 94);
            LoadTasks();
        }

        private void TasksDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (tasksDataGridView.SelectedRows.Count > 0)
            {
                var row = tasksDataGridView.SelectedRows[0];
                taskNameTextBox.Text = row.Cells["taskName"].Value?.ToString() ?? "";
                statusComboBox.Text = row.Cells["status"].Value?.ToString() ?? "Pending";
            }
        }

        private void ClearFields()
        {
            taskNameTextBox.Text = "";
            statusComboBox.SelectedIndex = 0;
            searchTextBox.Text = "";
        }
    }
}

// Enhanced on 2025-10-19 - Commit 2
