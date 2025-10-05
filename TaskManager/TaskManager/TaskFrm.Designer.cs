namespace TaskManager
{
    partial class TaskFrm
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
            this.startDate = new System.Windows.Forms.Label();
            this.registebtnTsk = new System.Windows.Forms.Button();
            this.emailLogin = new System.Windows.Forms.TextBox();
            this.statusLbl = new System.Windows.Forms.Label();
            this.EmailLoginlbl = new System.Windows.Forms.Label();
            this.labelTaskHeader = new System.Windows.Forms.Label();
            this.EndData = new System.Windows.Forms.Label();
            this.updTask = new System.Windows.Forms.Button();
            this.dltTask = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.searchTask = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.searchbtn = new System.Windows.Forms.Button();
            this.logoutBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // startDate
            // 
            this.startDate.AutoSize = true;
            this.startDate.Location = new System.Drawing.Point(111, 203);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(55, 13);
            this.startDate.TabIndex = 27;
            this.startDate.Text = "Start Date";
            // 
            // registebtnTsk
            // 
            this.registebtnTsk.BackColor = System.Drawing.Color.Green;
            this.registebtnTsk.Location = new System.Drawing.Point(113, 307);
            this.registebtnTsk.Name = "registebtnTsk";
            this.registebtnTsk.Size = new System.Drawing.Size(75, 23);
            this.registebtnTsk.TabIndex = 25;
            this.registebtnTsk.Text = "Add Task";
            this.registebtnTsk.UseVisualStyleBackColor = false;
            this.registebtnTsk.Click += new System.EventHandler(this.registebtnTsk_Click);
            // 
            // emailLogin
            // 
            this.emailLogin.Location = new System.Drawing.Point(113, 141);
            this.emailLogin.Name = "emailLogin";
            this.emailLogin.Size = new System.Drawing.Size(232, 20);
            this.emailLogin.TabIndex = 23;
            // 
            // statusLbl
            // 
            this.statusLbl.AutoSize = true;
            this.statusLbl.Location = new System.Drawing.Point(110, 164);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(35, 13);
            this.statusLbl.TabIndex = 22;
            this.statusLbl.Text = "Status";
            // 
            // EmailLoginlbl
            // 
            this.EmailLoginlbl.AutoSize = true;
            this.EmailLoginlbl.Location = new System.Drawing.Point(110, 125);
            this.EmailLoginlbl.Name = "EmailLoginlbl";
            this.EmailLoginlbl.Size = new System.Drawing.Size(62, 13);
            this.EmailLoginlbl.TabIndex = 21;
            this.EmailLoginlbl.Text = "Task Name";
            // 
            // labelTaskHeader
            // 
            this.labelTaskHeader.AutoSize = true;
            this.labelTaskHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTaskHeader.Location = new System.Drawing.Point(210, 52);
            this.labelTaskHeader.Name = "labelTaskHeader";
            this.labelTaskHeader.Size = new System.Drawing.Size(317, 33);
            this.labelTaskHeader.TabIndex = 20;
            this.labelTaskHeader.Text = "Task Management System";
            // 
            // EndData
            // 
            this.EndData.AutoSize = true;
            this.EndData.Location = new System.Drawing.Point(111, 260);
            this.EndData.Name = "EndData";
            this.EndData.Size = new System.Drawing.Size(49, 13);
            this.EndData.TabIndex = 30;
            this.EndData.Text = "End Date";
            // 
            // updTask
            // 
            this.updTask.BackColor = System.Drawing.Color.Yellow;
            this.updTask.Location = new System.Drawing.Point(194, 307);
            this.updTask.Name = "updTask";
            this.updTask.Size = new System.Drawing.Size(75, 23);
            this.updTask.TabIndex = 32;
            this.updTask.Text = "Update";
            this.updTask.UseVisualStyleBackColor = false;
            this.updTask.Click += new System.EventHandler(this.updTask_Click);
            // 
            // dltTask
            // 
            this.dltTask.BackColor = System.Drawing.Color.Red;
            this.dltTask.Location = new System.Drawing.Point(275, 307);
            this.dltTask.Name = "dltTask";
            this.dltTask.Size = new System.Drawing.Size(75, 23);
            this.dltTask.TabIndex = 33;
            this.dltTask.Text = "Delete";
            this.dltTask.UseVisualStyleBackColor = false;
            this.dltTask.Click += new System.EventHandler(this.dltTask_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(420, 180);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(368, 150);
            this.dataGridView1.TabIndex = 34;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // searchTask
            // 
            this.searchTask.Location = new System.Drawing.Point(456, 140);
            this.searchTask.Name = "searchTask";
            this.searchTask.Size = new System.Drawing.Size(146, 20);
            this.searchTask.TabIndex = 35;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(114, 179);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 37;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Location = new System.Drawing.Point(114, 230);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerStart.TabIndex = 38;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(114, 276);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 39;
            // 
            // searchbtn
            // 
            this.searchbtn.Location = new System.Drawing.Point(656, 137);
            this.searchbtn.Name = "searchbtn";
            this.searchbtn.Size = new System.Drawing.Size(75, 23);
            this.searchbtn.TabIndex = 40;
            this.searchbtn.Text = "Search";
            this.searchbtn.UseVisualStyleBackColor = true;
            this.searchbtn.Click += new System.EventHandler(this.searchbtn_Click);
            // 
            // logoutBtn
            // 
            this.logoutBtn.BackColor = System.Drawing.Color.Orange;
            this.logoutBtn.Location = new System.Drawing.Point(12, 12);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(75, 23);
            this.logoutBtn.TabIndex = 41;
            this.logoutBtn.Text = "Logout";
            this.logoutBtn.UseVisualStyleBackColor = false;
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.BackColor = System.Drawing.Color.LightBlue;
            this.clearBtn.Location = new System.Drawing.Point(356, 307);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 23);
            this.clearBtn.TabIndex = 42;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = false;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // TaskFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.searchbtn);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.searchTask);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dltTask);
            this.Controls.Add(this.updTask);
            this.Controls.Add(this.EndData);
            this.Controls.Add(this.startDate);
            this.Controls.Add(this.registebtnTsk);
            this.Controls.Add(this.emailLogin);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.EmailLoginlbl);
            this.Controls.Add(this.labelTaskHeader);
            this.Name = "TaskFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TaskFrm";
            this.Load += new System.EventHandler(this.TaskFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label startDate;
        private System.Windows.Forms.Button registebtnTsk;
        private System.Windows.Forms.TextBox emailLogin;
        private System.Windows.Forms.Label statusLbl;
        private System.Windows.Forms.Label EmailLoginlbl;
        private System.Windows.Forms.Label labelTaskHeader;
        private System.Windows.Forms.Label EndData;
        private System.Windows.Forms.Button updTask;
        private System.Windows.Forms.Button dltTask;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox searchTask;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button searchbtn;
        private System.Windows.Forms.Button logoutBtn;
        private System.Windows.Forms.Button clearBtn;
    }
}