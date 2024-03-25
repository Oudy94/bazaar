using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSandwichMakersHardwareStoreSolution.Classes;
using TheSandwichMakersHardwareStoreSolution.Enums;

namespace TheSandwichMakersHardwareStoreSolution.Helpers
{
    public class DatabaseHelper
    {
        private readonly string connectionString = "Server=mssqlstud.fhict.local;Database=dbi534217;User Id=dbi534217;Password=123456;"; //HERE ADD YOUR OWN CONNECTION STRING!!
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

        //public Employee AuthenticateUser(string email, string password, List<Role> roles, List<Department> departments)
        //{
        //    Employee authenticatedEmployee = null;
        //    try
        //    {
        //        string query = "SELECT * FROM employee WHERE email = @Email AND password = @Password";
        //        SqlCommand command = new SqlCommand(query, connection);
        //        command.Parameters.AddWithValue("@Email", email);
        //        command.Parameters.AddWithValue("@Password", password);

        //        using (var reader = command.ExecuteReader())
        //        {
        //            if (reader.Read())
        //            {
        //                var role = roles.FirstOrDefault(r => r.Id == reader.GetInt32(reader.GetOrdinal("role")));
        //                var department = departments.FirstOrDefault(d => d.Id == reader.GetInt32(reader.GetOrdinal("department")));

        //                if (role != null && department != null)
        //                {
        //                    authenticatedEmployee = new Employee(
        //                        reader.GetInt32(reader.GetOrdinal("id")),
        //                        reader.GetString(reader.GetOrdinal("name")),
        //                        reader.GetString(reader.GetOrdinal("email")),
        //                        reader.GetString(reader.GetOrdinal("password")), // Should be hashed
        //                        role,
        //                        reader.IsDBNull(reader.GetOrdinal("image")) ? null : reader.GetString(reader.GetOrdinal("image")),
        //                        reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
        //                        department,
        //                        reader.GetDecimal(reader.GetOrdinal("hourly_wage")),
        //                        reader.GetBoolean(reader.GetOrdinal("is_active"))
        //                    );
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Authentication failed: " + ex.Message);
        //    }
        //    finally
        //    {
        //        UserSession.Instance.CurrentEmployee = authenticatedEmployee;
        //    }

        //    return authenticatedEmployee;
        //}

        public void AddDepartment(Department department)
        {
            string query = "INSERT INTO department_list (id, name) VALUES (@Id, @Name)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", department.Id);
                cmd.Parameters.AddWithValue("@Name", department.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateDepartment(Department department)
        {
            string query = "UPDATE department_list SET name = @Name WHERE id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Name", department.Name);
                cmd.Parameters.AddWithValue("@Id", department.Id);
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

        public List<Department> RetrieveDepartments()
        {
            List<Department> department = new List<Department>();
            string query = "SELECT * FROM department_list;";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        department.Add(new Department(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("name"))
                        ));
                    }
                }
            }
            return department;
        }

        // Employee Management

        public void AddEmployee(Employee employee)
        {
            string query = @"INSERT INTO Employee (id, name, email, password, role, register_date, image, address, department, hourly_wage, is_active) 
                         VALUES (@Id, @Name, @Email, @Password, @Role, @RegisterDate, @Image, @Address, @Department, @HourlyWage, @IsActive)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                // Add parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@Id", employee.Id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Password", employee.Password); // Hash this password
                cmd.Parameters.AddWithValue("@Role", (int)employee.Role + 1);
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
                cmd.Parameters.AddWithValue("@Role", (int)employee.Role + 1);
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

        public List<Employee> GetEmployees(DepartmentManager departmentManager)
        {
            List<Employee> employees = new List<Employee>();
            string query = "SELECT * FROM employee;";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Department department = departmentManager.GetDepartment(reader.GetInt32(reader.GetOrdinal("department")));

                        employees.Add(new Employee(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("name")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("password")), // Should be hashed
                            (RoleEnum)reader.GetInt32(reader.GetOrdinal("role")) -1,
                            reader.IsDBNull(reader.GetOrdinal("image")) ? null : reader.GetString(reader.GetOrdinal("image")),
                            reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
                            department,
                            reader.GetDecimal(reader.GetOrdinal("hourly_wage")),
                            reader.GetBoolean(reader.GetOrdinal("is_active"))
                        ));
                    }
                }
            }
            return employees;
        }

        public void AddShift(Shift shift)
        {
            string query = "INSERT INTO shift (id, date, shift_type) VALUES (@Id, @Date, @ShiftType)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", shift.Id);
                cmd.Parameters.AddWithValue("@date", new DateTime(shift.Date.Year, shift.Date.Month, shift.Date.Day));
                cmd.Parameters.AddWithValue("@ShiftType", (int)shift.ShiftType + 1);

                cmd.ExecuteNonQuery();
            }
        }

        public void AddShiftEmployee(int shiftId, int employeeId)
        {
            string query = "INSERT INTO shift_employee (shift_id, employee_id) VALUES (@ShiftId, @EmployeeId)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ShiftId", shiftId);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Shift> RetrieveShiftsData()
        {
            string query = "SELECT * FROM shift";

            List<Shift> shifts = new List<Shift>();

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("date"));
                        DateOnly date = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

                        Shift shift = new Shift
                        (
                            reader.GetInt32(reader.GetOrdinal("id")),
                            date,
                            (ShiftTypeEnum)reader.GetInt32(reader.GetOrdinal("shift_type")) - 1
                        );

                        shifts.Add(shift);
                    }
                }
            }

            return shifts;
        }

        public List<(int, int)> RetrieveShiftEmployeeList()
        {
            List<(int, int)> shiftEmployee = new List<(int, int)>();

            string query = "SELECT shift_id, employee_id FROM shift_employee";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int shiftId = reader.GetInt32("shift_id");
                        int employeeId = reader.GetInt32("employee_id");
                        shiftEmployee.Add((shiftId, employeeId));
                    }
                }
            }

            return shiftEmployee;
        }

        public void AddItem(Item item)
        {
            string query = "INSERT INTO item (id, sku, name, quantity_warehouse, quantity_store, category, wholesale_price, sell_price) " +
                "VALUES (@Id, @Sku, @Name, @QuantityWarehouse, @QuantityStore, @Category, @WholePrice, @SellPrice)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Sku", item.Sku);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@QuantityWarehouse", item.QuantityWarehouse);
                cmd.Parameters.AddWithValue("@QuantityStore", item.QuantityStore);
                cmd.Parameters.AddWithValue("@Category",(int)item.Category + 1);
                cmd.Parameters.AddWithValue("@WholePrice", item.WholesalePrice);
                cmd.Parameters.AddWithValue("@SellPrice", item.SellPrice);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateItem(Item item)
        {
            string query = "UPDATE item SET sku = @Sku, name = @Name, quantity_warehouse = @QuantityWarehouse, " +
                           "quantity_store = @QuantityStore, category = @Category, wholesale_price = @WholePrice, " +
                           "sell_price = @SellPrice WHERE id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Sku", item.Sku);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@QuantityWarehouse", item.QuantityWarehouse);
                cmd.Parameters.AddWithValue("@QuantityStore", item.QuantityStore);
                cmd.Parameters.AddWithValue("@Category", (int)item.Category + 1);
                cmd.Parameters.AddWithValue("@WholePrice", item.WholesalePrice);
                cmd.Parameters.AddWithValue("@SellPrice", item.SellPrice);
                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveItem(int itemId)
        {
            string query = "DELETE FROM item WHERE id = @ItemId";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ItemId", itemId);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Item> RetrieveItems()
        {
            List<Item> items = new List<Item>();

            string query = "SELECT id, sku, name, quantity_warehouse, quantity_store, category, wholesale_price, sell_price FROM item";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Item item = new Item
                        (
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetInt32(reader.GetOrdinal("sku")),
                            reader.GetString(reader.GetOrdinal("name")),
                            reader.GetInt32(reader.GetOrdinal("quantity_warehouse")),
                            reader.GetInt32(reader.GetOrdinal("quantity_store")),
                            (CategoryEnum)reader.GetInt32(reader.GetOrdinal("category")) - 1,
                            reader.GetDouble(reader.GetOrdinal("wholesale_price")),
                            reader.GetDouble(reader.GetOrdinal("sell_price"))
                        );
                        items.Add(item);
                    }
                }
            }

            return items;
        }

        public void AddShelfRequest(ShelfRequest shelfRequest)
        {
            string query = "INSERT INTO shelf_request (id, item_id, quantity) " +
                "VALUES (@Id, @ItemId, @Quantity)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", shelfRequest.Id);
                cmd.Parameters.AddWithValue("@ItemId", shelfRequest.ItemId);
                cmd.Parameters.AddWithValue("@Quantity", shelfRequest.Quantity);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateShelfRequest(ShelfRequest shelfRequest)
        {
            string query = "UPDATE shelf_request SET item_id = @ItemId, quantity = @Quantity WHERE id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ItemId", shelfRequest.ItemId);
                cmd.Parameters.AddWithValue("@Quantity", shelfRequest.Quantity);
                cmd.Parameters.AddWithValue("@Id", shelfRequest.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveShelfRequest(int shelfRequestId)
        {
            string query = "DELETE FROM shelf_request WHERE id = @ShelfRequestId";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ShelfRequestId", shelfRequestId);

                cmd.ExecuteNonQuery();
            }
        }

        public List<ShelfRequest> RetrieveShelfRequests()
        {
            List<ShelfRequest> shelfRequests = new List<ShelfRequest>();

            string query = "SELECT id, item_id, quantity FROM shelf_request";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ShelfRequest shelfRequest = new ShelfRequest
                        (
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetInt32(reader.GetOrdinal("item_id")),
                            reader.GetInt32(reader.GetOrdinal("quantity"))
                        );
                        shelfRequests.Add(shelfRequest);
                    }
                }
            }

            return shelfRequests;
        }
    }
}
