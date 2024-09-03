using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class ModernRegistrationForm : Form
    {
        private Panel leftPanel;
        private Panel rightPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private TextBox usernameTextBox;
        private TextBox emailTextBox;
        private TextBox passwordTextBox;
        private TextBox confirmPasswordTextBox;
        private CheckBox termsCheckBox;
        private Button registerButton;
        private LinkLabel loginLink;
        private Panel usernamePanel;
        private Panel emailPanel;
        private Panel passwordPanel;
        private Panel confirmPasswordPanel;

        public ModernRegistrationForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 600);
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.Text = "Alain's Task Manager App - Registration";
        }

        private void InitializeComponent()
        {
            // Left Panel (Gradient Background)
            leftPanel = new Panel();
            leftPanel.Size = new Size(500, 600);
            leftPanel.Location = new Point(0, 0);
            leftPanel.Paint += LeftPanel_Paint;

            // Right Panel (Registration Form)
            rightPanel = new Panel();
            rightPanel.Size = new Size(500, 600);
            rightPanel.Location = new Point(500, 0);
            rightPanel.BackColor = Color.FromArgb(30, 41, 59);

            // Title
            titleLabel = new Label();
            titleLabel.Text = "Create Account";
            titleLabel.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(50, 60);
            rightPanel.Controls.Add(titleLabel);

            // Subtitle
            subtitleLabel = new Label();
            subtitleLabel.Text = "Join Alain's Task Manager and start managing your tasks efficiently.";
            subtitleLabel.Font = new Font("Segoe UI", 12);
            subtitleLabel.ForeColor = Color.FromArgb(156, 163, 175);
            subtitleLabel.AutoSize = true;
            subtitleLabel.Location = new Point(50, 100);
            rightPanel.Controls.Add(subtitleLabel);

            // Username Panel
            usernamePanel = new Panel();
            usernamePanel.Size = new Size(400, 50);
            usernamePanel.Location = new Point(50, 180);
            usernamePanel.BackColor = Color.FromArgb(51, 65, 85);
            usernamePanel.Paint += UsernamePanel_Paint;

            Label usernameLabel = new Label();
            usernameLabel.Text = "Username";
            usernameLabel.Font = new Font("Segoe UI", 10);
            usernameLabel.ForeColor = Color.FromArgb(156, 163, 175);
            usernameLabel.BackColor = Color.FromArgb(51, 65, 85);
            usernameLabel.Location = new Point(15, 5);
            usernamePanel.Controls.Add(usernameLabel);

            usernameTextBox = new TextBox();
            usernameTextBox.Font = new Font("Segoe UI", 12);
            usernameTextBox.ForeColor = Color.White;
            usernameTextBox.BackColor = Color.FromArgb(75, 85, 99);
            usernameTextBox.BorderStyle = BorderStyle.None;
            usernameTextBox.Location = new Point(15, 25);
            usernameTextBox.Size = new Size(370, 20);
            usernamePanel.Controls.Add(usernameTextBox);

            rightPanel.Controls.Add(usernamePanel);

            // Email Panel
            emailPanel = new Panel();
            emailPanel.Size = new Size(400, 50);
            emailPanel.Location = new Point(50, 250);
            emailPanel.BackColor = Color.FromArgb(51, 65, 85);
            emailPanel.Paint += EmailPanel_Paint;

            Label emailLabel = new Label();
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
            passwordPanel.Location = new Point(50, 320);
            passwordPanel.BackColor = Color.FromArgb(51, 65, 85);
            passwordPanel.Paint += PasswordPanel_Paint;

            Label passwordLabel = new Label();
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

            // Confirm Password Panel
            confirmPasswordPanel = new Panel();
            confirmPasswordPanel.Size = new Size(400, 50);
            confirmPasswordPanel.Location = new Point(50, 390);
            confirmPasswordPanel.BackColor = Color.FromArgb(51, 65, 85);
            confirmPasswordPanel.Paint += ConfirmPasswordPanel_Paint;

            Label confirmPasswordLabel = new Label();
            confirmPasswordLabel.Text = "Confirm Password";
            confirmPasswordLabel.Font = new Font("Segoe UI", 10);
            confirmPasswordLabel.ForeColor = Color.FromArgb(156, 163, 175);
            confirmPasswordLabel.BackColor = Color.FromArgb(51, 65, 85);
            confirmPasswordLabel.Location = new Point(15, 5);
            confirmPasswordPanel.Controls.Add(confirmPasswordLabel);

            confirmPasswordTextBox = new TextBox();
            confirmPasswordTextBox.Font = new Font("Segoe UI", 12);
            confirmPasswordTextBox.ForeColor = Color.White;
            confirmPasswordTextBox.BackColor = Color.FromArgb(75, 85, 99);
            confirmPasswordTextBox.BorderStyle = BorderStyle.None;
            confirmPasswordTextBox.Location = new Point(15, 25);
            confirmPasswordTextBox.Size = new Size(370, 20);
            confirmPasswordTextBox.UseSystemPasswordChar = true;
            confirmPasswordPanel.Controls.Add(confirmPasswordTextBox);

            rightPanel.Controls.Add(confirmPasswordPanel);

            // Terms Checkbox
            termsCheckBox = new CheckBox();
            termsCheckBox.Text = "I agree to the Terms and Conditions";
            termsCheckBox.Font = new Font("Segoe UI", 10);
            termsCheckBox.ForeColor = Color.FromArgb(156, 163, 175);
            termsCheckBox.Location = new Point(50, 460);
            termsCheckBox.AutoSize = true;
            rightPanel.Controls.Add(termsCheckBox);

            // Register Button
            registerButton = new Button();
            registerButton.Text = "Create Account";
            registerButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            registerButton.ForeColor = Color.White;
            registerButton.BackColor = Color.FromArgb(34, 197, 94);
            registerButton.FlatStyle = FlatStyle.Flat;
            registerButton.FlatAppearance.BorderSize = 0;
            registerButton.Size = new Size(400, 45);
            registerButton.Location = new Point(50, 500);
            registerButton.Cursor = Cursors.Hand;
            registerButton.Click += RegisterButton_Click;
            rightPanel.Controls.Add(registerButton);

            // Login Link
            loginLink = new LinkLabel();
            loginLink.Text = "Already have an account? Sign in here";
            loginLink.Font = new Font("Segoe UI", 10);
            loginLink.LinkColor = Color.FromArgb(59, 130, 246);
            loginLink.Location = new Point(50, 560);
            loginLink.AutoSize = true;
            loginLink.LinkClicked += LoginLink_LinkClicked;
            rightPanel.Controls.Add(loginLink);

            // Add panels to form
            this.Controls.Add(leftPanel);
            this.Controls.Add(rightPanel);
        }

        private void LeftPanel_Paint(object sender, PaintEventArgs e)
        {
            // Create gradient background
            using (LinearGradientBrush brush = new LinearGradientBrush(
                leftPanel.ClientRectangle,
                Color.FromArgb(34, 197, 94),
                Color.FromArgb(59, 130, 246),
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, leftPanel.ClientRectangle);
            }

            // Add some modern graphics
            using (Font font = new Font("Segoe UI", 24, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.White))
            {
                e.Graphics.DrawString("Join TaskManager Pro", font, brush, 50, 200);
                e.Graphics.DrawString("Today!", font, brush, 50, 240);
            }

            using (Font font = new Font("Segoe UI", 12))
            using (Brush brush = new SolidBrush(Color.FromArgb(255, 255, 255, 200)))
            {
                e.Graphics.DrawString("Create your account and start", font, brush, 50, 300);
                e.Graphics.DrawString("managing tasks like a professional", font, brush, 50, 320);
            }
        }

        private void UsernamePanel_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, usernamePanel.Width - 1, usernamePanel.Height - 1);
            }
        }

        private void EmailPanel_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, emailPanel.Width - 1, emailPanel.Height - 1);
            }
        }

        private void PasswordPanel_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, passwordPanel.Width - 1, passwordPanel.Height - 1);
            }
        }

        private void ConfirmPasswordPanel_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.FromArgb(75, 85, 99), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, confirmPasswordPanel.Width - 1, confirmPasswordPanel.Height - 1);
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            // Registration validation
            if (string.IsNullOrEmpty(usernameTextBox.Text) || string.IsNullOrEmpty(emailTextBox.Text) || 
                string.IsNullOrEmpty(passwordTextBox.Text) || string.IsNullOrEmpty(confirmPasswordTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                MessageBox.Show("Passwords do not match.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!termsCheckBox.Checked)
            {
                MessageBox.Show("Please agree to the terms and conditions.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // For demo purposes, accept any valid registration
            MessageBox.Show($"Registration successful for user: {usernameTextBox.Text}!\nYou can now login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // Navigate back to Login form
            FormManager.ShowForm<ModernLoginForm>("Login");
        }

        private void LoginLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormManager.ShowForm<ModernLoginForm>("Login");
        }
    }
}
