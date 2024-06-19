using System;

public class DaysOffRequest
{
	public DaysOffRequest(int id, int employeeId, string employeeName, DateTime startDate, DateTime endDate, string description, string status, string type)
	{
		Id = id;
		EmployeeId = employeeId;
		EmployeeName = employeeName;
		StartDate = startDate;
		EndDate = endDate;
		Description = description;
		Status = status;
		Type = type;
	}
	public int Id { get; set; }
	public int EmployeeId { get; set; }
	public string EmployeeName { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public string Description { get; set; }
	public string Status { get; set; } // 0 pending, 1 aprove, 2 declined 
	public string Type { get; set; }
}
