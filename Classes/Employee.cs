using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Employee
    {
        // Private fields
        private int id;
        private string name;
        private string email;
        private string password;
        private Role role;
        private DateTime registerDate;
        private string image;
        private string address;
        private Department department;
        private decimal hourlyWage;
        private bool isActive;

        // Properties
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Password { get { return password; } set { password = value; } }
        public Role Role { get { return role; } set { role = value; } }
        public DateTime RegisterDate { get { return registerDate; } set { registerDate = value; } }
        public string Image { get { return image; } set { image = value; } }
        public string Address { get { return address; } set { address = value; } }
        public Department Department { get { return department; } set { department = value; } }
        public decimal HourlyWage { get { return hourlyWage; } set { hourlyWage = value; } }
        public bool IsActive { get { return isActive; } set { isActive = value; } }

        // Constructor
        public Employee(int id, string name, string email, string password, Role role, string image, string address, Department department, decimal hourlyWage, bool isActive)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password; // ! hash passwords
            this.Role = role;
            this.Image = image;
            this.Address = address;
            this.Department = department;
            this.HourlyWage = hourlyWage;
            this.RegisterDate = DateTime.Now;
            this.IsActive = isActive;
        }

        public Employee(string name, string email, string password, Role role, string image, string address, Department department, decimal hourlyWage, bool isActive)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password; // ! hash passwords
            this.Role = role;
            this.Image = image;
            this.Address = address;
            this.Department = department;
            this.HourlyWage = hourlyWage;
            this.RegisterDate = DateTime.Now;
            this.IsActive = isActive;
        }
    }


}
