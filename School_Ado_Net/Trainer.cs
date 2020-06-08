using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Ado_Net
{
    class Trainer
    {
        public int trainer_id { get; set; }
        public int Course_id_fk { get; set; }

        public string first_name { get; set; }
        public string last_name { get; set; }
        public string subject { get; set; }


    }
}
