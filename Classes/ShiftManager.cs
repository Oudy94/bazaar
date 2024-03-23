using System;
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
        public Dictionary<int, Shift> ShiftDict { get; set; }
        public Dictionary<DateOnly, (Shift, Shift)> ShiftDateDict { get; set; }

        private readonly DatabaseHelper _dbHelper;

        public ShiftManager() 
        {
            this.ShiftDict = new Dictionary<int, Shift>();
            this.ShiftDateDict = new Dictionary<DateOnly, (Shift, Shift)>();

            this._dbHelper = new DatabaseHelper();
        }

        public void LoadShiftDataFromDatabase()
        {
            try
            {
                _dbHelper.OpenConnection();
                List<Shift> allShifts = _dbHelper.RetrieveShiftsData();

                foreach (Shift shift in allShifts)
                {
                    ShiftDict[shift.Id] = shift;

                    if (!ShiftDateDict.ContainsKey(shift.Date))
                    {
                        ShiftDateDict[shift.Date] = (null, null);
                    }

                    if (shift.ShiftType == ShiftTypeEnum.Morning)
                    {
                        ShiftDateDict[shift.Date] = (shift, ShiftDateDict[shift.Date].Item2);
                    }
                    else
                    {
                        ShiftDateDict[shift.Date] = (ShiftDateDict[shift.Date].Item1, shift);
                    }
                }
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

        public void LoadShiftEmployeeDataFromDatabase(Dictionary<int, Employee> Employees)
        {
            try
            {
                _dbHelper.OpenConnection();
                List<(int, int)> shiftEmployeeList = _dbHelper.RetrieveShiftEmployeeList();

                foreach (var (shiftId, employeeId) in shiftEmployeeList)
                {
                    if (ShiftDict.ContainsKey(shiftId))
                    {
                        if (Employees.ContainsKey(shiftId))
                        {
                            Employee employee = Employees[employeeId];
                            ShiftDict[shiftId].EmployeeDict.Add(employeeId, employee);
                        }
                    }
                }
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

        public void AddShift(Shift shift)
        {
            try
            {
                if (this.ShiftDict.ContainsKey(shift.Id))
                {
                    throw new Exception("Shift with the same Id already exists.");
                }

                if (ShiftDateDict.ContainsKey(shift.Date))
                {
                    var (morning, evening) = ShiftDateDict[shift.Date];
                    if (shift.ShiftType == ShiftTypeEnum.Morning && morning != null || shift.ShiftType == ShiftTypeEnum.Evening && evening != null)
                    {
                        throw new Exception($"{shift.ShiftType} Shift with the same date already exists.");
                    }

                    if (shift.ShiftType == ShiftTypeEnum.Morning)
                        ShiftDateDict[shift.Date] = (shift, evening);
                    else
                        ShiftDateDict[shift.Date] = (morning, shift);

                }
                else
                {
                    if (shift.ShiftType == ShiftTypeEnum.Morning)
                        ShiftDateDict[shift.Date] = (shift, null);
                    else
                        ShiftDateDict[shift.Date] = (null, shift);
                }

                this.ShiftDict.Add(shift.Id, shift);

                _dbHelper.OpenConnection();
                _dbHelper.AddShift(shift);
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

        public void RemoveShift(Shift shift)
        {
            try
            {
                this.ShiftDict.Remove(shift.Id);
                this.ShiftDateDict.Remove(shift.Date);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Shift> GetShiftsForEmployee(Employee employee)
        {
            List<Shift> shifts = new List<Shift>();

            foreach(Shift shift in this.ShiftDict.Values)
            {
                if (shift.EmployeeDict.ContainsKey(employee.Id))
                {
                    shifts.Add(shift);
                }
            }

            return shifts;
        }

        public List<Shift> GetShifts()
        {
            return ShiftDict.Values.ToList();
        }

        public void AssignEmployeeToShift(Employee employee, DateOnly date, ShiftTypeEnum shiftType)
        {
            try
            {
                Shift shift;
                if (!ShiftDateDict.ContainsKey(date) || shiftType == ShiftTypeEnum.Morning && ShiftDateDict[date].Item1 == null || shiftType == ShiftTypeEnum.Evening && ShiftDateDict[date].Item2 == null)
                {
                    shift = new Shift(date, shiftType);
                    AddShift(shift);
                }
                else
                {
                    if (shiftType == ShiftTypeEnum.Morning)
                    {
                        shift = ShiftDateDict[date].Item1;
                    }
                    else
                    {
                        shift = ShiftDateDict[date].Item2;
                    }
                }

                shift.EmployeeDict.Add(employee.Id, employee);

                _dbHelper.OpenConnection();
                _dbHelper.AddShiftEmployee(shift.Id, employee.Id);
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
            if (!ShiftDateDict.ContainsKey(date) || shiftType == ShiftTypeEnum.Morning && ShiftDateDict[date].Item1 == null || shiftType == ShiftTypeEnum.Evening && ShiftDateDict[date].Item2 == null)
            {
                throw new ArgumentException("Shift not found!");
            }

            if (shiftType == ShiftTypeEnum.Morning)
            {
                ShiftDateDict[date].Item1.EmployeeDict.Remove(employee.Id);
            }
            else
            {
                ShiftDateDict[date].Item2.EmployeeDict.Remove(employee.Id);
            }
        }

        public void AutoAssignShift(DateOnly date, List<Employee> unassignedEmployees)
        {
            int employeeCountMorningShift = 0;
            int employeeCountEveningShift = 0;

            if (ShiftDateDict.ContainsKey(date))
            {
                if (ShiftDateDict[date].Item1 != null)
                {
                    employeeCountMorningShift = ShiftDateDict[date].Item1.EmployeeDict.Values.Count();
                }

                if (ShiftDateDict[date].Item2 != null)
                {
                    employeeCountEveningShift = ShiftDateDict[date].Item2.EmployeeDict.Values.Count();
                }
            }

            for (int i = 0; i < unassignedEmployees.Count; i++)
            {
                if (employeeCountMorningShift <= employeeCountEveningShift)
                {
                    AssignEmployeeToShift(unassignedEmployees[i], date, ShiftTypeEnum.Morning);
                    employeeCountMorningShift++;
                }
                else
                {
                    AssignEmployeeToShift(unassignedEmployees[i], date, ShiftTypeEnum.Evening);
                    employeeCountEveningShift++;
                }
            }
        }
    }
}
