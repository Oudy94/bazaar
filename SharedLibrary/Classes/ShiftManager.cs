using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary.Enums;
using SharedLibrary.Helpers;

namespace SharedLibrary.Classes
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

                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return shifts;
        }

        public List<Shift> GetEmployeeShifts(Employee employee)
        {
            List<Shift> shifts = new List<Shift>();

            try
            {
                _dbHelper.OpenConnection();
                shifts = _dbHelper.GetEmployeeAllShiftsFromDB30d(employee.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return shifts;
        }

        public void AssignEmployeeToShift(Employee employee, DateOnly date, ShiftTypeEnum shiftType, DateTime? startTime = null, DateTime? endTime = null)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.AssignEmployeeToShiftInDB(employee, date, shiftType, startTime, endTime);
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

        // Get Employee attendance percentage
        public double GetEmployeeAttendancePercentage(Employee employee)
        {
            double attendancePercentage = 0;

            try
            {
                _dbHelper.OpenConnection();
                attendancePercentage = _dbHelper.GetEmployeeAttendancePercentageFromDB(employee.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return attendancePercentage;
        }

        // Get GetEmployeeShiftsOnDate
        public List<Shift> GetEmployeeShiftsOnDate(Employee employee, DateOnly date)
        {
            List<Shift> shifts = new List<Shift>();

            try
            {
                _dbHelper.OpenConnection();
                shifts = _dbHelper.GetEmployeeShiftsOnDateFromDB(employee.Id, date);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return shifts;
        }

        public void AddOrUpdateShift(DateOnly date, ShiftTypeEnum shiftType, DateTime startTime, DateTime endTime)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.AddOrUpdateShiftToDB(date, shiftType, startTime, endTime);
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

        public (DateTime, DateTime) GetShiftTimes(DateOnly date, ShiftTypeEnum shiftType)
        {
            (DateTime, DateTime) shiftTime = (DateTime.MinValue, DateTime.MinValue);

            try
            {
                _dbHelper.OpenConnection();
                shiftTime = _dbHelper.GetShiftTimesFromDB(date, shiftType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return shiftTime;
        }

        public List<Shift> GetEmployeeShiftsOnMonthFromDB(int employeeId, int month, int year)
        {
            List<Shift> shifts = new List<Shift>();

            try
            {
                _dbHelper.OpenConnection();
                shifts = _dbHelper.GetEmployeeShiftsOnMonthFromDB(employeeId, month, year);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return shifts;
        } 
        
        public Dictionary<Tuple<DateOnly, ShiftTypeEnum>, int> GetShiftEmployeeCountForMonth(int month, int year)
        {
            Dictionary<Tuple<DateOnly, ShiftTypeEnum>, int> employeeCountByShift = new Dictionary<Tuple<DateOnly, ShiftTypeEnum>, int>();

            try
            {
                _dbHelper.OpenConnection();
                employeeCountByShift = _dbHelper.GetShiftEmployeeCountForMonthFromDB(month, year);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return employeeCountByShift;
        }
    }
}
