using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracker
{
    class Class1
    {
        [PrimaryKey, AutoIncrement]
        public String Vendor { get; set; }

        public String Building { get; set; }
        public String Monday { get; set; }
        public String Tyuesda { get; set; }
        public String Wednesday { get; set; }
        public String Thursday { get; set; }
        public String Friday { get; set; }
        public String Saturday { get; set; }
        public String Sunday { get; set; }

    }
}
