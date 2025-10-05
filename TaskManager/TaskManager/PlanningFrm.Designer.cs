namespace TaskManager
{
    partial class LoginForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.EmailLoginlbl = new System.Windows.Forms.Label();
            this.psdLogin = new System.Windows.Forms.Label();
            this.emailLogin = new System.Windows.Forms.TextBox();
            this.passwordLogin = new System.Windows.Forms.TextBox();
            this.loginbtn = new System.Windows.Forms.Button();
            this.registerLink = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(410, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login Here";
            // 
            // EmailLoginlbl
            // 
            this.EmailLoginlbl.AutoSize = true;
            this.EmailLoginlbl.Location = new System.Drawing.Point(348, 184);
            this.EmailLoginlbl.Name = "EmailLoginlbl";
            this.EmailLoginlbl.Size = new System.Drawing.Size(32, 13);
            this.EmailLoginlbl.TabIndex = 1;
            this.EmailLoginlbl.Text = "Email";
            // 
            // psdLogin
            // 
            this.psdLogin.AutoSize = true;
            this.psdLogin.Location = new System.Drawing.Point(348, 235);
            this.psdLogin.Name = "psdLogin";
            this.psdLogin.Size = new System.Drawing.Size(53, 13);
            this.psdLogin.TabIndex = 2;
            this.psdLogin.Text = "Password";
            // 
            // emailLogin
            // 
            this.emailLogin.Location = new System.Drawing.Point(483, 177);
            this.emailLogin.Name = "emailLogin";
            this.emailLogin.Size = new System.Drawing.Size(100, 20);
            this.emailLogin.TabIndex = 3;
            // 
            // passwordLogin
            // 
            this.passwordLogin.Location = new System.Drawing.Point(483, 232);
            this.passwordLogin.Name = "passwordLogin";
            this.passwordLogin.Size = new System.Drawing.Size(100, 20);
            this.passwordLogin.TabIndex = 4;
            // 
            // loginbtn
            // 
            this.loginbtn.Location = new System.Drawing.Point(416, 289);
            this.loginbtn.Name = "loginbtn";
            this.loginbtn.Size = new System.Drawing.Size(75, 23);
            this.loginbtn.TabIndex = 5;
            this.loginbtn.Text = "Login";
            this.loginbtn.UseVisualStyleBackColor = true;
            this.loginbtn.Click += new System.EventHandler(this.loginbtn_Click);
            // 
            // registerLink
            // 
            this.registerLink.AutoSize = true;
            this.registerLink.Location = new System.Drawing.Point(395, 334);
            this.registerLink.Name = "registerLink";
            this.registerLink.Size = new System.Drawing.Size(111, 13);
            this.registerLink.TabIndex = 6;
            this.registerLink.TabStop = true;
            this.registerLink.Text = "ClickHere To Register";
            this.registerLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(13, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 392);
            this.panel1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(228, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "If You can\'t rember what you planned for ";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(277, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "You Need A  Alain\'s TaskManager App";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.registerLink);
            this.Controls.Add(this.loginbtn);
            this.Controls.Add(this.passwordLogin);
            this.Controls.Add(this.emailLogin);
            this.Controls.Add(this.psdLogin);
            this.Controls.Add(this.EmailLoginlbl);
            this.Controls.Add(this.label1);
            this.Name = "LoginForm";
            this.Text = "PlanningFrm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label EmailLoginlbl;
        private System.Windows.Forms.Label psdLogin;
        private System.Windows.Forms.TextBox emailLogin;
        private System.Windows.Forms.TextBox passwordLogin;
        private System.Windows.Forms.Button loginbtn;
        private System.Windows.Forms.LinkLabel registerLink;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}