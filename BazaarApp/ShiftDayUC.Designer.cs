namespace TheSandwichMakersHardwareStoreSolution
{
    partial class ShiftDayUC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShiftDayUC));
            pnlShiftDay = new Panel();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            chbEveningShift = new CheckBox();
            btnDayShifts = new Button();
            lblDay = new Label();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            chbMorningShift = new CheckBox();
            pnlShiftDay.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlShiftDay
            // 
            pnlShiftDay.BackColor = Color.White;
            pnlShiftDay.Controls.Add(panel2);
            pnlShiftDay.Controls.Add(btnDayShifts);
            pnlShiftDay.Controls.Add(lblDay);
            pnlShiftDay.Controls.Add(panel1);
            pnlShiftDay.Dock = DockStyle.Fill;
            pnlShiftDay.Location = new Point(1, 1);
            pnlShiftDay.Name = "pnlShiftDay";
            pnlShiftDay.Size = new Size(138, 100);
            pnlShiftDay.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(224, 224, 224);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(chbEveningShift);
            panel2.Location = new Point(2, 68);
            panel2.Name = "panel2";
            panel2.Size = new Size(133, 29);
            panel2.TabIndex = 5;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(101, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(30, 22);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // chbEveningShift
            // 
            chbEveningShift.AutoSize = true;
            chbEveningShift.Enabled = false;
            chbEveningShift.Location = new Point(2, 7);
            chbEveningShift.Name = "chbEveningShift";
            chbEveningShift.Size = new Size(104, 19);
            chbEveningShift.TabIndex = 3;
            chbEveningShift.Text = "000 employees";
            chbEveningShift.UseVisualStyleBackColor = true;
            chbEveningShift.CheckedChanged += chbEveningShift_CheckedChanged;
            // 
            // btnDayShifts
            // 
            btnDayShifts.Image = Properties.Resources.settings1;
            btnDayShifts.Location = new Point(4, 3);
            btnDayShifts.Name = "btnDayShifts";
            btnDayShifts.Size = new Size(28, 25);
            btnDayShifts.TabIndex = 2;
            btnDayShifts.UseVisualStyleBackColor = true;
            btnDayShifts.Click += btnDayShifts_Click;
            // 
            // lblDay
            // 
            lblDay.AutoSize = true;
            lblDay.Font = new Font("Century Gothic", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblDay.Location = new Point(107, 3);
            lblDay.Margin = new Padding(3);
            lblDay.Name = "lblDay";
            lblDay.Size = new Size(32, 22);
            lblDay.TabIndex = 0;
            lblDay.Text = "00";
            lblDay.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(224, 224, 224);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(chbMorningShift);
            panel1.Location = new Point(2, 34);
            panel1.Name = "panel1";
            panel1.Size = new Size(133, 29);
            panel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.sun;
            pictureBox1.Location = new Point(101, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(30, 22);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // chbMorningShift
            // 
            chbMorningShift.AutoSize = true;
            chbMorningShift.Enabled = false;
            chbMorningShift.Location = new Point(2, 7);
            chbMorningShift.Name = "chbMorningShift";
            chbMorningShift.Size = new Size(104, 19);
            chbMorningShift.TabIndex = 1;
            chbMorningShift.Text = "000 employees";
            chbMorningShift.UseVisualStyleBackColor = true;
            chbMorningShift.CheckedChanged += chbMorningShift_CheckedChanged;
            // 
            // ShiftDayUC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGray;
            Controls.Add(pnlShiftDay);
            Name = "ShiftDayUC";
            Padding = new Padding(1);
            Size = new Size(140, 102);
            Load += ShiftDayUC_Load;
            pnlShiftDay.ResumeLayout(false);
            pnlShiftDay.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlShiftDay;
        private Label lblDay;
        private CheckBox chbMorningShift;
        private CheckBox chbEveningShift;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Button btnDayShifts;
    }
}
