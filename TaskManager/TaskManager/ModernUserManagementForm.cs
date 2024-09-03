using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace TaskManager
{
    public partial class ModernUserManagementForm : Form
    {
        private Panel headerPanel;
        private Panel sidebarPanel;
        private Panel mainPanel;
        private Label titleLabel;
        private Button backButton;
        private DataGridView usersDataGridView;
        private Panel userInputPanel;
        private TextBox usernameTextBox;
        private TextBox emailTextBox;
        private TextBox passwordTextBox;
        private CheckBox adminCheckBox;
        private Button addUserButton;
        private Button toggleAdminButton;
        private Button deleteUserButton;
        private Label statsLabel;

        private readonly string connStr = @"Data Source=AlainF\NET_AU;Initial Catalog=PlanningDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

        public ModernUserManagementForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 600);
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.Text = "Alain's Task Manager App - User Management";
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

            titleLabel = new Label();
            titleLabel.Text = "User Management";
            titleLabel.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(147, 51, 234);
            titleLabel.Location = new Point(20, 18);
            titleLabel.AutoSize = true;
            titleLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            headerPanel.Controls.Add(titleLabel);

            backButton = new Button();
            backButton.Text = "Back to Admin";
            backButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            backButton.ForeColor = Color.White;
            backButton.BackColor = Color.FromArgb(107, 114, 128);
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.FlatAppearance.BorderSize = 0;
            backButton.Size = new Size(120, 35);
            backButton.Location = new Point(900, 18);
            backButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            backButton.Cursor = Cursors.Hand;
            backButton.Click += BackButton_Click;
            headerPanel.Controls.Add(backButton);

            this.Controls.Add(headerPanel);

            // Sidebar Panel
            sidebarPanel = new Panel();
            sidebarPanel.Size = new Size(250, 540);
            sidebarPanel.Location = new Point(0, 60);
            sidebarPanel.BackColor = Color.FromArgb(30, 41, 59);
            sidebarPanel.Paint += SidebarPanel_Paint;

            // User Input Panel
			userInputPanel = new Panel();
			userInputPanel.Size = new Size(230, 380);
            userInputPanel.Location = new Point(10, 20);
            userInputPanel.BackColor = Color.FromArgb(51, 65, 85);
            userInputPanel.Paint += UserInputPanel_Paint;

            Label userInputTitle = new Label();
            userInputTitle.Text = "Add New User";
            userInputTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            userInputTitle.ForeColor = Color.FromArgb(34, 197, 94);
            userInputTitle.Location = new Point(15, 15);
            userInputPanel.Controls.Add(userInputTitle);

            Label usernameLabel = new Label();
            usernameLabel.Text = "Username";
            usernameLabel.Font = new Font("Segoe UI", 10);
            usernameLabel.ForeColor = Color.FromArgb(156, 163, 175);
            usernameLabel.Location = new Point(15, 50);
            userInputPanel.Controls.Add(usernameLabel);

            usernameTextBox = new TextBox();
            usernameTextBox.Font = new Font("Segoe UI", 11);
            usernameTextBox.ForeColor = Color.White;
            usernameTextBox.BackColor = Color.FromArgb(75, 85, 99);
            usernameTextBox.BorderStyle = BorderStyle.FixedSingle;
            usernameTextBox.Size = new Size(200, 25);
            usernameTextBox.Location = new Point(15, 75);
            userInputPanel.Controls.Add(usernameTextBox);

            Label emailLabel = new Label();
            emailLabel.Text = "Email";
            emailLabel.Font = new Font("Segoe UI", 10);
            emailLabel.ForeColor = Color.FromArgb(156, 163, 175);
            emailLabel.Location = new Point(15, 110);
            userInputPanel.Controls.Add(emailLabel);

            emailTextBox = new TextBox();
            emailTextBox.Font = new Font("Segoe UI", 11);
            emailTextBox.ForeColor = Color.White;
            emailTextBox.BackColor = Color.FromArgb(75, 85, 99);
            emailTextBox.BorderStyle = BorderStyle.FixedSingle;
            emailTextBox.Size = new Size(200, 25);
            emailTextBox.Location = new Point(15, 135);
            userInputPanel.Controls.Add(emailTextBox);

            Label passwordLabel = new Label();
            passwordLabel.Text = "Password";
            passwordLabel.Font = new Font("Segoe UI", 10);
            passwordLabel.ForeColor = Color.FromArgb(156, 163, 175);
            passwordLabel.Location = new Point(15, 170);
            userInputPanel.Controls.Add(passwordLabel);

            passwordTextBox = new TextBox();
            passwordTextBox.Font = new Font("Segoe UI", 11);
            passwordTextBox.ForeColor = Color.White;
            passwordTextBox.BackColor = Color.FromArgb(75, 85, 99);
            passwordTextBox.BorderStyle = BorderStyle.FixedSingle;
            passwordTextBox.Size = new Size(200, 25);
            passwordTextBox.Location = new Point(15, 195);
            passwordTextBox.UseSystemPasswordChar = true;
            userInputPanel.Controls.Add(passwordTextBox);

			adminCheckBox = new CheckBox();
			adminCheckBox.Text = "Make Admin";
			adminCheckBox.Font = new Font("Segoe UI", 10);
			adminCheckBox.ForeColor = Color.FromArgb(239, 68, 68);
			adminCheckBox.Location = new Point(15, 230);
			adminCheckBox.AutoSize = true;
			userInputPanel.Controls.Add(adminCheckBox);

			addUserButton = new Button();
			addUserButton.Text = "Add User";
			addUserButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
			addUserButton.ForeColor = Color.White;
			addUserButton.BackColor = Color.FromArgb(34, 197, 94);
			addUserButton.FlatStyle = FlatStyle.Flat;
			addUserButton.FlatAppearance.BorderSize = 0;
			addUserButton.Size = new Size(200, 30);
			addUserButton.Location = new Point(15, 260);
			addUserButton.Cursor = Cursors.Hand;
			addUserButton.Click += AddUserButton_Click;
			userInputPanel.Controls.Add(addUserButton);

			toggleAdminButton = new Button();
			toggleAdminButton.Text = "Toggle Admin Status";
			toggleAdminButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
			toggleAdminButton.ForeColor = Color.White;
			toggleAdminButton.BackColor = Color.FromArgb(59, 130, 246);
			toggleAdminButton.FlatStyle = FlatStyle.Flat;
			toggleAdminButton.FlatAppearance.BorderSize = 0;
			toggleAdminButton.Size = new Size(200, 30);
			toggleAdminButton.Location = new Point(15, 300);
			toggleAdminButton.Cursor = Cursors.Hand;
			toggleAdminButton.Click += ToggleAdminButton_Click;
			userInputPanel.Controls.Add(toggleAdminButton);

			deleteUserButton = new Button();
			deleteUserButton.Text = "Delete User";
			deleteUserButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
			deleteUserButton.ForeColor = Color.White;
			deleteUserButton.BackColor = Color.FromArgb(239, 68, 68);
			deleteUserButton.FlatStyle = FlatStyle.Flat;
			deleteUserButton.FlatAppearance.BorderSize = 0;
			deleteUserButton.Size = new Size(200, 30);
			deleteUserButton.Location = new Point(15, 340);
			deleteUserButton.Cursor = Cursors.Hand;
			deleteUserButton.Click += DeleteUserButton_Click;
			userInputPanel.Controls.Add(deleteUserButton);

            sidebarPanel.Controls.Add(userInputPanel);

            this.Controls.Add(sidebarPanel);

            // Main Panel
            mainPanel = new Panel();
            mainPanel.Size = new Size(750, 540);
            mainPanel.Location = new Point(250, 60);
            mainPanel.BackColor = Color.FromArgb(15, 23, 42);

            // Stats Panel
            Panel statsPanel = new Panel();
            statsPanel.Size = new Size(730, 60);
            statsPanel.Location = new Point(10, 12);
            statsPanel.BackColor = Color.FromArgb(30, 41, 59);
            statsPanel.Paint += StatsPanel_Paint;

            statsLabel = new Label();
            statsLabel.Text = "Total Users: 0 | Admin Users: 0 | Regular Users: 0";
            statsLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            statsLabel.ForeColor = Color.FromArgb(34, 197, 94);
            statsLabel.Location = new Point(20, 18);
            statsLabel.AutoSize = true;
            statsPanel.Controls.Add(statsLabel);

            mainPanel.Controls.Add(statsPanel);

            // Users DataGridView
			usersDataGridView = new DataGridView();
            usersDataGridView.Size = new Size(730, 480);
            usersDataGridView.Location = new Point(10, 70);
            usersDataGridView.BackgroundColor = Color.FromArgb(30, 41, 59);
            usersDataGridView.BorderStyle = BorderStyle.None;
            usersDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 65, 85);
            usersDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            usersDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            usersDataGridView.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            usersDataGridView.DefaultCellStyle.ForeColor = Color.White;
            usersDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(59, 130, 246);
            usersDataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
			usersDataGridView.AllowUserToAddRows = false;
			usersDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            usersDataGridView.EnableHeadersVisualStyles = false;
            usersDataGridView.GridColor = Color.FromArgb(75, 85, 99);
            usersDataGridView.RowHeadersVisible = false;
            usersDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            usersDataGridView.SelectionChanged += UsersDataGridView_SelectionChanged;
            mainPanel.Controls.Add(usersDataGridView);

            this.Controls.Add(mainPanel);

            // Load data
            LoadUsers();
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

        private void UserInputPanel_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, userInputPanel.Width - 1, userInputPanel.Height - 1);
            }
        }

        private void StatsPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private void LoadUsers()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    // NOTE: Removed demo user seeding. Always load the real users from the database.
                    // Load users
                    string query = "SELECT Id, Username, Email, IsAdmin, CreatedDate FROM Users ORDER BY CreatedDate DESC";
                    using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                    {
                        DataSet ds = new DataSet();
                        sda.Fill(ds, "Users");
							usersDataGridView.DataSource = ds.Tables["Users"];
                        
                        // Set column headers
                        if (usersDataGridView.Columns.Count > 0)
                        {
                            usersDataGridView.Columns[0].HeaderText = "ID";
                            usersDataGridView.Columns[0].Width = 50;
                            if (usersDataGridView.Columns.Count > 1)
                            {
                                usersDataGridView.Columns[1].HeaderText = "Username";
                                usersDataGridView.Columns[1].Width = 150;
                            }
                            if (usersDataGridView.Columns.Count > 2)
                            {
                                usersDataGridView.Columns[2].HeaderText = "Email";
                                usersDataGridView.Columns[2].Width = 200;
                            }
                            if (usersDataGridView.Columns.Count > 3)
                            {
								usersDataGridView.Columns[3].HeaderText = "Is Admin";
								usersDataGridView.Columns[3].Width = 80;
								usersDataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }
                            if (usersDataGridView.Columns.Count > 4)
                            {
                                usersDataGridView.Columns[4].HeaderText = "Created Date";
                                usersDataGridView.Columns[4].Width = 150;
                            }
                        }

                        // Update stats
                        int totalUsers = ds.Tables["Users"].Rows.Count;
                        int adminUsers = 0;
                        foreach (DataRow row in ds.Tables["Users"].Rows)
                        {
                            if (Convert.ToBoolean(row["IsAdmin"]))
                                adminUsers++;
                        }
                        int regularUsers = totalUsers - adminUsers;

                        statsLabel.Text = $"Total Users: {totalUsers} | Admin Users: {adminUsers} | Regular Users: {regularUsers}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usernameTextBox.Text) || string.IsNullOrEmpty(emailTextBox.Text) || string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    string query = "INSERT INTO Users (Username, Email, Password, IsAdmin) VALUES (@username, @email, @password, @isAdmin)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = usernameTextBox.Text;
                        cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = emailTextBox.Text;
                        cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = passwordTextBox.Text;
                        cmd.Parameters.Add("@isAdmin", SqlDbType.Bit).Value = adminCheckBox.Checked;

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToggleAdminButton_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to toggle admin status.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = usersDataGridView.SelectedRows[0];
            string userId = row.Cells["Id"].Value?.ToString();
            string username = row.Cells["Username"].Value?.ToString();
            bool currentAdminStatus = Convert.ToBoolean(row.Cells["IsAdmin"].Value);
            bool newAdminStatus = !currentAdminStatus;

            var result = MessageBox.Show($"Are you sure you want to {(newAdminStatus ? "make" : "remove")} {username} {(newAdminStatus ? "an" : "from")} admin?", 
                "Confirm Admin Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        con.Open();
                        string query = "UPDATE Users SET IsAdmin = @isAdmin WHERE Id = @userId";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.Add("@isAdmin", SqlDbType.Bit).Value = newAdminStatus;
                            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = Convert.ToInt32(userId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"User {username} admin status updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating user: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = usersDataGridView.SelectedRows[0];
            string userId = row.Cells["Id"].Value?.ToString();
            string username = row.Cells["Username"].Value?.ToString();
            bool isAdmin = Convert.ToBoolean(row.Cells["IsAdmin"].Value);

            if (isAdmin)
            {
                MessageBox.Show("Cannot delete admin users.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete user: {username}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connStr))
                    {
                        con.Open();
                        string query = "DELETE FROM Users WHERE Id = @userId";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = Convert.ToInt32(userId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"User {username} deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            FormManager.ShowForm<ModernAdminForm>("Admin");
        }

        private void UsersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count > 0)
            {
                var row = usersDataGridView.SelectedRows[0];
                usernameTextBox.Text = row.Cells["Username"].Value?.ToString() ?? "";
                emailTextBox.Text = row.Cells["Email"].Value?.ToString() ?? "";
                adminCheckBox.Checked = Convert.ToBoolean(row.Cells["IsAdmin"].Value);
            }
        }

        private void ClearFields()
        {
            usernameTextBox.Text = "";
            emailTextBox.Text = "";
            passwordTextBox.Text = "";
            adminCheckBox.Checked = false;
        }
    }
}
