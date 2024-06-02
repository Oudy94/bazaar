using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheSandwichMakersHardwareStoreSolution
{
    public partial class ShiftDayUC : UserControl
    {
        public DateOnly Date { get; private set; }
        public bool EmployeeHasMorningShift { get; private set; }
        public bool EmployeeHasEveningShift { get; private set; }
        private bool suppressCheckedChangedEvent;

        public ShiftDayUC(DateOnly date)
        {
            InitializeComponent();
            this.Date = date;

            EmployeeHasMorningShift = false;
            EmployeeHasEveningShift = false;
            suppressCheckedChangedEvent = false;
        }

        private void ShiftDayUC_Load(object sender, EventArgs e)
        {
            lblDay.Text = Date.Day.ToString();

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now.Date);
            if (Date == currentDate)
            {
                lblDay.ForeColor = Color.Red;
            }
        }

        private void chbMorningShift_CheckedChanged(object sender, EventArgs e)
        {
            if (suppressCheckedChangedEvent)
                return;

            if (EmployeeHasMorningShift == chbMorningShift.Checked)
            {
                this.BackColor = Color.DarkGray;
            }
            else
            {
                this.BackColor = Color.FromArgb(255, 150, 79);
            }

            if (chbMorningShift.Checked)
            {
                if (chbEveningShift.Checked)
                {
                    chbEveningShift.Checked = false;
                }
            }
        }

        private void chbEveningShift_CheckedChanged(object sender, EventArgs e)
        {
            if (suppressCheckedChangedEvent)
                return;

            if (EmployeeHasEveningShift == chbEveningShift.Checked)
            {
                this.BackColor = Color.DarkGray;
            }
            else
            {
                this.BackColor = Color.FromArgb(255, 150, 79);
            }

            if (chbEveningShift.Checked)
            {
                if (chbMorningShift.Checked)
                {
                    chbMorningShift.Checked = false;
                }
            }
        }

        public void EnableCheckBox()
        {
            chbMorningShift.Enabled = true;
            chbEveningShift.Enabled = true;
        }

        public void ResetShiftsStatus()
        {
            suppressCheckedChangedEvent = true;
            chbMorningShift.Checked = false;
            chbEveningShift.Checked = false;
            suppressCheckedChangedEvent = false;
        }

        public void CheckMorningShift()
        {
            suppressCheckedChangedEvent = true;
            chbMorningShift.Checked = true;
            suppressCheckedChangedEvent = false;

            EmployeeHasMorningShift = true;
        }

        public void CheckEveningShift()
        {
            suppressCheckedChangedEvent = true;
            chbEveningShift.Checked = true;
            suppressCheckedChangedEvent = false;

            EmployeeHasEveningShift = true;
        }

        public void SetShiftCounts(int morningShiftCount, int eveningShiftCount)
        {
            chbMorningShift.Text = $"{morningShiftCount} employees";
            chbEveningShift.Text = $"{eveningShiftCount} employees";
        }

        public bool IsMorningShiftAdded()
        {
            return chbMorningShift.Checked == true && chbMorningShift.Checked != EmployeeHasMorningShift;
        }

        public bool IsMorningShiftRemoved()
        {
            return chbMorningShift.Checked == false && chbMorningShift.Checked != EmployeeHasMorningShift;
        }

        public bool IsEveningShiftAdded()
        {
            return chbEveningShift.Checked == true && chbEveningShift.Checked != EmployeeHasEveningShift;
        }

        public bool IsEveningShiftRemoved()
        {
            return chbEveningShift.Checked == false && chbEveningShift.Checked != EmployeeHasEveningShift;
        }

        private void btnDayShifts_Click(object sender, EventArgs e)
        {
            DayShiftForm dayShiftForm = new DayShiftForm(Date);

            dayShiftForm.ShowDialog();
        }
    }
}
