using SharedLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Classes
{
    public class Login
    {
        private readonly DatabaseHelper _dbHelper;

        public Login()
        {
            this._dbHelper = new DatabaseHelper();
        }

        public bool AuthenticateAdmin(string email, string password)
        {
            try
            {
                _dbHelper.OpenConnection();
                return _dbHelper.AuthenticateAdmin(email, password);
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

		public Employee AuthenticateUser(string email, string password)
		{
			try
			{
				_dbHelper.OpenConnection();
				return _dbHelper.AuthenticateUser(email, password);
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
