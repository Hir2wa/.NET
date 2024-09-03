using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LibraryMS
{
    public partial class Librarian : Form
    {
        private static string ExportDirectory => @"C:\Users\Aime\Desktop\Hospital";
        private static string BuildExportPath(string baseName, string extension)
        {
            Directory.CreateDirectory(ExportDirectory);
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            return Path.Combine(ExportDirectory, $"{baseName}_{stamp}.{extension}");
        }
        public Librarian()
        {
            InitializeComponent();
            ShowPanel("home");
            BuildUi();
        }

        private void BuildUi()
        {
            // Home small summary
            pnlLibHome.Controls.Add(new Label { Text = "Doctor: daily operations", AutoSize = true, Location = new System.Drawing.Point(20, 20) });
            var kpiPatients = new Label { Name = "lblKpiPatients", AutoSize = true, Location = new System.Drawing.Point(20, 60) };
            var kpiDoctors = new Label { Name = "lblKpiDoctors", AutoSize = true, Location = new System.Drawing.Point(20, 85) };
            var kpiAppointments = new Label { Name = "lblKpiAppointments", AutoSize = true, Location = new System.Drawing.Point(20, 110) };
            var btnRefreshHome = new Button { Name = "btnRefreshHome", Text = "Refresh", Location = new System.Drawing.Point(20, 140), Width = 100 };
            btnRefreshHome.Click += (s, e) => UpdateHomeSummary();
            pnlLibHome.Controls.AddRange(new Control[] { kpiPatients, kpiDoctors, kpiAppointments, btnRefreshHome });
            UpdateHomeSummary();

            // Medical Records panel for doctor interface
            var top = new Panel { Dock = DockStyle.Top, Height = 60 };
            top.Controls.Add(new Label { Text = "Patient", AutoSize = true, Location = new System.Drawing.Point(10, 5) });
            var txtPatient = new TextBox { Name = "txtPatientName", Width = 180, Location = new System.Drawing.Point(10, 20) };
            top.Controls.Add(new Label { Text = "Diagnosis", AutoSize = true, Location = new System.Drawing.Point(200, 5) });
            var txtDiagnosis = new TextBox { Name = "txtDiagnosis", Width = 200, Location = new System.Drawing.Point(200, 20) };
            top.Controls.Add(new Label { Text = "Treatment", AutoSize = true, Location = new System.Drawing.Point(410, 5) });
            var txtTreatment = new TextBox { Name = "txtTreatment", Width = 150, Location = new System.Drawing.Point(410, 20) };
            var btnAdd = new Button { Name = "btnMedicalAdd", Text = "Add", Location = new System.Drawing.Point(580, 17), Width = 70 };
            var btnUpdate = new Button { Name = "btnMedicalUpdate", Text = "Update", Location = new System.Drawing.Point(660, 17), Width = 70 };
            var btnDelete = new Button { Name = "btnMedicalDelete", Text = "Delete", Location = new System.Drawing.Point(740, 17), Width = 70 };
            top.Controls.AddRange(new Control[] { txtPatient, txtDiagnosis, txtTreatment, btnAdd, btnUpdate, btnDelete });

            var gridMedical = new DataGridView { Name = "gridMedical", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
            pnlLibBooks.Controls.Add(gridMedical);
            pnlLibBooks.Controls.Add(top);

            // Appointment management
            var appointmentTop = new Panel { Dock = DockStyle.Top, Height = 90 };
            // Requests section
            appointmentTop.Controls.Add(new Label { Text = "Appointment Requests (select to approve)", AutoSize = true, Location = new System.Drawing.Point(10, 5) });
            var btnApprove = new Button { Name = "btnApprove", Text = "Approve", Location = new System.Drawing.Point(10, 48), Width = 100 };
            var btnComplete = new Button { Name = "btnComplete", Text = "Complete", Location = new System.Drawing.Point(120, 48), Width = 120 };
            var btnReject = new Button { Name = "btnReject", Text = "Reject", Location = new System.Drawing.Point(245, 48), Width = 100 };
            var btnExpReqCsv = new Button { Name = "btnExpReqCsv", Text = "Export Requests (CSV)", Location = new System.Drawing.Point(360, 48), Width = 140 };
            var btnExpAppCsv = new Button { Name = "btnExpAppCsv", Text = "Export Appointments (CSV)", Location = new System.Drawing.Point(510, 48), Width = 150 };
            var btnExpAppPdf = new Button { Name = "btnExpAppPdf", Text = "Export Appointments (PDF)", Location = new System.Drawing.Point(670, 48), Width = 140 };
            appointmentTop.Controls.AddRange(new Control[] { btnApprove, btnComplete, btnReject, btnExpReqCsv, btnExpAppCsv, btnExpAppPdf });

            var gridRequests = new DataGridView { Name = "gridRequests", Dock = DockStyle.Top, Height = 150, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            var gridAppointments = new DataGridView { Name = "gridAppointments", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            pnlLibBorrow.Controls.Add(gridAppointments);
            pnlLibBorrow.Controls.Add(gridRequests);
            pnlLibBorrow.Controls.Add(appointmentTop);

            // Patient History panel
            var histTop = new Panel { Dock = DockStyle.Top, Height = 50 };
            histTop.Controls.Add(new Label { Text = "Patient Username", AutoSize = true, Location = new System.Drawing.Point(10, 15) });
            var txtHU = new TextBox { Name = "txtHistUser", Width = 160, Location = new System.Drawing.Point(120, 12) };
            var btnLoadHist = new Button { Name = "btnLoadHist", Text = "Load History", Location = new System.Drawing.Point(290, 10), Width = 100 };
            histTop.Controls.AddRange(new Control[] { txtHU, btnLoadHist });
            var gridHist = new DataGridView { Name = "gridHist", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false };
            pnlLibHistory.Controls.Add(gridHist);
            pnlLibHistory.Controls.Add(histTop);

            WireEvents();
        }

        private void ShowPanel(string key)
        {
            pnlLibHome.Visible = key == "home";
            pnlLibBooks.Visible = key == "medical";
            pnlLibBorrow.Visible = key == "appointments";
            pnlLibHistory.Visible = key == "history";
            pnlLibSettings.Visible = key == "settings";

            if (key == "home") UpdateHomeSummary();
            if (key == "medical") LoadMedicalRecords();
            if (key == "appointments") { LoadRequests(); LoadAppointments(); }
        }

        private void btnLibHome_Click(object sender, EventArgs e) => ShowPanel("home");
        private void btnLibBooks_Click(object sender, EventArgs e) => ShowPanel("medical");
        private void btnLibBorrow_Click(object sender, EventArgs e) => ShowPanel("appointments");
        private void btnLibHistory_Click(object sender, EventArgs e) => ShowPanel("history");
        private void btnLibSettings_Click(object sender, EventArgs e) => ShowPanel("settings");

        private void WireEvents()
        {
            // Medical Records CRUD
            var btnAdd = pnlLibBooks.Controls.Find("btnMedicalAdd", true).FirstOrDefault() as Button;
            var btnUpdate = pnlLibBooks.Controls.Find("btnMedicalUpdate", true).FirstOrDefault() as Button;
            var btnDelete = pnlLibBooks.Controls.Find("btnMedicalDelete", true).FirstOrDefault() as Button;
            var grid = pnlLibBooks.Controls.Find("gridMedical", true).FirstOrDefault() as DataGridView;
            
            if (btnAdd != null) btnAdd.Click += (s, e) =>
            {
                var patient = (pnlLibBooks.Controls.Find("txtPatientName", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var diagnosis = (pnlLibBooks.Controls.Find("txtDiagnosis", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var treatment = (pnlLibBooks.Controls.Find("txtTreatment", true).FirstOrDefault() as TextBox)?.Text.Trim();
                if (string.IsNullOrWhiteSpace(patient) || string.IsNullOrWhiteSpace(diagnosis)) 
                { 
                    MessageBox.Show("Patient and Diagnosis required"); 
                    return; 
                }
                MessageBox.Show("Medical record creation requires patient and doctor ID lookup - simplified for demo");
                LoadMedicalRecords();
            };
            
            if (btnUpdate != null) btnUpdate.Click += (s, e) =>
            {
                var row = grid?.CurrentRow; 
                if (row == null) 
                { 
                    MessageBox.Show("Select a medical record"); 
                    return; 
                }
                var recordId = Convert.ToInt32(row.Cells["RecordId"].Value);
                var diagnosis = (pnlLibBooks.Controls.Find("txtDiagnosis", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var treatment = (pnlLibBooks.Controls.Find("txtTreatment", true).FirstOrDefault() as TextBox)?.Text.Trim();
                if (string.IsNullOrWhiteSpace(diagnosis)) 
                { 
                    MessageBox.Show("Diagnosis required"); 
                    return; 
                }
                SqlHelper.MedicalRecordsUpdate(recordId, diagnosis, treatment, "", "", "Active");
                LoadMedicalRecords();
            };
            
            if (btnDelete != null) btnDelete.Click += (s, e) =>
            {
                var row = grid?.CurrentRow; 
                if (row == null) 
                { 
                    MessageBox.Show("Select a medical record"); 
                    return; 
                }
                var recordId = Convert.ToInt32(row.Cells["RecordId"].Value);
                if (MessageBox.Show("Delete this medical record?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlHelper.MedicalRecordsDelete(recordId);
                    LoadMedicalRecords();
                }
            };
            
            if (grid != null)
            {
                grid.SelectionChanged += (s, e) =>
                {
                    var r = grid.CurrentRow; 
                    if (r == null) return;
                    var patientTb = pnlLibBooks.Controls.Find("txtPatientName", true).FirstOrDefault() as TextBox;
                    var diagnosisTb = pnlLibBooks.Controls.Find("txtDiagnosis", true).FirstOrDefault() as TextBox;
                    var treatmentTb = pnlLibBooks.Controls.Find("txtTreatment", true).FirstOrDefault() as TextBox;
                    if (patientTb != null) patientTb.Text = r.Cells["PatientName"].Value?.ToString();
                    if (diagnosisTb != null) diagnosisTb.Text = r.Cells["Diagnosis"].Value?.ToString();
                    if (treatmentTb != null) treatmentTb.Text = r.Cells["Treatment"].Value?.ToString();
                };
            }

            // Appointment management
            var btnApprove = pnlLibBorrow.Controls.Find("btnApprove", true).FirstOrDefault() as Button;
            var btnComplete = pnlLibBorrow.Controls.Find("btnComplete", true).FirstOrDefault() as Button;
            var btnReject = pnlLibBorrow.Controls.Find("btnReject", true).FirstOrDefault() as Button;
            
            if (btnApprove != null) btnApprove.Click += (s, e) =>
            {
                var reqGrid = pnlLibBorrow.Controls.Find("gridRequests", true).FirstOrDefault() as DataGridView;
                var row = reqGrid?.CurrentRow; 
                if (row == null) 
                { 
                    MessageBox.Show("Select a request"); 
                    return; 
                }
                int requestId = Convert.ToInt32(row.Cells["RequestId"].Value);
                var ok = SqlApproveRequest(requestId, out string msg);
                if (!ok) 
                { 
                    MessageBox.Show(msg); 
                    return; 
                }
                LoadRequests(); 
                LoadAppointments();
            };
            
            if (btnComplete != null) btnComplete.Click += (s, e) =>
            {
                var gridApp = pnlLibBorrow.Controls.Find("gridAppointments", true).FirstOrDefault() as DataGridView;
                var row = gridApp?.CurrentRow; 
                if (row == null) 
                { 
                    MessageBox.Show("Select an appointment"); 
                    return; 
                }
                int appointmentId = Convert.ToInt32(row.Cells["AppointmentId"].Value);
                
                // Simple dialog for diagnosis
                var diagnosis = PromptForInput("Enter diagnosis:", "Complete Appointment");
                if (string.IsNullOrWhiteSpace(diagnosis)) return;
                
                var result = SqlHelper.CompleteAppointment(appointmentId, diagnosis);
                if (!result.Success)
                {
                    MessageBox.Show(result.ErrorMessage);
                    return;
                }
                MessageBox.Show("Appointment completed");
                LoadAppointments();
            };
            
            if (btnReject != null) btnReject.Click += (s, e) =>
            {
                var reqGrid = pnlLibBorrow.Controls.Find("gridRequests", true).FirstOrDefault() as DataGridView;
                var row = reqGrid?.CurrentRow; 
                if (row == null) 
                { 
                    MessageBox.Show("Select a request"); 
                    return; 
                }
                int requestId = Convert.ToInt32(row.Cells["RequestId"].Value);
                if (MessageBox.Show("Reject this request?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                var ok = SqlRejectRequest(requestId, out string msg);
                if (!ok) 
                { 
                    MessageBox.Show(msg); 
                    return; 
                }
                LoadRequests();
            };

            // Export buttons
            var btnReqCsv = pnlLibBorrow.Controls.Find("btnExpReqCsv", true).FirstOrDefault() as Button;
            if (btnReqCsv != null) btnReqCsv.Click += (s, e) =>
            {
                var gridR = pnlLibBorrow.Controls.Find("gridRequests", true).FirstOrDefault() as DataGridView;
                if (gridR == null) return;
                var path = BuildExportPath("Requests", "csv");
                ExportGridToCsv(gridR, path);
                MessageBox.Show("Exported: " + path);
            };

            var btnAppCsv = pnlLibBorrow.Controls.Find("btnExpAppCsv", true).FirstOrDefault() as Button;
            if (btnAppCsv != null) btnAppCsv.Click += (s, e) =>
            {
                var gridB = pnlLibBorrow.Controls.Find("gridAppointments", true).FirstOrDefault() as DataGridView;
                if (gridB == null) return;
                var path = BuildExportPath("Appointments", "csv");
                ExportGridToCsv(gridB, path);
                MessageBox.Show("Exported: " + path);
            };

            var btnAppPdf = pnlLibBorrow.Controls.Find("btnExpAppPdf", true).FirstOrDefault() as Button;
            if (btnAppPdf != null) btnAppPdf.Click += (s, e) =>
            {
                var gridB = pnlLibBorrow.Controls.Find("gridAppointments", true).FirstOrDefault() as DataGridView;
                if (gridB == null) return;
                var path = BuildExportPath("Appointments", "pdf");
                ExportGridToTextPdf(gridB, path, "Appointments Report");
                MessageBox.Show("Exported: " + path + " (simple text PDF)");
            };

            // History
            var btnLoad = pnlLibHistory.Controls.Find("btnLoadHist", true).FirstOrDefault() as Button;
            if (btnLoad != null) btnLoad.Click += (s, e) =>
            {
                var username = (pnlLibHistory.Controls.Find("txtHistUser", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var table = SqlUserHistory(username);
                var gridH = pnlLibHistory.Controls.Find("gridHist", true).FirstOrDefault() as DataGridView;
                if (gridH != null) gridH.DataSource = table;
            };
        }

        private void btnLibLogout_Click(object sender, EventArgs e)
        {
            new Authontiacation().Show();
            this.Close();
        }

        private void LoadMedicalRecords()
        {
            var grid = pnlLibBooks.Controls.Find("gridMedical", true).FirstOrDefault() as DataGridView;
            if (grid == null) return;
            grid.DataSource = SqlHelper.MedicalRecordsList();
        }

        private void LoadAppointments()
        {
            var grid = pnlLibBorrow.Controls.Find("gridAppointments", true).FirstOrDefault() as DataGridView;
            if (grid == null) return;
            grid.DataSource = SqlActiveAppointments();
        }

        private void LoadRequests()
        {
            var grid = pnlLibBorrow.Controls.Find("gridRequests", true).FirstOrDefault() as DataGridView;
            if (grid == null) return;
            grid.DataSource = SqlAppointmentRequests();
        }

        // DB calls
        private DataTable SqlActiveAppointments()
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT a.AppointmentId, p.FullName AS PatientName, d.FullName AS DoctorName, a.AppointmentDate, a.AppointmentTime, a.Status FROM Appointments a JOIN Users p ON p.UserId=a.PatientId JOIN Users d ON d.UserId=a.DoctorId WHERE a.Status='Scheduled' ORDER BY a.AppointmentDate DESC", conn))
            using (var da = new System.Data.SqlClient.SqlDataAdapter(cmd))
            {
                var t = new DataTable(); 
                da.Fill(t); 
                return t;
            }
        }

        private DataTable SqlAppointmentRequests()
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand("sp_ListAppointmentRequests", conn))
            using (var da = new System.Data.SqlClient.SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var t = new DataTable(); 
                da.Fill(t); 
                return t;
            }
        }

        private bool SqlApproveRequest(int requestId, out string message)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand("sp_ApproveAppointment", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestId", requestId);
                var ok = new System.Data.SqlClient.SqlParameter("@Ok", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var msg = new System.Data.SqlClient.SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.AddRange(new[] { ok, msg });
                conn.Open(); 
                cmd.ExecuteNonQuery();
                message = (msg.Value ?? "").ToString();
                return (bool)(ok.Value ?? false);
            }
        }

        private bool SqlRejectRequest(int requestId, out string message)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand("sp_RejectAppointment", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestId", requestId);
                var ok = new System.Data.SqlClient.SqlParameter("@Ok", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var msg = new System.Data.SqlClient.SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.AddRange(new[] { ok, msg });
                conn.Open(); 
                cmd.ExecuteNonQuery();
                message = (msg.Value ?? "").ToString();
                return (bool)(ok.Value ?? false);
            }
        }

        private DataTable SqlUserHistory(string username)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand("sp_GetPatientHistory", conn))
            using (var da = new System.Data.SqlClient.SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PatientUsername", username);
                var t = new DataTable();
                da.Fill(t);
                return t;
            }
        }

        private void UpdateHomeSummary()
        {
            try
            {
                var (totalPatients, totalDoctors, scheduledAppointments) = SqlHelper.GetDashboardSummary();
                var p = pnlLibHome.Controls.Find("lblKpiPatients", true).FirstOrDefault() as Label;
                var d = pnlLibHome.Controls.Find("lblKpiDoctors", true).FirstOrDefault() as Label;
                var a = pnlLibHome.Controls.Find("lblKpiAppointments", true).FirstOrDefault() as Label;
                if (p != null) p.Text = "Total patients: " + totalPatients;
                if (d != null) d.Text = "Total doctors: " + totalDoctors;
                if (a != null) a.Text = "Scheduled appointments: " + scheduledAppointments;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load summary: " + ex.Message);
            }
        }

        private void pnlLibHistory_Paint(object sender, PaintEventArgs e)
        {
            // Intentionally left blank
        }

        private void ExportGridToCsv(DataGridView grid, string filePath)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append('"').Append(grid.Columns[i].HeaderText.Replace("\"", "\"\"")).Append('"');
            }
            sb.AppendLine();
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                for (int i = 0; i < grid.Columns.Count; i++)
                {
                    if (i > 0) sb.Append(",");
                    var val = row.Cells[i].Value?.ToString() ?? string.Empty;
                    sb.Append('"').Append(val.Replace("\"", "\"\"")).Append('"');
                }
                sb.AppendLine();
            }
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private void ExportGridToTextPdf(DataGridView grid, string filePath, string title)
        {
            var content = new StringBuilder();
            content.AppendLine(title);
            content.AppendLine(new string('-', 80));
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                content.Append(grid.Columns[i].HeaderText);
                if (i < grid.Columns.Count - 1) content.Append(" | ");
            }
            content.AppendLine();
            content.AppendLine(new string('-', 80));
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                for (int i = 0; i < grid.Columns.Count; i++)
                {
                    content.Append(row.Cells[i].Value?.ToString());
                    if (i < grid.Columns.Count - 1) content.Append(" | ");
                }
                content.AppendLine();
            }
            File.WriteAllText(filePath, content.ToString(), Encoding.UTF8);
        }

        private string PromptForInput(string prompt, string title)
        {
            Form inputForm = new Form
            {
                Width = 400,
                Height = 150,
                Text = title,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label label = new Label { Left = 20, Top = 20, Text = prompt, Width = 350 };
            TextBox textBox = new TextBox { Left = 20, Top = 50, Width = 340 };
            Button confirmButton = new Button { Text = "OK", Left = 200, Width = 80, Top = 80, DialogResult = DialogResult.OK };
            Button cancelButton = new Button { Text = "Cancel", Left = 290, Width = 80, Top = 80, DialogResult = DialogResult.Cancel };

            confirmButton.Click += (sender, e) => { inputForm.Close(); };
            cancelButton.Click += (sender, e) => { inputForm.Close(); };

            inputForm.Controls.Add(label);
            inputForm.Controls.Add(textBox);
            inputForm.Controls.Add(confirmButton);
            inputForm.Controls.Add(cancelButton);
            inputForm.AcceptButton = confirmButton;
            inputForm.CancelButton = cancelButton;

            return inputForm.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
        }
    }
}