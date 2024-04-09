using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Classes
{
    public class UserSession
    {
        private static UserSession instance;
        private Employee currentEmployee;

        private UserSession() { }

        public static UserSession Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserSession();
                }
                return instance;
            }
        }

        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { currentEmployee = value; }
        }
    }

}
