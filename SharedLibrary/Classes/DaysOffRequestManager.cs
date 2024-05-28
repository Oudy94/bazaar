using System;
using SharedLibrary.Helpers;

public class DaysOffRequestManager
{
	public DaysOffRequestManager()
	{
		this._dbHelper = new DatabaseHelper();
	}

	private readonly DatabaseHelper _dbHelper;

	public List<DaysOffRequest> GetDaysOffRequest()
	{
		List<DaysOffRequest> daysoffrequests = new List<DaysOffRequest>();

		try
		{
			_dbHelper.OpenConnection();
			daysoffrequests = _dbHelper.GetDaysOffRequestsFromDatabase();
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
		finally
		{
			_dbHelper.CloseConnection();
		}

		return daysoffrequests;
	}

	public void AddDaysOffRequest(int employeeId, DateTime startDate, DateTime endDate, string description, int status, string type)
	{
		
		try
		{
			_dbHelper.OpenConnection();
			_dbHelper.AddDaysOffRequestToDatabase(employeeId, startDate, endDate, description, status, type);
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

	public void RemoveDaysOffRequest(int id)
	{
		try
		{
			_dbHelper.OpenConnection();
			_dbHelper.RemoveDaysOffRequestToDatabase(id);
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

	public void AproveDaysOffRequest(int id)
	{
        try
        {
            _dbHelper.OpenConnection();
            _dbHelper.ChangeStatusDaysOffRequest(id, 1);
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

	public void DeclineDaysOffRequest(int id)
	{
        try
        {
            _dbHelper.OpenConnection();
            _dbHelper.ChangeStatusDaysOffRequest(id, 2);
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
