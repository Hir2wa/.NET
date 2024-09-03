using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryMS
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            ShowPanel("home");
            BuildPlaceholders();
        }

        private void BuildPlaceholders()
        {
            // Home summary labels
            pnlHome.Controls.Add(new Label { Name = "lblTotalPatients", Text = "Total patients: -", AutoSize = true, Location = new System.Drawing.Point(20, 20) });
            pnlHome.Controls.Add(new Label { Name = "lblTotalDoctors", Text = "Total doctors: -", AutoSize = true, Location = new System.Drawing.Point(20, 45) });
            pnlHome.Controls.Add(new Label { Name = "lblScheduledAppointments", Text = "Scheduled appointments: -", AutoSize = true, Location = new System.Drawing.Point(20, 70) });

            // Users UI: top panel with inputs and actions + grid
            var usersTop = new Panel { Dock = DockStyle.Top, Height = 90 };
            var txtFull = new TextBox { Name = "txtUserFull", Width = 160, Location = new System.Drawing.Point(10, 20) };
            var txtUname = new TextBox { Name = "txtUserUname", Width = 120, Location = new System.Drawing.Point(180, 20) };
            var txtEmail = new TextBox { Name = "txtUserEmail", Width = 180, Location = new System.Drawing.Point(310, 20) };
            var txtPwd = new TextBox { Name = "txtUserPwd", Width = 120, Location = new System.Drawing.Point(500, 20) };
            var cmbRole = new ComboBox { Name = "cmbUserRole", Width = 100, Location = new System.Drawing.Point(630, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbRole.Items.AddRange(new object[] { "Patient", "Doctor", "Admin" });
            var btnUAdd = new Button { Name = "btnUserAdd", Text = "Add", Location = new System.Drawing.Point(10, 55), Width = 80 };
            var btnUUpdate = new Button { Name = "btnUserUpdate", Text = "Update", Location = new System.Drawing.Point(95, 55), Width = 80 };
            var btnUDelete = new Button { Name = "btnUserDelete", Text = "Delete", Location = new System.Drawing.Point(180, 55), Width = 80 };
            usersTop.Controls.AddRange(new Control[] { txtFull, txtUname, txtEmail, txtPwd, cmbRole, btnUAdd, btnUUpdate, btnUDelete });

            var gridUsers = new DataGridView { Name = "gridUsers", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
            pnlUsers.Controls.Add(gridUsers);
            pnlUsers.Controls.Add(usersTop);

            // Medical Records UI: top panel with inputs and actions + grid
            var medicalTop = new Panel { Dock = DockStyle.Top, Height = 90 };
            medicalTop.Controls.Add(new Label { Text = "Patient", AutoSize = true, Location = new System.Drawing.Point(10, 5) });
            var txtPatient = new TextBox { Name = "txtPatientName", Width = 180, Location = new System.Drawing.Point(10, 20) };
            medicalTop.Controls.Add(new Label { Text = "Doctor", AutoSize = true, Location = new System.Drawing.Point(200, 5) });
            var txtDoctor = new TextBox { Name = "txtDoctorName", Width = 150, Location = new System.Drawing.Point(200, 20) };
            medicalTop.Controls.Add(new Label { Text = "Diagnosis", AutoSize = true, Location = new System.Drawing.Point(360, 5) });
            var txtDiagnosis = new TextBox { Name = "txtDiagnosis", Width = 200, Location = new System.Drawing.Point(360, 20) };
            medicalTop.Controls.Add(new Label { Text = "Treatment", AutoSize = true, Location = new System.Drawing.Point(570, 5) });
            var txtTreatment = new TextBox { Name = "txtTreatment", Width = 150, Location = new System.Drawing.Point(570, 20) };
            var btnAdd = new Button { Name = "btnMedicalAdd", Text = "Add", Location = new System.Drawing.Point(10, 55), Width = 80 };
            var btnUpdate = new Button { Name = "btnMedicalUpdate", Text = "Update", Location = new System.Drawing.Point(95, 55), Width = 80 };
            var btnDelete = new Button { Name = "btnMedicalDelete", Text = "Delete", Location = new System.Drawing.Point(180, 55), Width = 80 };
            medicalTop.Controls.AddRange(new Control[] { txtPatient, txtDoctor, txtDiagnosis, txtTreatment, btnAdd, btnUpdate, btnDelete });

            var gridMedical = new DataGridView { Name = "gridMedical", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
            pnlBooks.Controls.Add(gridMedical);
            pnlBooks.Controls.Add(medicalTop);

            // Reports: export buttons
            var btnOverdue = new Button { Name = "btnExportOverdue", Text = "Export Overdue Appointments (CSV)", Location = new System.Drawing.Point(20, 20), Width = 250 };
            var btnMostActive = new Button { Name = "btnExportMostActive", Text = "Export Most Active Patients (CSV)", Location = new System.Drawing.Point(280, 20), Width = 250 };
            var btnOverduePdf = new Button { Name = "btnExportOverduePdf", Text = "Export Overdue Appointments (PDF)", Location = new System.Drawing.Point(20, 60), Width = 250 };
            var btnMostActivePdf = new Button { Name = "btnExportMostActivePdf", Text = "Export Most Active Patients (PDF)", Location = new System.Drawing.Point(280, 60), Width = 250 };
            btnOverdue.Click += (s, e) => SqlHelper.ExportCsv("sp_GetOverdueAppointments", "overdue_appointments.csv");
            btnMostActive.Click += (s, e) => SqlHelper.ExportCsv("sp_GetMostActivePatients", "most_active_patients.csv");
            btnOverduePdf.Click += (s, e) => SqlHelper.ExportPdf("sp_GetOverdueAppointments", "overdue_appointments.pdf", "Overdue Appointments");
            btnMostActivePdf.Click += (s, e) => SqlHelper.ExportPdf("sp_GetMostActivePatients", "most_active_patients.pdf", "Most Active Patients");
            pnlReports.Controls.Add(btnOverdue);
            pnlReports.Controls.Add(btnMostActive);
            pnlReports.Controls.Add(btnOverduePdf);
            pnlReports.Controls.Add(btnMostActivePdf);

            // Settings placeholder
            var btnLogout = new Button { Text = "Logout", Location = new System.Drawing.Point(20, 20) };
            btnLogout.Click += (s, e) => { this.Close(); Application.OpenForms[0]?.Show(); };
            pnlSettings.Controls.Add(btnLogout);
        }

        private void ShowPanel(string key)
        {
            pnlHome.Visible = key == "home";
            pnlUsers.Visible = key == "users";
            pnlBooks.Visible = key == "medical"; // Changed from "books" to "medical"
            pnlReports.Visible = key == "reports";
            pnlSettings.Visible = key == "settings";

            if (key == "medical")
            {
                LoadMedicalRecords();
            }
            else if (key == "users")
            {
                LoadUsers();
            }
            else if (key == "home")
            {
                LoadSummary();
            }
        }

        private void btnHome_Click(object sender, EventArgs e) => ShowPanel("home");
        private void btnManageUsers_Click(object sender, EventArgs e) => ShowPanel("users");
        private void btnManageBooks_Click(object sender, EventArgs e) => ShowPanel("medical");
        private void btnReports_Click(object sender, EventArgs e) => ShowPanel("reports");
        private void btnSettings_Click(object sender, EventArgs e) => ShowPanel("settings");

        private void LoadMedicalRecords()
        {
            var grid = pnlBooks.Controls.Find("gridMedical", true).FirstOrDefault() as DataGridView;
            if (grid == null) return;
            grid.DataSource = SqlHelper.MedicalRecordsList();
        }

        private void WireMedicalEvents()
        {
            var btnAdd = pnlBooks.Controls.Find("btnMedicalAdd", true).FirstOrDefault() as Button;
            var btnUpdate = pnlBooks.Controls.Find("btnMedicalUpdate", true).FirstOrDefault() as Button;
            var btnDelete = pnlBooks.Controls.Find("btnMedicalDelete", true).FirstOrDefault() as Button;
            var grid = pnlBooks.Controls.Find("gridMedical", true).FirstOrDefault() as DataGridView;

            if (btnAdd != null) btnAdd.Click += (s, e) =>
            {
                var patient = (pnlBooks.Controls.Find("txtPatientName", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var doctor = (pnlBooks.Controls.Find("txtDoctorName", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var diagnosis = (pnlBooks.Controls.Find("txtDiagnosis", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var treatment = (pnlBooks.Controls.Find("txtTreatment", true).FirstOrDefault() as TextBox)?.Text.Trim();
                if (string.IsNullOrWhiteSpace(patient) || string.IsNullOrWhiteSpace(doctor) || string.IsNullOrWhiteSpace(diagnosis)) { MessageBox.Show("Patient, Doctor and Diagnosis required"); return; }
                // Note: This is a simplified version - in a real system you'd need to look up user IDs
                MessageBox.Show("Medical record creation requires user ID lookup - simplified for demo");
                LoadMedicalRecords();
            };

            if (btnUpdate != null) btnUpdate.Click += (s, e) =>
            {
                var row = grid?.CurrentRow; if (row == null) { MessageBox.Show("Select a medical record"); return; }
                var recordId = Convert.ToInt32(row.Cells["RecordId"].Value);
                var diagnosis = (pnlBooks.Controls.Find("txtDiagnosis", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var treatment = (pnlBooks.Controls.Find("txtTreatment", true).FirstOrDefault() as TextBox)?.Text.Trim();
                if (string.IsNullOrWhiteSpace(diagnosis)) { MessageBox.Show("Diagnosis required"); return; }
                SqlHelper.MedicalRecordsUpdate(recordId, diagnosis, treatment, "", "", "Active");
                LoadMedicalRecords();
            };

            if (btnDelete != null) btnDelete.Click += (s, e) =>
            {
                var row = grid?.CurrentRow; if (row == null) { MessageBox.Show("Select a medical record"); return; }
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
                    var r = grid.CurrentRow; if (r == null) return;
                    var patientTb = pnlBooks.Controls.Find("txtPatientName", true).FirstOrDefault() as TextBox;
                    var doctorTb = pnlBooks.Controls.Find("txtDoctorName", true).FirstOrDefault() as TextBox;
                    var diagnosisTb = pnlBooks.Controls.Find("txtDiagnosis", true).FirstOrDefault() as TextBox;
                    var treatmentTb = pnlBooks.Controls.Find("txtTreatment", true).FirstOrDefault() as TextBox;
                    if (patientTb != null) patientTb.Text = r.Cells["PatientName"].Value?.ToString();
                    if (doctorTb != null) doctorTb.Text = r.Cells["DoctorName"].Value?.ToString();
                    if (diagnosisTb != null) diagnosisTb.Text = r.Cells["Diagnosis"].Value?.ToString();
                    if (treatmentTb != null) treatmentTb.Text = r.Cells["Treatment"].Value?.ToString();
                };
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            WireMedicalEvents();
            WireUserEvents();
            LoadSummary();
        }

        private void LoadSummary()
        {
            var (totalPatients, totalDoctors, scheduledAppointments) = SqlHelper.GetDashboardSummary();
            var lblPatients = pnlHome.Controls.Find("lblTotalPatients", true).FirstOrDefault() as Label;
            var lblDoctors = pnlHome.Controls.Find("lblTotalDoctors", true).FirstOrDefault() as Label;
            var lblScheduled = pnlHome.Controls.Find("lblScheduledAppointments", true).FirstOrDefault() as Label;
            if (lblPatients != null) lblPatients.Text = "Total patients: " + totalPatients;
            if (lblDoctors != null) lblDoctors.Text = "Total doctors: " + totalDoctors;
            if (lblScheduled != null) lblScheduled.Text = "Scheduled appointments: " + scheduledAppointments;
        }

        private void LoadUsers()
        {
            var grid = pnlUsers.Controls.Find("gridUsers", true).FirstOrDefault() as DataGridView;
            if (grid == null) return;
            grid.DataSource = SqlHelper.UsersList();
        }

        private void WireUserEvents()
        {
            var btnAdd = pnlUsers.Controls.Find("btnUserAdd", true).FirstOrDefault() as Button;
            var btnUpdate = pnlUsers.Controls.Find("btnUserUpdate", true).FirstOrDefault() as Button;
            var btnDelete = pnlUsers.Controls.Find("btnUserDelete", true).FirstOrDefault() as Button;
            var grid = pnlUsers.Controls.Find("gridUsers", true).FirstOrDefault() as DataGridView;

            if (btnAdd != null) btnAdd.Click += (s, e) =>
            {
                var full = (pnlUsers.Controls.Find("txtUserFull", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var uname = (pnlUsers.Controls.Find("txtUserUname", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var email = (pnlUsers.Controls.Find("txtUserEmail", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var pwd = (pnlUsers.Controls.Find("txtUserPwd", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var role = (pnlUsers.Controls.Find("cmbUserRole", true).FirstOrDefault() as ComboBox)?.SelectedItem as string ?? "User";
                if (string.IsNullOrWhiteSpace(full) || string.IsNullOrWhiteSpace(uname) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pwd)) { MessageBox.Show("All fields required"); return; }
                var res = SqlHelper.UsersAdd(full, uname, email, pwd, role);
                if (!res.Success) { MessageBox.Show(res.ErrorMessage); return; }
                LoadUsers();
            };

            if (btnUpdate != null) btnUpdate.Click += (s, e) =>
            {
                var row = grid?.CurrentRow; if (row == null) { MessageBox.Show("Select a user"); return; }
                var userId = Convert.ToInt32(row.Cells["UserId"].Value);
                var fullInput = (pnlUsers.Controls.Find("txtUserFull", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var emailInput = (pnlUsers.Controls.Find("txtUserEmail", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var roleSel = (pnlUsers.Controls.Find("cmbUserRole", true).FirstOrDefault() as ComboBox)?.SelectedItem as string;
                // If fields are empty, keep existing values from the selected row
                var full = string.IsNullOrWhiteSpace(fullInput) ? (row.Cells["FullName"].Value?.ToString() ?? "") : fullInput;
                var email = string.IsNullOrWhiteSpace(emailInput) ? (row.Cells["Email"].Value?.ToString() ?? "") : emailInput;
                var role = string.IsNullOrWhiteSpace(roleSel) ? (row.Cells["RoleName"].Value?.ToString() ?? "User") : roleSel;
                var res = SqlHelper.UsersUpdate(userId, full, email, role);
                if (!res.Success) { MessageBox.Show(res.ErrorMessage); return; }
                LoadUsers();
            };

            if (btnDelete != null) btnDelete.Click += (s, e) =>
            {
                var row = grid?.CurrentRow; if (row == null) { MessageBox.Show("Select a user"); return; }
                var userId = Convert.ToInt32(row.Cells["UserId"].Value);
                if (MessageBox.Show("Delete this user?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var res = SqlHelper.UsersDelete(userId);
                    if (!res.Success) { MessageBox.Show(res.ErrorMessage); return; }
                    LoadUsers();
                }
            };

            if (grid != null)
            {
                grid.SelectionChanged += (s, e) =>
                {
                    var r = grid.CurrentRow; if (r == null) return;
                    var txtFullName = pnlUsers.Controls.Find("txtUserFull", true).FirstOrDefault() as TextBox;
                    var txtUsername = pnlUsers.Controls.Find("txtUserUname", true).FirstOrDefault() as TextBox;
                    var txtEmail = pnlUsers.Controls.Find("txtUserEmail", true).FirstOrDefault() as TextBox;
                    var cmbRole = pnlUsers.Controls.Find("cmbUserRole", true).FirstOrDefault() as ComboBox;

                    if (txtFullName != null) txtFullName.Text = r.Cells["FullName"].Value?.ToString();
                    if (txtUsername != null) txtUsername.Text = r.Cells["Username"].Value?.ToString();
                    if (txtEmail != null) txtEmail.Text = r.Cells["Email"].Value?.ToString();
                    if (cmbRole != null) cmbRole.SelectedItem = r.Cells["RoleName"].Value?.ToString();
                };
            }
        }
    }
}
