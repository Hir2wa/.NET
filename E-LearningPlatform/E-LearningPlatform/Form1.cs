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
using System.Configuration;

namespace E_LearningPlatform
{
    public partial class Form1 : Form
    {
        // Data Storage - Now using Database instead of List
        private List<Course> courseList = new List<Course>(); // Keep for caching/display purposes
        private Course currentCourse = null;

        public Form1()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            // Test database connection first
            if (!DatabaseHelper.TestConnection())
            {
                MessageBox.Show("Cannot connect to database. Please check your connection settings.", 
                    "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Initialize combo boxes
            InitializeComboBoxes();

            // Set welcome as default
            ShowWelcomePanel();

            // Generate first course ID
            GenerateNewCourseID();

            // Wire up event handlers
            WireUpEventHandlers();
        }

        private void InitializeComboBoxes()
        {
            // Semester combo boxes
            string[] semesters =
            {
                "Summer 2025",
                "September - December 2025",
                "January - March 2026"
            };

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(semesters);
            cmbUpdateSemester.Items.Clear();
            cmbUpdateSemester.Items.AddRange(semesters);

            // Department combo boxes
            string[] departments =
            {
                "Software Engineering",
                "Networking",
                "Information Management",
                "Business",
                "Education",
                "Theology"
            };

            cmbDepartment.Items.Clear();
            cmbDepartment.Items.AddRange(departments);

            // Status combo boxes
            string[] statuses = { "Active", "Inactive" };
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(statuses);
            cmbStatus.SelectedIndex = 0; // Default to Active

            // Filter combo boxes
            cmbFilterSemester.Items.Clear();
            cmbFilterSemester.Items.Add("All Semesters");
            cmbFilterSemester.Items.AddRange(semesters);
            cmbFilterSemester.SelectedIndex = 0;

            cmbFilterDepartment.Items.Clear();
            cmbFilterDepartment.Items.Add("All Departments");
            cmbFilterDepartment.Items.AddRange(departments);
            cmbFilterDepartment.SelectedIndex = 0;
        }

        private void WireUpEventHandlers()
        {
            // Navigation buttons
            btnAddCourse.Click += BtnAddCourse_Click;
            btnRemoveCourse.Click += BtnRemoveCourse_Click;
            btnUpdateCourse.Click += BtnUpdateCourse_Click;
            btnDisplayCourse.Click += btnDisplayCourse_Click;

            // Add Course buttons
            btnSaveCourse.Click += BtnSaveCourse_Click;
            btnClearForm.Click += BtnClearForm_Click;
            btnCancel.Click += BtnCancel_Click;

            // Remove Course buttons
            btnSearchByID.Click += BtnSearchByID_Click;
            btnSearchByCode.Click += BtnSearchByCode_Click;
            btnDeleteCourse.Click += BtnDeleteCourse_Click;
            btnCancelRemove.Click += BtnCancel_Click;

            // Update Course buttons
            btnSearchByUpdateID.Click += BtnSearchByUpdateID_Click;
            btnSearchByUpdateCode.Click += BtnSearchByUpdateCode_Click;
            btnUpdateCourse1.Click += BtnUpdateCourseSave_Click;
            btnResetUpdate.Click += BtnResetUpdate_Click;
            btnCancelUpdate.Click += BtnCancel_Click;

            // Display Course buttons
            btnSearchDisplay.Click += BtnSearchDisplay_Click;
            btnRefreshList.Click += BtnRefreshList_Click;
            btnExportList.Click += BtnExportList_Click;
            dgvCourses.DoubleClick += DgvCourses_DoubleClick;

            // Filter events
            cmbFilterSemester.SelectedIndexChanged += FilterChanged;
            cmbFilterDepartment.SelectedIndexChanged += FilterChanged;
        }

        #region Navigation Events

        private void BtnAddCourse_Click(object sender, EventArgs e)
        {
            ShowAddCoursePanel();
            GenerateNewCourseID();
            ClearAddCourseForm();
        }

        private void BtnRemoveCourse_Click(object sender, EventArgs e)
        {
            ShowRemoveCoursePanel();
            ClearRemoveSearchForm();
        }

        private void BtnUpdateCourse_Click(object sender, EventArgs e)
        {
            ShowUpdateCoursePanel();
            ClearUpdateSearchForm();
        }

        private void btnDisplayCourse_Click(object sender, EventArgs e)
        {
            ShowDisplayCoursePanel();
            LoadCoursesToGrid();
        }

        #endregion

        #region Panel Management

        private void ShowWelcomePanel()
        {
            HideAllPanels();
            lblWelcome.Visible = true;
        }

        private void ShowAddCoursePanel()
        {
            HideAllPanels();
            // Show Add Course controls
            lblAddTitle.Visible = true;
            lblCourseID.Visible = txtCourseID.Visible = true;
            lblCourseCode.Visible = txtCourseCode.Visible = true;
            lblCourseName.Visible = txtCourseName.Visible = true;
            lblSemester.Visible = comboBox1.Visible = true;
            lblCredits.Visible = txtCredits.Visible = true;
            lblInstructor.Visible = txtInstructor.Visible = true;
            lblPrerequisites.Visible = textBox1.Visible = true;
            lblDepartment.Visible = cmbDepartment.Visible = true;
            lblRoom.Visible = txtRoom.Visible = true;
            lblMaxStudents.Visible = txtMaxStudents.Visible = true;
            lblStatus.Visible = cmbStatus.Visible = true;
            btnSaveCourse.Visible = btnClearForm.Visible = btnCancel.Visible = true;
        }

        private void ShowRemoveCoursePanel()
        {
            HideAllPanels();
            pnlRemoveCourse.Visible = true;
            pnlUpdateCourse.Visible = false;
            pnlDisplayCourse.Visible = false;
        }

        private void ShowUpdateCoursePanel()
        {
            HideAllPanels();
            pnlRemoveCourse.Visible = true;
            pnlUpdateCourse.Visible = true;
            pnlDisplayCourse.Visible = false;
        }

        private void ShowDisplayCoursePanel()
        {
            HideAllPanels();
            pnlRemoveCourse.Visible = true;
            pnlUpdateCourse.Visible = true;
            pnlDisplayCourse.Visible = true;
        }

        private void HideAllPanels()
        {
            // Hide welcome
            lblWelcome.Visible = false;
            

            //Hide Add Course controls
            lblCourseCode.Visible = false;
            textBox3.Visible = false;
            lblSemester.Visible = false;
            lblAddTitle.Visible = false;
            lblCourseID.Visible = txtCourseID.Visible = false;
            lblCourseCode.Visible = txtCourseCode.Visible = false;
            lblCourseName.Visible = txtCourseName.Visible = false;
            lblSemester.Visible = comboBox1.Visible = false;
            lblCredits.Visible = txtCredits.Visible = false;
            lblInstructor.Visible = txtInstructor.Visible = false;
            lblPrerequisites.Visible = textBox1.Visible = false;
            lblDepartment.Visible = cmbDepartment.Visible = false;
            lblRoom.Visible = txtRoom.Visible = false;
            lblMaxStudents.Visible = txtMaxStudents.Visible = false;
            lblStatus.Visible = cmbStatus.Visible = false;
            btnSaveCourse.Visible = btnClearForm.Visible = btnCancel.Visible = false;


            // Hide other panels
            pnlRemoveCourse.Visible = false;
            pnlUpdateCourse.Visible = false;
            pnlDisplayCourse.Visible = false;
        }

        #endregion

        #region Add Course Events

        private void BtnSaveCourse_Click(object sender, EventArgs e)
        {
            if (ValidateAddCourseForm())
            {
                try
                {
                    Course newCourse = new Course
                    {
                        CourseID = txtCourseID.Text,
                        CourseCode = txtCourseCode.Text.Trim().ToUpper(),
                        CourseName = txtCourseName.Text.Trim(),
                        Semester = comboBox1.SelectedItem?.ToString() ?? "",
                        Credits = string.IsNullOrEmpty(txtCredits.Text) ? 0 : int.Parse(txtCredits.Text),
                        Instructor = txtInstructor.Text.Trim(),
                        Prerequisites = textBox1.Text.Trim(),
                        Department = cmbDepartment.SelectedItem?.ToString() ?? "",
                        Room = txtRoom.Text.Trim(),
                        MaxStudents = string.IsNullOrEmpty(txtMaxStudents.Text) ? 0 : int.Parse(txtMaxStudents.Text),
                        Status = cmbStatus.SelectedItem?.ToString() ?? "Active"
                    };

                    // Save to database instead of list
                    if (CourseDataAccess.InsertCourse(newCourse))
                    {
                        ShowSuccessMessage($"Course '{newCourse.CourseCode}' has been saved successfully!");

                        DialogResult result = MessageBox.Show("Course saved successfully!\n\nDo you want to add another course?",
                            "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            ClearAddCourseForm();
                            GenerateNewCourseID();
                        }
                        else
                        {
                            ShowWelcomePanel();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Error saving course: {ex.Message}");
                }
            }
        }

        private void BtnClearForm_Click(object sender, EventArgs e)
        {
            ClearAddCourseForm();
            GenerateNewCourseID();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ShowWelcomePanel();
            currentCourse = null;
        }

        #endregion

        #region Remove Course Events

        private void BtnSearchByID_Click(object sender, EventArgs e)
        {
            string searchID = txtRemoveID.Text.Trim();
            if (string.IsNullOrEmpty(searchID))
            {
                ShowErrorMessage("Please enter a Course ID to search.");
                return;
            }

            Course course = CourseDataAccess.GetCourseByID(searchID);
            DisplayFoundCourse(course);
        }

        private void BtnSearchByCode_Click(object sender, EventArgs e)
        {
            string searchCode = txtRemoveCode.Text.Trim();
            if (string.IsNullOrEmpty(searchCode))
            {
                ShowErrorMessage("Please enter a Course Code to search.");
                return;
            }

            Course course = CourseDataAccess.GetCourseByCode(searchCode);
            DisplayFoundCourse(course);
        }

        private void BtnDeleteCourse_Click(object sender, EventArgs e)
        {
            if (currentCourse != null)
            {
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete course '{currentCourse.CourseCode} - {currentCourse.CourseName}'?\n\nThis action cannot be undone.",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (CourseDataAccess.DeleteCourse(currentCourse.CourseID))
                    {
                        ShowSuccessMessage($"Course '{currentCourse.CourseCode}' has been deleted successfully!");
                        ClearRemoveSearchForm();
                        currentCourse = null;
                    }
                }
            }
        }

        #endregion

        #region Update Course Events

        private void BtnSearchByUpdateID_Click(object sender, EventArgs e)
        {
            string searchID = txtUpdateID.Text.Trim();
            if (string.IsNullOrEmpty(searchID))
            {
                ShowErrorMessage("Please enter a Course ID to search.");
                return;
            }

            Course course = CourseDataAccess.GetCourseByID(searchID);
            LoadCourseForUpdate(course);
        }

        private void BtnSearchByUpdateCode_Click(object sender, EventArgs e)
        {
            string searchCode = txtUpdateCode.Text.Trim();
            if (string.IsNullOrEmpty(searchCode))
            {
                ShowErrorMessage("Please enter a Course Code to search.");
                return;
            }

            Course course = CourseDataAccess.GetCourseByCode(searchCode);
            LoadCourseForUpdate(course);
        }

        private void BtnUpdateCourseSave_Click(object sender, EventArgs e)
        {
            if (currentCourse != null && ValidateUpdateForm())
            {
                try
                {
                    // Update current course with new values
                    currentCourse.CourseCode = txtUpdateCourseCode.Text.Trim().ToUpper();
                    currentCourse.CourseName = txtUpdateCourseName.Text.Trim();
                    currentCourse.Semester = cmbUpdateSemester.SelectedItem?.ToString() ?? "";
                    currentCourse.Credits = string.IsNullOrEmpty(txtUpdateCredits.Text) ? 0 : int.Parse(txtUpdateCredits.Text);
                    currentCourse.Instructor = txtUpdateInstructor.Text.Trim();
                    currentCourse.Prerequisites = txtUpdatePrerequisites.Text.Trim();

                    // Update in database
                    if (CourseDataAccess.UpdateCourse(currentCourse))
                    {
                        ShowSuccessMessage($"Course '{currentCourse.CourseCode}' has been updated successfully!");
                        ClearUpdateSearchForm();
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Error updating course: {ex.Message}");
                }
            }
        }

        private void BtnResetUpdate_Click(object sender, EventArgs e)
        {
            if (currentCourse != null)
            {
                LoadCourseForUpdate(currentCourse);
            }
        }

        #endregion

        #region Display Course Events

        private void BtnSearchDisplay_Click(object sender, EventArgs e)
        {
            FilterAndDisplayCourses();
        }

        private void BtnRefreshList_Click(object sender, EventArgs e)
        {
            txtSearchDisplay.Clear();
            cmbFilterSemester.SelectedIndex = 0;
            cmbFilterDepartment.SelectedIndex = 0;
            LoadCoursesToGrid();
        }

        private void BtnExportList_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt",
                    DefaultExt = "csv",
                    FileName = $"CourseList_{DateTime.Now:yyyyMMdd}"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportCoursesToFile(saveDialog.FileName);
                    ShowSuccessMessage("Course list exported successfully!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error exporting data: {ex.Message}");
            }
        }

        private void DgvCourses_DoubleClick(object sender, EventArgs e)
        {
            if (dgvCourses.CurrentRow != null && dgvCourses.CurrentRow.Index >= 0)
            {
                string courseID = dgvCourses.CurrentRow.Cells[0].Value?.ToString();
                if (!string.IsNullOrEmpty(courseID))
                {
                    Course course = courseList.FirstOrDefault(c => c.CourseID == courseID);
                    if (course != null)
                    {
                        ShowCourseDetails(course);
                    }
                }
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            FilterAndDisplayCourses();
        }

        #endregion

        #region Helper Methods

        private void GenerateNewCourseID()
        {
            txtCourseID.Text = CourseDataAccess.GetNextCourseID();
        }

        private void ClearAddCourseForm()
        {
            txtCourseCode.Clear();
            txtCourseName.Clear();
            comboBox1.SelectedIndex = -1;
            txtCredits.Clear();
            txtInstructor.Clear();
            textBox1.Clear();
            cmbDepartment.SelectedIndex = -1;
            txtRoom.Clear();
            txtMaxStudents.Clear();
            cmbStatus.SelectedIndex = 0;
        }

        private void ClearRemoveSearchForm()
        {
            txtRemoveID.Clear();
            txtRemoveCode.Clear();
            grpCourseFound.Visible = false;
            btnDeleteCourse.Visible = false;
        }

        private void ClearUpdateSearchForm()
        {
            txtUpdateID.Clear();
            txtUpdateCode.Clear();
            grpUpdateCourse.Visible = false;
            btnUpdateCourse1.Visible = false;
            btnResetUpdate.Visible = false;
            currentCourse = null;
        }

        private bool ValidateAddCourseForm()
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(txtCourseCode.Text))
                errors += "• Course Code is required\n";
            else if (CourseDataAccess.CourseCodeExists(txtCourseCode.Text.Trim().ToUpper()))
                errors += "• Course Code already exists\n";

            if (string.IsNullOrWhiteSpace(txtCourseName.Text))
                errors += "• Course Name is required\n";

            if (comboBox1.SelectedIndex == -1)
                errors += "• Semester is required\n";

            if (string.IsNullOrWhiteSpace(txtCredits.Text))
                errors += "• Credits is required\n";
            else if (!int.TryParse(txtCredits.Text, out int credits) || credits < 1 || credits > 10)
                errors += "• Credits must be a number between 1 and 10\n";

            if (string.IsNullOrWhiteSpace(txtInstructor.Text))
                errors += "• Instructor is required\n";

            if (!string.IsNullOrWhiteSpace(txtMaxStudents.Text))
            {
                if (!int.TryParse(txtMaxStudents.Text, out int maxStudents) || maxStudents < 1)
                    errors += "• Max Students must be a positive number\n";
            }

            if (!string.IsNullOrEmpty(errors))
            {
                ShowErrorMessage("Please correct the following errors:\n\n" + errors);
                return false;
            }

            return true;
        }

        private bool ValidateUpdateForm()
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(txtUpdateCourseCode.Text))
                errors += "• Course Code is required\n";
            else if (CourseDataAccess.CourseCodeExists(txtUpdateCourseCode.Text.Trim().ToUpper(), currentCourse?.CourseID))
                errors += "• Course Code already exists\n";

            if (string.IsNullOrWhiteSpace(txtUpdateCourseName.Text))
                errors += "• Course Name is required\n";

            if (cmbUpdateSemester.SelectedIndex == -1)
                errors += "• Semester is required\n";

            if (string.IsNullOrWhiteSpace(txtUpdateCredits.Text))
                errors += "• Credits is required\n";
            else if (!int.TryParse(txtUpdateCredits.Text, out int credits) || credits < 1 || credits > 10)
                errors += "• Credits must be a number between 1 and 10\n";

            if (string.IsNullOrWhiteSpace(txtUpdateInstructor.Text))
                errors += "• Instructor is required\n";

            if (!string.IsNullOrEmpty(errors))
            {
                ShowErrorMessage("Please correct the following errors:\n\n" + errors);
                return false;
            }

            return true;
        }

        private void DisplayFoundCourse(Course course)
        {
            if (course != null)
            {
                currentCourse = course;
                lblFoundID.Text = $"Course ID: {course.CourseID}";
                lblFoundCode.Text = $"Course Code: {course.CourseCode}";
                lblFoundName.Text = $"Course Name: {course.CourseName}";
                lblFoundInstructor.Text = $"Instructor: {course.Instructor}";
                lblFoundSemester.Text = $"Semester: {course.Semester}";
                lblFoundStatus.Text = $"Status: {course.Status}";

                grpCourseFound.Visible = true;
                btnDeleteCourse.Visible = true;
            }
            else
            {
                ShowErrorMessage("Course not found!");
                grpCourseFound.Visible = false;
                btnDeleteCourse.Visible = false;
                currentCourse = null;
            }
        }

        private void LoadCourseForUpdate(Course course)
        {
            if (course != null)
            {
                currentCourse = course;
                txtUpdateCourseID.Text = course.CourseID;
                txtUpdateCourseCode.Text = course.CourseCode;
                txtUpdateCourseName.Text = course.CourseName;
                cmbUpdateSemester.Text = course.Semester;
                txtUpdateCredits.Text = course.Credits.ToString();
                txtUpdateInstructor.Text = course.Instructor;
                txtUpdatePrerequisites.Text = course.Prerequisites;

                grpUpdateCourse.Visible = true;
                btnUpdateCourse1.Visible = true;
                btnResetUpdate.Visible = true;
            }
            else
            {
                ShowErrorMessage("Course not found!");
                grpUpdateCourse.Visible = false;
                btnUpdateCourse1.Visible = false;
                btnResetUpdate.Visible = false;
                currentCourse = null;
            }
        }

        private void LoadCoursesToGrid()
        {
            dgvCourses.Rows.Clear();
            courseList = CourseDataAccess.GetAllCourses(); // Load from database
            
            foreach (Course course in courseList)
            {
                dgvCourses.Rows.Add(
                    course.CourseID,
                    course.CourseCode,
                    course.CourseName,
                    course.Credits,
                    course.Instructor,
                    course.Semester,
                    course.Status
                );
            }
            lblRecordInfo.Text = $"Total Courses: {courseList.Count}";
        }

        private void FilterAndDisplayCourses()
        {
            string searchTerm = txtSearchDisplay.Text.Trim();
            string semesterFilter = cmbFilterSemester.SelectedItem?.ToString();
            string departmentFilter = cmbFilterDepartment.SelectedItem?.ToString();

            // Use database search instead of filtering in memory
            var filteredCourses = CourseDataAccess.SearchCourses(
                string.IsNullOrEmpty(searchTerm) ? null : searchTerm,
                semesterFilter == "All Semesters" ? null : semesterFilter,
                departmentFilter == "All Departments" ? null : departmentFilter,
                null
            );

            dgvCourses.Rows.Clear();
            foreach (Course course in filteredCourses)
            {
                dgvCourses.Rows.Add(
                    course.CourseID,
                    course.CourseCode,
                    course.CourseName,
                    course.Credits,
                    course.Instructor,
                    course.Semester,
                    course.Status
                );
            }
            lblRecordInfo.Text = $"Showing {filteredCourses.Count} courses";
        }

        private void ShowCourseDetails(Course course)
        {
            string details = $"COURSE DETAILS - {course.CourseCode}\n\n" +
                            $"Course ID: {course.CourseID}\n" +
                            $"Course Code: {course.CourseCode}\n" +
                            $"Course Name: {course.CourseName}\n" +
                            $"Credits: {course.Credits}\n" +
                            $"Semester: {course.Semester}\n" +
                            $"Instructor: {course.Instructor}\n" +
                            $"Department: {course.Department}\n" +
                            $"Room: {course.Room}\n" +
                            $"Max Students: {course.MaxStudents}\n" +
                            $"Status: {course.Status}\n" +
                            $"Created: {course.CreatedDate:MM/dd/yyyy}\n\n" +
                            $"Prerequisites: {course.Prerequisites}";

            MessageBox.Show(details, "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportCoursesToFile(string fileName)
        {
            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine("CourseID,CourseCode,CourseName,Credits,Instructor,Semester,Department,Status");

                // Get all courses from database for export
                var allCourses = CourseDataAccess.GetAllCourses();
                foreach (Course course in allCourses)
                {
                    writer.WriteLine($"{course.CourseID},{course.CourseCode},{course.CourseName}," +
                                   $"{course.Credits},{course.Instructor},{course.Semester}," +
                                   $"{course.Department},{course.Status}");
                }
            }
        }

      

        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region Existing Event Handlers (keep these)

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Prerequisites textbox - keep as is
        }

        private void lblSemester_Click(object sender, EventArgs e)
        {
            // Keep as is
        }

        private void pnlRemoveCourse_Paint(object sender, PaintEventArgs e)
        {
            // Keep as is
        }

        private void grpCourseFound_Enter(object sender, EventArgs e)
        {
            // Keep as is
        }

        private void pnlDisplayCourse_Paint(object sender, PaintEventArgs e)
        {
            // Keep as is
        }

        #endregion
    }
}