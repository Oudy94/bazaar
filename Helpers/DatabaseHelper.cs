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
        // Department Management ==================================================

        // Employee Management ==================================================
        public void AddEmployeeToDB(string name, string email, string password, RoleEnum role, string image, string address, Department department, decimal hourlyWage, bool isActive)
        {
            string query = @"INSERT INTO Employee (name, email, password, role, register_date, image, address, department, hourly_wage, is_active) 
                         VALUES (@Name, @Email, @Password, @Role, @RegisterDate, @Image, @Address, @Department, @HourlyWage, @IsActive)";
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

                cmd.ExecuteNonQuery();
            }
        }

        // Method to update an existing employee
        public void UpdateEmployeeInDB(int id, string name, string email, string password, RoleEnum role, string image, string address, Department department, decimal hourlyWage, bool isActive)
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
                             is_active = @IsActive 
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
                            reader.GetBoolean(reader.GetOrdinal("is_active"))
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
                WHERE e.department = @Id;
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
                            reader.GetBoolean(reader.GetOrdinal("is_active"))
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
                            reader.GetBoolean(reader.GetOrdinal("is_active"))
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
                            reader.GetBoolean(reader.GetOrdinal("is_active"))
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
                            reader.GetBoolean(reader.GetOrdinal("is_active"))
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
        public void AddItemToDB(int sku, string name, int quantitywarehouse, int quantitystore, CategoryEnum category, double wholesaleprice, double sellprice)
        {
            string query = "INSERT INTO item (sku, name, quantity_warehouse, quantity_store, category, wholesale_price, sell_price) " +
                "VALUES (@Sku, @Name, @QuantityWarehouse, @QuantityStore, @Category, @WholePrice, @SellPrice)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Sku", sku);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@QuantityWarehouse", quantitywarehouse);
                cmd.Parameters.AddWithValue("@QuantityStore", quantitystore);
                cmd.Parameters.AddWithValue("@Category", (int)category + 1);
                cmd.Parameters.AddWithValue("@WholePrice", wholesaleprice);
                cmd.Parameters.AddWithValue("@SellPrice", sellprice);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateItemInDB(int id, int sku, string name, int quantitywarehouse, int quantitystore, CategoryEnum category, double wholesaleprice, double sellprice)
        {
            string query = "UPDATE item SET sku = @Sku, name = @Name, quantity_warehouse = @QuantityWarehouse, " +
                           "quantity_store = @QuantityStore, category = @Category, wholesale_price = @WholePrice, " +
                           "sell_price = @SellPrice WHERE id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Sku", sku);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@QuantityWarehouse", quantitywarehouse);
                cmd.Parameters.AddWithValue("@QuantityStore", quantitystore);
                cmd.Parameters.AddWithValue("@Category", (int)category + 1);
                cmd.Parameters.AddWithValue("@WholePrice", wholesaleprice);
                cmd.Parameters.AddWithValue("@SellPrice", sellprice);
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
                            reader.GetDouble(reader.GetOrdinal("sell_price"))
                        );
                    }
                }
            }

            return item;
        }

        // Item Management ==================================================

        // ShelfRequest Management ==================================================
        public void AddShelfRequestToDB(int itemId, int quantity)
        {
            string query = "INSERT INTO shelf_request (item_id, quantity) " +
                "VALUES (@ItemId, @Quantity)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ItemId", itemId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateShelfRequest(int id, int itemId, int quantity)
        {
            string query = "UPDATE shelf_request SET item_id = @ItemId, quantity = @Quantity WHERE id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ItemId", itemId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
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
                    string selectQuery = "SELECT item_id, quantity FROM shelf_request WHERE id = @ShelfRequestId";
                    cmd.CommandText = selectQuery;
                    cmd.Parameters.AddWithValue("@ShelfRequestId", shelfRequestId);
                    cmd.Connection = connection;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int itemId = reader.GetInt32(reader.GetOrdinal("item_id"));
                            int quantityToAdd = reader.GetInt32(reader.GetOrdinal("quantity"));
                            reader.Close();

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


        public List<ShelfRequest> GetShelfRequestFromDB()
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
                            (ShiftTypeEnum)reader.GetInt32(reader.GetOrdinal("shift_type")) - 1
                        );

                        shifts.Add(shift);
                    }
                }
            }

            return shifts;
        }

        public void AssignEmployeeToShiftInDB(Employee employee, DateOnly date, ShiftTypeEnum shiftType)
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
                        string insertQuery = "INSERT INTO shift (date, shift_type) OUTPUT INSERTED.Id VALUES (@Date, @ShiftType);";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@Date", new DateTime(date.Year, date.Month, date.Day));
                            insertCmd.Parameters.AddWithValue("@ShiftType", (int)shiftType + 1);
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



        // Shift Management ==================================================
    }
}

