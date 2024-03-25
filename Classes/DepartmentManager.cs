using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSandwichMakersHardwareStoreSolution.Enums;
using TheSandwichMakersHardwareStoreSolution.Helpers;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{
    public class DepartmentManager
    {
        public Dictionary<int, Department> DepartmentDict { get; set; }
        private readonly DatabaseHelper _dbHelper;

        public DepartmentManager() 
        { 
            this.DepartmentDict = new Dictionary<int, Department>();

            this._dbHelper = new DatabaseHelper();
        }

        public void LoadDepartmentDataFromDatabase()
        {
            try
            {
                _dbHelper.OpenConnection();
                List<Department> departments = _dbHelper.RetrieveDepartments();

                foreach (Department department in departments)
                {
                    DepartmentDict.Add(department.Id, department);
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

        public void AddDepartment(string name)
        {
            try
            {
                Department department = new Department(name);
                DepartmentDict.Add(department.Id, department);

                _dbHelper.OpenConnection();
                _dbHelper.AddDepartment(department);
            }
            catch (Exception ex)
            {
                throw new Exception("Problem adding department.");
            }
            finally
            {
                _dbHelper.CloseConnection();
            }
        }

        public Department GetDepartment(int id)
        {
            if (!DepartmentDict.ContainsKey(id))
            {
                throw new Exception("Department not found!");
            }

            return DepartmentDict[id];
        }

        public List<Department> GetDepartments()
        {
            return DepartmentDict.Values.ToList();
        }

        public void UpdateDepartment(int id, string name)
        {
            try
            {
                Department department = GetDepartment(id);
                department.Name = name;

                _dbHelper.OpenConnection();
                _dbHelper.UpdateDepartment(department);

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

        public void DeleteDeparment(int id)
        {
            if (!DepartmentDict.ContainsKey(id))
            {
                throw new Exception("Department not found!");
            }

            try
            {
                DepartmentDict.Remove(id);

                _dbHelper.OpenConnection();
                _dbHelper.RemoveDepartment(id);
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
