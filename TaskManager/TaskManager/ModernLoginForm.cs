using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class ModernLoginForm : Form
    {
        private Panel leftPanel;
        private Panel rightPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private TextBox emailTextBox;
        private TextBox passwordTextBox;
        private CheckBox adminCheckBox;
        private Button loginButton;
        private LinkLabel registerLink;
        private Label messageLabel;
        private Label emailLabel;
        private Label passwordLabel;
        private Panel emailPanel;
        private Panel passwordPanel;

        public ModernLoginForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 600);
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.Text = "Alain's Task Manager App - Login";
        }

        private void InitializeComponent()
        {
            // Left Panel (Gradient Background)
            leftPanel = new Panel();
            leftPanel.Size = new Size(500, 600);
            leftPanel.Location = new Point(0, 0);
            leftPanel.Paint += LeftPanel_Paint;

            // Right Panel (Login Form)
            rightPanel = new Panel();
            rightPanel.Size = new Size(500, 600);
            rightPanel.Location = new Point(500, 0);
            rightPanel.BackColor = Color.FromArgb(30, 41, 59);

            // Title
            titleLabel = new Label();
            titleLabel.Text = "Alain's Task Manager";
            titleLabel.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(50, 80);
            rightPanel.Controls.Add(titleLabel);

            // Subtitle
            subtitleLabel = new Label();
            subtitleLabel.Text = "Welcome back! Please sign in to your account.";
            subtitleLabel.Font = new Font("Segoe UI", 12);
            subtitleLabel.ForeColor = Color.FromArgb(156, 163, 175);
            subtitleLabel.AutoSize = true;
            subtitleLabel.Location = new Point(50, 120);
            rightPanel.Controls.Add(subtitleLabel);

            // Email Panel
            emailPanel = new Panel();
            emailPanel.Size = new Size(400, 50);
            emailPanel.Location = new Point(50, 220);
            emailPanel.BackColor = Color.FromArgb(51, 65, 85);
            emailPanel.Paint += EmailPanel_Paint;

            emailLabel = new Label();
            emailLabel.Text = "Email Address";
            emailLabel.Font = new Font("Segoe UI", 10);
            emailLabel.ForeColor = Color.FromArgb(156, 163, 175);
            emailLabel.BackColor = Color.FromArgb(51, 65, 85);
            emailLabel.Location = new Point(15, 5);
            emailPanel.Controls.Add(emailLabel);

            emailTextBox = new TextBox();
            emailTextBox.Font = new Font("Segoe UI", 12);
            emailTextBox.ForeColor = Color.White;
            emailTextBox.BackColor = Color.FromArgb(75, 85, 99);
            emailTextBox.BorderStyle = BorderStyle.None;
            emailTextBox.Location = new Point(15, 25);
            emailTextBox.Size = new Size(370, 20);
            emailPanel.Controls.Add(emailTextBox);

            rightPanel.Controls.Add(emailPanel);

            // Password Panel
            passwordPanel = new Panel();
            passwordPanel.Size = new Size(400, 50);
            passwordPanel.Location = new Point(50, 290);
            passwordPanel.BackColor = Color.FromArgb(51, 65, 85);
            passwordPanel.Paint += PasswordPanel_Paint;

            passwordLabel = new Label();
            passwordLabel.Text = "Password";
            passwordLabel.Font = new Font("Segoe UI", 10);
            passwordLabel.ForeColor = Color.FromArgb(156, 163, 175);
            passwordLabel.BackColor = Color.FromArgb(51, 65, 85);
            passwordLabel.Location = new Point(15, 5);
            passwordPanel.Controls.Add(passwordLabel);

            passwordTextBox = new TextBox();
            passwordTextBox.Font = new Font("Segoe UI", 12);
            passwordTextBox.ForeColor = Color.White;
            passwordTextBox.BackColor = Color.FromArgb(75, 85, 99);
            passwordTextBox.BorderStyle = BorderStyle.None;
            passwordTextBox.Location = new Point(15, 25);
            passwordTextBox.Size = new Size(370, 20);
            passwordTextBox.UseSystemPasswordChar = true;
            passwordPanel.Controls.Add(passwordTextBox);

            rightPanel.Controls.Add(passwordPanel);

            // Admin Checkbox
            adminCheckBox = new CheckBox();
            adminCheckBox.Text = "I am an Administrator";
            adminCheckBox.Font = new Font("Segoe UI", 10);
            adminCheckBox.ForeColor = Color.FromArgb(59, 130, 246);
            adminCheckBox.Location = new Point(50, 360);
            adminCheckBox.AutoSize = true;
            rightPanel.Controls.Add(adminCheckBox);

            // Login Button
            loginButton = new Button();
            loginButton.Text = "Sign In";
            loginButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            loginButton.ForeColor = Color.White;
            loginButton.BackColor = Color.FromArgb(59, 130, 246);
            loginButton.FlatStyle = FlatStyle.Flat;
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.Size = new Size(400, 45);
            loginButton.Location = new Point(50, 400);
            loginButton.Cursor = Cursors.Hand;
            loginButton.Click += LoginButton_Click;
            rightPanel.Controls.Add(loginButton);

            // Message Label
            messageLabel = new Label();
            messageLabel.Text = "";
            messageLabel.Font = new Font("Segoe UI", 10);
            messageLabel.ForeColor = Color.FromArgb(34, 197, 94);
            messageLabel.AutoSize = true;
            messageLabel.Location = new Point(50, 420);
            rightPanel.Controls.Add(messageLabel);

            // Register Link
            registerLink = new LinkLabel();
            registerLink.Text = "Don't have an account? Sign up here";
            registerLink.Font = new Font("Segoe UI", 10);
            registerLink.LinkColor = Color.FromArgb(59, 130, 246);
            registerLink.Location = new Point(50, 450);
            registerLink.AutoSize = true;
            registerLink.LinkClicked += RegisterLink_LinkClicked;
            rightPanel.Controls.Add(registerLink);

            // Add panels to form
            this.Controls.Add(leftPanel);
            this.Controls.Add(rightPanel);
        }

        private void LeftPanel_Paint(object sender, PaintEventArgs e)
        {
            // Create gradient background
            using (LinearGradientBrush brush = new LinearGradientBrush(
                leftPanel.ClientRectangle,
                Color.FromArgb(59, 130, 246),
                Color.FromArgb(147, 51, 234),
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, leftPanel.ClientRectangle);
            }

            // Add some modern graphics
            using (Font font = new Font("Segoe UI", 24, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.White))
            {
                e.Graphics.DrawString("Manage Your Tasks", font, brush, 50, 200);
                e.Graphics.DrawString("Like a Pro", font, brush, 50, 240);
            }

            using (Font font = new Font("Segoe UI", 12))
            using (Brush brush = new SolidBrush(Color.FromArgb(255, 255, 255, 200)))
            {
                e.Graphics.DrawString("Streamline your workflow with our", font, brush, 50, 300);
                e.Graphics.DrawString("advanced task management system", font, brush, 50, 320);
            }
        }

        private void EmailPanel_Paint(object sender, PaintEventArgs e)
        {
            // Add subtle border
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, emailPanel.Width - 1, emailPanel.Height - 1);
            }
        }

        private void PasswordPanel_Paint(object sender, PaintEventArgs e)
        {
            // Add subtle border
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, passwordPanel.Width - 1, passwordPanel.Height - 1);
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Clear previous message
            messageLabel.Text = "";
            messageLabel.ForeColor = Color.FromArgb(34, 197, 94);

            // Handle login logic
            if (string.IsNullOrEmpty(emailTextBox.Text) || string.IsNullOrEmpty(passwordTextBox.Text))
            {
                messageLabel.Text = "Please enter both email and password.";
                messageLabel.ForeColor = Color.FromArgb(239, 68, 68);
                return;
            }

            if (adminCheckBox.Checked)
            {
                if (emailTextBox.Text.ToLower().Contains("admin") || passwordTextBox.Text.ToLower().Contains("admin"))
                {
                    SessionManager.Login(emailTextBox.Text, "Admin", true);
                    messageLabel.Text = "Admin login successful! Redirecting...";
                    messageLabel.ForeColor = Color.FromArgb(34, 197, 94);
                    System.Threading.Thread.Sleep(1000); // Brief delay to show message
                    FormManager.ShowForm<ModernAdminForm>("Admin");
                }
                else
                {
                    messageLabel.Text = "Invalid admin credentials. Try 'admin' in email or password.";
                    messageLabel.ForeColor = Color.FromArgb(239, 68, 68);
                    return;
                }
            }
            else
            {
                string username = GetUsernameFromDatabase(emailTextBox.Text);
                SessionManager.Login(emailTextBox.Text, username, false);
                messageLabel.Text = $"Welcome {username}! Redirecting...";
                messageLabel.ForeColor = Color.FromArgb(34, 197, 94);
                System.Threading.Thread.Sleep(1000); // Brief delay to show message
                FormManager.ShowForm<ModernTaskForm>("TaskManager");
            }
        }

        private void RegisterLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormManager.ShowForm<ModernRegistrationForm>("Register");
        }

        private string GetUsernameFromDatabase(string email)
        {
            try
            {
                string connStr = @"Data Source=AlainF\NET_AU;Initial Catalog=PlanningDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connStr))
                {
                    con.Open();
                    string query = "SELECT Username FROM Users WHERE Email = @email";
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 50).Value = email;
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString();
                        }
                    }
                }
            }
            catch
            {
                // If database error, use email as username
            }
            
            return email.Split('@')[0];
        }
    }
}
