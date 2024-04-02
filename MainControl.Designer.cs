namespace TheSandwichMakersHardwareStoreSolution
{
    partial class MainControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControMain = new TabControl();
            tabPageMain = new TabPage();
            cbBoxDepartmentList = new ComboBox();
            label46 = new Label();
            label44 = new Label();
            label45 = new Label();
            groupBox4 = new GroupBox();
            label42 = new Label();
            label43 = new Label();
            label40 = new Label();
            label41 = new Label();
            label39 = new Label();
            label37 = new Label();
            label38 = new Label();
            label33 = new Label();
            label34 = new Label();
            label31 = new Label();
            label32 = new Label();
            label29 = new Label();
            label30 = new Label();
            pictureBox1 = new PictureBox();
            label27 = new Label();
            label26 = new Label();
            label25 = new Label();
            label20 = new Label();
            groupBox3 = new GroupBox();
            label48 = new Label();
            listBox2 = new ListBox();
            label47 = new Label();
            lstBoxFilteredEmployees = new ListBox();
            label19 = new Label();
            label18 = new Label();
            cbBoxRolesList = new ComboBox();
            tabPageEmployee = new TabPage();
            label50 = new Label();
            dtGrVEmployees = new DataGridView();
            groupBoxManageDepartments = new GroupBox();
            btnEditDepartment = new Button();
            txtBoxDepartmentName = new TextBox();
            btnRemoveDepartment = new Button();
            label5 = new Label();
            btnNewDepartment = new Button();
            listBoxDepartments = new ListBox();
            groupBoxManageEmployees = new GroupBox();
            txtBoxEmployeeAddress = new TextBox();
            label49 = new Label();
            btnEditEmployee = new Button();
            btnRemoveEmployee = new Button();
            btnNewEmployee = new Button();
            cmbBoxEmployeeIsActive = new ComboBox();
            label4 = new Label();
            txtBoxEmployeeHourlyWage = new TextBox();
            label3 = new Label();
            btnEmployeeAttachImage = new Button();
            lblImage = new Label();
            cmbBoxEmployeeRole = new ComboBox();
            label1 = new Label();
            txtBoxEmployeePswd = new TextBox();
            lblEmployeePswd = new Label();
            txtBoxEmployeeEmail = new TextBox();
            lblEmployeeEmail = new Label();
            txtBoxEmployeeName = new TextBox();
            lblEmployeeName = new Label();
            tabPageStock = new TabPage();
            groupBox2 = new GroupBox();
            dataGridView2 = new DataGridView();
            numericQuantityShelfRequest = new NumericUpDown();
            btnEditRequest = new Button();
            btnFulfillRequest = new Button();
            btAddShelfRequest = new Button();
            label21 = new Label();
            label23 = new Label();
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            numericQuantityStore = new NumericUpDown();
            label2 = new Label();
            numericQuantityWarehouse = new NumericUpDown();
            numericSellPrice = new NumericUpDown();
            numericWholesalePrice = new NumericUpDown();
            numericSKU = new NumericUpDown();
            label10 = new Label();
            btnEditProduct = new Button();
            btnRemoveProduct = new Button();
            btnNewProduct = new Button();
            label11 = new Label();
            cbCatergory = new ComboBox();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            tbNameProduct = new TextBox();
            label16 = new Label();
            tabPageShifts = new TabPage();
            btnUnassignEveningShift = new Button();
            dtpShift = new DateTimePicker();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            btnAutoAssign = new Button();
            btnUnassignMorningShift = new Button();
            btnAssignEvening = new Button();
            btnAssignMorning = new Button();
            lstBoxEveningShiftEmployees = new ListBox();
            lstBoxNoShiftEmployees = new ListBox();
            lstBoxMorningShiftEmployees = new ListBox();
            tabControMain.SuspendLayout();
            tabPageMain.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox3.SuspendLayout();
            tabPageEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtGrVEmployees).BeginInit();
            groupBoxManageDepartments.SuspendLayout();
            groupBoxManageEmployees.SuspendLayout();
            tabPageStock.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericQuantityShelfRequest).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericQuantityStore).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericQuantityWarehouse).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericSellPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericWholesalePrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericSKU).BeginInit();
            tabPageShifts.SuspendLayout();
            SuspendLayout();
            // 
            // tabControMain
            // 
            tabControMain.Controls.Add(tabPageMain);
            tabControMain.Controls.Add(tabPageEmployee);
            tabControMain.Controls.Add(tabPageStock);
            tabControMain.Controls.Add(tabPageShifts);
            tabControMain.Location = new Point(8, 8);
            tabControMain.Name = "tabControMain";
            tabControMain.SelectedIndex = 0;
            tabControMain.Size = new Size(960, 547);
            tabControMain.TabIndex = 1;
            tabControMain.SelectedIndexChanged += tabControMain_SelectedIndexChanged;
            // 
            // tabPageMain
            // 
            tabPageMain.Controls.Add(cbBoxDepartmentList);
            tabPageMain.Controls.Add(label46);
            tabPageMain.Controls.Add(label44);
            tabPageMain.Controls.Add(label45);
            tabPageMain.Controls.Add(groupBox4);
            tabPageMain.Controls.Add(groupBox3);
            tabPageMain.Controls.Add(lstBoxFilteredEmployees);
            tabPageMain.Controls.Add(label19);
            tabPageMain.Controls.Add(label18);
            tabPageMain.Controls.Add(cbBoxRolesList);
            tabPageMain.Location = new Point(4, 26);
            tabPageMain.Name = "tabPageMain";
            tabPageMain.Size = new Size(952, 517);
            tabPageMain.TabIndex = 0;
            tabPageMain.Text = "Main";
            tabPageMain.UseVisualStyleBackColor = true;
            // 
            // cbBoxDepartmentList
            // 
            cbBoxDepartmentList.FormattingEnabled = true;
            cbBoxDepartmentList.Location = new Point(164, 29);
            cbBoxDepartmentList.Name = "cbBoxDepartmentList";
            cbBoxDepartmentList.Size = new Size(133, 25);
            cbBoxDepartmentList.TabIndex = 47;
            // 
            // label46
            // 
            label46.AutoSize = true;
            label46.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label46.Location = new Point(12, 497);
            label46.Name = "label46";
            label46.Size = new Size(90, 13);
            label46.TabIndex = 46;
            label46.Text = "Based On Filters";
            // 
            // label44
            // 
            label44.AutoSize = true;
            label44.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label44.Location = new Point(12, 476);
            label44.Name = "label44";
            label44.Size = new Size(97, 21);
            label44.TabIndex = 45;
            label44.Text = "Employees:";
            // 
            // label45
            // 
            label45.AutoSize = true;
            label45.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label45.Location = new Point(115, 479);
            label45.Name = "label45";
            label45.Size = new Size(170, 17);
            label45.TabIndex = 44;
            label45.Text = "{EmployeesDisplayedCount}";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label42);
            groupBox4.Controls.Add(label43);
            groupBox4.Controls.Add(label40);
            groupBox4.Controls.Add(label41);
            groupBox4.Controls.Add(label39);
            groupBox4.Controls.Add(label37);
            groupBox4.Controls.Add(label38);
            groupBox4.Controls.Add(label33);
            groupBox4.Controls.Add(label34);
            groupBox4.Controls.Add(label31);
            groupBox4.Controls.Add(label32);
            groupBox4.Controls.Add(label29);
            groupBox4.Controls.Add(label30);
            groupBox4.Controls.Add(pictureBox1);
            groupBox4.Controls.Add(label27);
            groupBox4.Controls.Add(label26);
            groupBox4.Controls.Add(label25);
            groupBox4.Controls.Add(label20);
            groupBox4.Location = new Point(336, 11);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(601, 337);
            groupBox4.TabIndex = 22;
            groupBox4.TabStop = false;
            groupBox4.Text = "Employee Information";
            // 
            // label42
            // 
            label42.AutoSize = true;
            label42.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            label42.Location = new Point(131, 250);
            label42.Name = "label42";
            label42.Size = new Size(88, 20);
            label42.TabIndex = 43;
            label42.Text = "Total Shifts";
            // 
            // label43
            // 
            label43.AutoSize = true;
            label43.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label43.Location = new Point(131, 270);
            label43.Name = "label43";
            label43.Size = new Size(132, 17);
            label43.TabIndex = 42;
            label43.Text = "{EmployeeTotalShifts}";
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            label40.Location = new Point(185, 195);
            label40.Name = "label40";
            label40.Size = new Size(112, 20);
            label40.TabIndex = 41;
            label40.Text = "Evening Shifts:";
            // 
            // label41
            // 
            label41.AutoSize = true;
            label41.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label41.Location = new Point(185, 215);
            label41.Name = "label41";
            label41.Size = new Size(197, 17);
            label41.TabIndex = 40;
            label41.Text = "{EmployeeEveningShiftsNumber}";
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            label39.Location = new Point(17, 150);
            label39.Name = "label39";
            label39.Size = new Size(75, 30);
            label39.TabIndex = 39;
            label39.Text = "Today";
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            label37.Location = new Point(17, 195);
            label37.Name = "label37";
            label37.Size = new Size(117, 20);
            label37.TabIndex = 38;
            label37.Text = "Morning Shifts:";
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label38.Location = new Point(17, 215);
            label38.Name = "label38";
            label38.Size = new Size(202, 17);
            label38.TabIndex = 37;
            label38.Text = "{EmployeeMorningShiftsNumber}";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label33.Location = new Point(185, 83);
            label33.Name = "label33";
            label33.Size = new Size(106, 21);
            label33.TabIndex = 33;
            label33.Text = "Department:";
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label34.Location = new Point(185, 104);
            label34.Name = "label34";
            label34.Size = new Size(142, 17);
            label34.TabIndex = 32;
            label34.Text = "{EmployeeDepartment}";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label31.Location = new Point(17, 83);
            label31.Name = "label31";
            label31.Size = new Size(48, 21);
            label31.TabIndex = 31;
            label31.Text = "Role:";
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label32.Location = new Point(17, 104);
            label32.Name = "label32";
            label32.Size = new Size(99, 17);
            label32.TabIndex = 30;
            label32.Text = "{EmployeeRole}";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label29.Location = new Point(185, 29);
            label29.Name = "label29";
            label29.Size = new Size(57, 21);
            label29.TabIndex = 29;
            label29.Text = "Email:";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label30.Location = new Point(185, 50);
            label30.Name = "label30";
            label30.Size = new Size(104, 17);
            label30.TabIndex = 28;
            label30.Text = "{EmployeeEmail}";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(387, 53);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(187, 162);
            pictureBox1.TabIndex = 27;
            pictureBox1.TabStop = false;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label27.Location = new Point(17, 159);
            label27.Name = "label27";
            label27.Size = new Size(0, 21);
            label27.TabIndex = 25;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label26.Location = new Point(387, 29);
            label26.Name = "label26";
            label26.Size = new Size(62, 21);
            label26.TabIndex = 24;
            label26.Text = "Image:";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label25.Location = new Point(17, 29);
            label25.Name = "label25";
            label25.Size = new Size(60, 21);
            label25.TabIndex = 23;
            label25.Text = "Name:";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label20.Location = new Point(17, 50);
            label20.Name = "label20";
            label20.Size = new Size(108, 17);
            label20.TabIndex = 21;
            label20.Text = "{EmployeeName}";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label48);
            groupBox3.Controls.Add(listBox2);
            groupBox3.Controls.Add(label47);
            groupBox3.Location = new Point(336, 354);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(601, 143);
            groupBox3.TabIndex = 21;
            groupBox3.TabStop = false;
            groupBox3.Text = "Stock Information";
            // 
            // label48
            // 
            label48.AutoSize = true;
            label48.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label48.Location = new Point(229, 22);
            label48.Name = "label48";
            label48.Size = new Size(131, 17);
            label48.TabIndex = 44;
            label48.Text = "{NumberOfRequests}";
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 17;
            listBox2.Location = new Point(7, 42);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(290, 89);
            listBox2.TabIndex = 45;
            // 
            // label47
            // 
            label47.AutoSize = true;
            label47.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label47.Location = new Point(6, 18);
            label47.Name = "label47";
            label47.Size = new Size(217, 21);
            label47.TabIndex = 44;
            label47.Text = "Outstanding Shelf Request:";
            // 
            // lstBoxFilteredEmployees
            // 
            lstBoxFilteredEmployees.FormattingEnabled = true;
            lstBoxFilteredEmployees.ItemHeight = 17;
            lstBoxFilteredEmployees.Location = new Point(12, 58);
            lstBoxFilteredEmployees.Name = "lstBoxFilteredEmployees";
            lstBoxFilteredEmployees.Size = new Size(285, 395);
            lstBoxFilteredEmployees.TabIndex = 4;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(164, 11);
            label19.Name = "label19";
            label19.Size = new Size(77, 17);
            label19.TabIndex = 3;
            label19.Text = "Department";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(12, 11);
            label18.Name = "label18";
            label18.Size = new Size(34, 17);
            label18.TabIndex = 2;
            label18.Text = "Role";
            // 
            // cbBoxRolesList
            // 
            cbBoxRolesList.FormattingEnabled = true;
            cbBoxRolesList.Location = new Point(12, 29);
            cbBoxRolesList.Name = "cbBoxRolesList";
            cbBoxRolesList.Size = new Size(133, 25);
            cbBoxRolesList.TabIndex = 0;
            // 
            // tabPageEmployee
            // 
            tabPageEmployee.Controls.Add(label50);
            tabPageEmployee.Controls.Add(dtGrVEmployees);
            tabPageEmployee.Controls.Add(groupBoxManageDepartments);
            tabPageEmployee.Controls.Add(listBoxDepartments);
            tabPageEmployee.Controls.Add(groupBoxManageEmployees);
            tabPageEmployee.Location = new Point(4, 26);
            tabPageEmployee.Name = "tabPageEmployee";
            tabPageEmployee.Size = new Size(952, 517);
            tabPageEmployee.TabIndex = 1;
            tabPageEmployee.Text = "Employee & Departments";
            tabPageEmployee.UseVisualStyleBackColor = true;
            // 
            // label50
            // 
            label50.AutoSize = true;
            label50.Location = new Point(708, 246);
            label50.Name = "label50";
            label50.Size = new Size(83, 17);
            label50.TabIndex = 19;
            label50.Text = "Departments";
            // 
            // dtGrVEmployees
            // 
            dtGrVEmployees.AllowUserToAddRows = false;
            dtGrVEmployees.AllowUserToDeleteRows = false;
            dtGrVEmployees.AllowUserToOrderColumns = true;
            dtGrVEmployees.AllowUserToResizeColumns = false;
            dtGrVEmployees.AllowUserToResizeRows = false;
            dtGrVEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtGrVEmployees.Location = new Point(182, 25);
            dtGrVEmployees.Name = "dtGrVEmployees";
            dtGrVEmployees.RowTemplate.Height = 25;
            dtGrVEmployees.Size = new Size(520, 468);
            dtGrVEmployees.TabIndex = 2;
            dtGrVEmployees.SelectionChanged += dtGrVEmployees_SelectionChanged;
            // 
            // groupBoxManageDepartments
            // 
            groupBoxManageDepartments.Controls.Add(btnEditDepartment);
            groupBoxManageDepartments.Controls.Add(txtBoxDepartmentName);
            groupBoxManageDepartments.Controls.Add(btnRemoveDepartment);
            groupBoxManageDepartments.Controls.Add(label5);
            groupBoxManageDepartments.Controls.Add(btnNewDepartment);
            groupBoxManageDepartments.Location = new Point(708, 18);
            groupBoxManageDepartments.Name = "groupBoxManageDepartments";
            groupBoxManageDepartments.Size = new Size(224, 219);
            groupBoxManageDepartments.TabIndex = 1;
            groupBoxManageDepartments.TabStop = false;
            groupBoxManageDepartments.Text = "Manage Departments";
            // 
            // btnEditDepartment
            // 
            btnEditDepartment.Location = new Point(27, 146);
            btnEditDepartment.Name = "btnEditDepartment";
            btnEditDepartment.Size = new Size(177, 23);
            btnEditDepartment.TabIndex = 21;
            btnEditDepartment.Text = "Edit Department";
            btnEditDepartment.UseVisualStyleBackColor = true;
            btnEditDepartment.Click += btnEditDepartment_Click;
            // 
            // txtBoxDepartmentName
            // 
            txtBoxDepartmentName.Location = new Point(12, 48);
            txtBoxDepartmentName.Name = "txtBoxDepartmentName";
            txtBoxDepartmentName.Size = new Size(201, 25);
            txtBoxDepartmentName.TabIndex = 18;
            // 
            // btnRemoveDepartment
            // 
            btnRemoveDepartment.Location = new Point(27, 175);
            btnRemoveDepartment.Name = "btnRemoveDepartment";
            btnRemoveDepartment.Size = new Size(177, 23);
            btnRemoveDepartment.TabIndex = 20;
            btnRemoveDepartment.Text = "Remove Department";
            btnRemoveDepartment.UseVisualStyleBackColor = true;
            btnRemoveDepartment.Click += btnRemoveDepartment_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 28);
            label5.Name = "label5";
            label5.Size = new Size(43, 17);
            label5.TabIndex = 17;
            label5.Text = "Name";
            // 
            // btnNewDepartment
            // 
            btnNewDepartment.Location = new Point(27, 117);
            btnNewDepartment.Name = "btnNewDepartment";
            btnNewDepartment.Size = new Size(177, 23);
            btnNewDepartment.TabIndex = 19;
            btnNewDepartment.Text = "New Department";
            btnNewDepartment.UseVisualStyleBackColor = true;
            btnNewDepartment.Click += btnNewDepartment_Click;
            // 
            // listBoxDepartments
            // 
            listBoxDepartments.FormattingEnabled = true;
            listBoxDepartments.ItemHeight = 17;
            listBoxDepartments.Location = new Point(708, 266);
            listBoxDepartments.Name = "listBoxDepartments";
            listBoxDepartments.Size = new Size(224, 225);
            listBoxDepartments.TabIndex = 1;
            listBoxDepartments.SelectedIndexChanged += listBoxDepartments_SelectedIndexChanged;
            // 
            // groupBoxManageEmployees
            // 
            groupBoxManageEmployees.Controls.Add(txtBoxEmployeeAddress);
            groupBoxManageEmployees.Controls.Add(label49);
            groupBoxManageEmployees.Controls.Add(btnEditEmployee);
            groupBoxManageEmployees.Controls.Add(btnRemoveEmployee);
            groupBoxManageEmployees.Controls.Add(btnNewEmployee);
            groupBoxManageEmployees.Controls.Add(cmbBoxEmployeeIsActive);
            groupBoxManageEmployees.Controls.Add(label4);
            groupBoxManageEmployees.Controls.Add(txtBoxEmployeeHourlyWage);
            groupBoxManageEmployees.Controls.Add(label3);
            groupBoxManageEmployees.Controls.Add(btnEmployeeAttachImage);
            groupBoxManageEmployees.Controls.Add(lblImage);
            groupBoxManageEmployees.Controls.Add(cmbBoxEmployeeRole);
            groupBoxManageEmployees.Controls.Add(label1);
            groupBoxManageEmployees.Controls.Add(txtBoxEmployeePswd);
            groupBoxManageEmployees.Controls.Add(lblEmployeePswd);
            groupBoxManageEmployees.Controls.Add(txtBoxEmployeeEmail);
            groupBoxManageEmployees.Controls.Add(lblEmployeeEmail);
            groupBoxManageEmployees.Controls.Add(txtBoxEmployeeName);
            groupBoxManageEmployees.Controls.Add(lblEmployeeName);
            groupBoxManageEmployees.Location = new Point(12, 18);
            groupBoxManageEmployees.Name = "groupBoxManageEmployees";
            groupBoxManageEmployees.Size = new Size(164, 475);
            groupBoxManageEmployees.TabIndex = 0;
            groupBoxManageEmployees.TabStop = false;
            groupBoxManageEmployees.Text = "Manage Employees";
            // 
            // txtBoxEmployeeAddress
            // 
            txtBoxEmployeeAddress.Location = new Point(6, 170);
            txtBoxEmployeeAddress.Name = "txtBoxEmployeeAddress";
            txtBoxEmployeeAddress.Size = new Size(152, 25);
            txtBoxEmployeeAddress.TabIndex = 18;
            // 
            // label49
            // 
            label49.AutoSize = true;
            label49.Location = new Point(6, 152);
            label49.Name = "label49";
            label49.Size = new Size(56, 17);
            label49.TabIndex = 17;
            label49.Text = "Address";
            // 
            // btnEditEmployee
            // 
            btnEditEmployee.Location = new Point(6, 416);
            btnEditEmployee.Name = "btnEditEmployee";
            btnEditEmployee.Size = new Size(152, 23);
            btnEditEmployee.TabIndex = 16;
            btnEditEmployee.Text = "Edit Employee";
            btnEditEmployee.UseVisualStyleBackColor = true;
            btnEditEmployee.Click += btnEditEmployee_Click;
            // 
            // btnRemoveEmployee
            // 
            btnRemoveEmployee.Location = new Point(6, 445);
            btnRemoveEmployee.Name = "btnRemoveEmployee";
            btnRemoveEmployee.Size = new Size(152, 23);
            btnRemoveEmployee.TabIndex = 15;
            btnRemoveEmployee.Text = "Remove Employee";
            btnRemoveEmployee.UseVisualStyleBackColor = true;
            btnRemoveEmployee.Click += btnRemoveEmployee_Click;
            // 
            // btnNewEmployee
            // 
            btnNewEmployee.Location = new Point(6, 387);
            btnNewEmployee.Name = "btnNewEmployee";
            btnNewEmployee.Size = new Size(152, 23);
            btnNewEmployee.TabIndex = 14;
            btnNewEmployee.Text = "New Employee";
            btnNewEmployee.UseVisualStyleBackColor = true;
            btnNewEmployee.Click += btnNewEmployee_Click;
            // 
            // cmbBoxEmployeeIsActive
            // 
            cmbBoxEmployeeIsActive.FormattingEnabled = true;
            cmbBoxEmployeeIsActive.Location = new Point(6, 347);
            cmbBoxEmployeeIsActive.Name = "cmbBoxEmployeeIsActive";
            cmbBoxEmployeeIsActive.Size = new Size(152, 25);
            cmbBoxEmployeeIsActive.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 327);
            label4.Name = "label4";
            label4.Size = new Size(55, 17);
            label4.TabIndex = 12;
            label4.Text = "Is Active";
            // 
            // txtBoxEmployeeHourlyWage
            // 
            txtBoxEmployeeHourlyWage.Location = new Point(6, 301);
            txtBoxEmployeeHourlyWage.Name = "txtBoxEmployeeHourlyWage";
            txtBoxEmployeeHourlyWage.Size = new Size(152, 25);
            txtBoxEmployeeHourlyWage.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 283);
            label3.Name = "label3";
            label3.Size = new Size(83, 17);
            label3.TabIndex = 10;
            label3.Text = "Hourly Wage";
            // 
            // btnEmployeeAttachImage
            // 
            btnEmployeeAttachImage.Location = new Point(6, 258);
            btnEmployeeAttachImage.Name = "btnEmployeeAttachImage";
            btnEmployeeAttachImage.Size = new Size(152, 23);
            btnEmployeeAttachImage.TabIndex = 9;
            btnEmployeeAttachImage.Text = "Attach Image";
            btnEmployeeAttachImage.UseVisualStyleBackColor = true;
            btnEmployeeAttachImage.Click += btnEmployeeAttachImage_Click;
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Location = new Point(6, 240);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(44, 17);
            lblImage.TabIndex = 8;
            lblImage.Text = "Image";
            // 
            // cmbBoxEmployeeRole
            // 
            cmbBoxEmployeeRole.FormattingEnabled = true;
            cmbBoxEmployeeRole.Location = new Point(6, 214);
            cmbBoxEmployeeRole.Name = "cmbBoxEmployeeRole";
            cmbBoxEmployeeRole.Size = new Size(152, 25);
            cmbBoxEmployeeRole.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 196);
            label1.Name = "label1";
            label1.Size = new Size(34, 17);
            label1.TabIndex = 6;
            label1.Text = "Role";
            // 
            // txtBoxEmployeePswd
            // 
            txtBoxEmployeePswd.Location = new Point(6, 125);
            txtBoxEmployeePswd.Name = "txtBoxEmployeePswd";
            txtBoxEmployeePswd.Size = new Size(152, 25);
            txtBoxEmployeePswd.TabIndex = 5;
            // 
            // lblEmployeePswd
            // 
            lblEmployeePswd.AutoSize = true;
            lblEmployeePswd.Location = new Point(6, 107);
            lblEmployeePswd.Name = "lblEmployeePswd";
            lblEmployeePswd.Size = new Size(64, 17);
            lblEmployeePswd.TabIndex = 4;
            lblEmployeePswd.Text = "Password";
            // 
            // txtBoxEmployeeEmail
            // 
            txtBoxEmployeeEmail.Location = new Point(6, 81);
            txtBoxEmployeeEmail.Name = "txtBoxEmployeeEmail";
            txtBoxEmployeeEmail.Size = new Size(152, 25);
            txtBoxEmployeeEmail.TabIndex = 3;
            // 
            // lblEmployeeEmail
            // 
            lblEmployeeEmail.AutoSize = true;
            lblEmployeeEmail.Location = new Point(6, 63);
            lblEmployeeEmail.Name = "lblEmployeeEmail";
            lblEmployeeEmail.Size = new Size(39, 17);
            lblEmployeeEmail.TabIndex = 2;
            lblEmployeeEmail.Text = "Email";
            // 
            // txtBoxEmployeeName
            // 
            txtBoxEmployeeName.Location = new Point(6, 37);
            txtBoxEmployeeName.Name = "txtBoxEmployeeName";
            txtBoxEmployeeName.Size = new Size(152, 25);
            txtBoxEmployeeName.TabIndex = 1;
            // 
            // lblEmployeeName
            // 
            lblEmployeeName.AutoSize = true;
            lblEmployeeName.Location = new Point(6, 19);
            lblEmployeeName.Name = "lblEmployeeName";
            lblEmployeeName.Size = new Size(43, 17);
            lblEmployeeName.TabIndex = 0;
            lblEmployeeName.Text = "Name";
            // 
            // tabPageStock
            // 
            tabPageStock.Controls.Add(groupBox2);
            tabPageStock.Controls.Add(dataGridView1);
            tabPageStock.Controls.Add(groupBox1);
            tabPageStock.Location = new Point(4, 24);
            tabPageStock.Name = "tabPageStock";
            tabPageStock.Size = new Size(952, 519);
            tabPageStock.TabIndex = 2;
            tabPageStock.Text = "Stock";
            tabPageStock.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView2);
            groupBox2.Controls.Add(numericQuantityShelfRequest);
            groupBox2.Controls.Add(btnEditRequest);
            groupBox2.Controls.Add(btnFulfillRequest);
            groupBox2.Controls.Add(btAddShelfRequest);
            groupBox2.Controls.Add(label21);
            groupBox2.Controls.Add(label23);
            groupBox2.Location = new Point(750, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(186, 494);
            groupBox2.TabIndex = 25;
            groupBox2.TabStop = false;
            groupBox2.Text = "Manage Shelf Requests";
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(6, 204);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.Size = new Size(174, 284);
            dataGridView2.TabIndex = 25;
            // 
            // numericQuantityShelfRequest
            // 
            numericQuantityShelfRequest.Location = new Point(20, 47);
            numericQuantityShelfRequest.Name = "numericQuantityShelfRequest";
            numericQuantityShelfRequest.Size = new Size(139, 25);
            numericQuantityShelfRequest.TabIndex = 24;
            // 
            // btnEditRequest
            // 
            btnEditRequest.Location = new Point(20, 109);
            btnEditRequest.Name = "btnEditRequest";
            btnEditRequest.Size = new Size(139, 23);
            btnEditRequest.TabIndex = 16;
            btnEditRequest.Text = "Edit Request";
            btnEditRequest.UseVisualStyleBackColor = true;
            btnEditRequest.Click += btnEditRequest_Click;
            // 
            // btnFulfillRequest
            // 
            btnFulfillRequest.Location = new Point(20, 138);
            btnFulfillRequest.Name = "btnFulfillRequest";
            btnFulfillRequest.Size = new Size(139, 23);
            btnFulfillRequest.TabIndex = 15;
            btnFulfillRequest.Text = "Fulfill Request";
            btnFulfillRequest.UseVisualStyleBackColor = true;
            btnFulfillRequest.Click += btnFulfillRequest_Click;
            // 
            // btAddShelfRequest
            // 
            btAddShelfRequest.Location = new Point(20, 80);
            btAddShelfRequest.Name = "btAddShelfRequest";
            btAddShelfRequest.Size = new Size(139, 23);
            btAddShelfRequest.TabIndex = 14;
            btAddShelfRequest.Text = "New Request";
            btAddShelfRequest.UseVisualStyleBackColor = true;
            btAddShelfRequest.Click += btAddShelfRequest_Click;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(17, 177);
            label21.Name = "label21";
            label21.Size = new Size(93, 17);
            label21.TabIndex = 10;
            label21.Text = "Shelf Requests";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(17, 21);
            label23.Name = "label23";
            label23.Size = new Size(56, 17);
            label23.TabIndex = 4;
            label23.Text = "Quantity";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(215, 12);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(520, 494);
            dataGridView1.TabIndex = 4;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(numericQuantityStore);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(numericQuantityWarehouse);
            groupBox1.Controls.Add(numericSellPrice);
            groupBox1.Controls.Add(numericWholesalePrice);
            groupBox1.Controls.Add(numericSKU);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(btnEditProduct);
            groupBox1.Controls.Add(btnRemoveProduct);
            groupBox1.Controls.Add(btnNewProduct);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(cbCatergory);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(tbNameProduct);
            groupBox1.Controls.Add(label16);
            groupBox1.Location = new Point(14, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(186, 494);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Manage Products";
            // 
            // numericQuantityStore
            // 
            numericQuantityStore.Location = new Point(18, 187);
            numericQuantityStore.Name = "numericQuantityStore";
            numericQuantityStore.Size = new Size(140, 25);
            numericQuantityStore.TabIndex = 31;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 167);
            label2.Name = "label2";
            label2.Size = new Size(91, 17);
            label2.TabIndex = 30;
            label2.Text = "Quantity Store";
            // 
            // numericQuantityWarehouse
            // 
            numericQuantityWarehouse.Location = new Point(18, 136);
            numericQuantityWarehouse.Name = "numericQuantityWarehouse";
            numericQuantityWarehouse.Size = new Size(140, 25);
            numericQuantityWarehouse.TabIndex = 29;
            // 
            // numericSellPrice
            // 
            numericSellPrice.DecimalPlaces = 2;
            numericSellPrice.Location = new Point(18, 329);
            numericSellPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericSellPrice.Name = "numericSellPrice";
            numericSellPrice.Size = new Size(140, 25);
            numericSellPrice.TabIndex = 28;
            // 
            // numericWholesalePrice
            // 
            numericWholesalePrice.DecimalPlaces = 2;
            numericWholesalePrice.Location = new Point(18, 285);
            numericWholesalePrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericWholesalePrice.Name = "numericWholesalePrice";
            numericWholesalePrice.Size = new Size(140, 25);
            numericWholesalePrice.TabIndex = 27;
            // 
            // numericSKU
            // 
            numericSKU.Location = new Point(18, 92);
            numericSKU.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericSKU.Name = "numericSKU";
            numericSKU.Size = new Size(140, 25);
            numericSKU.TabIndex = 26;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(19, 309);
            label10.Name = "label10";
            label10.Size = new Size(60, 17);
            label10.TabIndex = 17;
            label10.Text = "Sell Price";
            // 
            // btnEditProduct
            // 
            btnEditProduct.Location = new Point(18, 392);
            btnEditProduct.Name = "btnEditProduct";
            btnEditProduct.Size = new Size(140, 23);
            btnEditProduct.TabIndex = 16;
            btnEditProduct.Text = "Edit Product";
            btnEditProduct.UseVisualStyleBackColor = true;
            btnEditProduct.Click += btnEditProduct_Click;
            // 
            // btnRemoveProduct
            // 
            btnRemoveProduct.Location = new Point(18, 421);
            btnRemoveProduct.Name = "btnRemoveProduct";
            btnRemoveProduct.Size = new Size(140, 23);
            btnRemoveProduct.TabIndex = 15;
            btnRemoveProduct.Text = "Remove Product";
            btnRemoveProduct.UseVisualStyleBackColor = true;
            btnRemoveProduct.Click += btnRemoveProduct_Click;
            // 
            // btnNewProduct
            // 
            btnNewProduct.Location = new Point(18, 363);
            btnNewProduct.Name = "btnNewProduct";
            btnNewProduct.Size = new Size(140, 23);
            btnNewProduct.TabIndex = 14;
            btnNewProduct.Text = "New Product";
            btnNewProduct.UseVisualStyleBackColor = true;
            btnNewProduct.Click += btnNewProduct_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(19, 265);
            label11.Name = "label11";
            label11.Size = new Size(100, 17);
            label11.TabIndex = 10;
            label11.Text = "Wholesale Price";
            // 
            // cbCatergory
            // 
            cbCatergory.FormattingEnabled = true;
            cbCatergory.Items.AddRange(new object[] { "Books", "Electronic", "Media", "Videogames" });
            cbCatergory.Location = new Point(18, 237);
            cbCatergory.Name = "cbCatergory";
            cbCatergory.Size = new Size(140, 25);
            cbCatergory.TabIndex = 7;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(18, 217);
            label13.Name = "label13";
            label13.Size = new Size(61, 17);
            label13.TabIndex = 6;
            label13.Text = "Category";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(18, 116);
            label14.Name = "label14";
            label14.Size = new Size(125, 17);
            label14.TabIndex = 4;
            label14.Text = "Quantity Warehouse";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(18, 72);
            label15.Name = "label15";
            label15.Size = new Size(32, 17);
            label15.TabIndex = 2;
            label15.Text = "SKU";
            // 
            // tbNameProduct
            // 
            tbNameProduct.Location = new Point(18, 46);
            tbNameProduct.Name = "tbNameProduct";
            tbNameProduct.Size = new Size(140, 25);
            tbNameProduct.TabIndex = 1;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(18, 28);
            label16.Name = "label16";
            label16.Size = new Size(43, 17);
            label16.TabIndex = 0;
            label16.Text = "Name";
            // 
            // tabPageShifts
            // 
            tabPageShifts.Controls.Add(btnUnassignEveningShift);
            tabPageShifts.Controls.Add(dtpShift);
            tabPageShifts.Controls.Add(label9);
            tabPageShifts.Controls.Add(label8);
            tabPageShifts.Controls.Add(label7);
            tabPageShifts.Controls.Add(label6);
            tabPageShifts.Controls.Add(btnAutoAssign);
            tabPageShifts.Controls.Add(btnUnassignMorningShift);
            tabPageShifts.Controls.Add(btnAssignEvening);
            tabPageShifts.Controls.Add(btnAssignMorning);
            tabPageShifts.Controls.Add(lstBoxEveningShiftEmployees);
            tabPageShifts.Controls.Add(lstBoxNoShiftEmployees);
            tabPageShifts.Controls.Add(lstBoxMorningShiftEmployees);
            tabPageShifts.Location = new Point(4, 24);
            tabPageShifts.Name = "tabPageShifts";
            tabPageShifts.Size = new Size(952, 519);
            tabPageShifts.TabIndex = 3;
            tabPageShifts.Text = "Shifts";
            tabPageShifts.UseVisualStyleBackColor = true;
            // 
            // btnUnassignEveningShift
            // 
            btnUnassignEveningShift.Location = new Point(307, 390);
            btnUnassignEveningShift.Name = "btnUnassignEveningShift";
            btnUnassignEveningShift.Size = new Size(153, 40);
            btnUnassignEveningShift.TabIndex = 12;
            btnUnassignEveningShift.Text = "Unassign Evening Shift";
            btnUnassignEveningShift.UseVisualStyleBackColor = true;
            btnUnassignEveningShift.Click += btnUnassignEveningShift_Click;
            // 
            // dtpShift
            // 
            dtpShift.Location = new Point(286, 64);
            dtpShift.Name = "dtpShift";
            dtpShift.Size = new Size(221, 25);
            dtpShift.TabIndex = 11;
            dtpShift.ValueChanged += dtpShift_ValueChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(286, 46);
            label9.Name = "label9";
            label9.Size = new Size(35, 17);
            label9.TabIndex = 10;
            label9.Text = "Date";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(741, 11);
            label8.Name = "label8";
            label8.Size = new Size(177, 17);
            label8.TabIndex = 9;
            label8.Text = "Evening Assigned Employees";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(536, 11);
            label7.Name = "label7";
            label7.Size = new Size(182, 17);
            label7.TabIndex = 8;
            label7.Text = "Morning Assigned Employees";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 11);
            label6.Name = "label6";
            label6.Size = new Size(143, 17);
            label6.TabIndex = 7;
            label6.Text = "Unassigned Employees";
            // 
            // btnAutoAssign
            // 
            btnAutoAssign.Location = new Point(307, 135);
            btnAutoAssign.Name = "btnAutoAssign";
            btnAutoAssign.Size = new Size(153, 40);
            btnAutoAssign.TabIndex = 6;
            btnAutoAssign.Text = "Auto Assign";
            btnAutoAssign.UseVisualStyleBackColor = true;
            btnAutoAssign.Click += btnAutoAssign_Click;
            // 
            // btnUnassignMorningShift
            // 
            btnUnassignMorningShift.Location = new Point(307, 344);
            btnUnassignMorningShift.Name = "btnUnassignMorningShift";
            btnUnassignMorningShift.Size = new Size(153, 40);
            btnUnassignMorningShift.TabIndex = 5;
            btnUnassignMorningShift.Text = "Unassign Morning Shift";
            btnUnassignMorningShift.UseVisualStyleBackColor = true;
            btnUnassignMorningShift.Click += btnUnassignMorningShift_Click;
            // 
            // btnAssignEvening
            // 
            btnAssignEvening.Location = new Point(307, 265);
            btnAssignEvening.Name = "btnAssignEvening";
            btnAssignEvening.Size = new Size(153, 40);
            btnAssignEvening.TabIndex = 4;
            btnAssignEvening.Text = "Assign Evening Shift";
            btnAssignEvening.UseVisualStyleBackColor = true;
            btnAssignEvening.Click += btnAssignEvening_Click;
            // 
            // btnAssignMorning
            // 
            btnAssignMorning.Location = new Point(307, 219);
            btnAssignMorning.Name = "btnAssignMorning";
            btnAssignMorning.Size = new Size(153, 40);
            btnAssignMorning.TabIndex = 3;
            btnAssignMorning.Text = "Assign Morning Shift";
            btnAssignMorning.UseVisualStyleBackColor = true;
            btnAssignMorning.Click += btnAssignMorning_Click;
            // 
            // lstBoxEveningShiftEmployees
            // 
            lstBoxEveningShiftEmployees.FormattingEnabled = true;
            lstBoxEveningShiftEmployees.ItemHeight = 17;
            lstBoxEveningShiftEmployees.Location = new Point(741, 29);
            lstBoxEveningShiftEmployees.Name = "lstBoxEveningShiftEmployees";
            lstBoxEveningShiftEmployees.Size = new Size(190, 395);
            lstBoxEveningShiftEmployees.TabIndex = 2;
            // 
            // lstBoxNoShiftEmployees
            // 
            lstBoxNoShiftEmployees.FormattingEnabled = true;
            lstBoxNoShiftEmployees.ItemHeight = 17;
            lstBoxNoShiftEmployees.Location = new Point(12, 29);
            lstBoxNoShiftEmployees.Name = "lstBoxNoShiftEmployees";
            lstBoxNoShiftEmployees.Size = new Size(224, 395);
            lstBoxNoShiftEmployees.TabIndex = 1;
            // 
            // lstBoxMorningShiftEmployees
            // 
            lstBoxMorningShiftEmployees.FormattingEnabled = true;
            lstBoxMorningShiftEmployees.ItemHeight = 17;
            lstBoxMorningShiftEmployees.Location = new Point(536, 29);
            lstBoxMorningShiftEmployees.Name = "lstBoxMorningShiftEmployees";
            lstBoxMorningShiftEmployees.Size = new Size(189, 395);
            lstBoxMorningShiftEmployees.TabIndex = 0;
            // 
            // MainControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControMain);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "MainControl";
            Size = new Size(980, 565);
            tabControMain.ResumeLayout(false);
            tabPageMain.ResumeLayout(false);
            tabPageMain.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tabPageEmployee.ResumeLayout(false);
            tabPageEmployee.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtGrVEmployees).EndInit();
            groupBoxManageDepartments.ResumeLayout(false);
            groupBoxManageDepartments.PerformLayout();
            groupBoxManageEmployees.ResumeLayout(false);
            groupBoxManageEmployees.PerformLayout();
            tabPageStock.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericQuantityShelfRequest).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericQuantityStore).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericQuantityWarehouse).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericSellPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericWholesalePrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericSKU).EndInit();
            tabPageShifts.ResumeLayout(false);
            tabPageShifts.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControMain;
        private TabPage tabPageMain;
        private ComboBox cbBoxDepartmentList;
        private Label label46;
        private Label label44;
        private Label label45;
        private GroupBox groupBox4;
        private Label label42;
        private Label label43;
        private Label label40;
        private Label label41;
        private Label label39;
        private Label label37;
        private Label label38;
        private Label label33;
        private Label label34;
        private Label label31;
        private Label label32;
        private Label label29;
        private Label label30;
        private PictureBox pictureBox1;
        private Label label27;
        private Label label26;
        private Label label25;
        private Label label20;
        private GroupBox groupBox3;
        private Label label48;
        private ListBox listBox2;
        private Label label47;
        private ListBox lstBoxFilteredEmployees;
        private Label label19;
        private Label label18;
        private ComboBox cbBoxRolesList;
        private TabPage tabPageEmployee;
        private DataGridView dtGrVEmployees;
        private GroupBox groupBoxManageDepartments;
        private Button btnEditDepartment;
        private TextBox txtBoxDepartmentName;
        private Button btnRemoveDepartment;
        private Label label5;
        private Button btnNewDepartment;
        private ListBox listBoxDepartments;
        private GroupBox groupBoxManageEmployees;
        private Button btnEditEmployee;
        private Button btnRemoveEmployee;
        private Button btnNewEmployee;
        private ComboBox cmbBoxEmployeeIsActive;
        private Label label4;
        private TextBox txtBoxEmployeeHourlyWage;
        private Label label3;
        private Button btnEmployeeAttachImage;
        private Label lblImage;
        private ComboBox cmbBoxEmployeeRole;
        private Label label1;
        private TextBox txtBoxEmployeePswd;
        private Label lblEmployeePswd;
        private TextBox txtBoxEmployeeEmail;
        private Label lblEmployeeEmail;
        private TextBox txtBoxEmployeeName;
        private Label lblEmployeeName;
        private TabPage tabPageStock;
        private GroupBox groupBox2;
        private Button btnEditRequest;
        private Button btnFulfillRequest;
        private Button btAddShelfRequest;
        private Label label21;
        private Label label23;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private Label label10;
        private Button btnEditProduct;
        private Button btnRemoveProduct;
        private Button btnNewProduct;
        private Label label11;
        private ComboBox cbCatergory;
        private Label label13;
        private Label label14;
        private Label label15;
        private TextBox tbNameProduct;
        private Label label16;
        private TabPage tabPageShifts;
        private DateTimePicker dtpShift;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Button btnAutoAssign;
        private Button btnUnassignMorningShift;
        private Button btnAssignEvening;
        private Button btnAssignMorning;
        private ListBox lstBoxEveningShiftEmployees;
        private ListBox lstBoxNoShiftEmployees;
        private ListBox lstBoxMorningShiftEmployees;
        private TextBox txtBoxEmployeeAddress;
        private Label label49;
        private Label label50;
        private NumericUpDown numericQuantityWarehouse;
        private NumericUpDown numericSellPrice;
        private NumericUpDown numericWholesalePrice;
        private NumericUpDown numericSKU;
        private NumericUpDown numericQuantityStore;
        private Label label2;
        private NumericUpDown numericQuantityShelfRequest;
        private DataGridView dataGridView2;
        private Button btnUnassignEveningShift;
    }
}
