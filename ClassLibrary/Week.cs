using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Week
    {
        // enum?
        public int ProjectId { get; set; }
        public int WeekNumber { get; set; }
        public List<DayOfWeek> dayOfWeek { get; set; }
        public enum Days: uint { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday }
        
    }
}
