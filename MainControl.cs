﻿using Newtonsoft.Json;
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
        public ShiftManager ShiftManager { get; set; }
        public EmployeeManager EmployeeManager { get; set; }
        public StockManager StockManager { get; set; }
        public DepartmentManager DepartmentManager { get; set; }

        private string _currentImageUrl = string.Empty;

        public MainControl()
        {
            InitializeComponent();
            this.ShiftManager = new ShiftManager();
            this.DepartmentManager = new DepartmentManager();
            this.EmployeeManager = new EmployeeManager(this.DepartmentManager, this.ShiftManager);
            this.StockManager = new StockManager();

            DepartmentManager.LoadDepartmentDataFromDatabase();

            EmployeeManager.LoadEmployees();

            ShiftManager.LoadShiftDataFromDatabase();
            ShiftManager.LoadShiftEmployeeDataFromDatabase(EmployeeManager.EmployeeDict);

            StockManager.LoadItemsFromDatabase();
            StockManager.LoadShelfRequestFromDatabase();

            InitializeUiElements();
        }

        public void AuthenticatedEmployee(string email)
        {
            Employee employee = EmployeeManager.GetEmployee(email);
            UserSession.Instance.CurrentEmployee = employee;
        }

        // Validation for Employee Role
        private bool EmployeeHasRequiredRole()
        {
            return UserSession.Instance.CurrentEmployee?.Role == RoleEnum.Owner || UserSession.Instance.CurrentEmployee?.Role == RoleEnum.Manager;
        }

        private void tabControMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControMain.SelectedTab == tabPageShifts)
            {
                RefreshShiftUI();
            }
            else if (tabControMain.SelectedTab == tabPageStock)
            {
                RefreshProductInfoDisplay();
                RefreshShelfRequestDisplay();
            }
        }

        private void InitializeUiElements()
        {
            RefreshEmployeesGrid();

            // Bind roles to the ComboBox
            cmbBoxEmployeeRole.DataSource = Enum.GetValues(typeof(RoleEnum));
            //cmbBoxEmployeeRole.DisplayMember = "Name";
            //cmbBoxEmployeeRole.ValueMember = "Id";
            //var defaultRole = _roles.FirstOrDefault(r => r.Name == "Retailer");
            //if (defaultRole != null)
            //{
            //cmbBoxEmployeeRole.SelectedItem = defaultRole;
            //}

            // Load Employee Status
            cmbBoxEmployeeIsActive.Items.Add(true);
            cmbBoxEmployeeIsActive.Items.Add(false);
            //cmbBoxEmployeeIsActive.SelectedIndex = 0;

            // Bind departments to the ListBox
            listBoxDepartments.DataSource = DepartmentManager.DepartmentDict.Values.ToList();
            listBoxDepartments.DisplayMember = "Name";
            listBoxDepartments.ValueMember = "Id";
            //listBoxDepartments.SelectedItem = null;
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
                if (EmployeeManager.IsNameUnique(txtBoxEmployeeName.Text))
                {
                    MessageBox.Show("An employee with this name already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (EmployeeManager.IsEmailUnique(txtBoxEmployeeEmail.Text))
                {
                    MessageBox.Show("An employee with this email already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Employee newEmployee = new Employee
                (

                    txtBoxEmployeeName.Text,
                    txtBoxEmployeeEmail.Text,
                    txtBoxEmployeePswd.Text,
                    (RoleEnum)cmbBoxEmployeeRole.SelectedItem,
                    _currentImageUrl,
                    txtBoxEmployeeAddress.Text,
                    (Department)listBoxDepartments.SelectedItem,
                    Convert.ToDecimal(txtBoxEmployeeHourlyWage.Text),
                    Convert.ToBoolean(cmbBoxEmployeeIsActive.SelectedItem)
                );

                try
                {
                    EmployeeManager.AddEmployee(newEmployee);
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

                try
                {
                    EmployeeManager.UpdateEmployee(selectedEmployeeId, txtBoxEmployeeName.Text, txtBoxEmployeeEmail.Text, txtBoxEmployeePswd.Text, (RoleEnum)cmbBoxEmployeeRole.SelectedItem, _currentImageUrl, txtBoxEmployeeAddress.Text, (Department)listBoxDepartments.SelectedItem, Convert.ToDecimal(txtBoxEmployeeHourlyWage.Text), Convert.ToBoolean(cmbBoxEmployeeIsActive.SelectedItem));
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
                        EmployeeManager.DeleteEmployee(selectedEmployeeId);
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
                listBoxDepartments.SelectedItem = selectedEmployee.Department;
                cmbBoxEmployeeRole.SelectedItem = selectedEmployee.Role;

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
                return false;
            }

            if (cmbBoxEmployeeRole.SelectedItem is not RoleEnum || listBoxDepartments.SelectedItem is not Department)
            {
                MessageBox.Show("Please select a valid role and department.");
                return false;
            }

            // If we reach this point, all validations passed
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
                        DepartmentManager.DeleteDeparment(selectedDepartment.Id);
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
            dataGridView1.DataSource = StockManager.itemList;
        }


        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            StockManager.AddNewItem(Convert.ToInt16(numericSKU.Value), tbNameProduct.Text, Convert.ToInt16(numericQuantityWarehouse.Value), Convert.ToInt16(numericQuantityStore.Value), (CategoryEnum)cbCatergory.SelectedIndex, Convert.ToDouble(numericWholesalePrice.Value), Convert.ToDouble(numericSellPrice.Value));
            RefreshProductInfoDisplay();

        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRow();
            StockManager.EditItem(id, Convert.ToInt16(numericSKU.Value), tbNameProduct.Text, Convert.ToInt16(numericQuantityWarehouse.Value), Convert.ToInt16(numericQuantityStore.Value), (CategoryEnum)cbCatergory.SelectedIndex, Convert.ToDouble(numericWholesalePrice.Value), Convert.ToDouble(numericSellPrice.Value));
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
            int itemId = GetIdSelectedRow();
            StockManager.AddShelfRequest(itemId, Convert.ToInt16(numericQuantityShelfRequest.Value));
            RefreshShelfRequestDisplay();
        }

        private void btnEditRequest_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRowShelfRequest();
            StockManager.EditShelfRequest(id, Convert.ToInt16(numericQuantityShelfRequest.Value));
            RefreshShelfRequestDisplay();
        }

        private void btnFulfillRequest_Click(object sender, EventArgs e)
        {
            int id = GetIdSelectedRowShelfRequest();
            StockManager.FulFillShelfRequest(id);
            RefreshShelfRequestDisplay();
            RefreshProductInfoDisplay();
        }

        public void RefreshShelfRequestDisplay()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = StockManager.shelfRequests;
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
