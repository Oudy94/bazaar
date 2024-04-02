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
        private readonly DatabaseHelper _dbHelper;

        public DepartmentManager() 
        { 
            this._dbHelper = new DatabaseHelper();
        }

        public void AddDepartment(string name)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.AddDepartmentToDB(name);
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

        public List<Department> GetDepartments()
        {
            List<Department> departments = null;

            try
            {
                _dbHelper.OpenConnection();
                departments = _dbHelper.GetDepartmentsFromDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }

            return departments;
        }

        public void UpdateDepartment(int id, string name)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.UpdateDepartmentInDB(id, name);

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

        public void RemoveDepartment(int id)
        {
            try
            {
                _dbHelper.OpenConnection();
                _dbHelper.RemoveDepartmentFromDB(id);
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
