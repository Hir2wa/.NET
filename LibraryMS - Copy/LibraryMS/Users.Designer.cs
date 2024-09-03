namespace LibraryMS
{
    partial class Users
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.gridBooks = new System.Windows.Forms.DataGridView();
            this.btnRequest = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblWelcomeUser = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabBooks = new System.Windows.Forms.TabPage();
            this.tabMyRequests = new System.Windows.Forms.TabPage();
            this.tabMyBorrowed = new System.Windows.Forms.TabPage();
            this.gridMyRequests = new System.Windows.Forms.DataGridView();
            this.btnCancelRequest = new System.Windows.Forms.Button();
            this.gridMyBorrowed = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBooks)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabBooks.SuspendLayout();
            this.tabMyRequests.SuspendLayout();
            this.tabMyBorrowed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMyRequests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMyBorrowed)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnLogout);
            this.panelTop.Controls.Add(this.lblWelcomeUser);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.btnRequest);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(984, 60);
            this.panelTop.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(260, 20);
            this.txtSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(278, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // gridBooks
            // 
            this.gridBooks.AllowUserToAddRows = false;
            this.gridBooks.AllowUserToDeleteRows = false;
            this.gridBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBooks.Location = new System.Drawing.Point(3, 3);
            this.gridBooks.MultiSelect = false;
            this.gridBooks.Name = "gridBooks";
            this.gridBooks.ReadOnly = true;
            this.gridBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridBooks.Size = new System.Drawing.Size(970, 470);
            this.gridBooks.TabIndex = 1;
            // 
            // btnRequest
            // 
            // add due date label and picker
            this.lblDue = new System.Windows.Forms.Label();
            this.lblDue.AutoSize = true;
            this.lblDue.Location = new System.Drawing.Point(359, 6);
            this.lblDue.Name = "lblDue";
            this.lblDue.Size = new System.Drawing.Size(85, 13);
            this.lblDue.TabIndex = 10;
            this.lblDue.Text = "Return by (≤14)";
            this.dtpDue = new System.Windows.Forms.DateTimePicker();
            this.dtpDue.Name = "dtpDue";
            this.dtpDue.Location = new System.Drawing.Point(362, 20);
            this.dtpDue.Size = new System.Drawing.Size(160, 20);
            this.panelTop.Controls.Add(this.lblDue);
            this.panelTop.Controls.Add(this.dtpDue);
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(528, 18);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(119, 23);
            this.btnRequest.TabIndex = 2;
            this.btnRequest.Text = "Request Selected";
            this.btnRequest.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(653, 18);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // lblWelcomeUser
            // 
            this.lblWelcomeUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWelcomeUser.AutoSize = true;
            this.lblWelcomeUser.Location = new System.Drawing.Point(740, 23);
            this.lblWelcomeUser.Name = "lblWelcomeUser";
            this.lblWelcomeUser.Size = new System.Drawing.Size(109, 13);
            this.lblWelcomeUser.TabIndex = 4;
            this.lblWelcomeUser.Text = "Welcome, <username>";
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Location = new System.Drawing.Point(865, 18);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(107, 23);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabBooks);
            this.tabMain.Controls.Add(this.tabMyRequests);
            this.tabMain.Controls.Add(this.tabMyBorrowed);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 60);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(984, 501);
            this.tabMain.TabIndex = 6;
            // 
            // tabBooks
            // 
            this.tabBooks.Controls.Add(this.gridBooks);
            this.tabBooks.Location = new System.Drawing.Point(4, 22);
            this.tabBooks.Name = "tabBooks";
            this.tabBooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabBooks.Size = new System.Drawing.Size(976, 475);
            this.tabBooks.TabIndex = 0;
            this.tabBooks.Text = "Books";
            this.tabBooks.UseVisualStyleBackColor = true;
            // 
            // tabMyRequests
            // 
            this.tabMyRequests.Controls.Add(this.gridMyRequests);
            this.tabMyRequests.Controls.Add(this.btnCancelRequest);
            this.tabMyRequests.Location = new System.Drawing.Point(4, 22);
            this.tabMyRequests.Name = "tabMyRequests";
            this.tabMyRequests.Padding = new System.Windows.Forms.Padding(3);
            this.tabMyRequests.Size = new System.Drawing.Size(976, 475);
            this.tabMyRequests.TabIndex = 1;
            this.tabMyRequests.Text = "My Requests";
            this.tabMyRequests.UseVisualStyleBackColor = true;
            // 
            // tabMyBorrowed
            // 
            this.lblNextDue = new System.Windows.Forms.Label();
            this.panelBorrowedTop = new System.Windows.Forms.Panel();
            this.panelBorrowedTop.Controls.Add(this.lblNextDue);
            this.panelBorrowedTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBorrowedTop.Height = 36;
            this.tabMyBorrowed.Controls.Add(this.gridMyBorrowed);
            this.tabMyBorrowed.Controls.Add(this.panelBorrowedTop);
            this.tabMyBorrowed.Location = new System.Drawing.Point(4, 22);
            this.tabMyBorrowed.Name = "tabMyBorrowed";
            this.tabMyBorrowed.Padding = new System.Windows.Forms.Padding(3);
            this.tabMyBorrowed.Size = new System.Drawing.Size(976, 475);
            this.tabMyBorrowed.TabIndex = 2;
            this.tabMyBorrowed.Text = "My Borrowed";
            this.tabMyBorrowed.UseVisualStyleBackColor = true;
            // 
            // lblNextDue
            // 
            this.lblNextDue.AutoSize = true;
            this.lblNextDue.Location = new System.Drawing.Point(6, 11);
            this.lblNextDue.Name = "lblNextDue";
            this.lblNextDue.Size = new System.Drawing.Size(163, 13);
            this.lblNextDue.TabIndex = 9;
            this.lblNextDue.Text = "Next due: (waiting for approval)";
            // 
            // gridMyRequests
            // 
            this.gridMyRequests.AllowUserToAddRows = false;
            this.gridMyRequests.AllowUserToDeleteRows = false;
            this.gridMyRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridMyRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMyRequests.Location = new System.Drawing.Point(6, 41);
            this.gridMyRequests.MultiSelect = false;
            this.gridMyRequests.Name = "gridMyRequests";
            this.gridMyRequests.ReadOnly = true;
            this.gridMyRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridMyRequests.Size = new System.Drawing.Size(964, 428);
            this.gridMyRequests.TabIndex = 7;
            // 
            // btnCancelRequest
            // 
            this.btnCancelRequest.Location = new System.Drawing.Point(6, 12);
            this.btnCancelRequest.Name = "btnCancelRequest";
            this.btnCancelRequest.Size = new System.Drawing.Size(119, 23);
            this.btnCancelRequest.TabIndex = 6;
            this.btnCancelRequest.Text = "Cancel Selected";
            this.btnCancelRequest.UseVisualStyleBackColor = true;
            // 
            // gridMyBorrowed
            // 
            this.gridMyBorrowed.AllowUserToAddRows = false;
            this.gridMyBorrowed.AllowUserToDeleteRows = false;
            this.gridMyBorrowed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMyBorrowed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMyBorrowed.Location = new System.Drawing.Point(3, 39);
            this.gridMyBorrowed.MultiSelect = false;
            this.gridMyBorrowed.Name = "gridMyBorrowed";
            this.gridMyBorrowed.ReadOnly = true;
            this.gridMyBorrowed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridMyBorrowed.Size = new System.Drawing.Size(970, 433);
            this.gridMyBorrowed.TabIndex = 8;
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.panelTop);
            this.Name = "Users";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Portal";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBooks)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabBooks.ResumeLayout(false);
            this.tabMyRequests.ResumeLayout(false);
            this.tabMyBorrowed.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMyRequests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMyBorrowed)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView gridBooks;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblWelcomeUser;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabBooks;
        private System.Windows.Forms.TabPage tabMyRequests;
        private System.Windows.Forms.TabPage tabMyBorrowed;
        private System.Windows.Forms.DataGridView gridMyRequests;
        private System.Windows.Forms.Button btnCancelRequest;
        private System.Windows.Forms.DataGridView gridMyBorrowed;
        private System.Windows.Forms.Panel panelBorrowedTop;
        private System.Windows.Forms.Label lblNextDue;
        private System.Windows.Forms.Label lblDue;
        private System.Windows.Forms.DateTimePicker dtpDue;
    }
}