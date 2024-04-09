using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Classes
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Department(string name)
        {
            this.Name = name;
        }  
        
        public Department(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
