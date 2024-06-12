using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedLibrary.Classes;
using SharedLibrary.Enums;
using SharedLibrary.Helpers;
using System.Globalization;

namespace TheSandwichMakersHardwareStoreSolution
{
    public partial class MainControl : UserControl
    {
        public ShiftManager ShiftManager { get; set; }
        public EmployeeManager EmployeeManager { get; set; }
        public StockManager StockManager { get; set; }
        public DepartmentManager DepartmentManager { get; set; }

        private string _currentImageUrl = string.Empty;

        private Dictionary<DateOnly, ShiftTypeEnum> _employeeShiftMap;
        public static int _year, _month;

        public MainControl()
        {
            InitializeComponent();
            this.ShiftManager = new ShiftManager();
            this.DepartmentManager = new DepartmentManager();
            this.EmployeeManager = new EmployeeManager();
            this.StockManager = new StockManager();

            _employeeShiftMap = new Dictionary<DateOnly, ShiftTypeEnum>();
            _month = DateTime.Now.Month;
            _year = DateTime.Now.Year;

            InitializeUiElements();
        }

        public void AuthenticatedEmployee(string email)
        {
            Employee employee = EmployeeManager.GetEmployeeByEmail(email);
            UserSession.Instance.CurrentEmployee = employee;
        }

        // Validation for Employee Role
        private bool EmployeeHasRequiredRole()
        {
            return UserSession.Instance.CurrentEmployee?.Role == RoleEnum.Owner || UserSession.Instance.CurrentEmployee?.Role == RoleEnum.Manager;
        }

        private void InitializeUiElements()
        {
            RefreshEmployeesGrid();
            RefreshShiftUI();
            RefreshProductInfoDisplay();
            RefreshShelfRequestDisplay();
            RefreshDaysOffRequest();
            ShowShiftCalendar(_month, _year);

            // Add roles to cmbRoleList
            cmbRoleList.DataSource = Enum.GetValues(typeof(RoleEnum));
            cmbRoleList.SelectedIndex = -1;

            // Add departments to cmbDepartmentList
            cmbDepartmentList.DataSource = DepartmentManager.GetDepartments();
            cmbDepartmentList.DisplayMember = "Name";
            cmbDepartmentList.ValueMember = "Id";
            cmbDepartmentList.SelectedIndex = -1;
            cmbBoxEmployeeDepartment.DataSource = DepartmentManager.GetDepartments();
            cmbBoxEmployeeDepartment.DisplayMember = "Name";
            cmbBoxEmployeeDepartment.ValueMember = "Id";
            cmbBoxEmployeeDepartment.SelectedIndex = -1;

            // Bind roles to the ComboBox
            cmbBoxEmployeeRole.DataSource = Enum.GetValues(typeof(RoleEnum));
            cmbBoxEmployeeRole.SelectedIndex = -1;

            // Load Employee Status
            cmbBoxEmployeeIsActive.Items.Add(true);
            cmbBoxEmployeeIsActive.Items.Add(false);

            // Bind departments to the ListBox
            listBoxDepartments.DataSource = DepartmentManager.GetDepartments();
            listBoxDepartments.DisplayMember = "Name";
            listBoxDepartments.ValueMember = "Id";
            listBoxDepartments.SelectedIndex = -1;

            cmbShelfRequestTypeFilter.SelectedIndex = 0;
        }

        private void ClearAllLabels()
        {
            lbEmployeeName.Text = string.Empty;
            lbEmployeeEmail.Text = string.Empty;
            lbEmployeeRole.Text = string.Empty;
            lbEmployeeDepartment.Text = string.Empty;

            pbEmployeeImage.ImageLocation = string.Empty;

            lbEmployeeMorning.Text = string.Empty;
            lbEmployeeEvening.Text = string.Empty;
            lbEmployeeTotal.Text = string.Empty;
        }

        private void cmbRoleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxFilteredEmployees.Items.Clear();

            if (cmbDepartmentList.SelectedItem is Department department)
            {
                // Filter employees by role and department and display them in the lstBoxFilteredEmployees
                if (cmbRoleList.SelectedItem is RoleEnum role)
                {
                    try
                    {
                        foreach (Employee employee in EmployeeManager.GetEmployeesByRoleAndDepartment(role, department))
                        {
                            lstBoxFilteredEmployees.Items.Add(employee);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    // Filter employees by role and display them in the lstBoxFilteredEmployees
                    try
                    {
                        foreach (Employee employee in EmployeeManager.GetEmployeesByDepartment(department))
                        {
                            lstBoxFilteredEmployees.Items.Add(employee);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                // Filter employees by role and display them in the lstBoxFilteredEmployees
                if (cmbRoleList.SelectedItem is RoleEnum role)
                {
                    try
                    {
                        foreach (Employee employee in EmployeeManager.GetEmployeesByRole(role))
                        {
                            lstBoxFilteredEmployees.Items.Add(employee);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    // Display all if none is selected
                    foreach (Employee employee in EmployeeManager.GetEmployees())
                    {
                        lstBoxFilteredEmployees.Items.Add(employee);
                    }
                }
            }

            lblFilteredCount.Text = lstBoxFilteredEmployees.Items.Count.ToString();
        }

        private void cmbDepartmentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxFilteredEmployees.Items.Clear();

            // Check if a Role is selected
            if (cmbRoleList.SelectedItem is RoleEnum role)
            {
                // Filter employees by role and department and display them in the lstBoxFilteredEmployees
                if (cmbDepartmentList.SelectedItem is Department department)
                {
                    foreach (Employee employee in EmployeeManager.GetEmployeesByRoleAndDepartment(role, department))
                    {
                        lstBoxFilteredEmployees.Items.Add(employee);
                    }
                }
                else
                {
                    try
                    {
                        foreach (Employee employee in EmployeeManager.GetEmployeesByRole(role))
                        {
                            lstBoxFilteredEmployees.Items.Add(employee);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                if (cmbDepartmentList.SelectedItem is Department department)
                {
                    try
                    {
                        foreach (Employee employee in EmployeeManager.GetEmployeesByDepartment(department))
                        {
                            lstBoxFilteredEmployees.Items.Add(employee);
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    // Display all if none is selected
                    lstBoxFilteredEmployees.Items.Clear();
                    foreach (Employee employee in EmployeeManager.GetEmployees())
                    {
                        lstBoxFilteredEmployees.Items.Add(employee);
                    }
                }
            }

            lblFilteredCount.Text = lstBoxFilteredEmployees.Items.Count.ToString();
        }

        // Once employee selected, display the details
        private void lstBoxFilteredEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBoxFilteredEmployees.SelectedItem is Employee employee)
            {
                ClearAllLabels();

                lbEmployeeName.Text = employee.Name;
                lbEmployeeEmail.Text = employee.Email;
                lbEmployeeRole.Text = employee.Role.ToString();
                lbEmployeeDepartment.Text = employee.Department.Name;
                pbEmployeeImage.ImageLocation = employee.Image;
                pbEmployeeImage.SizeMode = PictureBoxSizeMode.StretchImage;

                // Get employee shifts info past 30 days
                List<Shift> shifts = ShiftManager.GetEmployeeShifts(employee);

                // Display shifts info in labels

                // All the other shift info
                lbEmployeeMorning.Text = shifts.Count(s => s.ShiftType == ShiftTypeEnum.Morning).ToString();
                lbEmployeeEvening.Text = shifts.Count(s => s.ShiftType == ShiftTypeEnum.Evening).ToString();
                lbEmployeeTotal.Text = shifts.Count.ToString();
            }
        }

        // Show employee shift of selected date
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // Check if employee has a shift on selected date
            if (lstBoxFilteredEmployees.SelectedItem is Employee employee)
            {
                DateOnly date = new DateOnly(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day);
                List<Shift> shifts = ShiftManager.GetEmployeeShiftsOnDate(employee, date);

                // Display shift info on label (Morning | Evening | None)
                if (shifts.Count > 0)
                {
                    if (shifts.Any(s => s.ShiftType == ShiftTypeEnum.Morning))
                    {
                        lbEmployeeShiftOnDate.Text = "Morning Shift";
                    }
                    else if (shifts.Any(s => s.ShiftType == ShiftTypeEnum.Evening))
                    {
                        lbEmployeeShiftOnDate.Text = "Evening Shift";
                    }
                    else
                    {
                        lbEmployeeShiftOnDate.Text = "No Shift";
                    }
                }
                else
                {
                    lbEmployeeShiftOnDate.Text = "No Shift";
                }
            }
        }

        // On Manage click in Employee Info
        private void button4_Click(object sender, EventArgs e)
        {

            tabControMain.SelectedTab = tabPageEmployee;
            dtGrVEmployees.ClearSelection();

            // Get selected Employee from lstBoxFilteredEmployees
            if (lstBoxFilteredEmployees.SelectedItem is Employee employee)
            {
                foreach (DataGridViewRow row in dtGrVEmployees.Rows)
                {
                    if (row.Cells["Email"].Value.ToString() == employee.Email)
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
        }

        // On Manage click in Shelf requests
        private void button2_Click(object sender, EventArgs e)
        {
            string selectedItemName = listBox2.SelectedItem.ToString();

            dataGridView2.ClearSelection();

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["ItemName"].Value.ToString() == selectedItemName)
                {
                    MessageBox.Show("Found the item!");
                    row.Selected = true;
                    break;
                }
            }

            tabControMain.SelectedTab = tabPageStock;
        }

        // Attaching Image For User
        private async void btnEmployeeAttachImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg; *.jpeg; *.png) | *.jpg; *.jpeg; *.png",
                Title = "Select an Image"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > 67108864) // 64 MB in bytes
                {
                    MessageBox.Show("The image is larger than 64MB and cannot be uploaded.", "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    _currentImageUrl = await UploadImageToFreeImageHost(filePath, "6d207e02198a847aa98d0a2a901485a5");
                    lblImage.Text = $"Image uploaded! URL: {_currentImageUrl}";
                    MessageBox.Show($"Image uploaded successfully! URL: {_currentImageUrl}", "Upload Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to upload image: {ex.Message}", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private async Task<string> UploadImageToFreeImageHost(string imagePath, string apiKey)
        {
            using (var client = new HttpClient())
            {
                var form = new MultipartFormDataContent();
                using (var fs = File.OpenRead(imagePath))
                {
                    using (var streamContent = new StreamContent(fs))
                    {
                        using (var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync()))
                        {
                            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                            form.Add(fileContent, "source", Path.GetFileName(imagePath));
                            form.Add(new StringContent(apiKey), "key");

                            HttpResponseMessage response = await client.PostAsync("https://freeimage.host/api/1/upload", form);
                            response.EnsureSuccessStatusCode();
                            string responseBody = await response.Content.ReadAsStringAsync();

                            dynamic result = JsonConvert.DeserializeObject(responseBody);
                            if (result.status_code == 200)
                            {
                                return result.image.url;
                            }
                            else
                            {
                                throw new Exception("Error uploading image.");
                            }
                        }
                    }
                }
            }
        }

        // Managing Employees
        private void btnNewEmployee_Click(object sender, EventArgs e)
        {
            if (!EmployeeHasRequiredRole())
            {
                MessageBox.Show("Only Manager or Owner can perform this action.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ValidateEmployeeInput())
            {
                if (!EmployeeManager.IsEmployeeNameUnique(txtBoxEmployeeName.Text) || !EmployeeManager.IsEmployeeEmailUnique(txtBoxEmployeeEmail.Text))
                {
                    MessageBox.Show("An employee with this name or email already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    EmployeeManager.AddEmployee(
                        txtBoxEmployeeName.Text,
                        txtBoxEmployeeEmail.Text,
                        txtBoxEmployeePswd.Text,
                        (RoleEnum)cmbBoxEmployeeRole.SelectedItem,
                        _currentImageUrl,
                        txtBoxEmployeeAddress.Text,
                        (Department)cmbBoxEmployeeDepartment.SelectedItem,
                        Convert.ToDecimal(txtBoxEmployeeHourlyWage.Text),
                        Convert.ToBoolean(cmbBoxEmployeeIsActive.SelectedItem),
                        txtBoxEmployeePhoneNum.Text,
                        Convert.ToInt32(txtBoxEmployeeBsn.Text),
                        txtBoxEmployeeBankAcc.Text
                        );

                    RefreshEmployeesGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to add new employee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            if (!EmployeeHasRequiredRole())
            {
                MessageBox.Show("Only Manager or Owner can perform this action.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtGrVEmployees.SelectedRows.Count > 0 && ValidateEmployeeInput())
            {
                dynamic selectedRow = dtGrVEmployees.SelectedRows[0].DataBoundItem;
                int selectedEmployeeId = selectedRow.Id;

                if (!EmployeeManager.IsNameUniqueExceptCurrentEmployee(txtBoxEmployeeName.Text, selectedEmployeeId) || !EmployeeManager.IsEmailUniqueExceptCurrentEmployee(txtBoxEmployeeEmail.Text, selectedEmployeeId))
                {
                    MessageBox.Show("An employee with this name or email already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    EmployeeManager.UpdateEmployee(selectedEmployeeId, txtBoxEmployeeName.Text, txtBoxEmployeeEmail.Text, txtBoxEmployeePswd.Text, (RoleEnum)cmbBoxEmployeeRole.SelectedItem, _currentImageUrl, txtBoxEmployeeAddress.Text, (Department)cmbBoxEmployeeDepartment.SelectedItem, Convert.ToDecimal(txtBoxEmployeeHourlyWage.Text), Convert.ToBoolean(cmbBoxEmployeeIsActive.SelectedItem), txtBoxEmployeePhoneNum.Text, Convert.ToInt32(txtBoxEmployeeBsn.Text), txtBoxEmployeeBankAcc.Text);
                    RefreshEmployeesGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to edit.");
            }
        }

        private void btnRemoveEmployee_Click(object sender, EventArgs e)
        {
            if (dtGrVEmployees.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dynamic selectedRow = dtGrVEmployees.SelectedRows[0].DataBoundItem;
                    int selectedEmployeeId = selectedRow.Id;

                    try
                    {
                        EmployeeManager.DeactivateEmployee(selectedEmployeeId);
                        RefreshEmployeesGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.");
            }
        }

        private void dtGrVEmployees_SelectionChanged(object sender, EventArgs e)
        {
            //if (!EmployeeHasRequiredRole())
            //{
            //    MessageBox.Show("Only Manager or Owner can perform this action.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (dtGrVEmployees.SelectedRows.Count > 0)
            {
                var selectedRow = dtGrVEmployees.SelectedRows[0].DataBoundItem;
                dynamic selectedEmployee = selectedRow as dynamic;

                if (selectedEmployee == null) return;

                txtBoxEmployeeName.Text = selectedEmployee.Name;
                txtBoxEmployeeEmail.Text = selectedEmployee.Email;
                txtBoxEmployeePswd.Text = selectedEmployee.Password;
                txtBoxEmployeeAddress.Text = selectedEmployee.Address;
                _currentImageUrl = selectedEmployee.Image;
                lblImage.Text = $"Image uploaded! URL: {_currentImageUrl}";
                cmbBoxEmployeeDepartment.SelectedValue = selectedEmployee.Department.Id;
                cmbBoxEmployeeRole.SelectedItem = selectedEmployee.Role;
                txtBoxEmployeeHourlyWage.Text = selectedEmployee.HourlyWage.ToString();
                cmbBoxEmployeeIsActive.SelectedItem = selectedEmployee.IsActive == "Yes";
                txtBoxEmployeePhoneNum.Text = selectedEmployee.PhoneNumber;
                txtBoxEmployeeBsn.Text = selectedEmployee.Bsn.ToString();
                txtBoxEmployeeBankAcc.Text = selectedEmployee.BankAccount;
            }
        }


        private bool ValidateEmployeeInput()
        {
            // Check if the name is not empty
            if (string.IsNullOrWhiteSpace(txtBoxEmployeeName.Text))
            {
                MessageBox.Show("Employee name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Basic email validation
            if (string.IsNullOrWhiteSpace(txtBoxEmployeeEmail.Text) || !txtBoxEmployeeEmail.Text.Contains("@"))
            {
                MessageBox.Show("A valid email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Password is not empty (You might want to add more complex checks)
            if (string.IsNullOrWhiteSpace(txtBoxEmployeePswd.Text))
            {
                MessageBox.Show("Password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Address is not empty
            if (string.IsNullOrWhiteSpace(txtBoxEmployeeAddress.Text))
            {
                MessageBox.Show("Address is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Hourly Wage is a positive number
            if (!decimal.TryParse(txtBoxEmployeeHourlyWage.Text, out decimal hourlyWage) || hourlyWage <= 0)
            {
                MessageBox.Show("Hourly wage must be a positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            // Role and Department selection validation
            if (cmbBoxEmployeeRole.SelectedItem is not RoleEnum || cmbBoxEmployeeRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid role and department.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Phone number validation
            if (string.IsNullOrWhiteSpace(txtBoxEmployeePhoneNum.Text) || txtBoxEmployeePhoneNum.Text.Length <= 8)
            {
                MessageBox.Show("Please enter a valid phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // BSN validation
            if (string.IsNullOrWhiteSpace(txtBoxEmployeeBsn.Text) ||
                txtBoxEmployeeBsn.Text.Length != 9 || // Assuming BSN must be 9 digits
                !int.TryParse(txtBoxEmployeeBsn.Text, out int bsn))
            {
                MessageBox.Show("BSN must be a valid 9-digit integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Bank account validation
            if (string.IsNullOrWhiteSpace(txtBoxEmployeeBankAcc.Text) ||
                txtBoxEmployeeBankAcc.Text.Length > 34)
            {
                MessageBox.Show("Bank account number is required and cannot exceed 34 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            return true;
        }



        private void RefreshEmployeesGrid()
        {
            try
            {

                var employees = EmployeeManager.GetEmployees();

                // Project the employee data into a new format for display
                var employeeDisplayData = employees.Select(e => new
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Address = e.Address,
                    Password = e.Password,
                    Image = e.Image,
                    Role = e.Role,
                    Department = e.Department,
                    HourlyWage = e.HourlyWage,
                    RegisterDate = e.RegisterDate,
                    IsActive = e.IsActive ? "Yes" : "No",
                    PhoneNumber = e.PhoneNumber,
                    Bsn = e.BSN,
                    BankAccount = e.BankAccount

                }).ToList();

                dtGrVEmployees.DataSource = employeeDisplayData;

                // Optionally, adjust column headers here as needed
                dtGrVEmployees.Columns["Id"].HeaderText = "ID";
                dtGrVEmployees.Columns["Name"].HeaderText = "Name";
                dtGrVEmployees.Columns["Email"].HeaderText = "Email";
                dtGrVEmployees.Columns["PhoneNumber"].HeaderText = "PhoneNumber";
                dtGrVEmployees.Columns["Bsn"].HeaderText = "Bsn";
                dtGrVEmployees.Columns["BankAccount"].HeaderText = "BankAccount";
                dtGrVEmployees.Columns["Address"].HeaderText = "Address";
                dtGrVEmployees.Columns["Password"].HeaderText = "Password";
                dtGrVEmployees.Columns["Image"].HeaderText = "Image";
                dtGrVEmployees.Columns["Role"].HeaderText = "Role";
                dtGrVEmployees.Columns["Department"].HeaderText = "Department";
                dtGrVEmployees.Columns["HourlyWage"].HeaderText = "Hourly Wage";
                dtGrVEmployees.Columns["RegisterDate"].HeaderText = "Register Date";
                dtGrVEmployees.Columns["IsActive"].HeaderText = "Active";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to refresh employee grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Managing Departments

        // Adding Deparment
        private void btnNewDepartment_Click(object sender, EventArgs e)
        {
            if (!EmployeeHasRequiredRole())
            {
                MessageBox.Show("Only Manager or Owner can perform this action.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtBoxDepartmentName.Text))
            {
                try
                {
                    DepartmentManager.AddDepartment(txtBoxDepartmentName.Text);

                    MessageBox.Show("Department added successfully.");
                    txtBoxDepartmentName.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    RefreshDepartmentsList();
                }
            }
            else
            {
                MessageBox.Show("Please enter a name for the new department.");
            }
        }

        // Modify the btnEditDepartment_Click event to check for selection
        private void btnEditDepartment_Click(object sender, EventArgs e)
        {
            if (!EmployeeHasRequiredRole())
            {
                MessageBox.Show("Only Manager or Owner can perform this action.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (listBoxDepartments.SelectedItem is Department selectedDepartment)
            {
                try
                {
                    DepartmentManager.UpdateDepartment(selectedDepartment.Id, txtBoxDepartmentName.Text);
                    MessageBox.Show("Department updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    RefreshDepartmentsList();
                }
            }
            else
            {
                MessageBox.Show("Please select a department to edit.");
            }
        }

        // Modify the btnRemoveDepartment_Click event to check for selection
        private void btnRemoveDepartment_Click(object sender, EventArgs e)
        {
            if (!EmployeeHasRequiredRole())
            {
                MessageBox.Show("Only Manager or Owner can perform this action.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBoxDepartments.SelectedItem is Department selectedDepartment)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove this department?", "Remove Department", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        DepartmentManager.RemoveDepartment(selectedDepartment.Id);
                        MessageBox.Show("Department removed successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        RefreshDepartmentsList();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a department to remove.");
            }
        }


        private void RefreshDepartmentsList()
        {
            try
            {
                listBoxDepartments.DataSource = DepartmentManager.GetDepartments();
                listBoxDepartments.DisplayMember = "Name";
                listBoxDepartments.ValueMember = "Id";
                cmbBoxEmployeeDepartment.DataSource = DepartmentManager.GetDepartments();
                cmbBoxEmployeeDepartment.DisplayMember = "Name";
                cmbBoxEmployeeDepartment.ValueMember = "Id";
                cmbBoxEmployeeDepartment.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void listBoxDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDepartments.SelectedItem is Department selectedDepartment)
            {
                txtBoxDepartmentName.Text = selectedDepartment.Name;
            }
            else
            {
                txtBoxDepartmentName.Clear();
            }
        }

        private void RefreshProductInfoDisplay()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = StockManager.GetItems();
        }


        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            StockManager.AddNewItem(Convert.ToInt16(numericSKU.Value), tbNameProduct.Text, Convert.ToInt16(numericQuantityWarehouse.Value), Convert.ToInt16(numericQuantityStore.Value), (CategoryEnum)cbCatergory.SelectedIndex, Convert.ToDouble(numericWholesalePrice.Value), Convert.ToDouble(numericSellPrice.Value), dateTimePickerExperationdate.Value);
            RefreshProductInfoDisplay();

        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRow();
            StockManager.EditItem(id, Convert.ToInt16(numericSKU.Value), tbNameProduct.Text, Convert.ToInt16(numericQuantityWarehouse.Value), Convert.ToInt16(numericQuantityStore.Value), (CategoryEnum)cbCatergory.SelectedIndex, Convert.ToDouble(numericWholesalePrice.Value), Convert.ToDouble(numericSellPrice.Value), dateTimePickerExperationdate.Value);
            RefreshProductInfoDisplay();
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRow();
            StockManager.RemoveItem(id);
            RefreshProductInfoDisplay();
        }

        private int GetIdSelectedRow()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                int id = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                return id;
            }
            else
            {
                return -1;
            }

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int id = GetIdSelectedRow();
            if (id != -1)
            {
                Item item = StockManager.GetItemById(id);
                tbNameProduct.Text = item.Name;
                numericSKU.Value = item.Sku;
                numericQuantityWarehouse.Value = item.QuantityWarehouse;
                numericQuantityStore.Value = item.QuantityStore;
                cbCatergory.Text = item.Category.ToString();
                numericWholesalePrice.Value = Convert.ToDecimal(item.WholesalePrice);
                numericSellPrice.Value = Convert.ToDecimal(item.SellPrice);
            }

        }

        private void btAddShelfRequest_Click(object sender, EventArgs e)
        {
            int itemId = GetIdSelectedItemShelfRequestTab();
            if (itemId == -1)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            int quantity = Convert.ToInt16(numericQuantityShelfRequest.Value);
            if (quantity <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero.");
                return;
            }

            ShelfRequestType requestType = (ShelfRequestType)cmbShelfRequestType.SelectedIndex;
            if (!Enum.IsDefined(typeof(ShelfRequestType), requestType))
            {
                MessageBox.Show("Please select a valid shelf request type.");
                return;
            }

            StockManager.AddShelfRequest(itemId, quantity, requestType);
            RefreshShelfRequestDisplay();
        }

        private void btnEditRequest_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRowShelfRequest();
            if (id == -1)
            {
                MessageBox.Show("Please select a shelf request to edit.");
                return;
            }

            int itemId = GetIdSelectedItemShelfRequestTab();
            if (itemId == -1)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            int quantity = Convert.ToInt16(numericQuantityShelfRequest.Value);
            if (quantity <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero.");
                return;
            }

            ShelfRequestType requestType = (ShelfRequestType)cmbShelfRequestType.SelectedIndex;
            if (!Enum.IsDefined(typeof(ShelfRequestType), requestType))
            {
                MessageBox.Show("Please select a valid shelf request type.");
                return;
            }

            StockManager.EditShelfRequest(id, itemId, quantity, requestType);
            RefreshShelfRequestDisplay();
        }

        private void btnFulfillRequest_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRowShelfRequest();
            try
            {
                StockManager.FulFillShelfRequest(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            RefreshShelfRequestDisplay();
            RefreshProductInfoDisplay();
        }

        public void RefreshShelfRequestDisplay()
        {
            dataGridView2.DataSource = null;
            int shelfRequestSelectedIndex = cmbShelfRequestTypeFilter.SelectedIndex;
            ShelfRequestType? type = shelfRequestSelectedIndex > 0 ? (ShelfRequestType)cmbShelfRequestTypeFilter.SelectedIndex - 1 : null;

            var requests = StockManager.GetShelfRequests(type);

            dataGridView2.DataSource = requests;
            
            listBox2.Items.Clear();

            foreach (ShelfRequest request in requests)
            {
                listBox2.Items.Add(request.ItemName);
            }
            comboBoxSelectItemShelfRequest.Items.Clear();
            foreach (Item item in StockManager.GetItems())
            {
                comboBoxSelectItemShelfRequest.Items.Add(item.Id + "-" + item.Name);
            }
        }
        private int GetIdSelectedItemShelfRequestTab()
        {
            string temporary = comboBoxSelectItemShelfRequest.Text;
            if (temporary != "")
            {
                string[] temporarySplit = temporary.Split("-");
                int id = int.Parse(temporarySplit[0]);
                return id;
            }
            return -1;
        }
        private int GetIdSelectedRowShelfRequest()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                int id = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                return id;
            }
            else
            {
                return -1;
            }
        }

        //Shift Tab
        private void ShowShiftCalendar(int month, int year)
        {
            flpCalendar.Controls.Clear();
            lstEmployees.SelectedIndex = -1;

            _month = month;
            _year = year;

            Dictionary<Tuple<DateOnly, ShiftTypeEnum>, int> employeeCountByShift = ShiftManager.GetShiftEmployeeCountForMonth(month, year);

            string monthName = new DateTimeFormatInfo().GetMonthName(month);
            lblMonth.Text = monthName.ToUpper() + " " + year;

            DateTime startOfTheMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int startDayOfWeek = (int)startOfTheMonth.DayOfWeek;

            for (int i = 1; i < startDayOfWeek; i++)
            {
                Panel emptyPanel = new Panel
                {
                    Size = new Size(140, 102),
                    BorderStyle = BorderStyle.None
                };
                flpCalendar.Controls.Add(emptyPanel);
            }

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateOnly date = new DateOnly(year, month, day);
                ShiftDayUC shiftDayUC = new ShiftDayUC(date);

                int morningShiftCount = employeeCountByShift.TryGetValue(new Tuple<DateOnly, ShiftTypeEnum>(date, ShiftTypeEnum.Morning), out int morningCount) ? morningCount : 0;
                int eveningShiftCount = employeeCountByShift.TryGetValue(new Tuple<DateOnly, ShiftTypeEnum>(date, ShiftTypeEnum.Evening), out int eveningCount) ? eveningCount : 0;

                shiftDayUC.SetShiftCounts(morningShiftCount, eveningShiftCount);

                flpCalendar.Controls.Add(shiftDayUC);
            }
        }

        public void RefreshShiftUI()
        {
            lstEmployees.DataSource = EmployeeManager.GetEmployees();
            lstEmployees.SelectedIndex = -1;
        }

        private void btnPreviousMonth_Click(object sender, EventArgs e)
        {
            _month--;
            if (_month < 1)
            {
                _month = 12;
                _year--;
            }

            ShowShiftCalendar(_month, _year);
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            _month++;
            if (_month > 12)
            {
                _month = 1;
                _year++;
            }

            ShowShiftCalendar(_month, _year);
        }

        private void txtEmployeeSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtEmployeeSearch.Text.ToLower();

            lstEmployees.ClearSelected();

            for (int i = 0; i < lstEmployees.Items.Count; i++)
            {
                string currentItem = lstEmployees.Items[i].ToString().ToLower();
                if (currentItem.Contains(searchText))
                {
                    lstEmployees.SelectedIndex = i;
                    break;
                }
            }
        }

        private void lstEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstEmployees.SelectedItem != null)
            {
                Employee employee = (Employee)lstEmployees.SelectedItem;

                _employeeShiftMap = ShiftManager.GetEmployeeShiftsOnMonthFromDB(employee.Id, _month, _year).ToDictionary(shift => shift.Date, shift => shift.ShiftType);

                foreach (Control control in flpCalendar.Controls)
                {
                    if (control is ShiftDayUC shiftDayUC)
                    {
                        shiftDayUC.ResetShiftsStatus();

                        if (shiftDayUC.Date >= DateOnly.FromDateTime(DateTime.Now))
                        {
                            shiftDayUC.EnableCheckBox();
                        }

                        if (_employeeShiftMap.TryGetValue(shiftDayUC.Date, out ShiftTypeEnum shiftType))
                        {
                            if (shiftType == ShiftTypeEnum.Morning)
                            {
                                shiftDayUC.CheckMorningShift();
                            }
                            else if (shiftType == ShiftTypeEnum.Evening)
                            {
                                shiftDayUC.CheckEveningShift();
                            }
                        }
                    }
                }
            }
        }

        private void btnSaveShiftSchedule_Click(object sender, EventArgs e)
        {
            if (lstEmployees.SelectedItem == null)
            {
                MessageBox.Show("You have to select an employee.");
                return;
            }

            Dictionary<DateOnly, ShiftTypeEnum> addedShiftsMap = new Dictionary<DateOnly, ShiftTypeEnum>();
            Dictionary<DateOnly, ShiftTypeEnum> removedShiftsMap = new Dictionary<DateOnly, ShiftTypeEnum>();

            foreach (Control control in flpCalendar.Controls)
            {
                if (control is ShiftDayUC shiftDayUC)
                {
                    if (shiftDayUC.IsMorningShiftAdded())
                    {
                        addedShiftsMap.Add(shiftDayUC.Date, ShiftTypeEnum.Morning);
                    }
                    else if (shiftDayUC.IsEveningShiftAdded())
                    {
                        addedShiftsMap.Add(shiftDayUC.Date, ShiftTypeEnum.Evening);
                    }

                    if (shiftDayUC.IsMorningShiftRemoved())
                    {
                        removedShiftsMap.Add(shiftDayUC.Date, ShiftTypeEnum.Morning);
                    }
                    else if (shiftDayUC.IsEveningShiftRemoved())
                    {
                        removedShiftsMap.Add(shiftDayUC.Date, ShiftTypeEnum.Evening);
                    }
                }
            }

            if (addedShiftsMap.Count == 0 && removedShiftsMap.Count == 0)
            {
                MessageBox.Show("There was no change made.");
                return;
            }

            Employee employee = (Employee)lstEmployees.SelectedItem;

            foreach (var entry in addedShiftsMap)
            {
                DateOnly date = entry.Key;
                ShiftTypeEnum shiftType = entry.Value;

                ShiftManager.AssignEmployeeToShift(employee, date, shiftType);
            }

            foreach (var entry in removedShiftsMap)
            {
                DateOnly date = entry.Key;
                ShiftTypeEnum shiftType = entry.Value;

                ShiftManager.UnassignEmployeeFromShift(employee, date, shiftType);
            }

            ShowShiftCalendar(_month, _year);
            MessageBox.Show("New Shifts assignment updated successfully.");
        }

        private void btnRefreshShiftSchedule_Click(object sender, EventArgs e)
        {
            RefreshShiftUI();
            ShowShiftCalendar(_month, _year);
        }

        private void btnAutoAssignDate_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtpAutoAssignFrom.Value.Date;
            DateTime toDate = dtpAutoAssignTo.Value.Date;

            if (fromDate > toDate)
            {
                MessageBox.Show("From date must be less than or equal to To date.");
                return;
            }

            for (DateTime date = fromDate; date.Date <= toDate; date = date.AddDays(1))
            {
                DateOnly dateOnly = new DateOnly(date.Year, date.Month, date.Day);
                ShiftManager.AutoAssignShift(EmployeeManager.GetUnassignedEmployeesToShiftOnDate(dateOnly), dateOnly);
            }

            MessageBox.Show("Shift assigned successfully.");
            ShowShiftCalendar(_month, _year);
        }

        //DaysOffRequest Tab

        public DaysOffRequestManager DaysOffRequestManager = new DaysOffRequestManager();
        private void btRemoveDaysOffRequest_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRowDaysOffRequest();
            DaysOffRequestManager.RemoveDaysOffRequest(id);
            RefreshDaysOffRequest();
        }
        private void btAproveDaysOffRequest_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRowDaysOffRequest();
            DaysOffRequestManager.AproveDaysOffRequest(id);
            RefreshDaysOffRequest();
        }

        private void btDeclineDaysOffRequest_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRowDaysOffRequest();
            DaysOffRequestManager.DeclineDaysOffRequest(id);
            RefreshDaysOffRequest();
        }
        public void RefreshDaysOffRequest()
        {
            dataGridViewDaysOffRequest.DataSource = null;
            dataGridViewDaysOffRequest.DataSource = DaysOffRequestManager.GetDaysOffRequest();
        }

        private int GetIdSelectedRowDaysOffRequest()
        {
            if (dataGridViewDaysOffRequest.SelectedRows.Count > 0)
            {
                int selectedrowindex = dataGridViewDaysOffRequest.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridViewDaysOffRequest.Rows[selectedrowindex];
                int id = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                return id;
            }
            else
            {
                return -1;
            }

        }

        private void cmbShelfRequestTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshShelfRequestDisplay();
        }
    }
}
