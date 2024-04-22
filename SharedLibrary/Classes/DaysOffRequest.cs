using System;

public class DaysOffRequest
{
	public DaysOffRequest(int id, int employeeId, DateTime startDate, DateTime endDate, string description)
	{
		Id = id;
		EmployeeId = employeeId;
		StartDate = startDate;
		EndDate = endDate;
		Description = description;
	}
	public int Id { get; set; }
	public int EmployeeId { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public string Description { get; set; }
}
