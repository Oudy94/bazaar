using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSandwichMakersHardwareStoreSolution.Enums;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{
    public class EmployeeManager
    {
        public Dictionary<int, Employee> EmployeeDict { get; set; }
        public ShiftManager ShiftManager { get; set; }

        public EmployeeManager(ShiftManager shiftManager) 
        {
            this.EmployeeDict = new Dictionary<int, Employee>();
            this.ShiftManager = shiftManager;
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                EmployeeDict.Add(employee.Id, employee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Employee> GetUnassignedEmployees(DateOnly date)
        {
            List<Employee> employees = new List<Employee>();

            if (!ShiftManager.ShiftDateDict.ContainsKey(date))
                return EmployeeDict.Values.ToList();

            Shift morningShift = ShiftManager.ShiftDateDict[date].Item1;
            Shift eveningShift = ShiftManager.ShiftDateDict[date].Item2;

            foreach (Employee employee in EmployeeDict.Values)
            {
                if ((morningShift == null || !morningShift.EmployeeDict.ContainsKey(employee.Id)) &&
                    (eveningShift == null || !eveningShift.EmployeeDict.ContainsKey(employee.Id)))
                {
                    employees.Add(employee);
                }
            }

            return employees;
        }

        public List<Employee> GetEmployees()
        {
            return EmployeeDict.Values.ToList();
        }

        public List<Employee> GetAssignedEmployeed(DateOnly date, ShiftTypeEnum shiftType)
        {
            if (!ShiftManager.ShiftDateDict.ContainsKey(date) || shiftType == ShiftTypeEnum.Morning && ShiftManager.ShiftDateDict[date].Item1 == null || shiftType == ShiftTypeEnum.Evening && ShiftManager.ShiftDateDict[date].Item2 == null)
                return new List<Employee>();

            if (shiftType == ShiftTypeEnum.Morning)
            {
                return ShiftManager.ShiftDateDict[date].Item1.GetEmployees();
            }
            else
            {
                return ShiftManager.ShiftDateDict[date].Item2.GetEmployees();
            }
        }
    }
}
