using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSandwichMakersHardwareStoreSolution.Enums;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{
    public class ShiftManager
    {
        public Dictionary<int, Shift> ShiftDict { get; set; }
        public Dictionary<DateOnly, (Shift, Shift)> ShiftDateDict { get; set; }

        public ShiftManager() 
        {
            this.ShiftDict = new Dictionary<int, Shift>();
            this.ShiftDateDict = new Dictionary<DateOnly, (Shift, Shift)>();
        }

        public void AddShift(Shift shift)
        {
            try
            {
                if (this.ShiftDict.ContainsKey(shift.Id))
                {
                    throw new Exception("Shift with the same ID already exists.");
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

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            if (!ShiftDateDict.ContainsKey(date) || shiftType == ShiftTypeEnum.Morning && ShiftDateDict[date].Item1 == null || shiftType == ShiftTypeEnum.Evening && ShiftDateDict[date].Item2 == null)
            {
                Shift shift = new Shift(date, shiftType);
                AddShift(shift);
            }

            if (shiftType == ShiftTypeEnum.Morning)
            {
                ShiftDateDict[date].Item1.EmployeeDict.Add(employee.Id, employee);
            }
            else
            {
                ShiftDateDict[date].Item2.EmployeeDict.Add(employee.Id, employee);
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
