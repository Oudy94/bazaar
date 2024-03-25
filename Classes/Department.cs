using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{
    public class Department
    {
        private static int nextId = 1;

        public int Id { get; set; }
        public string Name { get; set; }

        public Department(string name)
        {
            this.Id = nextId++;
            this.Name = name;
        }  
        
        public Department(int id, string name)
        {
            this.Id = id;
            this.Name = name;

            nextId = id + 1;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
