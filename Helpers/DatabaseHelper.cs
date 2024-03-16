using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSandwichMakersHardwareStoreSolution.Classes;

namespace TheSandwichMakersHardwareStoreSolution.Helpers
{
    public class DatabaseHelper
    {
        private readonly string connectionString = "Server=mssqlstud.fhict.local;Database=dbi530788_hardware;User Id=dbi530788_hardware;Password=hardware;"; //HERE ADD YOUR OWN CONNECTION STRING!!
        private SqlConnection connection;

        public DatabaseHelper()
        {
            connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error opening database connection: {ex.Message}");
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error closing database connection: {ex.Message}");
            }
        }

        public bool AuthenticateUser(string email, string password)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM [dbo].[employee] WHERE email = '{email}' AND password = '{password}'";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error authenticating user: {ex.Message}", ex);
            }
        }


        // Role and Departments Management
        public List<Role> GetRoles()
        {
            var roles = new List<Role>();
            string query = "SELECT id, name FROM RoleList";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name"))
                        });
                    }
                }
            }
            return roles;
        }

        public List<Department> GetDepartments()
        {
            var departments = new List<Department>();
            string query = "SELECT id, name FROM DepartmentList";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name"))
                        });
                    }
                }
            }
            return departments;
        }


        public void AddDepartment(string departmentName)
        {
            string query = "INSERT INTO DepartmentList (name) VALUES (@name)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", departmentName);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateDepartment(int departmentId, string newDepartmentName)
        {
            string query = "UPDATE DepartmentList SET name = @name WHERE id = @id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", newDepartmentName);
                cmd.Parameters.AddWithValue("@id", departmentId);
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveDepartment(int departmentId)
        {
            string query = "DELETE FROM DepartmentList WHERE id = @id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", departmentId);
                cmd.ExecuteNonQuery();
            }
        }

        // Employee Management

        public void AddEmployee(Employee employee)
        {
            string query = @"INSERT INTO Employee (name, email, password, role, register_date, image, address, department, hourly_wage, is_active) 
                         VALUES (@Name, @Email, @Password, @Role, @RegisterDate, @Image, @Address, @Department, @HourlyWage, @IsActive)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                // Add parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Password", employee.Password); // Hash this password
                cmd.Parameters.AddWithValue("@Role", employee.Role.Id);
                cmd.Parameters.AddWithValue("@RegisterDate", employee.RegisterDate);
                cmd.Parameters.AddWithValue("@Image", employee.Image);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Department", employee.Department.Id);
                cmd.Parameters.AddWithValue("@HourlyWage", employee.HourlyWage);
                cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);

                cmd.ExecuteNonQuery();
            }
        }

        // Method to update an existing employee
        public void UpdateEmployee(Employee employee)
        {
            string query = @"UPDATE Employee 
                         SET name = @Name, 
                             email = @Email, 
                             password = @Password, 
                             role = @Role, 
                             register_date = @RegisterDate, 
                             image = @Image, 
                             address = @Address, 
                             department = @Department, 
                             hourly_wage = @HourlyWage, 
                             is_active = @IsActive 
                         WHERE id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", employee.Id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Password", employee.Password); // Hash this password
                cmd.Parameters.AddWithValue("@Role", employee.Role.Id);
                cmd.Parameters.AddWithValue("@RegisterDate", employee.RegisterDate);
                cmd.Parameters.AddWithValue("@Image", employee.Image);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Department", employee.Department.Id);
                cmd.Parameters.AddWithValue("@HourlyWage", employee.HourlyWage);
                cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);

                cmd.ExecuteNonQuery();
            }
        }

        // Method to delete an employee (soft delete by setting is_active to false)
        public void DeleteEmployee(int employeeId)
        {
            string query = "UPDATE Employee SET is_active = 0 WHERE id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", employeeId);
                cmd.ExecuteNonQuery();
            }
        }

        // Get employye by id
        public Employee GetEmployeeById(int employeeId, List<Role> roles, List<Department> departments)
        {
            string query = "SELECT e.*, r.Id as RoleId, r.Name as RoleName, d.Id as DepartmentId, d.Name as DepartmentName FROM Employee e " +
                           "INNER JOIN RoleList r ON e.role = r.Id " +
                           "INNER JOIN DepartmentList d ON e.department = d.Id " +
                           "WHERE e.id = @EmployeeId";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var role = roles.FirstOrDefault(r => r.Id == reader.GetInt32(reader.GetOrdinal("RoleId")));
                        var department = departments.FirstOrDefault(d => d.Id == reader.GetInt32(reader.GetOrdinal("DepartmentId")));

                        if (role != null && department != null)
                        {
                            return new Employee(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetString(reader.GetOrdinal("name")),
                                reader.GetString(reader.GetOrdinal("email")),
                                reader.GetString(reader.GetOrdinal("password")), // Should be hashed
                                role,
                                reader.IsDBNull(reader.GetOrdinal("image")) ? null : reader.GetString(reader.GetOrdinal("image")),
                                reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
                                department,
                                reader.GetDecimal(reader.GetOrdinal("hourly_wage")),
                                reader.GetBoolean(reader.GetOrdinal("is_active"))
                            );
                        }
                    }
                }
            }
            return null; // Employee not found
        }

        public List<Employee> GetEmployees(List<Role> roles, List<Department> departments)
        {
            List<Employee> employees = new List<Employee>();
            string query = "SELECT e.*, r.Id as RoleId, r.Name as RoleName, d.Id as DepartmentId, d.Name as DepartmentName FROM Employee e " +
                           "INNER JOIN RoleList r ON e.role = r.Id " +
                           "INNER JOIN DepartmentList d ON e.department = d.Id";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var role = roles.FirstOrDefault(r => r.Id == reader.GetInt32(reader.GetOrdinal("RoleId")));
                        var department = departments.FirstOrDefault(d => d.Id == reader.GetInt32(reader.GetOrdinal("DepartmentId")));

                        if (role != null && department != null)
                        {
                            employees.Add(new Employee(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetString(reader.GetOrdinal("name")),
                                reader.GetString(reader.GetOrdinal("email")),
                                reader.GetString(reader.GetOrdinal("password")), // Should be hashed
                                role,
                                reader.IsDBNull(reader.GetOrdinal("image")) ? null : reader.GetString(reader.GetOrdinal("image")),
                                reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
                                department,
                                reader.GetDecimal(reader.GetOrdinal("hourly_wage")),
                                reader.GetBoolean(reader.GetOrdinal("is_active"))
                            ));
                        }
                    }
                }
            }
            return employees;
        }

    }
}
