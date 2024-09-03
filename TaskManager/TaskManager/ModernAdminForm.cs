using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace TaskManager
{
    public partial class ModernAdminForm : Form
    {
        private Panel headerPanel;
        private Panel sidebarPanel;
        private Panel mainPanel;
        private Label welcomeLabel;
        private Label adminInfoLabel;
        private Button logoutButton;
        private Button generateReportButton;
        private Button manageUsersButton;
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
        private Label statsLabel;
        private Panel adminStatsPanel;

        private readonly string connStr = @"Data Source=AlainF\NET_AU;Initial Catalog=PlanningDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

        public ModernAdminForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 600);
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.Text = "Alain's Task Manager App - Admin Dashboard";
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
            welcomeLabel.Text = "ADMIN DASHBOARD";
            welcomeLabel.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            welcomeLabel.ForeColor = Color.FromArgb(239, 68, 68);
            welcomeLabel.Location = new Point(20, 18);
            welcomeLabel.AutoSize = true;
            welcomeLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            headerPanel.Controls.Add(welcomeLabel);

            adminInfoLabel = new Label();
            adminInfoLabel.Text = "";
            adminInfoLabel.Font = new Font("Segoe UI", 12);
            adminInfoLabel.ForeColor = Color.FromArgb(156, 163, 175);
            adminInfoLabel.Location = new Point(20, 40);
            adminInfoLabel.AutoSize = true;
            adminInfoLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            adminInfoLabel.Visible = false;
            headerPanel.Controls.Add(adminInfoLabel);

            logoutButton = new Button();
            logoutButton.Text = "Logout";
            logoutButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            logoutButton.ForeColor = Color.White;
            logoutButton.BackColor = Color.FromArgb(239, 68, 68);
            logoutButton.FlatStyle = FlatStyle.Flat;
            logoutButton.FlatAppearance.BorderSize = 0;
            logoutButton.Size = new Size(100, 40);
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

            // Admin Stats Panel
            adminStatsPanel = new Panel();
            adminStatsPanel.Size = new Size(230, 120);
            adminStatsPanel.Location = new Point(10, 20);
            adminStatsPanel.BackColor = Color.FromArgb(51, 65, 85);
            adminStatsPanel.Paint += AdminStatsPanel_Paint;

            Label adminStatsTitle = new Label();
            adminStatsTitle.Text = "Admin Statistics";
            adminStatsTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            adminStatsTitle.ForeColor = Color.FromArgb(239, 68, 68);
            adminStatsTitle.Location = new Point(15, 15);
            adminStatsPanel.Controls.Add(adminStatsTitle);

            Label totalUsersLabel = new Label();
            totalUsersLabel.Text = "Total Users: Loading...";
            totalUsersLabel.Font = new Font("Segoe UI", 10);
            totalUsersLabel.ForeColor = Color.White;
            totalUsersLabel.Location = new Point(15, 45);
            totalUsersLabel.Name = "totalUsersLabel";
            adminStatsPanel.Controls.Add(totalUsersLabel);

            Label totalTasksLabel = new Label();
            totalTasksLabel.Text = "Total Tasks: Loading...";
            totalTasksLabel.Font = new Font("Segoe UI", 10);
            totalTasksLabel.ForeColor = Color.White;
            totalTasksLabel.Location = new Point(15, 70);
            totalTasksLabel.Name = "totalTasksLabel";
            adminStatsPanel.Controls.Add(totalTasksLabel);

            Label adminUsersLabel = new Label();
            adminUsersLabel.Text = "Admin Users: Loading...";
            adminUsersLabel.Font = new Font("Segoe UI", 10);
            adminUsersLabel.ForeColor = Color.FromArgb(34, 197, 94);
            adminUsersLabel.Location = new Point(15, 95);
            adminUsersLabel.Name = "adminUsersLabel";
            adminStatsPanel.Controls.Add(adminUsersLabel);

            sidebarPanel.Controls.Add(adminStatsPanel);

            // Task Input Section
            taskInputPanel = new Panel();
            taskInputPanel.Size = new Size(230, 250);
            taskInputPanel.Location = new Point(10, 150);
            taskInputPanel.BackColor = Color.FromArgb(51, 65, 85);
            taskInputPanel.Paint += TaskInputPanel_Paint;

            Label taskInputTitle = new Label();
            taskInputTitle.Text = "Task Management";
            taskInputTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            taskInputTitle.ForeColor = Color.FromArgb(59, 130, 246);
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

            // Admin Action Buttons
            manageUsersButton = new Button();
            manageUsersButton.Text = "Manage Users";
            manageUsersButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            manageUsersButton.ForeColor = Color.White;
            manageUsersButton.BackColor = Color.FromArgb(147, 51, 234);
            manageUsersButton.FlatStyle = FlatStyle.Flat;
            manageUsersButton.FlatAppearance.BorderSize = 0;
            manageUsersButton.Size = new Size(230, 40);
            manageUsersButton.Location = new Point(10, 410);
            manageUsersButton.Cursor = Cursors.Hand;
            manageUsersButton.Click += ManageUsersButton_Click;
            sidebarPanel.Controls.Add(manageUsersButton);

            generateReportButton = new Button();
            generateReportButton.Text = "Generate Admin Report";
            generateReportButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            generateReportButton.ForeColor = Color.White;
            generateReportButton.BackColor = Color.FromArgb(34, 197, 94);
            generateReportButton.FlatStyle = FlatStyle.Flat;
            generateReportButton.FlatAppearance.BorderSize = 0;
            generateReportButton.Size = new Size(230, 40);
            generateReportButton.Location = new Point(10, 460);
            generateReportButton.Cursor = Cursors.Hand;
            generateReportButton.Click += GenerateReportButton_Click;
            sidebarPanel.Controls.Add(generateReportButton);

            this.Controls.Add(sidebarPanel);

            // Main Panel
            mainPanel = new Panel();
            mainPanel.Size = new Size(750, 540);
            mainPanel.Location = new Point(250, 60);
            mainPanel.BackColor = Color.FromArgb(15, 23, 42);

            // Search Panel
            Panel searchPanel = new Panel();
            searchPanel.Size = new Size(730, 60);
            searchPanel.Location = new Point(10, 12);
            searchPanel.BackColor = Color.FromArgb(30, 41, 59);
            searchPanel.Paint += SearchPanel_Paint;

            Label searchTitle = new Label();
            searchTitle.Text = "Task Overview";
            searchTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            searchTitle.ForeColor = Color.White;
            searchTitle.Location = new Point(20, 12);
            searchPanel.Controls.Add(searchTitle);

            searchTextBox = new TextBox();
            searchTextBox.Font = new Font("Segoe UI", 12);
            searchTextBox.ForeColor = Color.White;
            searchTextBox.BackColor = Color.FromArgb(51, 65, 85);
            searchTextBox.BorderStyle = BorderStyle.FixedSingle;
            searchTextBox.Size = new Size(300, 27);
            searchTextBox.Location = new Point(20, 32);
            searchPanel.Controls.Add(searchTextBox);

            searchButton = new Button();
            searchButton.Text = "Search";
            searchButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            searchButton.ForeColor = Color.White;
            searchButton.BackColor = Color.FromArgb(59, 130, 246);
            searchButton.FlatStyle = FlatStyle.Flat;
            searchButton.FlatAppearance.BorderSize = 0;
            searchButton.Size = new Size(84, 27);
            searchButton.Location = new Point(330, 32);
            searchButton.Cursor = Cursors.Hand;
            searchButton.Click += SearchButton_Click;
            searchPanel.Controls.Add(searchButton);

            statsLabel = new Label();
            statsLabel.Text = "Total Tasks: 0";
            statsLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            statsLabel.ForeColor = Color.FromArgb(34, 197, 94);
            statsLabel.Location = new Point(430, 36);
            statsLabel.AutoSize = true;
            searchPanel.Controls.Add(statsLabel);

            mainPanel.Controls.Add(searchPanel);

            // Tasks DataGridView
            tasksDataGridView = new DataGridView();
            tasksDataGridView.Size = new Size(730, 480);
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
            LoadAdminStats();
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {
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
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawLine(pen, sidebarPanel.Width - 1, 0, sidebarPanel.Width - 1, sidebarPanel.Height);
            }
        }

        private void AdminStatsPanel_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, adminStatsPanel.Width - 1, adminStatsPanel.Height - 1);
            }
        }

        private void TaskInputPanel_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, taskInputPanel.Width - 1, taskInputPanel.Height - 1);
            }
        }

        private void SearchPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private void LoadAdminStats()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    
                    // Get total users
                    string userQuery = "SELECT COUNT(*) FROM Users";
                    using (SqlCommand cmd = new SqlCommand(userQuery, con))
                    {
                        int totalUsers = (int)cmd.ExecuteScalar();
                        Control totalUsersLabel = adminStatsPanel.Controls["totalUsersLabel"];
                        if (totalUsersLabel != null)
                            totalUsersLabel.Text = $"Total Users: {totalUsers}";
                    }

                    // Get total tasks
                    string taskQuery = "SELECT COUNT(*) FROM Task";
                    using (SqlCommand cmd = new SqlCommand(taskQuery, con))
                    {
                        int totalTasks = (int)cmd.ExecuteScalar();
                        Control totalTasksLabel = adminStatsPanel.Controls["totalTasksLabel"];
                        if (totalTasksLabel != null)
                            totalTasksLabel.Text = $"Total Tasks: {totalTasks}";
                    }

                    // Get admin users
                    string adminQuery = "SELECT COUNT(*) FROM Users WHERE IsAdmin = 1";
                    using (SqlCommand cmd = new SqlCommand(adminQuery, con))
                    {
                        int adminUsers = (int)cmd.ExecuteScalar();
                        Control adminUsersLabel = adminStatsPanel.Controls["adminUsersLabel"];
                        if (adminUsersLabel != null)
                            adminUsersLabel.Text = $"Admin Users: {adminUsers}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading admin stats: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            tasksDataGridView.Columns[0].Width = 80;
                            if (tasksDataGridView.Columns.Count > 1)
                            {
                                tasksDataGridView.Columns[1].HeaderText = "Task Name";
                                tasksDataGridView.Columns[1].Width = 600;
                            }
                            if (tasksDataGridView.Columns.Count > 2)
                            {
                                tasksDataGridView.Columns[2].HeaderText = "Status";
                                tasksDataGridView.Columns[2].Width = 200;
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
                MessageBox.Show("Please enter a task name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                MessageBox.Show("Task added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                LoadTasks();
                LoadAdminStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding task: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    LoadAdminStats();
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
                                tasksDataGridView.Columns[0].Width = 80;
                                if (tasksDataGridView.Columns.Count > 1)
                                {
                                    tasksDataGridView.Columns[1].HeaderText = "Task Name";
                                    tasksDataGridView.Columns[1].Width = 600;
                                }
                                if (tasksDataGridView.Columns.Count > 2)
                                {
                                    tasksDataGridView.Columns[2].HeaderText = "Status";
                                    tasksDataGridView.Columns[2].Width = 200;
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
            PDFGenerator.GenerateAdminReport();
        }

        private void ManageUsersButton_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm<ModernUserManagementForm>("UserManagement");
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
