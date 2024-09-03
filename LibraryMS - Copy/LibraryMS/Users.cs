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
    public partial class Users : Form
    {
        private string currentUsername;
        public Users()
        {
            InitializeComponent();
            Build();
        }

        private void Build()
        {
            this.Load += (s, e) => { LoadBooks(); LoadMyRequests(); LoadMyBorrowed(); };
            btnSearch.Click += (s, e) => LoadBooks(txtSearch.Text.Trim());
            btnRequest.Click += (s, e) => RequestSelected();
            if (dtpDue != null)
            {
                dtpDue.MinDate = DateTime.Today.AddDays(1);
                dtpDue.MaxDate = DateTime.Today.AddDays(14);
                dtpDue.Value = DateTime.Today.AddDays(7);
            }
            btnRefresh.Click += (s, e) => RefreshCurrentTab();
            btnLogout.Click += (s, e) => { new Authontiacation().Show(); this.Close(); };
            tabMain.SelectedIndexChanged += (s, e) => RefreshCurrentTab();
            btnCancelRequest.Click += (s, e) => CancelSelectedRequest();
        }

        public void SetCurrentUsername(string username)
        {
            currentUsername = username;
            if (!string.IsNullOrWhiteSpace(currentUsername) && lblWelcomeUser != null)
            {
                lblWelcomeUser.Text = "Welcome, " + currentUsername;
            }
        }

        private void LoadBooks(string query = "")
        {
            var table = SqlHelper.BooksList();
            if (!string.IsNullOrWhiteSpace(query))
            {
                var rows = table.Select($"Title LIKE '%{query.Replace("'","''") }%' OR Author LIKE '%{query.Replace("'","''") }%' OR Category LIKE '%{query.Replace("'","''") }%'");
                table = rows.Length > 0 ? rows.CopyToDataTable() : table.Clone();
            }
            gridBooks.DataSource = table;
        }

        private void RequestSelected()
        {
            if (gridBooks.CurrentRow == null) { MessageBox.Show("Select a book"); return; }
            if (string.IsNullOrWhiteSpace(currentUsername)) { MessageBox.Show("Username missing"); return; }
            int bookId = Convert.ToInt32(gridBooks.CurrentRow.Cells["BookId"].Value);
            DateTime desiredDue = dtpDue != null ? dtpDue.Value.Date : DateTime.Today.AddDays(7);
            if (desiredDue > DateTime.Today.AddDays(14)) { MessageBox.Show("Return date cannot exceed 14 days."); return; }
            if (desiredDue <= DateTime.Today) { MessageBox.Show("Return date must be from tomorrow onward."); return; }
            var res = RequestBorrowWithDue(currentUsername, bookId, desiredDue);
            if (!res.Success) { MessageBox.Show(res.ErrorMessage); return; }
            MessageBox.Show("Request submitted");
            LoadMyRequests();
        }

        private SqlHelper.CreateResult RequestBorrowWithDue(string username, int bookId, DateTime due)
        {
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand("sp_RequestBorrowWithDue", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.Parameters.AddWithValue("@DueDate", due);
                var outOk = new SqlParameter("@Ok", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var outMsg = new SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outOk);
                cmd.Parameters.Add(outMsg);
                conn.Open();
                cmd.ExecuteNonQuery();
                var ok = (bool)(outOk.Value ?? false);
                var msg = (outMsg.Value ?? "").ToString();
                return new SqlHelper.CreateResult { Success = ok, ErrorMessage = ok ? null : msg };
            }
        }

        private void RefreshCurrentTab()
        {
            if (tabMain.SelectedTab == tabBooks) { LoadBooks(txtSearch.Text.Trim()); return; }
            if (tabMain.SelectedTab == tabMyRequests) { LoadMyRequests(); return; }
            if (tabMain.SelectedTab == tabMyBorrowed) { LoadMyBorrowed(); return; }
        }

        private string ConnStr => ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString;

        private void LoadMyRequests()
        {
            if (string.IsNullOrWhiteSpace(currentUsername)) return;
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(@"SELECT r.RequestId, b.Title, r.RequestedAt, r.Status
                                              FROM BorrowRequests r
                                              JOIN Users u ON u.UserId=r.UserId
                                              JOIN Books b ON b.BookId=r.BookId
                                              WHERE u.Username=@u
                                              ORDER BY r.RequestedAt DESC", conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@u", currentUsername);
                var t = new DataTable();
                da.Fill(t);
                gridMyRequests.DataSource = t;
            }
        }

        private void LoadMyBorrowed()
        {
            if (string.IsNullOrWhiteSpace(currentUsername)) return;
            // Use existing history proc and filter to active
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand("sp_GetBorrowedBooksByStudent", conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", currentUsername);
                var t = new DataTable();
                da.Fill(t);
                // Filter active borrows (ReturnDate IS NULL)
                var rows = t.Select("ReturnDate IS NULL");
                var active = rows.Length > 0 ? rows.CopyToDataTable() : t.Clone();
                gridMyBorrowed.DataSource = active;

                // Update next due label
                DateTime? nextDue = null;
                foreach (DataRow r in active.Rows)
                {
                    if (DateTime.TryParse(r["DueDate"]?.ToString(), out DateTime d))
                    {
                        if (nextDue == null || d < nextDue) nextDue = d;
                    }
                }
                if (lblNextDue != null)
                {
                    lblNextDue.Text = nextDue == null ? "Next due: none" : "Next due: " + nextDue.Value.ToString("dd/MM/yyyy");
                }

                // Color code rows: overdue red, due within 3 days orange
                gridMyBorrowed.CellFormatting -= GridMyBorrowed_CellFormatting;
                gridMyBorrowed.CellFormatting += GridMyBorrowed_CellFormatting;
            }
        }

        private void GridMyBorrowed_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = sender as DataGridView; if (grid == null) return;
            if (grid.Columns.Contains("DueDate") && e.RowIndex >= 0)
            {
                var row = grid.Rows[e.RowIndex];
                var dueObj = row.Cells["DueDate"].Value;
                if (dueObj == null) return;
                if (DateTime.TryParse(dueObj.ToString(), out DateTime due))
                {
                    if (due < DateTime.Today)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.MistyRose;
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkRed;
                    }
                    else if ((due - DateTime.Today).TotalDays <= 3)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LemonChiffon;
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkOrange;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
        }

        private void CancelSelectedRequest()
        {
            if (gridMyRequests.CurrentRow == null) { MessageBox.Show("Select a request"); return; }
            int requestId = Convert.ToInt32(gridMyRequests.CurrentRow.Cells["RequestId"].Value);
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand("sp_CancelBorrowRequest", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", currentUsername);
                cmd.Parameters.AddWithValue("@RequestId", requestId);
                var outOk = new SqlParameter("@Ok", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var outMsg = new SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.AddRange(new[] { outOk, outMsg });
                conn.Open(); cmd.ExecuteNonQuery();
                var ok = (bool)(outOk.Value ?? false);
                var msg = (outMsg.Value ?? "").ToString();
                if (!ok) { MessageBox.Show(msg); return; }
                MessageBox.Show("Request canceled.");
                LoadMyRequests();
            }
        }
    }
}
