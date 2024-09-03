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
            pnlHome.Controls.Add(new Label { Name = "lblTotalUsers", Text = "Total users: -", AutoSize = true, Location = new System.Drawing.Point(20, 20) });
            pnlHome.Controls.Add(new Label { Name = "lblTotalBooks", Text = "Total books: -", AutoSize = true, Location = new System.Drawing.Point(20, 45) });
            pnlHome.Controls.Add(new Label { Name = "lblBorrowed", Text = "Borrowed books: -", AutoSize = true, Location = new System.Drawing.Point(20, 70) });

            // Users UI: top panel with inputs and actions + grid
            var usersTop = new Panel { Dock = DockStyle.Top, Height = 90 };
            var txtFull = new TextBox { Name = "txtUserFull", Width = 160, Location = new System.Drawing.Point(10, 20) };
            var txtUname = new TextBox { Name = "txtUserUname", Width = 120, Location = new System.Drawing.Point(180, 20) };
            var txtEmail = new TextBox { Name = "txtUserEmail", Width = 180, Location = new System.Drawing.Point(310, 20) };
            var txtPwd = new TextBox { Name = "txtUserPwd", Width = 120, Location = new System.Drawing.Point(500, 20) };
            var cmbRole = new ComboBox { Name = "cmbUserRole", Width = 100, Location = new System.Drawing.Point(630, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbRole.Items.AddRange(new object[] { "User", "Librarian", "Admin" });
            var btnUAdd = new Button { Name = "btnUserAdd", Text = "Add", Location = new System.Drawing.Point(10, 55), Width = 80 };
            var btnUUpdate = new Button { Name = "btnUserUpdate", Text = "Update", Location = new System.Drawing.Point(95, 55), Width = 80 };
            var btnUDelete = new Button { Name = "btnUserDelete", Text = "Delete", Location = new System.Drawing.Point(180, 55), Width = 80 };
            usersTop.Controls.AddRange(new Control[] { txtFull, txtUname, txtEmail, txtPwd, cmbRole, btnUAdd, btnUUpdate, btnUDelete });

            var gridUsers = new DataGridView { Name = "gridUsers", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
            pnlUsers.Controls.Add(gridUsers);
            pnlUsers.Controls.Add(usersTop);

            // Books UI: top panel with inputs and actions + grid
            var booksTop = new Panel { Dock = DockStyle.Top, Height = 90 };
            booksTop.Controls.Add(new Label { Text = "Title", AutoSize = true, Location = new System.Drawing.Point(10, 5) });
            var txtTitle = new TextBox { Name = "txtBookTitle", Width = 180, Location = new System.Drawing.Point(10, 20) };
            booksTop.Controls.Add(new Label { Text = "Author", AutoSize = true, Location = new System.Drawing.Point(200, 5) });
            var txtAuthor = new TextBox { Name = "txtBookAuthor", Width = 150, Location = new System.Drawing.Point(200, 20) };
            booksTop.Controls.Add(new Label { Text = "Category", AutoSize = true, Location = new System.Drawing.Point(360, 5) });
            var txtCategory = new TextBox { Name = "txtBookCategory", Width = 120, Location = new System.Drawing.Point(360, 20) };
            booksTop.Controls.Add(new Label { Text = "Quantity", AutoSize = true, Location = new System.Drawing.Point(490, 5) });
            var numQty = new NumericUpDown { Name = "numBookQty", Width = 80, Location = new System.Drawing.Point(490, 20), Minimum = 0, Maximum = 100000, Value = 1 };
            var btnAdd = new Button { Name = "btnBookAdd", Text = "Add", Location = new System.Drawing.Point(10, 55), Width = 80 };
            var btnUpdate = new Button { Name = "btnBookUpdate", Text = "Update", Location = new System.Drawing.Point(95, 55), Width = 80 };
            var btnDelete = new Button { Name = "btnBookDelete", Text = "Delete", Location = new System.Drawing.Point(180, 55), Width = 80 };
            booksTop.Controls.AddRange(new Control[] { txtTitle, txtAuthor, txtCategory, numQty, btnAdd, btnUpdate, btnDelete });

            var gridBooks = new DataGridView { Name = "gridBooks", Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
            pnlBooks.Controls.Add(gridBooks);
            pnlBooks.Controls.Add(booksTop);

            // Reports: export buttons
            var btnOverdue = new Button { Name = "btnExportOverdue", Text = "Export Overdue (CSV)", Location = new System.Drawing.Point(20, 20), Width = 200 };
            var btnMostBorrowed = new Button { Name = "btnExportMostBorrowed", Text = "Export Most Borrowed (CSV)", Location = new System.Drawing.Point(230, 20), Width = 230 };
            var btnOverduePdf = new Button { Name = "btnExportOverduePdf", Text = "Export Overdue (PDF)", Location = new System.Drawing.Point(20, 60), Width = 200 };
            var btnMostBorrowedPdf = new Button { Name = "btnExportMostBorrowedPdf", Text = "Export Most Borrowed (PDF)", Location = new System.Drawing.Point(230, 60), Width = 230 };
            btnOverdue.Click += (s, e) => SqlHelper.ExportCsv("sp_GetOverdueBooks", "overdue.csv");
            btnMostBorrowed.Click += (s, e) => SqlHelper.ExportCsv("sp_GetMostBorrowedBooks", "most_borrowed.csv");
            btnOverduePdf.Click += (s, e) => SqlHelper.ExportPdf("sp_GetOverdueBooks", "overdue.pdf", "Overdue Books");
            btnMostBorrowedPdf.Click += (s, e) => SqlHelper.ExportPdf("sp_GetMostBorrowedBooks", "most_borrowed.pdf", "Most Borrowed Books");
            pnlReports.Controls.Add(btnOverdue);
            pnlReports.Controls.Add(btnMostBorrowed);
            pnlReports.Controls.Add(btnOverduePdf);
            pnlReports.Controls.Add(btnMostBorrowedPdf);

            // Settings placeholder
            var btnLogout = new Button { Text = "Logout", Location = new System.Drawing.Point(20, 20) };
            btnLogout.Click += (s, e) => { this.Close(); Application.OpenForms[0]?.Show(); };
            pnlSettings.Controls.Add(btnLogout);
        }

        private void ShowPanel(string key)
        {
            pnlHome.Visible = key == "home";
            pnlUsers.Visible = key == "users";
            pnlBooks.Visible = key == "books";
            pnlReports.Visible = key == "reports";
            pnlSettings.Visible = key == "settings";

            if (key == "books")
            {
                LoadBooks();
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
        private void btnManageBooks_Click(object sender, EventArgs e) => ShowPanel("books");
        private void btnReports_Click(object sender, EventArgs e) => ShowPanel("reports");
        private void btnSettings_Click(object sender, EventArgs e) => ShowPanel("settings");

        private void LoadBooks()
        {
            var grid = pnlBooks.Controls.Find("gridBooks", true).FirstOrDefault() as DataGridView;
            if (grid == null) return;
            grid.DataSource = SqlHelper.BooksList();
        }

        private void WireBookEvents()
        {
            var btnAdd = pnlBooks.Controls.Find("btnBookAdd", true).FirstOrDefault() as Button;
            var btnUpdate = pnlBooks.Controls.Find("btnBookUpdate", true).FirstOrDefault() as Button;
            var btnDelete = pnlBooks.Controls.Find("btnBookDelete", true).FirstOrDefault() as Button;
            var grid = pnlBooks.Controls.Find("gridBooks", true).FirstOrDefault() as DataGridView;

            if (btnAdd != null) btnAdd.Click += (s, e) =>
            {
                var title = (pnlBooks.Controls.Find("txtBookTitle", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var author = (pnlBooks.Controls.Find("txtBookAuthor", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var category = (pnlBooks.Controls.Find("txtBookCategory", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var qtyCtrl = pnlBooks.Controls.Find("numBookQty", true).FirstOrDefault() as NumericUpDown;
                var qty = qtyCtrl != null ? (int)qtyCtrl.Value : 0;
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author)) { MessageBox.Show("Title and Author required"); return; }
                SqlHelper.BooksAdd(title, author, category, qty);
                LoadBooks();
            };

            if (btnUpdate != null) btnUpdate.Click += (s, e) =>
            {
                var row = grid?.CurrentRow; if (row == null) { MessageBox.Show("Select a book"); return; }
                var bookId = Convert.ToInt32(row.Cells["BookId"].Value);
                var title = (pnlBooks.Controls.Find("txtBookTitle", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var author = (pnlBooks.Controls.Find("txtBookAuthor", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var category = (pnlBooks.Controls.Find("txtBookCategory", true).FirstOrDefault() as TextBox)?.Text.Trim();
                var qtyCtrl = pnlBooks.Controls.Find("numBookQty", true).FirstOrDefault() as NumericUpDown;
                var qty = qtyCtrl != null ? (int)qtyCtrl.Value : 0;
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author)) { MessageBox.Show("Title and Author required"); return; }
                SqlHelper.BooksUpdate(bookId, title, author, category, qty);
                LoadBooks();
            };

            if (btnDelete != null) btnDelete.Click += (s, e) =>
            {
                var row = grid?.CurrentRow; if (row == null) { MessageBox.Show("Select a book"); return; }
                var bookId = Convert.ToInt32(row.Cells["BookId"].Value);
                if (MessageBox.Show("Delete this book?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlHelper.BooksDelete(bookId);
                    LoadBooks();
                }
            };

            if (grid != null)
            {
                grid.SelectionChanged += (s, e) =>
                {
                    var r = grid.CurrentRow; if (r == null) return;
                    var titleTb = pnlBooks.Controls.Find("txtBookTitle", true).FirstOrDefault() as TextBox;
                    var authorTb = pnlBooks.Controls.Find("txtBookAuthor", true).FirstOrDefault() as TextBox;
                    var categoryTb = pnlBooks.Controls.Find("txtBookCategory", true).FirstOrDefault() as TextBox;
                    var qtyCtrl = pnlBooks.Controls.Find("numBookQty", true).FirstOrDefault() as NumericUpDown;
                    if (titleTb != null) titleTb.Text = r.Cells["Title"].Value?.ToString();
                    if (authorTb != null) authorTb.Text = r.Cells["Author"].Value?.ToString();
                    if (categoryTb != null) categoryTb.Text = r.Cells["Category"].Value?.ToString();
                    if (qtyCtrl != null) qtyCtrl.Value = Convert.ToDecimal(r.Cells["Quantity"].Value);
                };
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            WireBookEvents();
            WireUserEvents();
            LoadSummary();
        }

        private void LoadSummary()
        {
            var (totalUsers, totalBooks, borrowed) = SqlHelper.GetDashboardSummary();
            var lblUsers = pnlHome.Controls.Find("lblTotalUsers", true).FirstOrDefault() as Label;
            var lblBooks = pnlHome.Controls.Find("lblTotalBooks", true).FirstOrDefault() as Label;
            var lblBorrowed = pnlHome.Controls.Find("lblBorrowed", true).FirstOrDefault() as Label;
            if (lblUsers != null) lblUsers.Text = "Total users: " + totalUsers;
            if (lblBooks != null) lblBooks.Text = "Total books: " + totalBooks;
            if (lblBorrowed != null) lblBorrowed.Text = "Borrowed books: " + borrowed;
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
