using SharedLibrary.Classes;
using SharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheSandwichMakersHardwareStoreSolution
{
    public partial class DayShiftForm : Form
    {
        private DateOnly _date;
        private EmployeeManager _employeeManager;
        private ShiftManager _shiftManager;

        public DayShiftForm(DateOnly date)
        {
            InitializeComponent();

            _date = date;

            _employeeManager = new EmployeeManager();
            _shiftManager = new ShiftManager();
        }

        private void DayShiftForm_Load(object sender, EventArgs e)
        {
            RefreshShiftUI();
        }

        private void RefreshShiftUI()
        {
            lblDay.Text = _date.DayOfWeek.ToString();
            lblDate.Text = _date.ToString();

            lstUnassignedEmployees.Items.Clear();
            foreach (Employee employee in _employeeManager.GetUnassignedEmployeesToShiftOnDate(_date))
            {
                lstUnassignedEmployees.Items.Add(employee);
            }

            lstMorningShiftEmployees.Items.Clear();
            foreach (Employee employee in _employeeManager.GetAssignedEmployeesToShiftOnDate(_date, ShiftTypeEnum.Morning))
            {
                lstMorningShiftEmployees.Items.Add(employee);
            }

            lstEveningShiftEmployees.Items.Clear();
            foreach (Employee employee in _employeeManager.GetAssignedEmployeesToShiftOnDate(_date, ShiftTypeEnum.Evening))
            {
                lstEveningShiftEmployees.Items.Add(employee);
            }

            (dtpMorningShiftStartTime.Value, dtpMorningShiftEndTime.Value) = _shiftManager.GetShiftTimes(_date, ShiftTypeEnum.Morning);
            (dtpEveningShiftStartTime.Value, dtpEveningShiftEndTime.Value) = _shiftManager.GetShiftTimes(_date, ShiftTypeEnum.Evening);
        }

        public bool ValidateShiftInput(ListBox listBox)
        {
            if (listBox.SelectedIndex < 0)
            {
                MessageBox.Show("you have to select employee first.");
                return false;
            }

            return true;
        }

        private void btnAutoAssignDay_Click(object sender, EventArgs e)
        {
            _shiftManager.AutoAssignShift(_employeeManager.GetUnassignedEmployeesToShiftOnDate(_date), _date);
            RefreshShiftUI();
        }

        private void btnAssignMorningShift_Click(object sender, EventArgs e)
        {
            if (!ValidateShiftInput(lstUnassignedEmployees))
            {
                return;
            }

            Employee employee = (Employee)lstUnassignedEmployees.SelectedItem;

            _shiftManager.AssignEmployeeToShift(employee, _date, ShiftTypeEnum.Morning);
            RefreshShiftUI();
        }

        private void btnAssignEveningShift_Click(object sender, EventArgs e)
        {
            if (!ValidateShiftInput(lstUnassignedEmployees))
            {
                return;
            }

            Employee employee = (Employee)lstUnassignedEmployees.SelectedItem;

            _shiftManager.AssignEmployeeToShift(employee, _date, ShiftTypeEnum.Evening);
            RefreshShiftUI();
        }

        private void btnUnassignMorningShift_Click(object sender, EventArgs e)
        {
            if (!ValidateShiftInput(lstMorningShiftEmployees))
            {
                return;
            }

            Employee employee = (Employee)lstMorningShiftEmployees.SelectedItem;

            _shiftManager.UnassignEmployeeFromShift(employee, _date, ShiftTypeEnum.Morning);
            RefreshShiftUI();
        }

        private void btnUnassignEveningShift_Click(object sender, EventArgs e)
        {
            if (!ValidateShiftInput(lstEveningShiftEmployees))
            {
                return;
            }

            Employee employee = (Employee)lstEveningShiftEmployees.SelectedItem;

            _shiftManager.UnassignEmployeeFromShift(employee, _date, ShiftTypeEnum.Evening);
            RefreshShiftUI();
        }

        private void btnSaveMorningShiftTime_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startTime = dtpMorningShiftStartTime.Value;
                DateTime endTime = dtpMorningShiftEndTime.Value;

                if (startTime > endTime)
                {
                    MessageBox.Show("Start time must be less than or equal to end time.");
                    return;
                }

                _shiftManager.AddOrUpdateShift(_date, ShiftTypeEnum.Morning, startTime, endTime);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show("Morning shift time is saved.");
        }

        private void btnSaveEveningShiftTime_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startTime = dtpEveningShiftStartTime.Value;
                DateTime endTime = dtpEveningShiftEndTime.Value;

                if (startTime > endTime)
                {
                    MessageBox.Show("Start time must be less than or equal to end time.");
                    return;
                }

                _shiftManager.AddOrUpdateShift(_date, ShiftTypeEnum.Evening, startTime, endTime);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show("Evening shift time is saved.");
        }

        private void txtSearchUnassignedEmployees_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearchUnassignedEmployees.Text.ToLower();

            lstUnassignedEmployees.ClearSelected();

            for (int i = 0; i < lstUnassignedEmployees.Items.Count; i++)
            {
                string currentItem = lstUnassignedEmployees.Items[i].ToString().ToLower();
                if (currentItem.Contains(searchText))
                {
                    lstUnassignedEmployees.SelectedIndex = i;
                    break;
                }
            }
        }

        private void txtSearchMorningShiftEmployees_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearchMorningShiftEmployees.Text.ToLower();

            lstMorningShiftEmployees.ClearSelected();

            for (int i = 0; i < lstMorningShiftEmployees.Items.Count; i++)
            {
                string currentItem = lstMorningShiftEmployees.Items[i].ToString().ToLower();
                if (currentItem.Contains(searchText))
                {
                    lstMorningShiftEmployees.SelectedIndex = i;
                    break;
                }
            }
        }

        private void txtSearchEveningShiftEmployees_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearchEveningShiftEmployees.Text.ToLower();

            lstEveningShiftEmployees.ClearSelected();

            for (int i = 0; i < lstEveningShiftEmployees.Items.Count; i++)
            {
                string currentItem = lstEveningShiftEmployees.Items[i].ToString().ToLower();
                if (currentItem.Contains(searchText))
                {
                    lstEveningShiftEmployees.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
