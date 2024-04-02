﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSandwichMakersHardwareStoreSolution.Enums;
using TheSandwichMakersHardwareStoreSolution.Helpers;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{
    public class ShiftManager
    {
        private readonly DatabaseHelper _dbHelper;

        public ShiftManager() 
        {
            this._dbHelper = new DatabaseHelper();
        }

        public List<Shift> GetShifts()
        {
            List<Shift> shifts = new List<Shift>();

            try
            {
                _dbHelper.OpenConnection();
                shifts = _dbHelper.GetShiftsFromDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return shifts;
        }

        public void AssignEmployeeToShift(Employee employee, DateOnly date, ShiftTypeEnum shiftType)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.AssignEmployeeToShiftInDB(employee, date, shiftType);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }
        }

        public void UnassignEmployeeFromShift(Employee employee, DateOnly date, ShiftTypeEnum shiftType)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.UnassignEmployeeFromShiftInDB(employee, date, shiftType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }
        }

        public void AutoAssignShift(List<Employee> unassignedEmployees, DateOnly date)
        {
            try
            {
                _dbHelper.OpenConnection();
                int employeeCountMorningShift = _dbHelper.GetAssignedEmployeeCountForShiftFromDB(date, ShiftTypeEnum.Morning);
                int employeeCountEveningShift = _dbHelper.GetAssignedEmployeeCountForShiftFromDB(date, ShiftTypeEnum.Evening);

                for (int i = 0; i < unassignedEmployees.Count; i++)
                {
                    if (employeeCountMorningShift <= employeeCountEveningShift)
                    {
                        _dbHelper.AssignEmployeeToShiftInDB(unassignedEmployees[i], date, ShiftTypeEnum.Morning);
                        employeeCountMorningShift++;
                    }
                    else
                    {
                        _dbHelper.AssignEmployeeToShiftInDB(unassignedEmployees[i], date, ShiftTypeEnum.Evening);
                        employeeCountEveningShift++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            { 
                _dbHelper.CloseConnection(); 
            }
        }
    }
}
