using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSandwichMakersHardwareStoreSolution.Classes;

namespace TheSandwichMakersHardwareStoreSolution.Helpers
{
    public class DatabaseHelper
    {
        private readonly string connectionString = ""; //HERE ADD YOUR OWN CONNECTION STRING!!
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
        /*
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
        }*/

        public Employee AuthenticateUser(string email, string password, List<Role> roles, List<Department> departments)
        {
            Employee authenticatedEmployee = null;
            try
            {
                string query = "SELECT * FROM employee WHERE email = @Email AND password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var role = roles.FirstOrDefault(r => r.Id == reader.GetInt32(reader.GetOrdinal("role")));
                        var department = departments.FirstOrDefault(d => d.Id == reader.GetInt32(reader.GetOrdinal("department")));

                        if (role != null && department != null)
                        {
                            authenticatedEmployee = new Employee(
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
            catch (Exception ex)
            {
                throw new Exception("Authentication failed: " + ex.Message);
            }
            finally
            {
                UserSession.Instance.CurrentEmployee = authenticatedEmployee;
            }

            return authenticatedEmployee;
        }



        // Role and Departments Management
        public List<Role> GetRoles()
        {
            var roles = new List<Role>();
            string query = "SELECT id, name FROM role_list";
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
            string query = "SELECT id, name FROM department_list";
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
            string query = "INSERT INTO department_list (name) VALUES (@Name)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Name", departmentName);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateDepartment(int departmentId, string newDepartmentName)
        {
            string query = "UPDATE department_list SET name = @Name WHERE id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Name", newDepartmentName);
                cmd.Parameters.AddWithValue("@Id", departmentId);
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveDepartment(int departmentId)
        {
            string query = "DELETE FROM department_list WHERE id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", departmentId);
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
            string query = @"UPDATE employee 
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
            string query = "UPDATE employee SET is_active = 0 WHERE id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", employeeId);
                cmd.ExecuteNonQuery();
            }
        }

        // Get employye by id
        public Employee GetEmployeeById(int employeeId, List<Role> roles, List<Department> departments)
        {
            string query = "SELECT e.*, r.Id as RoleId, r.name as RoleName, d.id as DepartmentId, d.name as DepartmentName FROM employee e " +
                           "INNER JOIN role_list r ON e.role = r.id " +
                           "INNER JOIN department_list d ON e.department = d.id " +
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
            string query = "SELECT e.*, r.id as RoleId, r.name as RoleName, d.id as DepartmentId, d.name as DepartmentName FROM employee e " +
                           "INNER JOIN role_list r ON e.role = r.id " +
                           "INNER JOIN department_list d ON e.department = d.id";

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

        public bool CheckEmployeeNameExists(string name)
        {
            var query = "SELECT COUNT(*) FROM employee WHERE name = @Name";
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                var count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool CheckEmployeeEmailExists(string email)
        {
            var query = "SELECT COUNT(*) FROM employee WHERE email = @Email";
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                var count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
