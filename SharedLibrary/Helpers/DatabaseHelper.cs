﻿using SharedLibrary.Classes;
using SharedLibrary.Enums;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Xml.Linq;
using System;

namespace SharedLibrary.Helpers
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

        public bool AuthenticateAdmin(string email, string password)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM [dbo].[employee] WHERE email = '{email}' AND password = '{password}' AND role IN (1, 2);";
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

        public Employee AuthenticateUser(string email, string password)
        {
            Employee employee = null;

            try
            {
                string query = $"SELECT * FROM [dbo].[employee] WHERE email = '{email}' AND password = '{password}';";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employee = new Employee
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name"))
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error authenticating user: {ex.Message}", ex);
            }

            return employee;
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

        // Department Management ==================================================
        public void AddDepartmentToDB(string name)
        {
            string query = "INSERT INTO department_list (name) VALUES (@Name)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateDepartmentInDB(int id, string name)
        {
            string query = "UPDATE department_list SET name = @Name WHERE id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveDepartmentFromDB(int departmentId)
        {
            string query = "DELETE FROM department_list WHERE id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", departmentId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Department> GetDepartmentsFromDB()
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

        public bool HasEmployeesInDepartment(int id)
        {
            string query = "SELECT COUNT(*) FROM employee WHERE department = @Id;";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // Department Management ==================================================

        // Employee Management ==================================================
        public void AddEmployeeToDB(string name, string email, string password, RoleEnum role, string image, string address, Department department, decimal hourlyWage, bool isActive, string phone_number, int bsn, string bank_account)
        {
            string query = @"INSERT INTO Employee (name, email, password, role, register_date, image, address, department, hourly_wage, is_active, phone_number, bsn, bank_account) 
                         VALUES (@Name, @Email, @Password, @Role, @RegisterDate, @Image, @Address, @Department, @HourlyWage, @IsActive, @PhoneNumber, @Bsn, @BankAccount)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                // Add parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password); // Hash this password
                cmd.Parameters.AddWithValue("@Role", (int)role + 1);
                cmd.Parameters.AddWithValue("@RegisterDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Image", image);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Department", department.Id);
                cmd.Parameters.AddWithValue("@HourlyWage", hourlyWage);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@PhoneNumber", phone_number);
                cmd.Parameters.AddWithValue("@Bsn", bsn);
                cmd.Parameters.AddWithValue("@BankAccount", bank_account);

                cmd.ExecuteNonQuery();
            }
        }

        // Method to update an existing employee
        public void UpdateEmployeeInDB(int id, string name, string email, string password, RoleEnum role, string image, string address, Department department, decimal hourlyWage, bool isActive, string phone_number, int bsn, string bank_account)
        {
            string query = @"UPDATE employee 
                            SET name = @Name, 
                                email = @Email, 
                                password = @Password, 
                                role = @Role, 
                                image = @Image, 
                                address = @Address, 
                                department = @Department, 
                                hourly_wage = @HourlyWage, 
                                is_active = @IsActive,
                                phone_number = @PhoneNumber,
                                bsn = @Bsn,
                                bank_account = @BankAccount
                            WHERE id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password); // Hash this password
                cmd.Parameters.AddWithValue("@Role", (int)role + 1);
                cmd.Parameters.AddWithValue("@Image", image);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Department", department.Id);
                cmd.Parameters.AddWithValue("@HourlyWage", hourlyWage);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@PhoneNumber", phone_number);
                cmd.Parameters.AddWithValue("@Bsn", bsn);
                cmd.Parameters.AddWithValue("@BankAccount", bank_account);

                cmd.ExecuteNonQuery();
            }
        }

        // Method to delete an employee (soft delete by setting is_active to false)
        public void DeactivateEmployeeInDB(int employeeId)
        {
            string query = "UPDATE employee SET is_active = 0 WHERE id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", employeeId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Employee> GetEmployeesFromDB()
        {
            List<Employee> employees = new List<Employee>();
            string query = @"
                SELECT e.*, 
                        d.id AS department_id, 
                        d.name AS department_name 
                FROM employee e 
                LEFT JOIN department_list d 
                ON e.department = d.id;
            ";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Department department = new Department(
                            reader.GetInt32(reader.GetOrdinal("department_id")),
                            reader.GetString(reader.GetOrdinal("department_name"))
                        );

                        employees.Add(new Employee(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("name")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("password")), // Should be hashed
                            (RoleEnum)reader.GetInt32(reader.GetOrdinal("role")) - 1,
                            reader.IsDBNull(reader.GetOrdinal("image")) ? null : reader.GetString(reader.GetOrdinal("image")),
                            reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
                            department,
                            reader.GetDecimal(reader.GetOrdinal("hourly_wage")),
                            reader.GetBoolean(reader.GetOrdinal("is_active")),
                            reader.GetString(reader.GetOrdinal("phone_number")),
                            reader.GetInt32(reader.GetOrdinal("bsn")),
                            reader.GetString(reader.GetOrdinal("bank_account"))
                        ));
                    }
                }
            }
            return employees;
        }

        public Employee GetEmployeeByIdFromDB(int id)
        {
            Employee employee = null;

            string query = @"
                SELECT e.*, d.id as department_id, d.name as department_name
                FROM employee e
                LEFT JOIN department_list d ON e.department = d.id
                WHERE e.id = @Id;
            ";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Department department = new Department(
                            reader.GetInt32(reader.GetOrdinal("department_id")),
                            reader.GetString(reader.GetOrdinal("department_name"))
                        );

                        employee = new Employee(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("name")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("password")), // Should be hashed
                            (RoleEnum)reader.GetInt32(reader.GetOrdinal("role")) - 1,
                            reader.IsDBNull(reader.GetOrdinal("image")) ? null : reader.GetString(reader.GetOrdinal("image")),
                            reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
                            department,
                            reader.GetDecimal(reader.GetOrdinal("hourly_wage")),
                            reader.GetBoolean(reader.GetOrdinal("is_active")),
                            reader.GetString(reader.GetOrdinal("phone_number")),
                            reader.GetInt32(reader.GetOrdinal("bsn")),
                            reader.GetString(reader.GetOrdinal("bank_account"))
                        );
                    }
                }
            }

            return employee;
        }

        public Employee GetEmployeeByEmailFromDB(string email)
        {
            Employee employee = null;

            string query = @"
                SELECT e.*, d.id as department_id, d.name as department_name
                FROM employee e
                LEFT JOIN department_list d ON e.department = d.id
                WHERE e.email = @Email;
            ";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Email", email);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Department department = new Department(
                            reader.GetInt32(reader.GetOrdinal("department_id")),
                            reader.GetString(reader.GetOrdinal("department_name"))
                        );

                        employee = new Employee(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("name")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("password")), // Should be hashed
                            (RoleEnum)reader.GetInt32(reader.GetOrdinal("role")) - 1,
                            reader.IsDBNull(reader.GetOrdinal("image")) ? null : reader.GetString(reader.GetOrdinal("image")),
                            reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
                            department,
                            reader.GetDecimal(reader.GetOrdinal("hourly_wage")),
                            reader.GetBoolean(reader.GetOrdinal("is_active")),
                            reader.GetString(reader.GetOrdinal("phone_number")),
                            reader.GetInt32(reader.GetOrdinal("bsn")),
                            reader.GetString(reader.GetOrdinal("bank_account"))
                        );
                    }
                }
            }

            return employee;
        }

        public List<Employee> GetUnassignedEmployeesToShiftFromDB(DateOnly date)
        {
            List<Employee> employees = new List<Employee>();

            string query = @"
                SELECT e.*, d.id as department_id, d.name as department_name
                FROM employee e
                LEFT JOIN department_list d ON e.department = d.id
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM shift_employee se
                    INNER JOIN shift s ON se.shift_id = s.id
                    WHERE se.employee_id = e.id
                    AND s.date = @Date
                );
            ";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Date", new DateTime(date.Year, date.Month, date.Day));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Department department = new Department(
                            reader.GetInt32(reader.GetOrdinal("department_id")),
                            reader.GetString(reader.GetOrdinal("department_name"))
                        );

                        employees.Add(new Employee(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("name")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("password")), // Should be hashed
                            (RoleEnum)reader.GetInt32(reader.GetOrdinal("role")) - 1,
                            reader.IsDBNull(reader.GetOrdinal("image")) ? null : reader.GetString(reader.GetOrdinal("image")),
                            reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
                            department,
                            reader.GetDecimal(reader.GetOrdinal("hourly_wage")),
                            reader.GetBoolean(reader.GetOrdinal("is_active")),
                            reader.GetString(reader.GetOrdinal("phone_number")),
                            reader.GetInt32(reader.GetOrdinal("bsn")),
                            reader.GetString(reader.GetOrdinal("bank_account"))
                        ));
                    }
                }
            }

            return employees;
        }

        public List<Employee> GetAssignedEmployeesToShiftFromDB(DateOnly date, ShiftTypeEnum shiftType)
        {
            List<Employee> employees = new List<Employee>();

            string query = @"
                SELECT e.*, d.id as department_id, d.name as department_name
                FROM employee e
                LEFT JOIN shift_employee se ON e.id = se.employee_id
                LEFT JOIN shift s ON se.shift_id = s.id
                LEFT JOIN department_list d ON e.department = d.id
                WHERE s.date = @Date AND s.shift_type = @ShiftType
            ";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Date", new DateTime(date.Year, date.Month, date.Day));
                cmd.Parameters.AddWithValue("@ShiftType", shiftType + 1);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Department department = new Department(
                            reader.GetInt32(reader.GetOrdinal("department_id")),
                            reader.GetString(reader.GetOrdinal("department_name"))
                        );

                        employees.Add(new Employee(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("name")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("password")), // Should be hashed
                            (RoleEnum)reader.GetInt32(reader.GetOrdinal("role")) - 1,
                            reader.IsDBNull(reader.GetOrdinal("image")) ? null : reader.GetString(reader.GetOrdinal("image")),
                            reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
                            department,
                            reader.GetDecimal(reader.GetOrdinal("hourly_wage")),
                            reader.GetBoolean(reader.GetOrdinal("is_active")),
                            reader.GetString(reader.GetOrdinal("phone_number")),
                            reader.GetInt32(reader.GetOrdinal("bsn")),
                            reader.GetString(reader.GetOrdinal("bank_account"))
                        ));
                    }
                }
            }

            return employees;
        }


        public bool IsEmployeeNameUniqueInDB(string name)
        {
            string query = "SELECT COUNT(*) FROM employee WHERE name = @Name;";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                int count = (int)command.ExecuteScalar();

                return count == 0;
            }
        }

        public bool IsEmployeeEmailUniqueInDB(string email)
        {
            string query = "SELECT COUNT(*) FROM employee WHERE email = @Email;";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                int count = (int)command.ExecuteScalar();

                return count == 0;
            }
        }

        public bool IsEmailUniqueExceptCurrentEmployeeInDB(string email, int employeeId)
        {
            bool isUnique = true;

            try
            {
                string query = "SELECT COUNT(*) FROM employee WHERE email = @Email AND id != @EmployeeId;";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    int count = (int)cmd.ExecuteScalar();

                    isUnique = count == 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return isUnique;
        }

        public bool IsNameUniqueExceptCurrentEmployeeinDB(string name, int employeeId)
        {
            bool isUnique = true;

            try
            {
                string query = "SELECT COUNT(*) FROM employee WHERE name = @Name AND id != @EmployeeId;";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    int count = (int)cmd.ExecuteScalar();

                    isUnique = count == 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return isUnique;
        }

        // Employee Management ==================================================

        // Item Management ==================================================
        public void AddItemToDB(int sku, string name, int quantitywarehouse, int quantitystore, CategoryEnum category, double wholesaleprice, double sellprice, DateTime experationdate)
        {
            string query = "INSERT INTO item (sku, name, quantity_warehouse, quantity_store, category, wholesale_price, sell_price, experationdate) " +
                "VALUES (@Sku, @Name, @QuantityWarehouse, @QuantityStore, @Category, @WholePrice, @SellPrice, @Experationdate)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Sku", sku);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@QuantityWarehouse", quantitywarehouse);
                cmd.Parameters.AddWithValue("@QuantityStore", quantitystore);
                cmd.Parameters.AddWithValue("@Category", (int)category + 1);
                cmd.Parameters.AddWithValue("@WholePrice", wholesaleprice);
                cmd.Parameters.AddWithValue("@SellPrice", sellprice);
                cmd.Parameters.AddWithValue("@Experationdate", experationdate);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateItemInDB(int id, int sku, string name, int quantitywarehouse, int quantitystore, CategoryEnum category, double wholesaleprice, double sellprice, DateTime experationdate)
        {
            string query = "UPDATE item SET sku = @Sku, name = @Name, quantity_warehouse = @QuantityWarehouse, " +
                           "quantity_store = @QuantityStore, category = @Category, wholesale_price = @WholePrice, " +
                           "sell_price = @SellPrice, experationdate = @Experationdate WHERE id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Sku", sku);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@QuantityWarehouse", quantitywarehouse);
                cmd.Parameters.AddWithValue("@QuantityStore", quantitystore);
                cmd.Parameters.AddWithValue("@Category", (int)category + 1);
                cmd.Parameters.AddWithValue("@WholePrice", wholesaleprice);
                cmd.Parameters.AddWithValue("@SellPrice", sellprice);
                cmd.Parameters.AddWithValue("@Experationdate", experationdate);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveItemFromDB(int itemId)
        {
            string query = "DELETE FROM item WHERE id = @ItemId";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ItemId", itemId);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Item> GetItemsFromDB()
        {
            List<Item> items = new List<Item>();

            string query = "SELECT id, sku, name, quantity_warehouse, quantity_store, category, wholesale_price, sell_price, experationdate FROM item";

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
                            reader.GetDouble(reader.GetOrdinal("sell_price")),
                            reader.GetDateTime(reader.GetOrdinal("experationdate"))
                        );
                        items.Add(item);
                    }
                }
            }

            return items;
        }

        public Item GetItemByIdFromDB(int id)
        {
            Item item = null;

            string query = "SELECT * FROM item WHERE id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = new Item
                        (
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetInt32(reader.GetOrdinal("sku")),
                            reader.GetString(reader.GetOrdinal("name")),
                            reader.GetInt32(reader.GetOrdinal("quantity_warehouse")),
                            reader.GetInt32(reader.GetOrdinal("quantity_store")),
                            (CategoryEnum)reader.GetInt32(reader.GetOrdinal("category")) - 1,
                            reader.GetDouble(reader.GetOrdinal("wholesale_price")),
                            reader.GetDouble(reader.GetOrdinal("sell_price")),
                            reader.GetDateTime(reader.GetOrdinal("experationdate"))
                        );
                    }
                }
            }

            return item;
        }

        public List<int> ListItemIdInDatabase()
        {
            List<int> Ids = new List<int>();

            string query = "SELECT id FROM item";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Ids.Add(reader.GetInt32(reader.GetOrdinal("id")));
                    }
                }
            }

            return Ids;
        }

        // Item Management ==================================================

        // ShelfRequest Management ==================================================
        public void AddShelfRequestToDB(int itemId, int quantity, ShelfRequestType type)
        {
            string query = "INSERT INTO shelf_request (item_id, quantity, type) " +
                "VALUES (@ItemId, @Quantity, @Type)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ItemId", itemId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Type",  (int)type);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateShelfRequest(int id, int itemId, int quantity, ShelfRequestType type)
		{
            string query = "UPDATE shelf_request SET item_id = @ItemId, quantity = @Quantity, type = @Type WHERE id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ItemId", itemId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
				cmd.Parameters.AddWithValue("@Type", (int)type);
				cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }

        public void FulFillShelfRequestInDB(int shelfRequestId)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlTransaction transaction = connection.BeginTransaction();
                cmd.Transaction = transaction;

                try
                {
                    string selectQuery = "SELECT item_id, quantity, type FROM shelf_request WHERE id = @ShelfRequestId";
                    cmd.CommandText = selectQuery;
                    cmd.Parameters.AddWithValue("@ShelfRequestId", shelfRequestId);
                    cmd.Connection = connection;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int itemId = reader.GetInt32(reader.GetOrdinal("item_id"));
                            int quantityToAdd = reader.GetInt32(reader.GetOrdinal("quantity"));
							ShelfRequestType type = (ShelfRequestType)reader.GetInt32(reader.GetOrdinal("type"));

                            reader.Close();

                            if (type == ShelfRequestType.INVENTORY)
                            {
								string checkQuantityQuery = "SELECT quantity_warehouse FROM item WHERE id = @ItemId";
								cmd.CommandText = checkQuantityQuery;
								cmd.Parameters.Clear();
								cmd.Parameters.AddWithValue("@ItemId", itemId);
								int warehouseQuantity = Convert.ToInt32(cmd.ExecuteScalar());

								if (warehouseQuantity < quantityToAdd)
								{
									transaction.Rollback();
									throw new Exception("Not enough items in the warehouse.");
								}

								string updateQuery = @"
                                UPDATE item 
                                SET 
                                quantity_warehouse = quantity_warehouse - @QuantityToAdd,
                                quantity_store = quantity_store + @QuantityToAdd 
                                WHERE id = @ItemId";
								cmd.CommandText = updateQuery;
								cmd.Parameters.Clear();
								cmd.Parameters.AddWithValue("@QuantityToAdd", quantityToAdd);
								cmd.Parameters.AddWithValue("@ItemId", itemId);
								cmd.ExecuteNonQuery();
							}
                            else if (type == ShelfRequestType.WAREHOUSE)
                            {
								string updateQuery = @"
                                UPDATE item 
                                SET 
                                quantity_warehouse = quantity_warehouse + @QuantityToAdd
                                WHERE id = @ItemId";
								cmd.CommandText = updateQuery;
								cmd.Parameters.Clear();
								cmd.Parameters.AddWithValue("@QuantityToAdd", quantityToAdd);
								cmd.Parameters.AddWithValue("@ItemId", itemId);
								cmd.ExecuteNonQuery();
							}
                        }
                        else
                        {
                            transaction.Rollback();
                            throw new Exception("Shelf request not found");
                        }
                    }

                    string deleteQuery = "DELETE FROM shelf_request WHERE id = @ShelfRequestId";
                    cmd.CommandText = deleteQuery;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ShelfRequestId", shelfRequestId);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (transaction.Connection != null && transaction.Connection.State == ConnectionState.Open)
                    {
                        transaction.Rollback();
                    }
                    throw new Exception("Failed to fulfill shelf request: " + ex.Message);
                }
            }
        }


		public List<ShelfRequest> GetShelfRequestFromDB(ShelfRequestType? type = null)
		{
			List<ShelfRequest> shelfRequests = new List<ShelfRequest>();

            string query = @"
                SELECT s.*, i.id as item_id, i.name as item_name
                FROM shelf_request s
                LEFT JOIN item i ON s.item_id = i.id
            ";

            if (type != null)
            {
                query += " WHERE type = @Type";
			}

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                if (type != null)
                { 
                    cmd.Parameters.AddWithValue("@Type", type);
                }

				using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string itemName = reader.GetString("item_name");

                        ShelfRequest shelfRequest = new ShelfRequest
                        (
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetInt32(reader.GetOrdinal("item_id")),
                            itemName,
                            reader.GetInt32(reader.GetOrdinal("quantity")),
                            (ShelfRequestType)reader.GetInt32(reader.GetOrdinal("type"))
                        );
                        shelfRequests.Add(shelfRequest);
                    }
                }
            }

            return shelfRequests;
        }
        // ShelfRequest Management ==================================================

        // Shift Management ==================================================
        public List<Shift> GetShiftsFromDB()
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
                            (ShiftTypeEnum)reader.GetInt32(reader.GetOrdinal("shift_type")) - 1,
                            reader.GetDateTime(reader.GetOrdinal("start_time")),
                            reader.GetDateTime(reader.GetOrdinal("end_time"))
                        );

                        shifts.Add(shift);
                    }
                }
            }

            return shifts;
        }

        public void AssignEmployeeToShiftInDB(Employee employee, DateOnly date, ShiftTypeEnum shiftType, DateTime? startTime = null, DateTime? endTime = null)
        {
            SqlTransaction transaction = null;

            try
            {
                int shiftId = -1;

                transaction = connection.BeginTransaction();

                string selectQuery = "SELECT id FROM shift WHERE date = @Date AND shift_type = @ShiftType;";
                using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection, transaction))
                {
                    selectCmd.Parameters.AddWithValue("@Date", new DateTime(date.Year, date.Month, date.Day));
                    selectCmd.Parameters.AddWithValue("@ShiftType", (int)shiftType + 1);
                    object result = selectCmd.ExecuteScalar();

                    if (result != null)
                    {
                        shiftId = Convert.ToInt32(result);
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO shift (date, shift_type, start_time, end_time) OUTPUT INSERTED.Id VALUES (@Date, @ShiftType, @StartDate, @EndDate);";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection, transaction))
                        {
                            DateTime startShiftDateTime = startTime ?? date.ToDateTime(shiftType == ShiftTypeEnum.Morning ? new TimeOnly(9, 0) : new TimeOnly(16, 0));
                            DateTime endShiftDateTime = endTime ?? date.ToDateTime(shiftType == ShiftTypeEnum.Morning ? new TimeOnly(16, 0) : new TimeOnly(23, 0));

                            insertCmd.Parameters.AddWithValue("@Date", new DateTime(date.Year, date.Month, date.Day));
                            insertCmd.Parameters.AddWithValue("@ShiftType", (int)shiftType + 1);
                            insertCmd.Parameters.AddWithValue("@StartDate", startShiftDateTime);
                            insertCmd.Parameters.AddWithValue("@EndDate", endShiftDateTime);
                            shiftId = Convert.ToInt32(insertCmd.ExecuteScalar());
                        }
                    }
                }

                if (shiftId != -1)
                {
                    string assignQuery = "INSERT INTO shift_employee (shift_id, employee_id) VALUES (@ShiftId, @EmployeeId);";
                    using (SqlCommand assignCmd = new SqlCommand(assignQuery, connection, transaction))
                    {
                        assignCmd.Parameters.AddWithValue("@ShiftId", shiftId);
                        assignCmd.Parameters.AddWithValue("@EmployeeId", employee.Id);
                        assignCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                else
                {
                    throw new Exception($"Failed to create or find {shiftType} shift for {date.ToShortDateString()}.");
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw new Exception(ex.Message);
            }
        }

        public void UnassignEmployeeFromShiftInDB(Employee employee, DateOnly date, ShiftTypeEnum shiftType)
        {
            SqlTransaction transaction = null;

            try
            {
                string selectQuery = "SELECT id FROM shift WHERE date = @Date AND shift_type = @ShiftType;";
                int shiftId = -1;

                using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection, transaction))
                {
                    selectCmd.Parameters.AddWithValue("@Date", new DateTime(date.Year, date.Month, date.Day));
                    selectCmd.Parameters.AddWithValue("@ShiftType", (int)shiftType + 1);
                    object result = selectCmd.ExecuteScalar();

                    if (result != null)
                    {
                        shiftId = Convert.ToInt32(result);
                    }
                }

                if (shiftId != -1)
                {
                    transaction = connection.BeginTransaction();

                    string deleteQuery = "DELETE FROM shift_employee WHERE shift_id = @ShiftId AND employee_id = @EmployeeId;";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection, transaction))
                    {
                        deleteCmd.Parameters.AddWithValue("@ShiftId", shiftId);
                        deleteCmd.Parameters.AddWithValue("@EmployeeId", employee.Id);
                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception($"Employee {employee.Name} is not assigned to {shiftType} shift on {date.ToShortDateString()}.");
                        }
                    }

                    transaction.Commit();
                }
                else
                {
                    throw new Exception($"No {shiftType} shift found for {date.ToShortDateString()}.");
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw new Exception(ex.Message);
            }
        }

        public int GetAssignedEmployeeCountForShiftFromDB(DateOnly date, ShiftTypeEnum shiftType)
        {
            int assignedEmployeeCount = 0;

            try
            {
                string selectShiftIdQuery = "SELECT id FROM shift WHERE date = @Date AND shift_type = @ShiftType;";
                int shiftId = -1;

                using (SqlCommand selectCmd = new SqlCommand(selectShiftIdQuery, connection))
                {
                    selectCmd.Parameters.AddWithValue("@Date", new DateTime(date.Year, date.Month, date.Day));
                    selectCmd.Parameters.AddWithValue("@ShiftType", (int)shiftType + 1);
                    object result = selectCmd.ExecuteScalar();

                    if (result != null)
                    {
                        shiftId = Convert.ToInt32(result);
                    }
                }

                if (shiftId != -1)
                {
                    string countQuery = "SELECT COUNT(*) FROM shift_employee WHERE shift_id = @ShiftId;";
                    using (SqlCommand countCmd = new SqlCommand(countQuery, connection))
                    {
                        countCmd.Parameters.AddWithValue("@ShiftId", shiftId);
                        assignedEmployeeCount = (int)countCmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return assignedEmployeeCount;
        }

        public (DateTime, DateTime) GetShiftTimesFromDB(DateOnly date, ShiftTypeEnum shiftType)
        {
            DateTime startShiftDateTime = date.ToDateTime(shiftType == ShiftTypeEnum.Morning ? new TimeOnly(9, 0) : new TimeOnly(16, 0));
            DateTime endShiftDateTime = date.ToDateTime(shiftType == ShiftTypeEnum.Morning ? new TimeOnly(16, 0) : new TimeOnly(23, 0));

            (DateTime, DateTime) shiftTime = (startShiftDateTime, endShiftDateTime);

            string query = "SELECT start_time, end_time FROM shift WHERE date = @Date AND shift_type = @ShiftType;";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Date", new DateTime(date.Year, date.Month, date.Day));
                cmd.Parameters.AddWithValue("@ShiftType", (int)shiftType + 1);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        shiftTime = (reader.GetDateTime(reader.GetOrdinal("start_time")), reader.GetDateTime(reader.GetOrdinal("end_time")));
                    }
                }
            }

            return shiftTime;
        }

        public void AddOrUpdateShiftToDB(DateOnly date, ShiftTypeEnum shiftType, DateTime startTime, DateTime endTime)
        {
            string query = "SELECT COUNT(*) FROM shift WHERE date = @Date AND shift_type = @ShiftType";
            using (SqlCommand selectCmd = new SqlCommand(query, connection))
            {
                selectCmd.Parameters.AddWithValue("@Date", new DateTime(date.Year, date.Month, date.Day));
                selectCmd.Parameters.AddWithValue("@ShiftType", (int)shiftType + 1);
                int count = (int)selectCmd.ExecuteScalar();

                if (count > 0)
                {
                    string updateQuery = "UPDATE shift SET start_time = @StartTime, end_time = @EndTime WHERE date = @Date AND shift_type = @ShiftType";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
                    {
                        updateCmd.Parameters.AddWithValue("@StartTime", startTime);
                        updateCmd.Parameters.AddWithValue("@EndTime", endTime);
                        updateCmd.Parameters.AddWithValue("@Date", new DateTime(date.Year, date.Month, date.Day));
                        updateCmd.Parameters.AddWithValue("@ShiftType", (int)shiftType + 1);
                        updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string insertQuery = "INSERT INTO shift (date, shift_type, start_time, end_time) VALUES (@Date, @ShiftType, @StartTime, @EndTime)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                    insertCmd.Parameters.AddWithValue("@Date", new DateTime(date.Year, date.Month, date.Day));
                    insertCmd.Parameters.AddWithValue("@ShiftType", (int)shiftType + 1);
                    insertCmd.Parameters.AddWithValue("@StartTime", startTime);
                    insertCmd.Parameters.AddWithValue("@EndTime", endTime);
                    insertCmd.ExecuteNonQuery();
                }
            }
        }
        // Shift Management ==================================================

        // Dashboard Management ==================================================

        public List<Employee> GetEmployeeByRoleFromDB(RoleEnum role)
        {
            List<Employee> employees = new List<Employee>();

            string query = @"
                SELECT e.*, d.id as department_id, d.name as department_name
                FROM employee e
                LEFT JOIN department_list d ON e.department = d.id
                WHERE e.role = @Role;
            ";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Role", (int)role + 1);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Department department = new Department(
                            reader.GetInt32(reader.GetOrdinal("department_id")),
                            reader.GetString(reader.GetOrdinal("department_name"))
                         );

                        employees.Add(new Employee(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("name")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("password")),
                            (RoleEnum)reader.GetInt32(reader.GetOrdinal("role")) - 1,
                            reader.IsDBNull(reader.GetOrdinal("image")) ? null : reader.GetString(reader.GetOrdinal("image")),
                            reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
                            department,
                            reader.GetDecimal(reader.GetOrdinal("hourly_wage")),
                            reader.GetBoolean(reader.GetOrdinal("is_active")),
                            reader.GetString(reader.GetOrdinal("phone_number")),
                            reader.GetInt32(reader.GetOrdinal("bsn")),
                            reader.GetString(reader.GetOrdinal("bank_account"))
                        ));
                    }
                }
            }
            return employees;
        }

        public List<Employee> GetEmployeeByDepartmentFromDB(Department department)
        {
            List<Employee> employees = new List<Employee>();

            string query = @"
                SELECT e.*, d.id as department_id, d.name as department_name
                FROM employee e
                LEFT JOIN department_list d ON e.department = d.id
                WHERE e.department = @DepartmentId;
            ";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@DepartmentId", department.Id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Department employeeDepartment = new Department(
                            reader.GetInt32(reader.GetOrdinal("department_id")),
                            reader.GetString(reader.GetOrdinal("department_name"))
                        );

                        employees.Add(new Employee(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("name")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("password")),
                            (RoleEnum)reader.GetInt32(reader.GetOrdinal("role")) - 1,
                            reader.IsDBNull(reader.GetOrdinal("image")) ? null : reader.GetString(reader.GetOrdinal("image")),
                            reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
                            employeeDepartment,
                            reader.GetDecimal(reader.GetOrdinal("hourly_wage")),
                            reader.GetBoolean(reader.GetOrdinal("is_active")),
                            reader.GetString(reader.GetOrdinal("phone_number")),
                            reader.GetInt32(reader.GetOrdinal("bsn")),
                            reader.GetString(reader.GetOrdinal("bank_account"))
                        ));
                    }
                }
            }

            return employees;
        }

        public List<Employee> GetEmployeeByRoleAndDepartmentFromDB(RoleEnum role, Department department)
        {
            List<Employee> employees = new List<Employee>();

            string query = @"
                SELECT e.*, d.id as department_id, d.name as department_name
                FROM employee e
                LEFT JOIN department_list d ON e.department = d.id
                WHERE e.role = @Role
                AND e.department = @DepartmentId;
            ";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Role", (int)role + 1);
                cmd.Parameters.AddWithValue("@DepartmentId", department.Id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Department employeeDepartment = new Department(
                            reader.GetInt32(reader.GetOrdinal("department_id")),
                            reader.GetString(reader.GetOrdinal("department_name"))
                        );

                        employees.Add(new Employee(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("name")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("password")),
                            (RoleEnum)reader.GetInt32(reader.GetOrdinal("role")) - 1,
                            reader.IsDBNull(reader.GetOrdinal("image")) ? null : reader.GetString(reader.GetOrdinal("image")),
                            reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address")),
                            employeeDepartment,
                            reader.GetDecimal(reader.GetOrdinal("hourly_wage")),
                            reader.GetBoolean(reader.GetOrdinal("is_active")),
                            reader.GetString(reader.GetOrdinal("phone_number")),
                            reader.GetInt32(reader.GetOrdinal("bsn")),
                            reader.GetString(reader.GetOrdinal("bank_account"))
                        ));
                    }
                }
            }
            return employees;
        }

        public List<Shift> GetEmployeeAllShiftsFromDB30d(int employeeId)
        {
            List<Shift> shifts = new List<Shift>();

            string query = @"
                SELECT s.id, s.date, s.shift_type, s.start_time, s.end_time
                FROM shift s
                INNER JOIN shift_employee se ON s.id = se.shift_id
                WHERE se.employee_id = @EmployeeId
                AND s.date >= DATEADD(day, -30, GETDATE());";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

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
                            (ShiftTypeEnum)reader.GetInt32(reader.GetOrdinal("shift_type")) - 1,
                            reader.GetDateTime(reader.GetOrdinal("start_time")),
                            reader.GetDateTime(reader.GetOrdinal("end_time"))
                        );

                        shifts.Add(shift);
                    }
                }
            }

            return shifts;
        }

        public double GetEmployeeAttendancePercentageFromDB(int employeeId)
        {
            double attendancePercentage = 0;

            try
            {
                string query = @"
                    SELECT COUNT(*) AS total_shifts, 
                           COUNT(se.employee_id) AS attended_shifts
                    FROM shift s
                    LEFT JOIN shift_employee se ON s.id = se.shift_id
                    WHERE se.employee_id = @EmployeeId;
                ";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int totalShifts = reader.GetInt32(reader.GetOrdinal("total_shifts"));
                            int attendedShifts = reader.GetInt32(reader.GetOrdinal("attended_shifts"));

                            if (totalShifts > 0)
                            {
                                attendancePercentage = (double)attendedShifts / totalShifts;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return attendancePercentage;
        }

        // Get GetEmployeeShiftsOnDateFromDB
        public List<Shift> GetEmployeeShiftsOnDateFromDB(int employeeId, DateOnly date)
        {
            List<Shift> shifts = new List<Shift>();

            string query = @"
                SELECT s.id, s.date, s.shift_type, s.start_time, s.end_time
                FROM shift s
                INNER JOIN shift_employee se ON s.id = se.shift_id
                WHERE se.employee_id = @EmployeeId
                AND s.date = @Date;";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                cmd.Parameters.AddWithValue("@Date", new DateTime(date.Year, date.Month, date.Day));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("date"));
                        DateOnly shiftDate = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

                        Shift shift = new Shift
                        (
                            reader.GetInt32(reader.GetOrdinal("id")),
                            shiftDate,
                            (ShiftTypeEnum)reader.GetInt32(reader.GetOrdinal("shift_type")) - 1,
                            reader.GetDateTime(reader.GetOrdinal("start_time")),
                            reader.GetDateTime(reader.GetOrdinal("end_time"))
                        );

                        shifts.Add(shift);
                    }
                }
            }

            return shifts;
        }        
        
        public List<Shift> GetEmployeeShiftsOnMonthFromDB(int employeeId, int month, int year)
        {
            List<Shift> shifts = new List<Shift>();

            string query = @"
                SELECT s.id, s.date, s.shift_type, s.start_time, s.end_time
                FROM shift s
                INNER JOIN shift_employee se ON s.id = se.shift_id
                WHERE employee_id = @EmployeeId
                AND DATEPART(month, s.date) = @Month
                AND DATEPART(year, s.date) = @Year";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Year", year);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("date"));
                        DateOnly shiftDate = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

                        Shift shift = new Shift
                        (
                            reader.GetInt32(reader.GetOrdinal("id")),
                            shiftDate,
                            (ShiftTypeEnum)reader.GetInt32(reader.GetOrdinal("shift_type")) - 1,
                            reader.GetDateTime(reader.GetOrdinal("start_time")),
                            reader.GetDateTime(reader.GetOrdinal("end_time"))
                        );

                        shifts.Add(shift);
                    }
                }
            }

            return shifts;
        }

        public Dictionary<Tuple<DateOnly, ShiftTypeEnum>, int> GetShiftEmployeeCountForMonthFromDB(int month, int year)
        {
            Dictionary<Tuple<DateOnly, ShiftTypeEnum>, int> employeeCountByShift = new Dictionary<Tuple<DateOnly, ShiftTypeEnum>, int>();

            string query = @"
                            SELECT s.date, s.shift_type, COUNT(*) AS shift_count
                            FROM shift s
                            INNER JOIN shift_employee se ON s.id = se.shift_id
                            WHERE DATEPART(month, s.date) = @Month AND DATEPART(year, s.date) = @Year
                            GROUP BY s.date,s.shift_type;";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Year", year);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime dateTime = reader.GetDateTime(0);
                        ShiftTypeEnum shiftType = (ShiftTypeEnum)reader.GetInt32(1) -1;
                        int shiftCount = reader.GetInt32(2);

                        DateOnly shiftDate = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
                        Tuple<DateOnly, ShiftTypeEnum> key = new Tuple<DateOnly, ShiftTypeEnum>(shiftDate, shiftType);

                        employeeCountByShift[key] = shiftCount;
                    }
                }
            }

            return employeeCountByShift;
        }

        //daysoffrequest database management
        public List<DaysOffRequest> GetDaysOffRequestsFromDatabase()
        {
            List<DaysOffRequest> daysOffRequests = new List<DaysOffRequest>();

            string query = @"
                SELECT d.*, e.id as employee_id, e.name as employee_name
                FROM dayoffrequest d
                LEFT JOIN employee e ON d.employee_id = e.id;
            " ;

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string employeeName = reader.GetString("employee_name");
                        int intStatus = reader.GetInt32(reader.GetOrdinal("status"));
                        string status;
                        if (intStatus == 0)
                        {
                            status = "pending";
                        }
                        else if (intStatus == 1)
                        {
                            status = "aproved";
                        }
                        else if (intStatus == 2)
                        {
                            status = "declined";
                        }
                        else
                        {
                            status = "failed to load";
                        }
                        DaysOffRequest daysOffRequest = new DaysOffRequest
                        (
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetInt32(reader.GetOrdinal("employee_id")),
                            employeeName,
                            reader.GetDateTime(reader.GetOrdinal("start_date")),
                            reader.GetDateTime(reader.GetOrdinal("end_date")),
                            reader.GetString(reader.GetOrdinal("description")),
                            status,
                            reader.GetString(reader.GetOrdinal("type"))
                        );
                        daysOffRequests.Add(daysOffRequest);
                    }
                }
            }

            return daysOffRequests;
        }

        public void AddDaysOffRequestToDatabase(int employeeId, DateTime startDate, DateTime endDate, string description, int status, string type)
        {
            string query = "INSERT INTO dayoffrequest (employee_id, start_date, end_date, description, status, type) " +
                "VALUES (@EmployeeId, @StartDate, @EndDate, @Description, @Status, @Type)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Type", type);

                cmd.ExecuteNonQuery();
            }
        }

        public void ChangeStatusDaysOffRequest(int id, int status)
        {
            string query = @"UPDATE dayoffrequest 
                            SET status = @Status
                            WHERE id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Status", status);

                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveDaysOffRequestToDatabase(int id)
        {
            string query = "DELETE FROM dayoffrequest WHERE id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }

        public List<int> GetEmployeeShiftIDs(int employeeID)
        {
            string query = "SELECT * FROM shift_employee WHERE employee_id = @employeeID";

            List<int> shiftIDs = new List<int>();

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@employeeID", employeeID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shiftIDs.Add(reader.GetInt32(reader.GetOrdinal("shift_id")));
                    }
                }
            }

            return shiftIDs;
        }

        public List<Shift> GetEmployeeSchedule30D(int employeeID)
        {
            List<Shift> EmployeeSchedule = new List<Shift>();

            DateOnly currentDate = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            DateOnly endDate = (new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)).AddDays(7);

            // Find current weeks monday
            while (currentDate.DayOfWeek != DayOfWeek.Monday)
            {
                currentDate = currentDate.AddDays(-1);
            }

            while (endDate.DayOfWeek != DayOfWeek.Sunday) // Find the next Monday
            {
                endDate = endDate.AddDays(1);
            }

            OpenConnection();

            List<int> employeeShiftIDs = GetEmployeeShiftIDs(employeeID);

            string query = "SELECT * FROM shift WHERE id = @shiftID AND date >= @currentDate AND date <= @endDate";

            foreach (int shiftID in employeeShiftIDs)
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@shiftID", shiftID);
                    cmd.Parameters.AddWithValue("@currentDate", new DateTime(currentDate.Year, currentDate.Month, currentDate.Day));
                    cmd.Parameters.AddWithValue("@endDate", new DateTime(endDate.Year, endDate.Month, endDate.Day));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("date"));
                            DateOnly date = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

                            Shift shift = new Shift
                            (
                                reader.GetInt32(reader.GetOrdinal("id")),
                                date,
                                (ShiftTypeEnum)reader.GetInt32(reader.GetOrdinal("shift_type")) - 1,
                                reader.GetDateTime(reader.GetOrdinal("start_time")),
                                reader.GetDateTime(reader.GetOrdinal("end_time"))
                            );

                            EmployeeSchedule.Add(shift);
                        }
                    }
                }
            }
            return EmployeeSchedule;
        }
    }
}