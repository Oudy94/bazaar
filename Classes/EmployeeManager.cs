using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using TheSandwichMakersHardwareStoreSolution.Enums;
using TheSandwichMakersHardwareStoreSolution.Helpers;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{
    public class EmployeeManager
    {
        public Dictionary<int, Employee> EmployeeDict { get; set; }
        public DepartmentManager DepartmentManager { get; set; }
        public ShiftManager ShiftManager { get; set; }

        private readonly DatabaseHelper _dbHelper;

        public EmployeeManager(DepartmentManager departmentManager, ShiftManager shiftManager) 
        {
            this.EmployeeDict = new Dictionary<int, Employee>();
            this.DepartmentManager = departmentManager;
            this.ShiftManager = shiftManager;

            this._dbHelper = new DatabaseHelper();
        }

        public void LoadEmployees()
        {
            try
            {
                _dbHelper.OpenConnection();
                List<Employee> employees = _dbHelper.GetEmployees(DepartmentManager);

                foreach (Employee employee in employees)
                {
                    EmployeeDict.Add(employee.Id, employee);
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

        public void AddEmployee(Employee employee)
        {
            try
            {
                EmployeeDict.Add(employee.Id, employee);

                _dbHelper.OpenConnection();
                _dbHelper.AddEmployee(employee);
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

        public Employee GetEmployee(int id)
        {
            if (!EmployeeDict.ContainsKey(id))
            {
                throw new Exception("Employee not found!");
            }

            return EmployeeDict[id];
        }

        public Employee GetEmployee(string email)
        {
            foreach(Employee employee in EmployeeDict.Values)
            {
                if (employee.Email == email)
                {
                    return employee;
                }
            }

            return null;
        }

        public void UpdateEmployee(int id, string name, string email, string password, RoleEnum role, string image, string address, Department department, decimal hourlyWage, bool isActive)
        {
            try
            {
                Employee employee = GetEmployee(id);
                employee.Name = name;
                employee.Email = email;
                employee.Password = password;
                employee.Role = role;
                employee.Image = image ?? employee.Image;
                employee.Address = address;
                employee.Department = department;
                employee.HourlyWage = hourlyWage;
                employee.IsActive = isActive;

                _dbHelper.OpenConnection();
                _dbHelper.UpdateEmployee(employee);

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

        public void DeleteEmployee(int id)
        {
            try
            {
                Employee employee = GetEmployee(id);
                employee.IsActive = false;

                _dbHelper.OpenConnection();
                _dbHelper.DeleteEmployee(id);
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

        public bool IsNameUnique(string name)
        {
            foreach(Employee employee in EmployeeDict.Values)
            {
                if (employee.Name == name) return true;
            }

            return false;
        }

        public bool IsEmailUnique(string email)
        {
            foreach (Employee employee in EmployeeDict.Values)
            {
                if (employee.Email == email) return true;
            }

            return false;
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
