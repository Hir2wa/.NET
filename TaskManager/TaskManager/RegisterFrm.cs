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
    public partial class RegisterFrm : Form
    {
        public RegisterFrm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void registerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Navigate back to Login form
            FormManager.ShowForm<LoginForm>("Login");
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            // Registration validation
            if (string.IsNullOrEmpty(emailLogin.Text) || string.IsNullOrEmpty(passwordLogin.Text) || string.IsNullOrEmpty(confirmpsdLbl.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (passwordLogin.Text != confirmpsdLbl.Text)
            {
                MessageBox.Show("Passwords do not match.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!checkConditionBox.Checked)
            {
                MessageBox.Show("Please agree to the terms and conditions.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // For demo purposes, accept any valid registration
            // In a real app, you would save to database
            MessageBox.Show("Registration successful! You can now login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // Navigate back to Login form
            FormManager.ShowForm<LoginForm>("Login");
        }

        private void passwordLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void emailLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void psdLogin_Click(object sender, EventArgs e)
        {

        }

        private void EmailLoginlbl_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
