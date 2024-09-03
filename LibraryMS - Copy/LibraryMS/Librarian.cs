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
        private static string ExportDirectory => @"C:\\Users\\Aime\\Desktop\\Library";
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
            pnlLibHome.Controls.Add(new Label { Text = "Librarian: daily operations", AutoSize = true, Location = new System.Drawing.Point(20, 20) });
            var kpiUsers = new Label { Name = "lblKpiUsers", AutoSize = true, Location = new System.Drawing.Point(20, 60) };
            var kpiBooks = new Label { Name = "lblKpiBooks", AutoSize = true, Location = new System.Drawing.Point(20, 85) };
            var kpiBorrowed = new Label { Name = "lblKpiBorrowed", AutoSize = true, Location = new System.Drawing.Point(20, 110) };
            var btnRefreshHome = new Button { Name = "btnRefreshHome", Text = "Refresh", Location = new System.Drawing.Point(20, 140), Width = 100 };
            btnRefreshHome.Click += (s, e) => UpdateHomeSummary();
            pnlLibHome.Controls.AddRange(new Control[] { kpiUsers, kpiBooks, kpiBorrowed, btnRefreshHome });
            UpdateHomeSummary();

            // Books panel reuses Admin Books grid layout for quick adjustments
            var top = new Panel { Dock = DockStyle.Top, Height = 60 };
            top.Controls.Add(new Label { Text = "Title", AutoSize = true, Location = new System.Drawing.Point(10, 5) });
            var txtTitle = new TextBox { Name = "txtBookTitle", Width = 180, Location = new System.Drawing.Point(10, 20) };
            top.Controls.Add(new Label { Text = "Author", AutoSize = true, Location = new System.Drawing.Point(200, 5) });
            var txtAuthor = new TextBox { Name = "txtBookAuthor", Width = 150, Location = new System.Drawing.Point(200, 20) };
            top.Controls.Add(new Label { Text = "Category", AutoSize = true, Location = new System.Drawing.Point(360, 5) });
            var txtCategory = new TextBox { Name = "txtBookCategory", Width = 120, Location = new System.Drawing.Point(360, 20) };
            top.Controls.Add(new Label { Text = "Quantity", AutoSize = true, Location = new System.Drawing.Point(490, 5) });
            var numQty = new NumericUpDown { Name = "numBookQty", Width = 80, Location = new System.Drawing.Point(490, 20), Minimum = 0, Maximum = 100000, Value = 1 };
            var btnAdd = new Button { Name = "btnBookAdd", Text = "Add", Location = new System.Drawing.Point(580, 17), Width = 70 };
            var btnUpdate = new Button { Name = "btnBookUpdate", Text = "Update", Location = new System.Drawing.Point(660, 17), Width = 70 };
            var btnDelete = new Button { Name = "btnBookDelete", Text = "Delete", Location = new System.Drawing.Point(740, 17), Width = 70 };
            top.Controls.AddRange(new Control[] { txtTitle, txtAuthor, txtCategory, numQty, btnAdd, btnUpdate, btnDelete });

            var gridBooks = new DataGridView { Name = "gridBooks", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
            pnlLibBooks.Controls.Add(gridBooks);
            pnlLibBooks.Controls.Add(top);

            // Borrow management
            var borrowTop = new Panel { Dock = DockStyle.Top, Height = 90 };
            // Requests section
            borrowTop.Controls.Add(new Label { Text = "Requests (select to approve)", AutoSize = true, Location = new System.Drawing.Point(10, 5) });
            var btnApprove = new Button { Name = "btnApprove", Text = "Approve", Location = new System.Drawing.Point(10, 48), Width = 100 };
            var btnReturn = new Button { Name = "btnReturn", Text = "Mark Returned", Location = new System.Drawing.Point(120, 48), Width = 120 };
            var btnReject = new Button { Name = "btnReject", Text = "Reject", Location = new System.Drawing.Point(245, 48), Width = 100 };
            var btnExpReqCsv = new Button { Name = "btnExpReqCsv", Text = "Export Requests (CSV)", Location = new System.Drawing.Point(360, 48), Width = 140 };
            var btnExpBorCsv = new Button { Name = "btnExpBorCsv", Text = "Export Borrows (CSV)", Location = new System.Drawing.Point(510, 48), Width = 150 };
            var btnExpBorPdf = new Button { Name = "btnExpBorPdf", Text = "Export Borrows (PDF)", Location = new System.Drawing.Point(670, 48), Width = 140 };
            borrowTop.Controls.AddRange(new Control[] { btnApprove, btnReturn, btnReject, btnExpReqCsv, btnExpBorCsv, btnExpBorPdf });

            var gridRequests = new DataGridView { Name = "gridRequests", Dock = DockStyle.Top, Height = 150, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            var gridBorrows = new DataGridView { Name = "gridBorrows", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            pnlLibBorrow.Controls.Add(gridBorrows);
            pnlLibBorrow.Controls.Add(gridRequests);
            pnlLibBorrow.Controls.Add(borrowTop);

            // History panel
            var histTop = new Panel { Dock = DockStyle.Top, Height = 50 };
            histTop.Controls.Add(new Label { Text = "Username", AutoSize = true, Location = new System.Drawing.Point(10, 15) });
            var txtHU = new TextBox { Name = "txtHistUser", Width = 160, Location = new System.Drawing.Point(80, 12) };
            var btnLoadHist = new Button { Name = "btnLoadHist", Text = "Load", Location = new System.Drawing.Point(250, 10), Width = 70 };
            histTop.Controls.AddRange(new Control[] { txtHU, btnLoadHist });
            var gridHist = new DataGridView { Name = "gridHist", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false };
            pnlLibHistory.Controls.Add(gridHist);
            pnlLibHistory.Controls.Add(histTop);

            WireEvents();
        }

        private void ShowPanel(string key)
        {
            pnlLibHome.Visible = key == "home";
            pnlLibBooks.Visible = key == "books";
            pnlLibBorrow.Visible = key == "borrow";
            pnlLibHistory.Visible = key == "history";
            pnlLibSettings.Visible = key == "settings";

            if (key == "home") UpdateHomeSummary();
            if (key == "books") LoadBooks();
            if (key == "borrow") { LoadRequests(); LoadBorrows(); }
        }

        private void btnLibHome_Click(object sender, EventArgs e) => ShowPanel("home");
        private void btnLibBooks_Click(object sender, EventArgs e) => ShowPanel("books");
        private void btnLibBorrow_Click(object sender, EventArgs e) => ShowPanel("borrow");
        private void btnLibHistory_Click(object sender, EventArgs e) => ShowPanel("history");
        private void btnLibSettings_Click(object sender, EventArgs e) => ShowPanel("settings");

        private void WireEvents()
        {
            // reuse SqlHelper books actions
            var btnAdd = pnlLibBooks.Controls.Find("btnBookAdd", true).FirstOrDefault() as Button;
            var btnUpdate = pnlLibBooks.Controls.Find("btnBookUpdate", true).FirstOrDefault() as Button;
            var btnDelete = pnlLibBooks.Controls.Find("btnBookDelete", true).FirstOrDefault() as Button;
            var grid = pnlLibBooks.Controls.Find("gridBooks", true).FirstOrDefault() as DataGridView;
            if (btnAdd != null) btnAdd.Click += (s, e) =>
            {
                var title = (pnlLibBooks.Controls.Find("txtBookTitle", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var author = (pnlLibBooks.Controls.Find("txtBookAuthor", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var category = (pnlLibBooks.Controls.Find("txtBookCategory", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var qtyCtrl = pnlLibBooks.Controls.Find("numBookQty", true).FirstOrDefault() as NumericUpDown;
                var qty = qtyCtrl != null ? (int)qtyCtrl.Value : 0;
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author)) { MessageBox.Show("Title and Author required"); return; }
                SqlHelper.BooksAdd(title, author, category, qty); LoadBooks();
            };
            if (btnUpdate != null) btnUpdate.Click += (s, e) =>
            {
                var row = grid?.CurrentRow; if (row == null) { MessageBox.Show("Select a book"); return; }
                var bookId = Convert.ToInt32(row.Cells["BookId"].Value);
                var title = (pnlLibBooks.Controls.Find("txtBookTitle", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var author = (pnlLibBooks.Controls.Find("txtBookAuthor", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var category = (pnlLibBooks.Controls.Find("txtBookCategory", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var qtyCtrl = pnlLibBooks.Controls.Find("numBookQty", true).FirstOrDefault() as NumericUpDown;
                var qty = qtyCtrl != null ? (int)qtyCtrl.Value : 0;
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author)) { MessageBox.Show("Title and Author required"); return; }
                SqlHelper.BooksUpdate(bookId, title, author, category, qty); LoadBooks();
            };
            if (btnDelete != null) btnDelete.Click += (s, e) =>
            {
                var row = grid?.CurrentRow; if (row == null) { MessageBox.Show("Select a book"); return; }
                var bookId = Convert.ToInt32(row.Cells["BookId"].Value);
                if (MessageBox.Show("Delete this book?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                { SqlHelper.BooksDelete(bookId); LoadBooks(); }
            };
            if (grid != null)
            {
                grid.SelectionChanged += (s, e) =>
                {
                    var r = grid.CurrentRow; if (r == null) return;
                    var titleTb = pnlLibBooks.Controls.Find("txtBookTitle", true).FirstOrDefault() as TextBox;
                    var authorTb = pnlLibBooks.Controls.Find("txtBookAuthor", true).FirstOrDefault() as TextBox;
                    var categoryTb = pnlLibBooks.Controls.Find("txtBookCategory", true).FirstOrDefault() as TextBox;
                    var qtyCtrl = pnlLibBooks.Controls.Find("numBookQty", true).FirstOrDefault() as NumericUpDown;
                    if (titleTb != null) titleTb.Text = r.Cells["Title"].Value?.ToString();
                    if (authorTb != null) authorTb.Text = r.Cells["Author"].Value?.ToString();
                    if (categoryTb != null) categoryTb.Text = r.Cells["Category"].Value?.ToString();
                    if (qtyCtrl != null) qtyCtrl.Value = Convert.ToDecimal(r.Cells["Quantity"].Value);
                };
            }

            // Borrow approve/return
            var btnApprove = pnlLibBorrow.Controls.Find("btnApprove", true).FirstOrDefault() as Button;
            var btnReturn = pnlLibBorrow.Controls.Find("btnReturn", true).FirstOrDefault() as Button;
            if (btnApprove != null) btnApprove.Click += (s, e) =>
            {
                var reqGrid = pnlLibBorrow.Controls.Find("gridRequests", true).FirstOrDefault() as DataGridView;
                var row = reqGrid?.CurrentRow; if (row == null) { MessageBox.Show("Select a request"); return; }
                int requestId = Convert.ToInt32(row.Cells["RequestId"].Value);
                var ok = SqlApproveRequest(requestId, out string msg);
                if (!ok) { MessageBox.Show(msg); return; }
                LoadRequests(); LoadBorrows();
            };
            if (btnReturn != null) btnReturn.Click += (s, e) =>
            {
                var gridB = pnlLibBorrow.Controls.Find("gridBorrows", true).FirstOrDefault() as DataGridView;
                var row = gridB?.CurrentRow; if (row == null) { MessageBox.Show("Select a borrow row"); return; }
                int borrowId = Convert.ToInt32(row.Cells["BorrowId"].Value);
                var ok = SqlReturn(borrowId, out string msg);
                if (!ok) { MessageBox.Show(msg); return; }
                LoadRequests(); LoadBorrows();
            };

            var btnRejectCtrl = pnlLibBorrow.Controls.Find("btnReject", true).FirstOrDefault() as Button;
            if (btnRejectCtrl != null) btnRejectCtrl.Click += (s, e) =>
            {
                var reqGrid = pnlLibBorrow.Controls.Find("gridRequests", true).FirstOrDefault() as DataGridView;
                var row = reqGrid?.CurrentRow; if (row == null) { MessageBox.Show("Select a request"); return; }
                int requestId = Convert.ToInt32(row.Cells["RequestId"].Value);
                if (MessageBox.Show("Reject this request?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                var ok = SqlRejectRequest(requestId, out string msg);
                if (!ok) { MessageBox.Show(msg); return; }
                LoadRequests();
            };

            var btnReqCsv = pnlLibBorrow.Controls.Find("btnExpReqCsv", true).FirstOrDefault() as Button;
            if (btnReqCsv != null) btnReqCsv.Click += (s, e) =>
            {
                var gridR = pnlLibBorrow.Controls.Find("gridRequests", true).FirstOrDefault() as DataGridView;
                if (gridR == null) return;
                var path = BuildExportPath("Requests", "csv");
                ExportGridToCsv(gridR, path);
                MessageBox.Show("Exported: " + path);
            };

            var btnBorCsv = pnlLibBorrow.Controls.Find("btnExpBorCsv", true).FirstOrDefault() as Button;
            if (btnBorCsv != null) btnBorCsv.Click += (s, e) =>
            {
                var gridB = pnlLibBorrow.Controls.Find("gridBorrows", true).FirstOrDefault() as DataGridView;
                if (gridB == null) return;
                var path = BuildExportPath("ActiveBorrows", "csv");
                ExportGridToCsv(gridB, path);
                MessageBox.Show("Exported: " + path);
            };

            var btnBorPdf = pnlLibBorrow.Controls.Find("btnExpBorPdf", true).FirstOrDefault() as Button;
            if (btnBorPdf != null) btnBorPdf.Click += (s, e) =>
            {
                var gridB = pnlLibBorrow.Controls.Find("gridBorrows", true).FirstOrDefault() as DataGridView;
                if (gridB == null) return;
                var path = BuildExportPath("ActiveBorrows", "pdf");
                ExportGridToTextPdf(gridB, path, "Active Borrows Report");
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

        private void LoadBooks()
        {
            var grid = pnlLibBooks.Controls.Find("gridBooks", true).FirstOrDefault() as DataGridView;
            if (grid == null) return;
            grid.DataSource = SqlHelper.BooksList();
        }

        private void LoadBorrows()
        {
            var grid = pnlLibBorrow.Controls.Find("gridBorrows", true).FirstOrDefault() as DataGridView;
            if (grid == null) return;
            grid.DataSource = SqlActiveBorrows();
        }

        private void LoadRequests()
        {
            var grid = pnlLibBorrow.Controls.Find("gridRequests", true).FirstOrDefault() as DataGridView;
            if (grid == null) return;
            grid.DataSource = SqlBorrowRequests();
            // Optional: set friendly headers
            if (grid.Columns.Contains("RequestedDueDate")) grid.Columns["RequestedDueDate"].HeaderText = "Requested Due";
        }

        // DB calls via helper/inline
        private DataTable SqlActiveBorrows()
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand("SELECT br.BorrowId, u.Username, b.Title, br.BorrowDate, br.DueDate, br.ReturnDate, br.Status FROM BorrowRecords br JOIN Users u ON u.UserId=br.UserId JOIN Books b ON b.BookId=br.BookId WHERE br.ReturnDate IS NULL ORDER BY br.BorrowDate DESC", conn))
            using (var da = new System.Data.SqlClient.SqlDataAdapter(cmd))
            {
                var t = new DataTable(); da.Fill(t); return t;
            }
        }

        private bool SqlBorrow(string username, int bookId, DateTime due, out string message)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand("sp_BorrowBook", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.Parameters.AddWithValue("@DueDate", due);
                var ok = new System.Data.SqlClient.SqlParameter("@Ok", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var msg = new System.Data.SqlClient.SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.AddRange(new[] { ok, msg });
                conn.Open(); cmd.ExecuteNonQuery();
                message = (msg.Value ?? "").ToString();
                return (bool)(ok.Value ?? false);
            }
        }

        private bool SqlReturn(int borrowId, out string message)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand("sp_ReturnBook", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BorrowId", borrowId);
                var ok = new System.Data.SqlClient.SqlParameter("@Ok", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var msg = new System.Data.SqlClient.SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.AddRange(new[] { ok, msg });
                conn.Open(); cmd.ExecuteNonQuery();
                message = (msg.Value ?? "").ToString();
                return (bool)(ok.Value ?? false);
            }
        }

        private DataTable SqlBorrowRequests()
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand("sp_ListBorrowRequests", conn))
            using (var da = new System.Data.SqlClient.SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var t = new DataTable(); da.Fill(t); return t;
            }
        }

        private bool SqlApproveRequest(int requestId, out string message)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand("sp_ApproveBorrow", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestId", requestId);
                var ok = new System.Data.SqlClient.SqlParameter("@Ok", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var msg = new System.Data.SqlClient.SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.AddRange(new[] { ok, msg });
                conn.Open(); cmd.ExecuteNonQuery();
                message = (msg.Value ?? "").ToString();
                return (bool)(ok.Value ?? false);
            }
        }

        private bool SqlRejectRequest(int requestId, out string message)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand("sp_RejectBorrow", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestId", requestId);
                var ok = new System.Data.SqlClient.SqlParameter("@Ok", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var msg = new System.Data.SqlClient.SqlParameter("@Message", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                cmd.Parameters.AddRange(new[] { ok, msg });
                conn.Open(); cmd.ExecuteNonQuery();
                message = (msg.Value ?? "").ToString();
                return (bool)(ok.Value ?? false);
            }
        }

        private DataTable SqlUserHistory(string username)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString))
            using (var cmd = new System.Data.SqlClient.SqlCommand("sp_GetBorrowedBooksByStudent", conn))
            using (var da = new System.Data.SqlClient.SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                var t = new DataTable();
                da.Fill(t);
                return t;
            }
        }

        private void UpdateHomeSummary()
        {
            try
            {
                var (totalUsers, totalBooks, borrowed) = SqlHelper.GetDashboardSummary();
                var u = pnlLibHome.Controls.Find("lblKpiUsers", true).FirstOrDefault() as Label;
                var b = pnlLibHome.Controls.Find("lblKpiBooks", true).FirstOrDefault() as Label;
                var br = pnlLibHome.Controls.Find("lblKpiBorrowed", true).FirstOrDefault() as Label;
                if (u != null) u.Text = "Total users: " + totalUsers;
                if (b != null) b.Text = "Total books (qty sum): " + totalBooks;
                if (br != null) br.Text = "Currently borrowed: " + borrowed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load summary: " + ex.Message);
            }
        }

        // Designer-attached handler stub (to satisfy event wire in Designer)
        private void pnlLibHistory_Paint(object sender, PaintEventArgs e)
        {
            // Intentionally left blank; history grid is populated via button in WireEvents.
        }

        private void ExportGridToCsv(DataGridView grid, string filePath)
        {
            var sb = new StringBuilder();
            // headers
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
            // headers
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

       
    }
}
