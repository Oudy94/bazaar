using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSandwichMakersHardwareStoreSolution.Enums;

namespace TheSandwichMakersHardwareStoreSolution.Classes
{
    public class Shift
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public ShiftTypeEnum ShiftType { get; set; }

        public Shift(int id, DateOnly date, ShiftTypeEnum shiftType)
        {
            this.Id = id;
            this.Date = date;
            this.ShiftType = shiftType;
        }

    }
}
