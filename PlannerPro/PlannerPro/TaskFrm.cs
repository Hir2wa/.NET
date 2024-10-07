using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
namespace PlannerPro
{
    public partial class TaskFrm : Form
    {
        public TaskFrm()
        {
            InitializeComponent();
        }


    

        private void TaskFrm_Load(object sender, EventArgs e)
        {
            dispay();
        }

        private void dispay()
        {
            string con = ConfigurationManager.ConnectionStrings["plannerCon"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM TaskEnhanced", connection);
  
                DataSet ds = new DataSet();
                sda.Fill(ds, "TaskEnhanced");
                taskview.DataSource = ds.Tables["TaskEnhanced"];
            }
        }

        private void registerbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["plannerCon"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Ps_InsertTask", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tskName", tasklable.Text);
                    cmd.Parameters.AddWithValue("@descr", discriptiointxt.Text);
                    cmd.Parameters.AddWithValue("@priority", combotask.Text);
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Task registered successfully!");
                    dispay(); // Refresh the display
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void taskview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a valid row was clicked (not header row)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = taskview.Rows[e.RowIndex];
                
                // Populate the form fields with the selected row data
                tasklable.Text = row.Cells["taskname"].Value?.ToString() ?? "";
                discriptiointxt.Text = row.Cells["description"].Value?.ToString() ?? "";
                combotask.Text = row.Cells["priority"].Value?.ToString() ?? "";
            }
        }
    }
}

// Enhanced on 2025-10-19 - Commit 2
