namespace LibraryMS
{
    partial class Authontiacation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlWelcome = new System.Windows.Forms.Panel();
            this.btnGoToLogin = new System.Windows.Forms.Button();
            this.btnGoToSignup = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.chkIsStaff = new System.Windows.Forms.CheckBox();
            this.cmbStaffRole = new System.Windows.Forms.ComboBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnToSignup = new System.Windows.Forms.Button();
            this.txtLoginPassword = new System.Windows.Forms.TextBox();
            this.txtLoginUsername = new System.Windows.Forms.TextBox();
            this.lblLoginPassword = new System.Windows.Forms.Label();
            this.lblLoginUsername = new System.Windows.Forms.Label();
            this.pnlSignup = new System.Windows.Forms.Panel();
            this.cmbSignupRole = new System.Windows.Forms.ComboBox();
            this.btnCreateAccount = new System.Windows.Forms.Button();
            this.btnBackToLogin = new System.Windows.Forms.Button();
            this.txtSignupPassword = new System.Windows.Forms.TextBox();
            this.txtSignupEmail = new System.Windows.Forms.TextBox();
            this.txtSignupUsername = new System.Windows.Forms.TextBox();
            this.txtSignupFullName = new System.Windows.Forms.TextBox();
            this.lblSignupPassword = new System.Windows.Forms.Label();
            this.lblSignupEmail = new System.Windows.Forms.Label();
            this.lblSignupUsername = new System.Windows.Forms.Label();
            this.lblSignupFullName = new System.Windows.Forms.Label();
            this.pnlWelcome.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.pnlSignup.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWelcome
            // 
            this.pnlWelcome.Controls.Add(this.btnGoToLogin);
            this.pnlWelcome.Controls.Add(this.btnGoToSignup);
            this.pnlWelcome.Controls.Add(this.lblWelcome);
            this.pnlWelcome.Location = new System.Drawing.Point(12, 12);
            this.pnlWelcome.Name = "pnlWelcome";
            this.pnlWelcome.Size = new System.Drawing.Size(776, 426);
            this.pnlWelcome.TabIndex = 0;
            // 
            // btnGoToLogin
            // 
            this.btnGoToLogin.Location = new System.Drawing.Point(182, 203);
            this.btnGoToLogin.Name = "btnGoToLogin";
            this.btnGoToLogin.Size = new System.Drawing.Size(131, 36);
            this.btnGoToLogin.TabIndex = 2;
            this.btnGoToLogin.Text = "Login";
            this.btnGoToLogin.UseVisualStyleBackColor = true;
            this.btnGoToLogin.Click += new System.EventHandler(this.btnGoToLogin_Click);
            // 
            // btnGoToSignup
            // 
            this.btnGoToSignup.Location = new System.Drawing.Point(421, 203);
            this.btnGoToSignup.Name = "btnGoToSignup";
            this.btnGoToSignup.Size = new System.Drawing.Size(131, 36);
            this.btnGoToSignup.TabIndex = 1;
            this.btnGoToSignup.Text = "Sign Up";
            this.btnGoToSignup.UseVisualStyleBackColor = true;
            this.btnGoToSignup.Click += new System.EventHandler(this.btnGoToSignup_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(3, 61);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(770, 62);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome to LibraryMS";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLogin
            // 
            this.pnlLogin.Controls.Add(this.chkIsStaff);
            this.pnlLogin.Controls.Add(this.cmbStaffRole);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Controls.Add(this.btnToSignup);
            this.pnlLogin.Controls.Add(this.txtLoginPassword);
            this.pnlLogin.Controls.Add(this.txtLoginUsername);
            this.pnlLogin.Controls.Add(this.lblLoginPassword);
            this.pnlLogin.Controls.Add(this.lblLoginUsername);
            this.pnlLogin.Location = new System.Drawing.Point(12, 12);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(776, 426);
            this.pnlLogin.TabIndex = 1;
            this.pnlLogin.Visible = false;
            // 
            // chkIsStaff
            // 
            this.chkIsStaff.AutoSize = true;
            this.chkIsStaff.Location = new System.Drawing.Point(267, 175);
            this.chkIsStaff.Name = "chkIsStaff";
            this.chkIsStaff.Size = new System.Drawing.Size(83, 17);
            this.chkIsStaff.TabIndex = 7;
            this.chkIsStaff.Text = "I am a staff";
            this.chkIsStaff.UseVisualStyleBackColor = true;
            this.chkIsStaff.CheckedChanged += new System.EventHandler(this.chkIsStaff_CheckedChanged);
            // 
            // cmbStaffRole
            // 
            this.cmbStaffRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStaffRole.Enabled = false;
            this.cmbStaffRole.FormattingEnabled = true;
            this.cmbStaffRole.Items.AddRange(new object[] {
            "Admin",
            "Librarian"});
            this.cmbStaffRole.Location = new System.Drawing.Point(267, 198);
            this.cmbStaffRole.Name = "cmbStaffRole";
            this.cmbStaffRole.Size = new System.Drawing.Size(242, 21);
            this.cmbStaffRole.TabIndex = 6;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(267, 238);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(101, 32);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnToSignup
            // 
            this.btnToSignup.Location = new System.Drawing.Point(408, 238);
            this.btnToSignup.Name = "btnToSignup";
            this.btnToSignup.Size = new System.Drawing.Size(101, 32);
            this.btnToSignup.TabIndex = 4;
            this.btnToSignup.Text = "Sign Up";
            this.btnToSignup.UseVisualStyleBackColor = true;
            this.btnToSignup.Click += new System.EventHandler(this.btnToSignup_Click);
            // 
            // txtLoginPassword
            // 
            this.txtLoginPassword.Location = new System.Drawing.Point(267, 136);
            this.txtLoginPassword.Name = "txtLoginPassword";
            this.txtLoginPassword.PasswordChar = '*';
            this.txtLoginPassword.Size = new System.Drawing.Size(242, 20);
            this.txtLoginPassword.TabIndex = 3;
            // 
            // txtLoginUsername
            // 
            this.txtLoginUsername.Location = new System.Drawing.Point(267, 98);
            this.txtLoginUsername.Name = "txtLoginUsername";
            this.txtLoginUsername.Size = new System.Drawing.Size(242, 20);
            this.txtLoginUsername.TabIndex = 2;
            // 
            // lblLoginPassword
            // 
            this.lblLoginPassword.AutoSize = true;
            this.lblLoginPassword.Location = new System.Drawing.Point(197, 139);
            this.lblLoginPassword.Name = "lblLoginPassword";
            this.lblLoginPassword.Size = new System.Drawing.Size(56, 13);
            this.lblLoginPassword.TabIndex = 1;
            this.lblLoginPassword.Text = "Password:";
            // 
            // lblLoginUsername
            // 
            this.lblLoginUsername.AutoSize = true;
            this.lblLoginUsername.Location = new System.Drawing.Point(197, 101);
            this.lblLoginUsername.Name = "lblLoginUsername";
            this.lblLoginUsername.Size = new System.Drawing.Size(58, 13);
            this.lblLoginUsername.TabIndex = 0;
            this.lblLoginUsername.Text = "Username:";
            // 
            // pnlSignup
            // 
            this.pnlSignup.Controls.Add(this.cmbSignupRole);
            this.pnlSignup.Controls.Add(this.btnCreateAccount);
            this.pnlSignup.Controls.Add(this.btnBackToLogin);
            this.pnlSignup.Controls.Add(this.txtSignupPassword);
            this.pnlSignup.Controls.Add(this.txtSignupEmail);
            this.pnlSignup.Controls.Add(this.txtSignupUsername);
            this.pnlSignup.Controls.Add(this.txtSignupFullName);
            this.pnlSignup.Controls.Add(this.lblSignupPassword);
            this.pnlSignup.Controls.Add(this.lblSignupEmail);
            this.pnlSignup.Controls.Add(this.lblSignupUsername);
            this.pnlSignup.Controls.Add(this.lblSignupFullName);
            this.pnlSignup.Location = new System.Drawing.Point(12, 12);
            this.pnlSignup.Name = "pnlSignup";
            this.pnlSignup.Size = new System.Drawing.Size(776, 426);
            this.pnlSignup.TabIndex = 2;
            this.pnlSignup.Visible = false;
            // 
            // cmbSignupRole
            // 
            this.cmbSignupRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSignupRole.Enabled = false;
            this.cmbSignupRole.FormattingEnabled = true;
            this.cmbSignupRole.Items.AddRange(new object[] {
            "User"});
            this.cmbSignupRole.Location = new System.Drawing.Point(267, 214);
            this.cmbSignupRole.Name = "cmbSignupRole";
            this.cmbSignupRole.Size = new System.Drawing.Size(242, 21);
            this.cmbSignupRole.TabIndex = 10;
            // 
            // btnCreateAccount
            // 
            this.btnCreateAccount.Location = new System.Drawing.Point(267, 254);
            this.btnCreateAccount.Name = "btnCreateAccount";
            this.btnCreateAccount.Size = new System.Drawing.Size(121, 32);
            this.btnCreateAccount.TabIndex = 9;
            this.btnCreateAccount.Text = "Create Account";
            this.btnCreateAccount.UseVisualStyleBackColor = true;
            this.btnCreateAccount.Click += new System.EventHandler(this.btnCreateAccount_Click);
            // 
            // btnBackToLogin
            // 
            this.btnBackToLogin.Location = new System.Drawing.Point(388, 254);
            this.btnBackToLogin.Name = "btnBackToLogin";
            this.btnBackToLogin.Size = new System.Drawing.Size(121, 32);
            this.btnBackToLogin.TabIndex = 8;
            this.btnBackToLogin.Text = "Back to Login";
            this.btnBackToLogin.UseVisualStyleBackColor = true;
            this.btnBackToLogin.Click += new System.EventHandler(this.btnBackToLogin_Click);
            // 
            // txtSignupPassword
            // 
            this.txtSignupPassword.Location = new System.Drawing.Point(267, 176);
            this.txtSignupPassword.Name = "txtSignupPassword";
            this.txtSignupPassword.PasswordChar = '*';
            this.txtSignupPassword.Size = new System.Drawing.Size(242, 20);
            this.txtSignupPassword.TabIndex = 7;
            // 
            // txtSignupEmail
            // 
            this.txtSignupEmail.Location = new System.Drawing.Point(267, 138);
            this.txtSignupEmail.Name = "txtSignupEmail";
            this.txtSignupEmail.Size = new System.Drawing.Size(242, 20);
            this.txtSignupEmail.TabIndex = 6;
            // 
            // txtSignupUsername
            // 
            this.txtSignupUsername.Location = new System.Drawing.Point(267, 100);
            this.txtSignupUsername.Name = "txtSignupUsername";
            this.txtSignupUsername.Size = new System.Drawing.Size(242, 20);
            this.txtSignupUsername.TabIndex = 5;
            // 
            // txtSignupFullName
            // 
            this.txtSignupFullName.Location = new System.Drawing.Point(267, 62);
            this.txtSignupFullName.Name = "txtSignupFullName";
            this.txtSignupFullName.Size = new System.Drawing.Size(242, 20);
            this.txtSignupFullName.TabIndex = 4;
            // 
            // lblSignupPassword
            // 
            this.lblSignupPassword.AutoSize = true;
            this.lblSignupPassword.Location = new System.Drawing.Point(197, 179);
            this.lblSignupPassword.Name = "lblSignupPassword";
            this.lblSignupPassword.Size = new System.Drawing.Size(56, 13);
            this.lblSignupPassword.TabIndex = 3;
            this.lblSignupPassword.Text = "Password:";
            // 
            // lblSignupEmail
            // 
            this.lblSignupEmail.AutoSize = true;
            this.lblSignupEmail.Location = new System.Drawing.Point(197, 141);
            this.lblSignupEmail.Name = "lblSignupEmail";
            this.lblSignupEmail.Size = new System.Drawing.Size(35, 13);
            this.lblSignupEmail.TabIndex = 2;
            this.lblSignupEmail.Text = "Email:";
            // 
            // lblSignupUsername
            // 
            this.lblSignupUsername.AutoSize = true;
            this.lblSignupUsername.Location = new System.Drawing.Point(197, 103);
            this.lblSignupUsername.Name = "lblSignupUsername";
            this.lblSignupUsername.Size = new System.Drawing.Size(58, 13);
            this.lblSignupUsername.TabIndex = 1;
            this.lblSignupUsername.Text = "Username:";
            // 
            // lblSignupFullName
            // 
            this.lblSignupFullName.AutoSize = true;
            this.lblSignupFullName.Location = new System.Drawing.Point(197, 65);
            this.lblSignupFullName.Name = "lblSignupFullName";
            this.lblSignupFullName.Size = new System.Drawing.Size(57, 13);
            this.lblSignupFullName.TabIndex = 0;
            this.lblSignupFullName.Text = "Full Name:";
            // 
            // Authontiacation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlSignup);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.pnlWelcome);
            this.Name = "Authontiacation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authentication";
            this.pnlWelcome.ResumeLayout(false);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.pnlSignup.ResumeLayout(false);
            this.pnlSignup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlWelcome;
        private System.Windows.Forms.Button btnGoToLogin;
        private System.Windows.Forms.Button btnGoToSignup;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnToSignup;
        private System.Windows.Forms.TextBox txtLoginPassword;
        private System.Windows.Forms.TextBox txtLoginUsername;
        private System.Windows.Forms.Label lblLoginPassword;
        private System.Windows.Forms.Label lblLoginUsername;
        private System.Windows.Forms.Panel pnlSignup;
        private System.Windows.Forms.ComboBox cmbSignupRole;
        private System.Windows.Forms.Button btnCreateAccount;
        private System.Windows.Forms.Button btnBackToLogin;
        private System.Windows.Forms.TextBox txtSignupPassword;
        private System.Windows.Forms.TextBox txtSignupEmail;
        private System.Windows.Forms.TextBox txtSignupUsername;
        private System.Windows.Forms.TextBox txtSignupFullName;
        private System.Windows.Forms.Label lblSignupPassword;
        private System.Windows.Forms.Label lblSignupEmail;
        private System.Windows.Forms.Label lblSignupUsername;
        private System.Windows.Forms.Label lblSignupFullName;
        private System.Windows.Forms.ComboBox cmbStaffRole;
        private System.Windows.Forms.CheckBox chkIsStaff;
    }
}

