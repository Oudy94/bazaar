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
using TheSandwichMakersHardwareStoreSolution.Classes;
using TheSandwichMakersHardwareStoreSolution.Enums;
using TheSandwichMakersHardwareStoreSolution.Helpers;

namespace TheSandwichMakersHardwareStoreSolution
{
    public partial class MainControl : UserControl
    {

        private readonly DatabaseHelper _dbHelper;
        private List<Role> _roles;
        private List<Department> _departments;
        public List<Role> GetRoles() => _roles;
        public List<Department> GetDepartments() => _departments;
        private string _currentImageUrl = string.Empty;

        public ShiftManager ShiftManager { get; set; }
        public EmployeeManager EmployeeManager { get; set; }

        public MainControl()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            this.ShiftManager = new ShiftManager();
            this.EmployeeManager = new EmployeeManager(this.ShiftManager);

            LoadRolesAndDepartments();
            LoadEmployees();
            RefreshEmployeesGrid();
        }

        // Validation for Employee Role

        private bool EmployeeHasRequiredRole()
        {
            return UserSession.Instance.CurrentEmployee?.Role.Name == "Manager" || UserSession.Instance.CurrentEmployee?.Role.Name == "Owner";
        }

        // Loading data
        private void LoadRolesAndDepartments()
        {
            try
            {
                _dbHelper.OpenConnection();
                _roles = _dbHelper.GetRoles();
                _departments = _dbHelper.GetDepartments();

                // Bind roles to the ComboBox
                cmbBoxEmployeeRole.DataSource = _roles;
                cmbBoxEmployeeRole.DisplayMember = "Name";
                cmbBoxEmployeeRole.ValueMember = "Id";
                var defaultRole = _roles.FirstOrDefault(r => r.Name == "Retailer");
                if (defaultRole != null)
                {
                    cmbBoxEmployeeRole.SelectedItem = defaultRole;
                }

                // Load Employee Status
                cmbBoxEmployeeIsActive.Items.Add(true);
                cmbBoxEmployeeIsActive.Items.Add(false);
                cmbBoxEmployeeIsActive.SelectedIndex = 0; // Default to True


                // Bind departments to the ListBox
                listBoxDepartments.DataSource = _departments;
                listBoxDepartments.DisplayMember = "Name";
                listBoxDepartments.ValueMember = "Id";
                listBoxDepartments.SelectedItem = null;

                _dbHelper.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load roles and departments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadEmployees()
        {
            try
            {
                _dbHelper.OpenConnection();
                List<Employee> employees = _dbHelper.GetEmployees(_roles, _departments);

                foreach (Employee employee in employees)
                {
                    EmployeeManager.AddEmployee(employee);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Employee Tab


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
                var role = cmbBoxEmployeeRole.SelectedItem as Role;
                var department = listBoxDepartments.SelectedItem as Department;
                if (role == null || department == null)
                {
                    MessageBox.Show("Please select a valid role and department.");
                    return;
                }

                Employee newEmployee = new Employee
                (

                    txtBoxEmployeeName.Text,
                    txtBoxEmployeeEmail.Text,
                    txtBoxEmployeePswd.Text,
                    role,
                    _currentImageUrl,
                    txtBoxEmployeeAddress.Text,
                    department,
                    Convert.ToDecimal(txtBoxEmployeeHourlyWage.Text),
                    Convert.ToBoolean(cmbBoxEmployeeIsActive.SelectedItem)
                );

                try
                {
                    _dbHelper.OpenConnection();
                    _dbHelper.AddEmployee(newEmployee);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to add new employee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _dbHelper.CloseConnection();
                    RefreshEmployeesGrid();
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
                _dbHelper.OpenConnection();
                Employee selectedEmployee = _dbHelper.GetEmployeeById(selectedEmployeeId, _roles, _departments);

                if (selectedEmployee != null)
                {
                    var role = cmbBoxEmployeeRole.SelectedItem as Role;
                    var department = listBoxDepartments.SelectedItem as Department;
                    if (role == null || department == null)
                    {
                        MessageBox.Show("Please select a valid role and department.");
                        return;
                    }

                    selectedEmployee.Name = txtBoxEmployeeName.Text;
                    selectedEmployee.Email = txtBoxEmployeeEmail.Text;
                    selectedEmployee.Password = txtBoxEmployeePswd.Text;
                    selectedEmployee.Role = role;
                    selectedEmployee.Image = _currentImageUrl ?? selectedEmployee.Image;
                    selectedEmployee.Address = txtBoxEmployeeAddress.Text;
                    selectedEmployee.Department = department;
                    selectedEmployee.HourlyWage = Convert.ToDecimal(txtBoxEmployeeHourlyWage.Text);
                    selectedEmployee.IsActive = Convert.ToBoolean(cmbBoxEmployeeIsActive.SelectedItem);


                    _dbHelper.UpdateEmployee(selectedEmployee);
                    _dbHelper.CloseConnection();

                    RefreshEmployeesGrid();
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

                    _dbHelper.OpenConnection();
                    _dbHelper.DeleteEmployee(selectedEmployeeId); // Assumes DeleteEmployee can accept an ID directly
                    _dbHelper.CloseConnection();

                    RefreshEmployeesGrid();
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.");
            }
        }


        private void dtGrVEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (!EmployeeHasRequiredRole())
            {
                MessageBox.Show("Only Manager or Owner can perform this action.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
                listBoxDepartments.SelectedItem = _departments.FirstOrDefault(d => d.Name == selectedEmployee.Department);
                cmbBoxEmployeeRole.SelectedItem = _roles.FirstOrDefault(r => r.Name == selectedEmployee.Role);

                txtBoxEmployeeHourlyWage.Text = selectedEmployee.HourlyWage.ToString();
                cmbBoxEmployeeIsActive.SelectedItem = selectedEmployee.IsActive == "Yes";
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

            // Department Selected
            if (listBoxDepartments.SelectedItem == null)
            {
                MessageBox.Show("Please select a department to edit.");
            }


            // Duplicates checking
            _dbHelper.OpenConnection();
            bool nameExists = _dbHelper.CheckEmployeeNameExists(txtBoxEmployeeName.Text);
            bool emailExists = _dbHelper.CheckEmployeeEmailExists(txtBoxEmployeeEmail.Text);
            _dbHelper.CloseConnection();

            if (nameExists)
            {
                MessageBox.Show("An employee with this name already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (emailExists)
            {
                MessageBox.Show("An employee with this email already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



            // If we reach this point, all validations passed
            return true;
        }


        private void RefreshEmployeesGrid()
        {
            try
            {
                //_dbHelper.OpenConnection();
                //var employees = _dbHelper.GetEmployees(_roles, _departments);

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
                    Role = e.Role.Name,
                    Department = e.Department.Name,
                    HourlyWage = e.HourlyWage,
                    RegisterDate = e.RegisterDate,
                    IsActive = e.IsActive ? "Yes" : "No"
                }).ToList();

                dtGrVEmployees.DataSource = employeeDisplayData;

                // Optionally, adjust column headers here as needed
                dtGrVEmployees.Columns["Id"].HeaderText = "ID";
                dtGrVEmployees.Columns["Name"].HeaderText = "Name";
                dtGrVEmployees.Columns["Email"].HeaderText = "Email";
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
            finally
            {
                _dbHelper.CloseConnection();
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
                    _dbHelper.OpenConnection();
                    _dbHelper.AddDepartment(txtBoxDepartmentName.Text);
                    MessageBox.Show("Department added successfully.");
                    txtBoxDepartmentName.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    _dbHelper.CloseConnection();
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
                    _dbHelper.OpenConnection();
                    _dbHelper.UpdateDepartment(selectedDepartment.Id, txtBoxDepartmentName.Text);
                    MessageBox.Show("Department updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    _dbHelper.CloseConnection();
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
                        _dbHelper.OpenConnection();
                        _dbHelper.RemoveDepartment(selectedDepartment.Id);
                        MessageBox.Show("Department removed successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        _dbHelper.CloseConnection();
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
                _dbHelper.OpenConnection();
                _departments = _dbHelper.GetDepartments();
                listBoxDepartments.DataSource = _departments;
                listBoxDepartments.DisplayMember = "Name";
                listBoxDepartments.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
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


        private StockManager stockManager = new StockManager();

        private void RefreshProductInfoDisplay()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = stockManager.itemList;
        }


        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            stockManager.AddNewItem(Convert.ToInt16(numericSKU.Value), tbNameProduct.Text, Convert.ToInt16(numericQuantityWarehouse.Value), Convert.ToInt16(numericQuantityStore.Value), cbCatergory.Text, Convert.ToDouble(numericWholesalePrice.Value), Convert.ToDouble(numericSellPrice.Value));
            RefreshProductInfoDisplay();

        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRow();
            stockManager.EditItem(id, Convert.ToInt16(numericSKU.Value), tbNameProduct.Text, Convert.ToInt16(numericQuantityWarehouse.Value), Convert.ToInt16(numericQuantityStore.Value), cbCatergory.Text, Convert.ToDouble(numericWholesalePrice.Value), Convert.ToDouble(numericSellPrice.Value));
            RefreshProductInfoDisplay();
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRow();
            stockManager.RemoveItem(id);
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
                Item item = stockManager.GetItemById(id);
                tbNameProduct.Text = item.Name;
                numericSKU.Value = item.Sku;
                numericQuantityWarehouse.Value = item.QuantityWarehouse;
                numericQuantityStore.Value = item.QuantityStore;
                cbCatergory.Text = item.Category;
                numericWholesalePrice.Value = Convert.ToDecimal(item.WholesalePrice);
                numericSellPrice.Value = Convert.ToDecimal(item.SellPrice);
            }

        }

        private void btAddShelfRequest_Click(object sender, EventArgs e)
        {
            int itemId = GetIdSelectedRow();
            stockManager.AddShelfRequest(itemId, Convert.ToInt16(numericQuantityShelfRequest.Value));
            RefreshShelfRequestDisplay();
        }

        private void btnEditRequest_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRowShelfRequest();
            stockManager.EditShelfRequest(id, Convert.ToInt16(numericQuantityShelfRequest.Value));
            RefreshShelfRequestDisplay();
        }

        private void btnFulfillRequest_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRowShelfRequest();
            stockManager.FulFillShelfRequest(id);
            RefreshShelfRequestDisplay();
            RefreshProductInfoDisplay();
        }

        public void RefreshShelfRequestDisplay()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = stockManager.shelfRequests;
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

        private void tabControMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControMain.SelectedTab == tabPageShifts)
            {
                RefreshShiftUI();
            }
        }

        private void btnAssignMorning_Click(object sender, EventArgs e)
        {
            if (!ValidateShiftInput(lstBoxNoShiftEmployees))
            {
                return;
            }

            Employee employee = (Employee)lstBoxNoShiftEmployees.SelectedItem;
            DateTime dateTime = dtpShift.Value;
            DateOnly date = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

            ShiftManager.AssignEmployeeToShift(employee, date, ShiftTypeEnum.Morning);
            RefreshShiftUI();
        }

        private void btnAssignEvening_Click(object sender, EventArgs e)
        {
            if (!ValidateShiftInput(lstBoxNoShiftEmployees))
            {
                return;
            }

            Employee employee = (Employee)lstBoxNoShiftEmployees.SelectedItem;
            DateTime dateTime = dtpShift.Value;
            DateOnly date = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

            ShiftManager.AssignEmployeeToShift(employee, date, ShiftTypeEnum.Evening);
            RefreshShiftUI();
        }

        private void btnUnassignMorningShift_Click(object sender, EventArgs e)
        {
            if (!ValidateShiftInput(lstBoxMorningShiftEmployees))
            {
                return;
            }

            Employee employee = (Employee)lstBoxMorningShiftEmployees.SelectedItem;
            DateTime dateTime = dtpShift.Value;
            DateOnly date = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

            ShiftManager.UnassignEmployeeFromShift(employee, date, ShiftTypeEnum.Morning);
            RefreshShiftUI();
        }

        private void btnUnassignEveningShift_Click(object sender, EventArgs e)
        {
            if (!ValidateShiftInput(lstBoxEveningShiftEmployees))
            {
                return;
            }

            Employee employee = (Employee)lstBoxEveningShiftEmployees.SelectedItem;
            DateTime dateTime = dtpShift.Value;
            DateOnly date = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

            ShiftManager.UnassignEmployeeFromShift(employee, date, ShiftTypeEnum.Evening);
            RefreshShiftUI();
        }

        public bool ValidateShiftInput(ListBox listBox)
        {
            if (listBox.SelectedIndex < 0)
            {
                MessageBox.Show("you have to select employee first.");
                return false;
            }

            if (dtpShift.Value.Date == DateTime.MinValue)
            {
                MessageBox.Show("you have to select correct date.");
                return false;
            }

            return true;
        }

        private void dtpShift_ValueChanged(object sender, EventArgs e)
        {
            RefreshShiftUI();
        }

        public void RefreshShiftUI()
        {
            DateTime dateTime = dtpShift.Value;
            DateOnly date = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

            lstBoxNoShiftEmployees.Items.Clear();
            foreach (Employee employee in EmployeeManager.GetUnassignedEmployees(date))
            {
                lstBoxNoShiftEmployees.Items.Add(employee);
            }

            lstBoxMorningShiftEmployees.Items.Clear();
            foreach (Employee employee in EmployeeManager.GetAssignedEmployeed(date, ShiftTypeEnum.Morning))
            {
                lstBoxMorningShiftEmployees.Items.Add(employee);
            }

            lstBoxEveningShiftEmployees.Items.Clear();
            foreach (Employee employee in EmployeeManager.GetAssignedEmployeed(date, ShiftTypeEnum.Evening))
            {
                lstBoxEveningShiftEmployees.Items.Add(employee);
            }
        }

        private void btnAutoAssign_Click(object sender, EventArgs e)
        {
            if (dtpShift.Value.Date == DateTime.MinValue)
            {
                MessageBox.Show("you have to select correct date.");
                return;
            }

            DateTime dateTime = dtpShift.Value;
            DateOnly date = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

            ShiftManager.AutoAssignShift(date, EmployeeManager.GetUnassignedEmployees(date));
            RefreshShiftUI();
        }
    }
}
