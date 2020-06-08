using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Ado_Net
{
    class Course
    {
        public int course_id { get; set; }
        public string title { get; set; }
        public string stream { get; set; }
        public string type { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}
