using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary.Enums;

namespace SharedLibrary.Classes
{
    public class Shift
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public ShiftTypeEnum ShiftType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Shift(int id, DateOnly date, ShiftTypeEnum shiftType, DateTime startTime, DateTime endTime)
        {
            this.Id = id;
            this.Date = date;
            this.ShiftType = shiftType;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }
    }
}
