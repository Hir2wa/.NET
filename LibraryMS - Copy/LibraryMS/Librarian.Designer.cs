namespace LibraryMS
{
    partial class Librarian
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
            this.sidebar = new System.Windows.Forms.Panel();
            this.btnLibLogout = new System.Windows.Forms.Button();
            this.btnLibSettings = new System.Windows.Forms.Button();
            this.btnLibHistory = new System.Windows.Forms.Button();
            this.btnLibBorrow = new System.Windows.Forms.Button();
            this.btnLibBooks = new System.Windows.Forms.Button();
            this.btnLibHome = new System.Windows.Forms.Button();
            this.pnlLibHome = new System.Windows.Forms.Panel();
            this.pnlLibBooks = new System.Windows.Forms.Panel();
            this.pnlLibBorrow = new System.Windows.Forms.Panel();
            this.pnlLibHistory = new System.Windows.Forms.Panel();
            this.pnlLibSettings = new System.Windows.Forms.Panel();
            this.sidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.Gainsboro;
            this.sidebar.Controls.Add(this.btnLibLogout);
            this.sidebar.Controls.Add(this.btnLibSettings);
            this.sidebar.Controls.Add(this.btnLibHistory);
            this.sidebar.Controls.Add(this.btnLibBorrow);
            this.sidebar.Controls.Add(this.btnLibBooks);
            this.sidebar.Controls.Add(this.btnLibHome);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Location = new System.Drawing.Point(0, 0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(180, 561);
            this.sidebar.TabIndex = 0;
            // 
            // btnLibLogout
            // 
            this.btnLibLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLibLogout.Location = new System.Drawing.Point(12, 513);
            this.btnLibLogout.Name = "btnLibLogout";
            this.btnLibLogout.Size = new System.Drawing.Size(156, 36);
            this.btnLibLogout.TabIndex = 5;
            this.btnLibLogout.Text = "Logout";
            this.btnLibLogout.UseVisualStyleBackColor = true;
            this.btnLibLogout.Click += new System.EventHandler(this.btnLibLogout_Click);
            // 
            // btnLibSettings
            // 
            this.btnLibSettings.Location = new System.Drawing.Point(12, 220);
            this.btnLibSettings.Name = "btnLibSettings";
            this.btnLibSettings.Size = new System.Drawing.Size(156, 36);
            this.btnLibSettings.TabIndex = 4;
            this.btnLibSettings.Text = "Settings";
            this.btnLibSettings.UseVisualStyleBackColor = true;
            this.btnLibSettings.Click += new System.EventHandler(this.btnLibSettings_Click);
            // 
            // btnLibHistory
            // 
            this.btnLibHistory.Location = new System.Drawing.Point(12, 178);
            this.btnLibHistory.Name = "btnLibHistory";
            this.btnLibHistory.Size = new System.Drawing.Size(156, 36);
            this.btnLibHistory.TabIndex = 3;
            this.btnLibHistory.Text = "User History";
            this.btnLibHistory.UseVisualStyleBackColor = true;
            this.btnLibHistory.Click += new System.EventHandler(this.btnLibHistory_Click);
            // 
            // btnLibBorrow
            // 
            this.btnLibBorrow.Location = new System.Drawing.Point(12, 136);
            this.btnLibBorrow.Name = "btnLibBorrow";
            this.btnLibBorrow.Size = new System.Drawing.Size(156, 36);
            this.btnLibBorrow.TabIndex = 2;
            this.btnLibBorrow.Text = "Borrow Management";
            this.btnLibBorrow.UseVisualStyleBackColor = true;
            this.btnLibBorrow.Click += new System.EventHandler(this.btnLibBorrow_Click);
            // 
            // btnLibBooks
            // 
            this.btnLibBooks.Location = new System.Drawing.Point(12, 94);
            this.btnLibBooks.Name = "btnLibBooks";
            this.btnLibBooks.Size = new System.Drawing.Size(156, 36);
            this.btnLibBooks.TabIndex = 1;
            this.btnLibBooks.Text = "Manage Books";
            this.btnLibBooks.UseVisualStyleBackColor = true;
            this.btnLibBooks.Click += new System.EventHandler(this.btnLibBooks_Click);
            // 
            // btnLibHome
            // 
            this.btnLibHome.Location = new System.Drawing.Point(12, 52);
            this.btnLibHome.Name = "btnLibHome";
            this.btnLibHome.Size = new System.Drawing.Size(156, 36);
            this.btnLibHome.TabIndex = 0;
            this.btnLibHome.Text = "Home";
            this.btnLibHome.UseVisualStyleBackColor = true;
            this.btnLibHome.Click += new System.EventHandler(this.btnLibHome_Click);
            // 
            // pnlLibHome
            // 
            this.pnlLibHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLibHome.Location = new System.Drawing.Point(180, 0);
            this.pnlLibHome.Name = "pnlLibHome";
            this.pnlLibHome.Size = new System.Drawing.Size(804, 561);
            this.pnlLibHome.TabIndex = 1;
            this.pnlLibHome.Visible = false;
            // 
            // pnlLibBooks
            // 
            this.pnlLibBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLibBooks.Location = new System.Drawing.Point(180, 0);
            this.pnlLibBooks.Name = "pnlLibBooks";
            this.pnlLibBooks.Size = new System.Drawing.Size(804, 561);
            this.pnlLibBooks.TabIndex = 2;
            this.pnlLibBooks.Visible = false;
            // 
            // pnlLibBorrow
            // 
            this.pnlLibBorrow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLibBorrow.Location = new System.Drawing.Point(180, 0);
            this.pnlLibBorrow.Name = "pnlLibBorrow";
            this.pnlLibBorrow.Size = new System.Drawing.Size(804, 561);
            this.pnlLibBorrow.TabIndex = 3;
            this.pnlLibBorrow.Visible = false;
            // 
            // pnlLibHistory
            // 
            this.pnlLibHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLibHistory.Location = new System.Drawing.Point(180, 0);
            this.pnlLibHistory.Name = "pnlLibHistory";
            this.pnlLibHistory.Size = new System.Drawing.Size(804, 561);
            this.pnlLibHistory.TabIndex = 4;
            this.pnlLibHistory.Visible = false;
            this.pnlLibHistory.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlLibHistory_Paint);
            // 
            // pnlLibSettings
            // 
            this.pnlLibSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLibSettings.Location = new System.Drawing.Point(0, 0);
            this.pnlLibSettings.Name = "pnlLibSettings";
            this.pnlLibSettings.Size = new System.Drawing.Size(984, 561);
            this.pnlLibSettings.TabIndex = 5;
            this.pnlLibSettings.Visible = false;
            // 
            // Librarian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.pnlLibHistory);
            this.Controls.Add(this.pnlLibBorrow);
            this.Controls.Add(this.pnlLibBooks);
            this.Controls.Add(this.pnlLibHome);
            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.pnlLibSettings);
            this.Name = "Librarian";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Librarian Dashboard";
            this.sidebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel sidebar;
        private System.Windows.Forms.Button btnLibSettings;
        private System.Windows.Forms.Button btnLibHistory;
        private System.Windows.Forms.Button btnLibBorrow;
        private System.Windows.Forms.Button btnLibBooks;
        private System.Windows.Forms.Button btnLibHome;
        private System.Windows.Forms.Button btnLibLogout;
        private System.Windows.Forms.Panel pnlLibHome;
        private System.Windows.Forms.Panel pnlLibBooks;
        private System.Windows.Forms.Panel pnlLibBorrow;
        private System.Windows.Forms.Panel pnlLibHistory;
        private System.Windows.Forms.Panel pnlLibSettings;
    }
}