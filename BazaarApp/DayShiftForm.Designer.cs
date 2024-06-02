namespace TheSandwichMakersHardwareStoreSolution
{
    partial class DayShiftForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btnSaveMorningShiftTime = new Button();
            label2 = new Label();
            dtpMorningShiftEndTime = new DateTimePicker();
            label1 = new Label();
            dtpMorningShiftStartTime = new DateTimePicker();
            tabPage2 = new TabPage();
            btnSaveEveningShiftTime = new Button();
            label3 = new Label();
            dtpEveningShiftEndTime = new DateTimePicker();
            label4 = new Label();
            dtpEveningShiftStartTime = new DateTimePicker();
            lstUnassignedEmployees = new ListBox();
            groupBox1 = new GroupBox();
            btnAutoAssignDay = new Button();
            btnAssignEveningShift = new Button();
            btnAssignMorningShift = new Button();
            lblDay = new Label();
            txtSearchUnassignedEmployees = new TextBox();
            label7 = new Label();
            lblDate = new Label();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            btnUnassignMorningShift = new Button();
            lstMorningShiftEmployees = new ListBox();
            txtSearchMorningShiftEmployees = new TextBox();
            label8 = new Label();
            groupBox4 = new GroupBox();
            btnUnassignEveningShift = new Button();
            lstEveningShiftEmployees = new ListBox();
            txtSearchEveningShiftEmployees = new TextBox();
            label9 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(6, 200);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(184, 139);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnSaveMorningShiftTime);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(dtpMorningShiftEndTime);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(dtpMorningShiftStartTime);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(176, 111);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Morning";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSaveMorningShiftTime
            // 
            btnSaveMorningShiftTime.Location = new Point(6, 82);
            btnSaveMorningShiftTime.Name = "btnSaveMorningShiftTime";
            btnSaveMorningShiftTime.Size = new Size(162, 23);
            btnSaveMorningShiftTime.TabIndex = 4;
            btnSaveMorningShiftTime.Text = "Save shift time";
            btnSaveMorningShiftTime.UseVisualStyleBackColor = true;
            btnSaveMorningShiftTime.Click += btnSaveMorningShiftTime_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 50);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 3;
            label2.Text = "End time:";
            // 
            // dtpMorningShiftEndTime
            // 
            dtpMorningShiftEndTime.Format = DateTimePickerFormat.Time;
            dtpMorningShiftEndTime.Location = new Point(73, 44);
            dtpMorningShiftEndTime.Name = "dtpMorningShiftEndTime";
            dtpMorningShiftEndTime.Size = new Size(95, 23);
            dtpMorningShiftEndTime.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 21);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 1;
            label1.Text = "Start time:";
            // 
            // dtpMorningShiftStartTime
            // 
            dtpMorningShiftStartTime.Format = DateTimePickerFormat.Time;
            dtpMorningShiftStartTime.Location = new Point(73, 15);
            dtpMorningShiftStartTime.Name = "dtpMorningShiftStartTime";
            dtpMorningShiftStartTime.Size = new Size(95, 23);
            dtpMorningShiftStartTime.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnSaveEveningShiftTime);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(dtpEveningShiftEndTime);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(dtpEveningShiftStartTime);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(176, 111);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Evening";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnSaveEveningShiftTime
            // 
            btnSaveEveningShiftTime.Location = new Point(6, 82);
            btnSaveEveningShiftTime.Name = "btnSaveEveningShiftTime";
            btnSaveEveningShiftTime.Size = new Size(162, 23);
            btnSaveEveningShiftTime.TabIndex = 9;
            btnSaveEveningShiftTime.Text = "Save shift time";
            btnSaveEveningShiftTime.UseVisualStyleBackColor = true;
            btnSaveEveningShiftTime.Click += btnSaveEveningShiftTime_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 50);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 8;
            label3.Text = "End time:";
            // 
            // dtpEveningShiftEndTime
            // 
            dtpEveningShiftEndTime.Format = DateTimePickerFormat.Time;
            dtpEveningShiftEndTime.Location = new Point(73, 44);
            dtpEveningShiftEndTime.Name = "dtpEveningShiftEndTime";
            dtpEveningShiftEndTime.Size = new Size(95, 23);
            dtpEveningShiftEndTime.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 21);
            label4.Name = "label4";
            label4.Size = new Size(61, 15);
            label4.TabIndex = 6;
            label4.Text = "Start time:";
            // 
            // dtpEveningShiftStartTime
            // 
            dtpEveningShiftStartTime.Format = DateTimePickerFormat.Time;
            dtpEveningShiftStartTime.Location = new Point(73, 15);
            dtpEveningShiftStartTime.Name = "dtpEveningShiftStartTime";
            dtpEveningShiftStartTime.Size = new Size(95, 23);
            dtpEveningShiftStartTime.TabIndex = 5;
            // 
            // lstUnassignedEmployees
            // 
            lstUnassignedEmployees.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lstUnassignedEmployees.FormattingEnabled = true;
            lstUnassignedEmployees.ItemHeight = 17;
            lstUnassignedEmployees.Location = new Point(10, 80);
            lstUnassignedEmployees.Name = "lstUnassignedEmployees";
            lstUnassignedEmployees.Size = new Size(169, 276);
            lstUnassignedEmployees.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnAutoAssignDay);
            groupBox1.Controls.Add(tabControl1);
            groupBox1.Location = new Point(12, 92);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(198, 347);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Shifts Settings";
            // 
            // btnAutoAssignDay
            // 
            btnAutoAssignDay.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnAutoAssignDay.Location = new Point(10, 38);
            btnAutoAssignDay.Name = "btnAutoAssignDay";
            btnAutoAssignDay.Size = new Size(169, 28);
            btnAutoAssignDay.TabIndex = 13;
            btnAutoAssignDay.Text = "Auto Assign Employees";
            btnAutoAssignDay.UseVisualStyleBackColor = true;
            btnAutoAssignDay.Click += btnAutoAssignDay_Click;
            // 
            // btnAssignEveningShift
            // 
            btnAssignEveningShift.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnAssignEveningShift.Location = new Point(10, 391);
            btnAssignEveningShift.Name = "btnAssignEveningShift";
            btnAssignEveningShift.Size = new Size(169, 28);
            btnAssignEveningShift.TabIndex = 2;
            btnAssignEveningShift.Text = "Assign to evening shift";
            btnAssignEveningShift.UseVisualStyleBackColor = true;
            btnAssignEveningShift.Click += btnAssignEveningShift_Click;
            // 
            // btnAssignMorningShift
            // 
            btnAssignMorningShift.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnAssignMorningShift.Location = new Point(10, 362);
            btnAssignMorningShift.Name = "btnAssignMorningShift";
            btnAssignMorningShift.Size = new Size(169, 28);
            btnAssignMorningShift.TabIndex = 1;
            btnAssignMorningShift.Text = "Assign to morning shift";
            btnAssignMorningShift.UseVisualStyleBackColor = true;
            btnAssignMorningShift.Click += btnAssignMorningShift_Click;
            // 
            // lblDay
            // 
            lblDay.AutoSize = true;
            lblDay.Font = new Font("Century Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblDay.Location = new Point(20, 19);
            lblDay.Name = "lblDay";
            lblDay.Size = new Size(48, 22);
            lblDay.TabIndex = 7;
            lblDay.Text = "Day";
            // 
            // txtSearchUnassignedEmployees
            // 
            txtSearchUnassignedEmployees.Location = new Point(10, 49);
            txtSearchUnassignedEmployees.Name = "txtSearchUnassignedEmployees";
            txtSearchUnassignedEmployees.Size = new Size(169, 25);
            txtSearchUnassignedEmployees.TabIndex = 9;
            txtSearchUnassignedEmployees.TextChanged += txtSearchUnassignedEmployees_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(10, 31);
            label7.Name = "label7";
            label7.Size = new Size(45, 15);
            label7.TabIndex = 12;
            label7.Text = "Search:";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Century Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblDate.Location = new Point(20, 52);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(114, 22);
            lblDate.TabIndex = 8;
            lblDate.Text = "09/09/9999";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnAssignEveningShift);
            groupBox2.Controls.Add(txtSearchUnassignedEmployees);
            groupBox2.Controls.Add(btnAssignMorningShift);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(lstUnassignedEmployees);
            groupBox2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox2.Location = new Point(216, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(192, 428);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Unassigned employees";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btnUnassignMorningShift);
            groupBox3.Controls.Add(lstMorningShiftEmployees);
            groupBox3.Controls.Add(txtSearchMorningShiftEmployees);
            groupBox3.Controls.Add(label8);
            groupBox3.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox3.Location = new Point(414, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(192, 428);
            groupBox3.TabIndex = 16;
            groupBox3.TabStop = false;
            groupBox3.Text = "Morning Shift";
            // 
            // btnUnassignMorningShift
            // 
            btnUnassignMorningShift.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnUnassignMorningShift.Location = new Point(10, 389);
            btnUnassignMorningShift.Name = "btnUnassignMorningShift";
            btnUnassignMorningShift.Size = new Size(169, 28);
            btnUnassignMorningShift.TabIndex = 13;
            btnUnassignMorningShift.Text = "Unassign employee";
            btnUnassignMorningShift.UseVisualStyleBackColor = true;
            btnUnassignMorningShift.Click += btnUnassignMorningShift_Click;
            // 
            // lstMorningShiftEmployees
            // 
            lstMorningShiftEmployees.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lstMorningShiftEmployees.FormattingEnabled = true;
            lstMorningShiftEmployees.ItemHeight = 17;
            lstMorningShiftEmployees.Location = new Point(10, 80);
            lstMorningShiftEmployees.Name = "lstMorningShiftEmployees";
            lstMorningShiftEmployees.Size = new Size(169, 293);
            lstMorningShiftEmployees.TabIndex = 17;
            // 
            // txtSearchMorningShiftEmployees
            // 
            txtSearchMorningShiftEmployees.Location = new Point(10, 49);
            txtSearchMorningShiftEmployees.Name = "txtSearchMorningShiftEmployees";
            txtSearchMorningShiftEmployees.Size = new Size(169, 25);
            txtSearchMorningShiftEmployees.TabIndex = 9;
            txtSearchMorningShiftEmployees.TextChanged += txtSearchMorningShiftEmployees_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(10, 31);
            label8.Name = "label8";
            label8.Size = new Size(45, 15);
            label8.TabIndex = 12;
            label8.Text = "Search:";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btnUnassignEveningShift);
            groupBox4.Controls.Add(lstEveningShiftEmployees);
            groupBox4.Controls.Add(txtSearchEveningShiftEmployees);
            groupBox4.Controls.Add(label9);
            groupBox4.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox4.Location = new Point(616, 12);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(192, 428);
            groupBox4.TabIndex = 18;
            groupBox4.TabStop = false;
            groupBox4.Text = "Evening Shift";
            // 
            // btnUnassignEveningShift
            // 
            btnUnassignEveningShift.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnUnassignEveningShift.Location = new Point(10, 389);
            btnUnassignEveningShift.Name = "btnUnassignEveningShift";
            btnUnassignEveningShift.Size = new Size(169, 28);
            btnUnassignEveningShift.TabIndex = 18;
            btnUnassignEveningShift.Text = "Unassign employee";
            btnUnassignEveningShift.UseVisualStyleBackColor = true;
            btnUnassignEveningShift.Click += btnUnassignEveningShift_Click;
            // 
            // lstEveningShiftEmployees
            // 
            lstEveningShiftEmployees.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lstEveningShiftEmployees.FormattingEnabled = true;
            lstEveningShiftEmployees.ItemHeight = 17;
            lstEveningShiftEmployees.Location = new Point(10, 80);
            lstEveningShiftEmployees.Name = "lstEveningShiftEmployees";
            lstEveningShiftEmployees.Size = new Size(169, 293);
            lstEveningShiftEmployees.TabIndex = 17;
            // 
            // txtSearchEveningShiftEmployees
            // 
            txtSearchEveningShiftEmployees.Location = new Point(10, 49);
            txtSearchEveningShiftEmployees.Name = "txtSearchEveningShiftEmployees";
            txtSearchEveningShiftEmployees.Size = new Size(169, 25);
            txtSearchEveningShiftEmployees.TabIndex = 9;
            txtSearchEveningShiftEmployees.TextChanged += txtSearchEveningShiftEmployees_TextChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(10, 31);
            label9.Name = "label9";
            label9.Size = new Size(45, 15);
            label9.TabIndex = 12;
            label9.Text = "Search:";
            // 
            // DayShiftForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 450);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(lblDate);
            Controls.Add(lblDay);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Name = "DayShiftForm";
            Text = "DayShiftForm";
            Load += DayShiftForm_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Label label2;
        private DateTimePicker dtpMorningShiftEndTime;
        private Label label1;
        private DateTimePicker dtpMorningShiftStartTime;
        private TabPage tabPage2;
        private Button btnSaveMorningShiftTime;
        private Button btnSaveEveningShiftTime;
        private Label label3;
        private DateTimePicker dtpEveningShiftEndTime;
        private Label label4;
        private DateTimePicker dtpEveningShiftStartTime;
        private ListBox lstUnassignedEmployees;
        private GroupBox groupBox1;
        private Button btnAssignMorningShift;
        private Label lblDay;
        private TextBox txtSearchUnassignedEmployees;
        private Label label7;
        private Label lblDate;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ListBox lstMorningShiftEmployees;
        private TextBox txtSearchMorningShiftEmployees;
        private Label label8;
        private GroupBox groupBox4;
        private ListBox lstEveningShiftEmployees;
        private TextBox txtSearchEveningShiftEmployees;
        private Label label9;
        private Button btnAssignEveningShift;
        private Button btnAutoAssignDay;
        private Button btnUnassignMorningShift;
        private Button btnUnassignEveningShift;
    }
}