using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSandwichMakersHardwareStoreSolution.Enums;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{
    public class Shift
    {
        public static int nextId = 1;

        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public ShiftTypeEnum ShiftType { get; set; }
        public Dictionary<int, Employee> EmployeeDict { get; set; }

        public Shift(int id, DateOnly date, ShiftTypeEnum shiftType)
        {
            this.Id = id;
            this.Date = date;
            this.ShiftType = shiftType;
            EmployeeDict = new Dictionary<int, Employee>();

            nextId = id;
        }

        public Shift(DateOnly date, ShiftTypeEnum shiftType)
        {
            this.Id = nextId++;
            this.Date = date;
            this.ShiftType = shiftType;
            EmployeeDict = new Dictionary<int, Employee>();
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                this.EmployeeDict.Add(employee.Id, employee);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Employee> GetEmployees()
        {
            return EmployeeDict.Values.ToList();
        }
    }
}
