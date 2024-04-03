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
        private readonly DatabaseHelper _dbHelper;

        public EmployeeManager() 
        {
            this._dbHelper = new DatabaseHelper();
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                _dbHelper.OpenConnection();
                employees = _dbHelper.GetEmployeesFromDB();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return employees;
        }

        public void AddEmployee(string name, string email, string password, RoleEnum role, string image, string address, Department department, decimal hourlyWage, bool isActive)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.AddEmployeeToDB(name, email, password, role, image, address, department, hourlyWage, isActive);
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

        public Employee GetEmployeeById(int id)
        {
            Employee employee = null;

            try
            {
                _dbHelper.OpenConnection();
                employee = _dbHelper.GetEmployeeByIdFromDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return employee;
        }

        public Employee GetEmployeeByEmail(string email)
        {
            try
            {
                _dbHelper.OpenConnection();
                return _dbHelper.GetEmployeeByEmailFromDB(email);
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

        public void UpdateEmployee(int id, string name, string email, string password, RoleEnum role, string image, string address, Department department, decimal hourlyWage, bool isActive)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.UpdateEmployeeInDB(id, name, email, password, role, image, address, department, hourlyWage, isActive);
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

        public void DeactivateEmployee(int id)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.DeactivateEmployeeInDB(id);
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

        public bool IsEmployeeNameUnique(string name)
        {
            try
            {
                _dbHelper.OpenConnection();
                return _dbHelper.IsEmployeeNameUniqueInDB(name);
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

        public bool IsEmployeeEmailUnique(string email)
        {
            try
            {
                _dbHelper.OpenConnection();
                return _dbHelper.IsEmployeeEmailUniqueInDB(email);
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

        public bool IsNameUniqueExceptCurrentEmployee(string name, int id)
        {
            try
            {
                _dbHelper.OpenConnection();
                return _dbHelper.IsNameUniqueExceptCurrentEmployeeinDB(name, id);
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

        public bool IsEmailUniqueExceptCurrentEmployee(string email, int id)
        {
            try
            {
                _dbHelper.OpenConnection();
                return _dbHelper.IsEmailUniqueExceptCurrentEmployeeInDB(email, id);
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

        public List<Employee> GetUnassignedEmployeesToShiftOnDate(DateOnly date)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                _dbHelper.OpenConnection();
                employees = _dbHelper.GetUnassignedEmployeesToShiftFromDB(date);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return employees;
        }

        public List<Employee> GetAssignedEmployeesToShiftOnDate(DateOnly date, ShiftTypeEnum shiftType)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                _dbHelper.OpenConnection();
                employees = _dbHelper.GetAssignedEmployeesToShiftFromDB(date, shiftType);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return employees;
        }

        public List<Employee> GetEmployeesByRole(RoleEnum role)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                _dbHelper.OpenConnection();
                employees = _dbHelper.GetEmployeeByRoleFromDB(role);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return employees;
        }

        public List<Employee> GetEmployeesByDepartment(Department department)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                _dbHelper.OpenConnection();
                employees = _dbHelper.GetEmployeeByDepartmentFromDB(department);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return employees;
        }

        public List<Employee> GetEmployeesByRoleAndDepartment(RoleEnum role, Department department)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                _dbHelper.OpenConnection();
                employees = _dbHelper.GetEmployeeByRoleAndDepartmentFromDB(role, department);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return employees;
        }
    }
}
