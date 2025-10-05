namespace TaskManager
{
    partial class RegisterFrm
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
            this.descriptionpnlRegister = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.registerLink = new System.Windows.Forms.LinkLabel();
            this.registebtn = new System.Windows.Forms.Button();
            this.passwordLogin = new System.Windows.Forms.TextBox();
            this.emailLogin = new System.Windows.Forms.TextBox();
            this.psdLogin = new System.Windows.Forms.Label();
            this.EmailLoginlbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.confirmpsd = new System.Windows.Forms.Label();
            this.confirmpsdLbl = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkConditionBox = new System.Windows.Forms.CheckBox();
            this.descriptionpnlRegister.SuspendLayout();
            this.SuspendLayout();
            // 
            // descriptionpnlRegister
            // 
            this.descriptionpnlRegister.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.descriptionpnlRegister.Controls.Add(this.label5);
            this.descriptionpnlRegister.Controls.Add(this.label4);
            this.descriptionpnlRegister.Location = new System.Drawing.Point(115, 29);
            this.descriptionpnlRegister.Name = "descriptionpnlRegister";
            this.descriptionpnlRegister.Size = new System.Drawing.Size(249, 392);
            this.descriptionpnlRegister.TabIndex = 15;
            this.descriptionpnlRegister.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(228, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "If You can\'t rember what you planned for ";
            // 
            // registerLink
            // 
            this.registerLink.AutoSize = true;
            this.registerLink.Location = new System.Drawing.Point(505, 394);
            this.registerLink.Name = "registerLink";
            this.registerLink.Size = new System.Drawing.Size(111, 13);
            this.registerLink.TabIndex = 14;
            this.registerLink.TabStop = true;
            this.registerLink.Text = "ClickHere To Register";
            this.registerLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.registerLink_LinkClicked);
            // 
            // registebtn
            // 
            this.registebtn.Location = new System.Drawing.Point(518, 303);
            this.registebtn.Name = "registebtn";
            this.registebtn.Size = new System.Drawing.Size(75, 23);
            this.registebtn.TabIndex = 13;
            this.registebtn.Text = "Register";
            this.registebtn.UseVisualStyleBackColor = true;
            this.registebtn.Click += new System.EventHandler(this.loginbtn_Click);
            // 
            // passwordLogin
            // 
            this.passwordLogin.Location = new System.Drawing.Point(585, 215);
            this.passwordLogin.Name = "passwordLogin";
            this.passwordLogin.Size = new System.Drawing.Size(100, 20);
            this.passwordLogin.TabIndex = 12;
            this.passwordLogin.TextChanged += new System.EventHandler(this.passwordLogin_TextChanged);
            // 
            // emailLogin
            // 
            this.emailLogin.Location = new System.Drawing.Point(585, 160);
            this.emailLogin.Name = "emailLogin";
            this.emailLogin.Size = new System.Drawing.Size(100, 20);
            this.emailLogin.TabIndex = 11;
            this.emailLogin.TextChanged += new System.EventHandler(this.emailLogin_TextChanged);
            // 
            // psdLogin
            // 
            this.psdLogin.AutoSize = true;
            this.psdLogin.Location = new System.Drawing.Point(450, 218);
            this.psdLogin.Name = "psdLogin";
            this.psdLogin.Size = new System.Drawing.Size(53, 13);
            this.psdLogin.TabIndex = 10;
            this.psdLogin.Text = "Password";
            this.psdLogin.Click += new System.EventHandler(this.psdLogin_Click);
            // 
            // EmailLoginlbl
            // 
            this.EmailLoginlbl.AutoSize = true;
            this.EmailLoginlbl.Location = new System.Drawing.Point(450, 167);
            this.EmailLoginlbl.Name = "EmailLoginlbl";
            this.EmailLoginlbl.Size = new System.Drawing.Size(32, 13);
            this.EmailLoginlbl.TabIndex = 9;
            this.EmailLoginlbl.Text = "Email";
            this.EmailLoginlbl.Click += new System.EventHandler(this.EmailLoginlbl_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(512, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 33);
            this.label1.TabIndex = 8;
            this.label1.Text = " Register Here";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // confirmpsd
            // 
            this.confirmpsd.AutoSize = true;
            this.confirmpsd.Location = new System.Drawing.Point(450, 258);
            this.confirmpsd.Name = "confirmpsd";
            this.confirmpsd.Size = new System.Drawing.Size(94, 13);
            this.confirmpsd.TabIndex = 16;
            this.confirmpsd.Text = "Confirm Password ";
            // 
            // confirmpsdLbl
            // 
            this.confirmpsdLbl.Location = new System.Drawing.Point(585, 255);
            this.confirmpsdLbl.Name = "confirmpsdLbl";
            this.confirmpsdLbl.Size = new System.Drawing.Size(100, 20);
            this.confirmpsdLbl.TabIndex = 17;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkConditionBox
            // 
            this.checkConditionBox.AutoSize = true;
            this.checkConditionBox.Location = new System.Drawing.Point(468, 356);
            this.checkConditionBox.Name = "checkConditionBox";
            this.checkConditionBox.Size = new System.Drawing.Size(171, 17);
            this.checkConditionBox.TabIndex = 19;
            this.checkConditionBox.Text = "Agree to Terms and Conditions";
            this.checkConditionBox.UseVisualStyleBackColor = true;
            // 
            // RegisterFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkConditionBox);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.confirmpsdLbl);
            this.Controls.Add(this.confirmpsd);
            this.Controls.Add(this.descriptionpnlRegister);
            this.Controls.Add(this.registerLink);
            this.Controls.Add(this.registebtn);
            this.Controls.Add(this.passwordLogin);
            this.Controls.Add(this.emailLogin);
            this.Controls.Add(this.psdLogin);
            this.Controls.Add(this.EmailLoginlbl);
            this.Controls.Add(this.label1);
            this.Name = "RegisterFrm";
            this.Text = "RegisterFrm";
            this.descriptionpnlRegister.ResumeLayout(false);
            this.descriptionpnlRegister.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel descriptionpnlRegister;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel registerLink;
        private System.Windows.Forms.Button registebtn;
        private System.Windows.Forms.TextBox passwordLogin;
        private System.Windows.Forms.TextBox emailLogin;
        private System.Windows.Forms.Label psdLogin;
        private System.Windows.Forms.Label EmailLoginlbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label confirmpsd;
        private System.Windows.Forms.TextBox confirmpsdLbl;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkConditionBox;
    }
}