namespace PlannerPro
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
            this.taskName = new System.Windows.Forms.Label();
            this.descriptionlbl = new System.Windows.Forms.Label();
            this.tasklable = new System.Windows.Forms.TextBox();
            this.discriptiointxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.taskview = new System.Windows.Forms.DataGridView();
            this.searchLabel = new System.Windows.Forms.TextBox();
            this.search = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Prioritylbl = new System.Windows.Forms.Label();
            this.registerbtn = new System.Windows.Forms.Button();
            this.updtbtn = new System.Windows.Forms.Button();
            this.combotask = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.taskview)).BeginInit();
            this.SuspendLayout();
            // 
            // taskName
            // 
            this.taskName.AutoSize = true;
            this.taskName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskName.Location = new System.Drawing.Point(160, 120);
            this.taskName.Name = "taskName";
            this.taskName.Size = new System.Drawing.Size(75, 16);
            this.taskName.TabIndex = 0;
            this.taskName.Text = "TaskName";
            // 
            // descriptionlbl
            // 
            this.descriptionlbl.AutoSize = true;
            this.descriptionlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionlbl.Location = new System.Drawing.Point(160, 170);
            this.descriptionlbl.Name = "descriptionlbl";
            this.descriptionlbl.Size = new System.Drawing.Size(63, 16);
            this.descriptionlbl.TabIndex = 1;
            this.descriptionlbl.Text = "Disription";
            // 
            // tasklable
            // 
            this.tasklable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tasklable.Location = new System.Drawing.Point(259, 120);
            this.tasklable.Name = "tasklable";
            this.tasklable.Size = new System.Drawing.Size(144, 22);
            this.tasklable.TabIndex = 2;
            // 
            // discriptiointxt
            // 
            this.discriptiointxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.discriptiointxt.Location = new System.Drawing.Point(259, 170);
            this.discriptiointxt.Name = "discriptiointxt";
            this.discriptiointxt.Size = new System.Drawing.Size(144, 22);
            this.discriptiointxt.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(426, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Task Management Form";
            // 
            // taskview
            // 
            this.taskview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.taskview.Location = new System.Drawing.Point(429, 151);
            this.taskview.Name = "taskview";
            this.taskview.Size = new System.Drawing.Size(450, 150);
            this.taskview.TabIndex = 5;
            this.taskview.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.taskview_CellClick);
            // 
            // searchLabel
            // 
            this.searchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchLabel.Location = new System.Drawing.Point(483, 114);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(199, 22);
            this.searchLabel.TabIndex = 6;
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(688, 113);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(75, 23);
            this.search.TabIndex = 7;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(161, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 16);
            this.label4.TabIndex = 9;
            // 
            // Prioritylbl
            // 
            this.Prioritylbl.AutoSize = true;
            this.Prioritylbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Prioritylbl.Location = new System.Drawing.Point(161, 210);
            this.Prioritylbl.Name = "Prioritylbl";
            this.Prioritylbl.Size = new System.Drawing.Size(48, 16);
            this.Prioritylbl.TabIndex = 8;
            this.Prioritylbl.Text = "Priority";
            // 
            // registerbtn
            // 
            this.registerbtn.Location = new System.Drawing.Point(160, 260);
            this.registerbtn.Name = "registerbtn";
            this.registerbtn.Size = new System.Drawing.Size(75, 23);
            this.registerbtn.TabIndex = 12;
            this.registerbtn.Text = "Register";
            this.registerbtn.UseVisualStyleBackColor = true;
            this.registerbtn.Click += new System.EventHandler(this.registerbtn_Click);
            // 
            // updtbtn
            // 
            this.updtbtn.Location = new System.Drawing.Point(319, 260);
            this.updtbtn.Name = "updtbtn";
            this.updtbtn.Size = new System.Drawing.Size(75, 23);
            this.updtbtn.TabIndex = 13;
            this.updtbtn.Text = "Update";
            this.updtbtn.UseVisualStyleBackColor = true;
            // 
            // combotask
            // 
            this.combotask.FormattingEnabled = true;
            this.combotask.Location = new System.Drawing.Point(259, 205);
            this.combotask.Name = "combotask";
            this.combotask.Size = new System.Drawing.Size(144, 21);
            this.combotask.TabIndex = 14;
            // 
            // TaskFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 450);
            this.Controls.Add(this.combotask);
            this.Controls.Add(this.updtbtn);
            this.Controls.Add(this.registerbtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Prioritylbl);
            this.Controls.Add(this.search);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.taskview);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.discriptiointxt);
            this.Controls.Add(this.tasklable);
            this.Controls.Add(this.descriptionlbl);
            this.Controls.Add(this.taskName);
            this.Name = "TaskFrm";
            this.Text = "TaskFrm";
            this.Load += new System.EventHandler(this.TaskFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.taskview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label taskName;
        private System.Windows.Forms.Label descriptionlbl;
        private System.Windows.Forms.TextBox tasklable;
        private System.Windows.Forms.TextBox discriptiointxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView taskview;
        private System.Windows.Forms.TextBox searchLabel;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Prioritylbl;
        private System.Windows.Forms.Button registerbtn;
        private System.Windows.Forms.Button updtbtn;
        private System.Windows.Forms.ComboBox combotask;
    }
}