using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Ado_Net
{
    class Grades_and_Middle_table
    {
        public int Student_id_fk { get; set; }
        public int Assignment_id_fk { get; set; }
        public decimal Oral_Mark { get; set; }
        public decimal Total_Mark { get; set; }

    }
}
