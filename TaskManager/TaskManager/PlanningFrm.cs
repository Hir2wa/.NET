using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Navigate to Register form
            FormManager.ShowForm<RegisterFrm>("Register");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            // Simple login validation (you can enhance this with database validation)
            if (string.IsNullOrEmpty(emailLogin.Text) || string.IsNullOrEmpty(passwordLogin.Text))
            {
                MessageBox.Show("Please enter both email and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // For demo purposes, accept any non-empty credentials
            // In a real app, you would validate against a database
            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // Navigate to Task Management form
            FormManager.ShowForm<TaskFrm>("TaskManager");
        }
    }
}

