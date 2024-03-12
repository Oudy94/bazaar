namespace TheSandwichMakersHardwareStoreSolution
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControMain = new TabControl();
            tabPageMain = new TabPage();
            tabPageEmployee = new TabPage();
            tabPageStock = new TabPage();
            tabPageShifts = new TabPage();
            groupBoxManageEmployees = new GroupBox();
            listBoxDepartments = new ListBox();
            groupBoxManageDepartments = new GroupBox();
            lblEmployeeName = new Label();
            txtBoxEmployeeName = new TextBox();
            txtBoxEmployeeEmail = new TextBox();
            lblEmployeeEmail = new Label();
            txtBoxEmployeePswd = new TextBox();
            lblEmployeePswd = new Label();
            label1 = new Label();
            cmbBoxEmployeeRole = new ComboBox();
            label2 = new Label();
            btnEmployeeAttachImage = new Button();
            txtBoxEmployeeHourlyWage = new TextBox();
            label3 = new Label();
            cmbBoxEmployeeIsActive = new ComboBox();
            label4 = new Label();
            btnNewEmployee = new Button();
            btnRemoveEmployee = new Button();
            btnEditEmployee = new Button();
            btnEditDepartment = new Button();
            btnRemoveDepartment = new Button();
            btnNewDepartment = new Button();
            txtBoxDepartmentName = new TextBox();
            label5 = new Label();
            dtGrVEmployees = new DataGridView();
            lstBoxMorningShiftEmployees = new ListBox();
            lstBoxNoShiftEmployees = new ListBox();
            lstBoxEveningShiftEmployees = new ListBox();
            btnAssignMorning = new Button();
            btnAssignEvening = new Button();
            btnUnassign = new Button();
            btnAutoAssign = new Button();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            dateTimePicker1 = new DateTimePicker();
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            btnEditProduct = new Button();
            btnRemoveProduct = new Button();
            btnNewProduct = new Button();
            textBox1 = new TextBox();
            label11 = new Label();
            comboBox2 = new ComboBox();
            label13 = new Label();
            textBox2 = new TextBox();
            label14 = new Label();
            textBox3 = new TextBox();
            label15 = new Label();
            textBox4 = new TextBox();
            label16 = new Label();
            textBox5 = new TextBox();
            label10 = new Label();
            textBox6 = new TextBox();
            label12 = new Label();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            textBox7 = new TextBox();
            label17 = new Label();
            groupBox2 = new GroupBox();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            btnEditRequest = new Button();
            btnFulfillRequest = new Button();
            button3 = new Button();
            label21 = new Label();
            comboBox1 = new ComboBox();
            label22 = new Label();
            textBox12 = new TextBox();
            label23 = new Label();
            listBox1 = new ListBox();
            button1 = new Button();
            tabControMain.SuspendLayout();
            tabPageEmployee.SuspendLayout();
            tabPageStock.SuspendLayout();
            tabPageShifts.SuspendLayout();
            groupBoxManageEmployees.SuspendLayout();
            groupBoxManageDepartments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtGrVEmployees).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControMain
            // 
            tabControMain.Controls.Add(tabPageMain);
            tabControMain.Controls.Add(tabPageEmployee);
            tabControMain.Controls.Add(tabPageStock);
            tabControMain.Controls.Add(tabPageShifts);
            tabControMain.Location = new Point(12, 12);
            tabControMain.Name = "tabControMain";
            tabControMain.SelectedIndex = 0;
            tabControMain.Size = new Size(960, 537);
            tabControMain.TabIndex = 0;
            // 
            // tabPageMain
            // 
            tabPageMain.Location = new Point(4, 24);
            tabPageMain.Name = "tabPageMain";
            tabPageMain.Size = new Size(952, 509);
            tabPageMain.TabIndex = 0;
            tabPageMain.Text = "Main";
            tabPageMain.UseVisualStyleBackColor = true;
            // 
            // tabPageEmployee
            // 
            tabPageEmployee.Controls.Add(dtGrVEmployees);
            tabPageEmployee.Controls.Add(groupBoxManageDepartments);
            tabPageEmployee.Controls.Add(listBoxDepartments);
            tabPageEmployee.Controls.Add(groupBoxManageEmployees);
            tabPageEmployee.Location = new Point(4, 24);
            tabPageEmployee.Name = "tabPageEmployee";
            tabPageEmployee.Size = new Size(952, 509);
            tabPageEmployee.TabIndex = 1;
            tabPageEmployee.Text = "Employee & Departments";
            tabPageEmployee.UseVisualStyleBackColor = true;
            // 
            // tabPageStock
            // 
            tabPageStock.Controls.Add(groupBox2);
            tabPageStock.Controls.Add(dataGridView1);
            tabPageStock.Controls.Add(groupBox1);
            tabPageStock.Location = new Point(4, 24);
            tabPageStock.Name = "tabPageStock";
            tabPageStock.Size = new Size(952, 509);
            tabPageStock.TabIndex = 2;
            tabPageStock.Text = "Stock";
            tabPageStock.UseVisualStyleBackColor = true;
            // 
            // tabPageShifts
            // 
            tabPageShifts.Controls.Add(dateTimePicker1);
            tabPageShifts.Controls.Add(label9);
            tabPageShifts.Controls.Add(label8);
            tabPageShifts.Controls.Add(label7);
            tabPageShifts.Controls.Add(label6);
            tabPageShifts.Controls.Add(btnAutoAssign);
            tabPageShifts.Controls.Add(btnUnassign);
            tabPageShifts.Controls.Add(btnAssignEvening);
            tabPageShifts.Controls.Add(btnAssignMorning);
            tabPageShifts.Controls.Add(lstBoxEveningShiftEmployees);
            tabPageShifts.Controls.Add(lstBoxNoShiftEmployees);
            tabPageShifts.Controls.Add(lstBoxMorningShiftEmployees);
            tabPageShifts.Location = new Point(4, 24);
            tabPageShifts.Name = "tabPageShifts";
            tabPageShifts.Size = new Size(952, 509);
            tabPageShifts.TabIndex = 3;
            tabPageShifts.Text = "Shifts";
            tabPageShifts.UseVisualStyleBackColor = true;
            // 
            // groupBoxManageEmployees
            // 
            groupBoxManageEmployees.Controls.Add(btnEditEmployee);
            groupBoxManageEmployees.Controls.Add(btnRemoveEmployee);
            groupBoxManageEmployees.Controls.Add(btnNewEmployee);
            groupBoxManageEmployees.Controls.Add(cmbBoxEmployeeIsActive);
            groupBoxManageEmployees.Controls.Add(label4);
            groupBoxManageEmployees.Controls.Add(txtBoxEmployeeHourlyWage);
            groupBoxManageEmployees.Controls.Add(label3);
            groupBoxManageEmployees.Controls.Add(btnEmployeeAttachImage);
            groupBoxManageEmployees.Controls.Add(label2);
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
            // listBoxDepartments
            // 
            listBoxDepartments.FormattingEnabled = true;
            listBoxDepartments.ItemHeight = 15;
            listBoxDepartments.Location = new Point(708, 249);
            listBoxDepartments.Name = "listBoxDepartments";
            listBoxDepartments.Size = new Size(224, 244);
            listBoxDepartments.TabIndex = 1;
            // 
            // groupBoxManageDepartments
            // 
            groupBoxManageDepartments.Controls.Add(button1);
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
            // lblEmployeeName
            // 
            lblEmployeeName.AutoSize = true;
            lblEmployeeName.Location = new Point(18, 28);
            lblEmployeeName.Name = "lblEmployeeName";
            lblEmployeeName.Size = new Size(39, 15);
            lblEmployeeName.TabIndex = 0;
            lblEmployeeName.Text = "Name";
            // 
            // txtBoxEmployeeName
            // 
            txtBoxEmployeeName.Location = new Point(18, 46);
            txtBoxEmployeeName.Name = "txtBoxEmployeeName";
            txtBoxEmployeeName.Size = new Size(139, 23);
            txtBoxEmployeeName.TabIndex = 1;
            // 
            // txtBoxEmployeeEmail
            // 
            txtBoxEmployeeEmail.Location = new Point(18, 90);
            txtBoxEmployeeEmail.Name = "txtBoxEmployeeEmail";
            txtBoxEmployeeEmail.Size = new Size(139, 23);
            txtBoxEmployeeEmail.TabIndex = 3;
            // 
            // lblEmployeeEmail
            // 
            lblEmployeeEmail.AutoSize = true;
            lblEmployeeEmail.Location = new Point(18, 72);
            lblEmployeeEmail.Name = "lblEmployeeEmail";
            lblEmployeeEmail.Size = new Size(36, 15);
            lblEmployeeEmail.TabIndex = 2;
            lblEmployeeEmail.Text = "Email";
            // 
            // txtBoxEmployeePswd
            // 
            txtBoxEmployeePswd.Location = new Point(18, 134);
            txtBoxEmployeePswd.Name = "txtBoxEmployeePswd";
            txtBoxEmployeePswd.Size = new Size(139, 23);
            txtBoxEmployeePswd.TabIndex = 5;
            // 
            // lblEmployeePswd
            // 
            lblEmployeePswd.AutoSize = true;
            lblEmployeePswd.Location = new Point(18, 116);
            lblEmployeePswd.Name = "lblEmployeePswd";
            lblEmployeePswd.Size = new Size(57, 15);
            lblEmployeePswd.TabIndex = 4;
            lblEmployeePswd.Text = "Password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 160);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 6;
            label1.Text = "Role";
            // 
            // cmbBoxEmployeeRole
            // 
            cmbBoxEmployeeRole.FormattingEnabled = true;
            cmbBoxEmployeeRole.Location = new Point(18, 178);
            cmbBoxEmployeeRole.Name = "cmbBoxEmployeeRole";
            cmbBoxEmployeeRole.Size = new Size(139, 23);
            cmbBoxEmployeeRole.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 204);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 8;
            label2.Text = "Image";
            // 
            // btnEmployeeAttachImage
            // 
            btnEmployeeAttachImage.Location = new Point(18, 222);
            btnEmployeeAttachImage.Name = "btnEmployeeAttachImage";
            btnEmployeeAttachImage.Size = new Size(139, 23);
            btnEmployeeAttachImage.TabIndex = 9;
            btnEmployeeAttachImage.Text = "Attach Image";
            btnEmployeeAttachImage.UseVisualStyleBackColor = true;
            // 
            // txtBoxEmployeeHourlyWage
            // 
            txtBoxEmployeeHourlyWage.Location = new Point(18, 265);
            txtBoxEmployeeHourlyWage.Name = "txtBoxEmployeeHourlyWage";
            txtBoxEmployeeHourlyWage.Size = new Size(139, 23);
            txtBoxEmployeeHourlyWage.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 247);
            label3.Name = "label3";
            label3.Size = new Size(76, 15);
            label3.TabIndex = 10;
            label3.Text = "Hourly Wage";
            // 
            // cmbBoxEmployeeIsActive
            // 
            cmbBoxEmployeeIsActive.FormattingEnabled = true;
            cmbBoxEmployeeIsActive.Location = new Point(18, 305);
            cmbBoxEmployeeIsActive.Name = "cmbBoxEmployeeIsActive";
            cmbBoxEmployeeIsActive.Size = new Size(139, 23);
            cmbBoxEmployeeIsActive.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 291);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 12;
            label4.Text = "Is Active";
            // 
            // btnNewEmployee
            // 
            btnNewEmployee.Location = new Point(18, 375);
            btnNewEmployee.Name = "btnNewEmployee";
            btnNewEmployee.Size = new Size(139, 23);
            btnNewEmployee.TabIndex = 14;
            btnNewEmployee.Text = "New Employee";
            btnNewEmployee.UseVisualStyleBackColor = true;
            // 
            // btnRemoveEmployee
            // 
            btnRemoveEmployee.Location = new Point(18, 433);
            btnRemoveEmployee.Name = "btnRemoveEmployee";
            btnRemoveEmployee.Size = new Size(139, 23);
            btnRemoveEmployee.TabIndex = 15;
            btnRemoveEmployee.Text = "Remove Employee";
            btnRemoveEmployee.UseVisualStyleBackColor = true;
            // 
            // btnEditEmployee
            // 
            btnEditEmployee.Location = new Point(18, 404);
            btnEditEmployee.Name = "btnEditEmployee";
            btnEditEmployee.Size = new Size(139, 23);
            btnEditEmployee.TabIndex = 16;
            btnEditEmployee.Text = "Edit Employee";
            btnEditEmployee.UseVisualStyleBackColor = true;
            // 
            // btnEditDepartment
            // 
            btnEditDepartment.Location = new Point(30, 119);
            btnEditDepartment.Name = "btnEditDepartment";
            btnEditDepartment.Size = new Size(139, 23);
            btnEditDepartment.TabIndex = 21;
            btnEditDepartment.Text = "Edit Department";
            btnEditDepartment.UseVisualStyleBackColor = true;
            // 
            // btnRemoveDepartment
            // 
            btnRemoveDepartment.Location = new Point(30, 148);
            btnRemoveDepartment.Name = "btnRemoveDepartment";
            btnRemoveDepartment.Size = new Size(139, 23);
            btnRemoveDepartment.TabIndex = 20;
            btnRemoveDepartment.Text = "Remove Department";
            btnRemoveDepartment.UseVisualStyleBackColor = true;
            // 
            // btnNewDepartment
            // 
            btnNewDepartment.Location = new Point(30, 90);
            btnNewDepartment.Name = "btnNewDepartment";
            btnNewDepartment.Size = new Size(139, 23);
            btnNewDepartment.TabIndex = 19;
            btnNewDepartment.Text = "New Department";
            btnNewDepartment.UseVisualStyleBackColor = true;
            // 
            // txtBoxDepartmentName
            // 
            txtBoxDepartmentName.Location = new Point(30, 46);
            txtBoxDepartmentName.Name = "txtBoxDepartmentName";
            txtBoxDepartmentName.Size = new Size(139, 23);
            txtBoxDepartmentName.TabIndex = 18;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(30, 28);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 17;
            label5.Text = "Name";
            // 
            // dtGrVEmployees
            // 
            dtGrVEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtGrVEmployees.Location = new Point(182, 25);
            dtGrVEmployees.Name = "dtGrVEmployees";
            dtGrVEmployees.RowTemplate.Height = 25;
            dtGrVEmployees.Size = new Size(520, 468);
            dtGrVEmployees.TabIndex = 2;
            // 
            // lstBoxMorningShiftEmployees
            // 
            lstBoxMorningShiftEmployees.FormattingEnabled = true;
            lstBoxMorningShiftEmployees.ItemHeight = 15;
            lstBoxMorningShiftEmployees.Location = new Point(536, 29);
            lstBoxMorningShiftEmployees.Name = "lstBoxMorningShiftEmployees";
            lstBoxMorningShiftEmployees.Size = new Size(189, 409);
            lstBoxMorningShiftEmployees.TabIndex = 0;
            // 
            // lstBoxNoShiftEmployees
            // 
            lstBoxNoShiftEmployees.FormattingEnabled = true;
            lstBoxNoShiftEmployees.ItemHeight = 15;
            lstBoxNoShiftEmployees.Location = new Point(12, 29);
            lstBoxNoShiftEmployees.Name = "lstBoxNoShiftEmployees";
            lstBoxNoShiftEmployees.Size = new Size(224, 409);
            lstBoxNoShiftEmployees.TabIndex = 1;
            // 
            // lstBoxEveningShiftEmployees
            // 
            lstBoxEveningShiftEmployees.FormattingEnabled = true;
            lstBoxEveningShiftEmployees.ItemHeight = 15;
            lstBoxEveningShiftEmployees.Location = new Point(741, 29);
            lstBoxEveningShiftEmployees.Name = "lstBoxEveningShiftEmployees";
            lstBoxEveningShiftEmployees.Size = new Size(190, 409);
            lstBoxEveningShiftEmployees.TabIndex = 2;
            // 
            // btnAssignMorning
            // 
            btnAssignMorning.Location = new Point(307, 258);
            btnAssignMorning.Name = "btnAssignMorning";
            btnAssignMorning.Size = new Size(153, 40);
            btnAssignMorning.TabIndex = 3;
            btnAssignMorning.Text = "Assign Morning Shift";
            btnAssignMorning.UseVisualStyleBackColor = true;
            // 
            // btnAssignEvening
            // 
            btnAssignEvening.Location = new Point(307, 304);
            btnAssignEvening.Name = "btnAssignEvening";
            btnAssignEvening.Size = new Size(153, 40);
            btnAssignEvening.TabIndex = 4;
            btnAssignEvening.Text = "Assign Evening Shift";
            btnAssignEvening.UseVisualStyleBackColor = true;
            // 
            // btnUnassign
            // 
            btnUnassign.Location = new Point(307, 392);
            btnUnassign.Name = "btnUnassign";
            btnUnassign.Size = new Size(153, 40);
            btnUnassign.TabIndex = 5;
            btnUnassign.Text = "Unassign";
            btnUnassign.UseVisualStyleBackColor = true;
            // 
            // btnAutoAssign
            // 
            btnAutoAssign.Location = new Point(307, 173);
            btnAutoAssign.Name = "btnAutoAssign";
            btnAutoAssign.Size = new Size(153, 40);
            btnAutoAssign.TabIndex = 6;
            btnAutoAssign.Text = "Auto Assign";
            btnAutoAssign.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 11);
            label6.Name = "label6";
            label6.Size = new Size(128, 15);
            label6.TabIndex = 7;
            label6.Text = "Unassigned Employees";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(536, 11);
            label7.Name = "label7";
            label7.Size = new Size(164, 15);
            label7.TabIndex = 8;
            label7.Text = "Morning Assigned Employees";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(741, 11);
            label8.Name = "label8";
            label8.Size = new Size(160, 15);
            label8.TabIndex = 9;
            label8.Text = "Evening Assigned Employees";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(286, 46);
            label9.Name = "label9";
            label9.Size = new Size(31, 15);
            label9.TabIndex = 10;
            label9.Text = "Date";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(286, 64);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 11;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(215, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(520, 494);
            dataGridView1.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox7);
            groupBox1.Controls.Add(label17);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(textBox6);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(textBox5);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(btnEditProduct);
            groupBox1.Controls.Add(btnRemoveProduct);
            groupBox1.Controls.Add(btnNewProduct);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(textBox4);
            groupBox1.Controls.Add(label16);
            groupBox1.Location = new Point(14, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(186, 494);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Manage Products";
            // 
            // btnEditProduct
            // 
            btnEditProduct.Location = new Point(18, 436);
            btnEditProduct.Name = "btnEditProduct";
            btnEditProduct.Size = new Size(139, 23);
            btnEditProduct.TabIndex = 16;
            btnEditProduct.Text = "Edit Product";
            btnEditProduct.UseVisualStyleBackColor = true;
            // 
            // btnRemoveProduct
            // 
            btnRemoveProduct.Location = new Point(18, 465);
            btnRemoveProduct.Name = "btnRemoveProduct";
            btnRemoveProduct.Size = new Size(139, 23);
            btnRemoveProduct.TabIndex = 15;
            btnRemoveProduct.Text = "Remove Product";
            btnRemoveProduct.UseVisualStyleBackColor = true;
            // 
            // btnNewProduct
            // 
            btnNewProduct.Location = new Point(18, 407);
            btnNewProduct.Name = "btnNewProduct";
            btnNewProduct.Size = new Size(139, 23);
            btnNewProduct.TabIndex = 14;
            btnNewProduct.Text = "New Product";
            btnNewProduct.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(18, 263);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(139, 23);
            textBox1.TabIndex = 11;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(18, 245);
            label11.Name = "label11";
            label11.Size = new Size(90, 15);
            label11.TabIndex = 10;
            label11.Text = "Wholesale Price";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(18, 219);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(139, 23);
            comboBox2.TabIndex = 7;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(18, 201);
            label13.Name = "label13";
            label13.Size = new Size(55, 15);
            label13.TabIndex = 6;
            label13.Text = "Category";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(18, 134);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(139, 23);
            textBox2.TabIndex = 5;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(18, 116);
            label14.Name = "label14";
            label14.Size = new Size(53, 15);
            label14.TabIndex = 4;
            label14.Text = "Quantity";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(18, 90);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(139, 23);
            textBox3.TabIndex = 3;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(18, 72);
            label15.Name = "label15";
            label15.Size = new Size(28, 15);
            label15.TabIndex = 2;
            label15.Text = "SKU";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(18, 46);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(139, 23);
            textBox4.TabIndex = 1;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(18, 28);
            label16.Name = "label16";
            label16.Size = new Size(39, 15);
            label16.TabIndex = 0;
            label16.Text = "Name";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(18, 307);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(139, 23);
            textBox5.TabIndex = 18;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(18, 289);
            label10.Name = "label10";
            label10.Size = new Size(54, 15);
            label10.TabIndex = 17;
            label10.Text = "Sell Price";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(18, 354);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(139, 23);
            textBox6.TabIndex = 20;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(18, 336);
            label12.Name = "label12";
            label12.Size = new Size(50, 15);
            label12.TabIndex = 19;
            label12.Text = "Supplier";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(17, 384);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(84, 19);
            radioButton1.TabIndex = 21;
            radioButton1.TabStop = true;
            radioButton1.Text = "Warehouse";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(107, 384);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(52, 19);
            radioButton2.TabIndex = 22;
            radioButton2.TabStop = true;
            radioButton2.Text = "Store";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(18, 178);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(139, 23);
            textBox7.TabIndex = 24;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(18, 160);
            label17.Name = "label17";
            label17.Size = new Size(53, 15);
            label17.TabIndex = 23;
            label17.Text = "Location";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(listBox1);
            groupBox2.Controls.Add(radioButton3);
            groupBox2.Controls.Add(radioButton4);
            groupBox2.Controls.Add(btnEditRequest);
            groupBox2.Controls.Add(btnFulfillRequest);
            groupBox2.Controls.Add(button3);
            groupBox2.Controls.Add(label21);
            groupBox2.Controls.Add(comboBox1);
            groupBox2.Controls.Add(label22);
            groupBox2.Controls.Add(textBox12);
            groupBox2.Controls.Add(label23);
            groupBox2.Location = new Point(750, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(186, 494);
            groupBox2.TabIndex = 25;
            groupBox2.TabStop = false;
            groupBox2.Text = "Manage Shelf Requests";
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(107, 123);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(52, 19);
            radioButton3.TabIndex = 22;
            radioButton3.TabStop = true;
            radioButton3.Text = "Store";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(17, 123);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(84, 19);
            radioButton4.TabIndex = 21;
            radioButton4.TabStop = true;
            radioButton4.Text = "Warehouse";
            radioButton4.UseVisualStyleBackColor = true;
            // 
            // btnEditRequest
            // 
            btnEditRequest.Location = new Point(20, 177);
            btnEditRequest.Name = "btnEditRequest";
            btnEditRequest.Size = new Size(139, 23);
            btnEditRequest.TabIndex = 16;
            btnEditRequest.Text = "Edit Request";
            btnEditRequest.UseVisualStyleBackColor = true;
            // 
            // btnFulfillRequest
            // 
            btnFulfillRequest.Location = new Point(20, 206);
            btnFulfillRequest.Name = "btnFulfillRequest";
            btnFulfillRequest.Size = new Size(139, 23);
            btnFulfillRequest.TabIndex = 15;
            btnFulfillRequest.Text = "Fulfill Request";
            btnFulfillRequest.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(20, 148);
            button3.Name = "button3";
            button3.Size = new Size(139, 23);
            button3.TabIndex = 14;
            button3.Text = "New Request";
            button3.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(17, 245);
            label21.Name = "label21";
            label21.Size = new Size(83, 15);
            label21.TabIndex = 10;
            label21.Text = "Shelf Requests";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(18, 46);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(139, 23);
            comboBox1.TabIndex = 7;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(18, 28);
            label22.Name = "label22";
            label22.Size = new Size(49, 15);
            label22.TabIndex = 6;
            label22.Text = "Product";
            // 
            // textBox12
            // 
            textBox12.Location = new Point(18, 90);
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(139, 23);
            textBox12.TabIndex = 5;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(17, 72);
            label23.Name = "label23";
            label23.Size = new Size(53, 15);
            label23.TabIndex = 4;
            label23.Text = "Quantity";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(17, 274);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(142, 199);
            listBox1.TabIndex = 23;
            // 
            // button1
            // 
            button1.Location = new Point(0, 190);
            button1.Name = "button1";
            button1.Size = new Size(218, 23);
            button1.TabIndex = 22;
            button1.Text = "Assign Employee To Department";
            button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(tabControMain);
            Name = "Form1";
            Text = "Form1";
            tabControMain.ResumeLayout(false);
            tabPageEmployee.ResumeLayout(false);
            tabPageStock.ResumeLayout(false);
            tabPageShifts.ResumeLayout(false);
            tabPageShifts.PerformLayout();
            groupBoxManageEmployees.ResumeLayout(false);
            groupBoxManageEmployees.PerformLayout();
            groupBoxManageDepartments.ResumeLayout(false);
            groupBoxManageDepartments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtGrVEmployees).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControMain;
        private TabPage tabPageMain;
        private TabPage tabPageEmployee;
        private TabPage tabPageStock;
        private TabPage tabPageShifts;
        private GroupBox groupBoxManageDepartments;
        private ListBox listBoxDepartments;
        private GroupBox groupBoxManageEmployees;
        private Label lblEmployeeName;
        private TextBox txtBoxEmployeePswd;
        private Label lblEmployeePswd;
        private TextBox txtBoxEmployeeEmail;
        private Label lblEmployeeEmail;
        private TextBox txtBoxEmployeeName;
        private ComboBox cmbBoxEmployeeRole;
        private Label label1;
        private TextBox txtBoxEmployeeHourlyWage;
        private Label label3;
        private Button btnEmployeeAttachImage;
        private Label label2;
        private ComboBox cmbBoxEmployeeIsActive;
        private Label label4;
        private Button btnEditEmployee;
        private Button btnRemoveEmployee;
        private Button btnNewEmployee;
        private Button btnEditDepartment;
        private TextBox txtBoxDepartmentName;
        private Button btnRemoveDepartment;
        private Label label5;
        private Button btnNewDepartment;
        private DataGridView dtGrVEmployees;
        private ListBox lstBoxEveningShiftEmployees;
        private ListBox lstBoxNoShiftEmployees;
        private ListBox lstBoxMorningShiftEmployees;
        private Button btnAutoAssign;
        private Button btnUnassign;
        private Button btnAssignEvening;
        private Button btnAssignMorning;
        private Label label8;
        private Label label7;
        private Label label6;
        private DateTimePicker dateTimePicker1;
        private Label label9;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private Button btnEditProduct;
        private Button btnRemoveProduct;
        private Button btnNewProduct;
        private TextBox textBox1;
        private Label label11;
        private ComboBox comboBox2;
        private Label label13;
        private TextBox textBox2;
        private Label label14;
        private TextBox textBox3;
        private Label label15;
        private TextBox textBox4;
        private Label label16;
        private RadioButton radioButton1;
        private TextBox textBox6;
        private Label label12;
        private TextBox textBox5;
        private Label label10;
        private RadioButton radioButton2;
        private TextBox textBox7;
        private Label label17;
        private GroupBox groupBox2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private Button btnEditRequest;
        private Button btnFulfillRequest;
        private Button button3;
        private Label label21;
        private ComboBox comboBox1;
        private Label label22;
        private TextBox textBox12;
        private Label label23;
        private ListBox listBox1;
        private Button button1;
    }
}
