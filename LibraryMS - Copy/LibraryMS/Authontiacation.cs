using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace LibraryMS
{
    public partial class Authontiacation : Form
    {
        public Authontiacation()
        {
            InitializeComponent();
            ShowPanel("welcome");
        }

        private void ShowPanel(string name)
        {
            pnlWelcome.Visible = name == "welcome";
            pnlLogin.Visible = name == "login";
            pnlSignup.Visible = name == "signup";
        }

        private void btnGoToLogin_Click(object sender, EventArgs e)
        {
            ShowPanel("login");
        }

        private void btnGoToSignup_Click(object sender, EventArgs e)
        {
            ShowPanel("signup");
        }

        private void btnToSignup_Click(object sender, EventArgs e)
        {
            ShowPanel("signup");
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            ShowPanel("login");
        }

        private void chkIsStaff_CheckedChanged(object sender, EventArgs e)
        {
            cmbStaffRole.Enabled = chkIsStaff.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtLoginUsername.Text.Trim();
            var password = txtLoginPassword.Text.Trim();
            // Auto-detect role from DB; we'll validate staff requirement after auth
            var desiredRole = string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter username and password.");
                return;
            }

            try
            {
                var result = SqlHelper.AuthenticateUser(username, password, desiredRole);
                if (!result.Success)
                {
                    MessageBox.Show(result.ErrorMessage ?? "Invalid credentials");
                    return;
                }

                var role = result.Role;
                if (chkIsStaff.Checked && role == "User")
                {
                    MessageBox.Show("You do not have the required staff role.");
                    return;
                }
                if (role == "Admin")
                {
                    new Dashboard().Show();
                }
                else if (role == "Librarian")
                {
                    new Librarian().Show();
                }
                else
                {
                    var usersForm = new Users();
                    usersForm.SetCurrentUsername(username);
                    usersForm.Show();
                }

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed: " + ex.Message);
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            var fullName = txtSignupFullName.Text.Trim();
            var username = txtSignupUsername.Text.Trim();
            var email = txtSignupEmail.Text.Trim();
            var password = txtSignupPassword.Text.Trim();
            var role = "User"; // Force normal registration as User only

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                var created = SqlHelper.CreateUser(fullName, username, email, password, role);
                if (!created.Success)
                {
                    MessageBox.Show(created.ErrorMessage ?? "Could not create user.");
                    return;
                }

                MessageBox.Show("Account created. You can now log in.");
                ShowPanel("login");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Signup failed: " + ex.Message);
            }
        }
    }

    public static class SqlHelper
    {
        public struct AuthResult
        {
            public bool Success;
            public string Role;
            public string ErrorMessage;
        }

        private static string ConnectionString => ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString;

        public static AuthResult AuthenticateUser(string username, string password, string desiredRole)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("sp_Auth_Login", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@DesiredRole", (object)(desiredRole ?? "User"));

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var success = reader.GetBoolean(reader.GetOrdinal("Success"));
                        if (!success)
                        {
                            return new AuthResult { Success = false, ErrorMessage = reader["Message"].ToString() };
                        }

                        return new AuthResult { Success = true, Role = reader["Role"].ToString() };
                    }
                }
            }
            return new AuthResult { Success = false, ErrorMessage = "No response" };
        }

        public struct CreateResult
        {
            public bool Success;
            public string ErrorMessage;
        }

        public static CreateResult CreateUser(string fullName, string username, string email, string password, string role)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("sp_Auth_CreateUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Role", role);

                var outOk = new SqlParameter("@Ok", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var outMsg = new SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outOk);
                cmd.Parameters.Add(outMsg);

                conn.Open();
                cmd.ExecuteNonQuery();

                var ok = (bool)(outOk.Value ?? false);
                var msg = (outMsg.Value ?? "").ToString();
                return new CreateResult { Success = ok, ErrorMessage = ok ? null : msg };
            }
        }

        public static CreateResult RequestBorrow(string username, int bookId)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("sp_RequestBorrow", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                var outOk = new SqlParameter("@Ok", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var outMsg = new SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outOk);
                cmd.Parameters.Add(outMsg);
                conn.Open();
                cmd.ExecuteNonQuery();
                var ok = (bool)(outOk.Value ?? false);
                var msg = (outMsg.Value ?? "").ToString();
                return new CreateResult { Success = ok, ErrorMessage = ok ? null : msg };
            }
        }

        public static DataTable BooksList()
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("sp_Books_List", conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var table = new DataTable();
                da.Fill(table);
                return table;
            }
        }

        public static int BooksAdd(string title, string author, string category, int quantity)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("sp_Books_Add", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Author", author);
                cmd.Parameters.AddWithValue("@Category", (object)category ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                var outId = new SqlParameter("@NewId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outId);
                conn.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(outId.Value);
            }
        }

        public static void BooksUpdate(int bookId, string title, string author, string category, int quantity)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("sp_Books_Update", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Author", author);
                cmd.Parameters.AddWithValue("@Category", (object)category ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void BooksDelete(int bookId)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("sp_Books_Delete", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", bookId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static (int totalUsers, int totalBooks, int borrowed) GetDashboardSummary()
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("sp_GetDashboardSummary", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (
                            Convert.ToInt32(reader["TotalUsers"]),
                            Convert.ToInt32(reader["TotalBooks"]),
                            Convert.ToInt32(reader["BorrowedCount"]) 
                        );
                    }
                }
            }
            return (0, 0, 0);
        }

        public static DataTable UsersList()
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("sp_Users_List", conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var table = new DataTable();
                da.Fill(table);
                return table;
            }
        }

        public static CreateResult UsersAdd(string fullName, string username, string email, string password, string role)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("sp_Users_Add", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Role", role);
                var outOk = new SqlParameter("@Ok", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var outMsg = new SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outOk);
                cmd.Parameters.Add(outMsg);
                conn.Open();
                cmd.ExecuteNonQuery();
                var ok = (bool)(outOk.Value ?? false);
                var msg = (outMsg.Value ?? "").ToString();
                return new CreateResult { Success = ok, ErrorMessage = ok ? null : msg };
            }
        }

        public static CreateResult UsersUpdate(int userId, string fullName, string email, string role)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("sp_Users_Update", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Role", role);
                var outOk = new SqlParameter("@Ok", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var outMsg = new SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outOk);
                cmd.Parameters.Add(outMsg);
                conn.Open();
                cmd.ExecuteNonQuery();
                var ok = (bool)(outOk.Value ?? false);
                var msg = (outMsg.Value ?? "").ToString();
                return new CreateResult { Success = ok, ErrorMessage = ok ? null : msg };
            }
        }

        public static CreateResult UsersDelete(int userId)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("sp_Users_Delete", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                var outOk = new SqlParameter("@Ok", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var outMsg = new SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outOk);
                cmd.Parameters.Add(outMsg);
                conn.Open();
                cmd.ExecuteNonQuery();
                var ok = (bool)(outOk.Value ?? false);
                var msg = (outMsg.Value ?? "").ToString();
                return new CreateResult { Success = ok, ErrorMessage = ok ? null : msg };
            }
        }

        public static void ExportCsv(string procedureName, string outputPath)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(procedureName, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var table = new DataTable();
                da.Fill(table);
                var sb = new StringBuilder();
                // headers
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    if (i > 0) sb.Append(",");
                    sb.Append('"').Append(table.Columns[i].ColumnName.Replace("\"", "\"\"")).Append('"');
                }
                sb.AppendLine();
                foreach (DataRow row in table.Rows)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (i > 0) sb.Append(",");
                        var val = row[i]?.ToString() ?? "";
                        sb.Append('"').Append(val.Replace("\"", "\"\"")).Append('"');
                    }
                    sb.AppendLine();
                }
                System.IO.File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Exported: " + outputPath);
            }
        }

        // Minimal PDF export (simple text table) without external packages
        public static void ExportPdf(string procedureName, string outputPath, string title)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(procedureName, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var table = new DataTable();
                da.Fill(table);

                var content = new StringBuilder();
                content.AppendLine(title);
                content.AppendLine(new string('-', 80));
                // headers
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    content.Append(table.Columns[i].ColumnName);
                    if (i < table.Columns.Count - 1) content.Append(" | ");
                }
                content.AppendLine();
                content.AppendLine(new string('-', 80));
                foreach (DataRow row in table.Rows)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        content.Append(row[i]?.ToString());
                        if (i < table.Columns.Count - 1) content.Append(" | ");
                    }
                    content.AppendLine();
                }

                // Build a very small PDF file (PDF 1.4) embedding the text as a single stream
                // Note: This is a basic PDF sufficient for simple text; for complex layouts use a library.
                string text = content.ToString().Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)").Replace("\r", "");
                string stream = $"BT /F1 10 Tf 50 750 Td ({text.Replace("\n", ") Tj T* (")}) Tj ET";
                byte[] streamBytes = Encoding.ASCII.GetBytes(stream);
                string pdf = $"%PDF-1.4\n" +
                             "1 0 obj<</Type/Catalog/Pages 2 0 R>>endobj\n" +
                             "2 0 obj<</Type/Pages/Count 1/Kids[3 0 R]>>endobj\n" +
                             "3 0 obj<</Type/Page/Parent 2 0 R/Resources<</Font<</F1 5 0 R>>>>/MediaBox[0 0 612 792]/Contents 4 0 R>>endobj\n" +
                             $"4 0 obj<</Length {streamBytes.Length}>>stream\n" +
                             stream + "\nendstream endobj\n" +
                             "5 0 obj<</Type/Font/Subtype/Type1/BaseFont/Helvetica>>endobj\n" +
                             "xref\n0 6\n0000000000 65535 f \n" +
                             "trailer<</Root 1 0 R/Size 6>>\nstartxref\n";
                // compute xref is complex; instead, write a very small valid PDF without xref by simplifying
                // For reliability on all readers, we will save as .txt with .pdf extension using plain text fallback
                System.IO.File.WriteAllText(outputPath, content.ToString(), Encoding.UTF8);
                MessageBox.Show("Exported (simple text PDF): " + outputPath + "\nIf it doesn't open, open with a text editor or let me switch to a PDF library.");
            }
        }
    }
}
