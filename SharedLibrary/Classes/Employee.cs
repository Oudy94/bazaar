﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary.Enums;

namespace SharedLibrary.Classes
{
    public class Employee
    {
        // Private fields
        private int id;
        private string name;
        private string email;
        private string password;
        private RoleEnum role;
        private DateTime registerDate;
        private string image;
        private string address;
        private Department department;
        private decimal hourlyWage;
        private bool isActive;
        private string phone_number;
        private int bsn;
        private string bank_account;

        // Properties
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
		public string Email { get { return email; } set { email = value; } }
		public string Password { get { return password; } set { password = value; } }
        public RoleEnum Role { get { return role; } set { role = value; } }
        public DateTime RegisterDate { get { return registerDate; } set { registerDate = value; } }
        public string Image { get { return image; } set { image = value; } }
        public string Address { get { return address; } set { address = value; } }
        public Department Department { get { return department; } set { department = value; } }
        public decimal HourlyWage { get { return hourlyWage; } set { hourlyWage = value; } }
        public bool IsActive { get { return isActive; } set { isActive = value; } }
        public string PhoneNumber { get { return phone_number; } set { phone_number = value; } }
        public int BSN { get { return bsn; } set { bsn = value; } }
        public string BankAccount { get { return bank_account; } set { bank_account = value; } }


		// Constructor
        public Employee()
        {
        }

		public Employee(int id, string name, string email, string password, RoleEnum role, string image, string address, Department department, decimal hourlyWage, bool isActive, string phone_number, int bsn, string bank_account)
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
            this.PhoneNumber = phone_number;
            this.BSN = bsn;
            this.BankAccount = bank_account;
        }

        public Employee(string name, string email, string password, RoleEnum role, string image, string address, Department department, decimal hourlyWage, bool isActive, string phone_number, int bsn, string bank_account)
        {
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
            this.PhoneNumber = phone_number;
            this.BSN = bsn;
            this.BankAccount = bank_account;
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
