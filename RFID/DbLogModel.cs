using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFID
{
    public class DbLogModel
    {
        public int log_id { get; set; }
        public string code { get; set; }
        public string place { get; set; }
        public string action { get; set; }
        public long created_at { get; set; }
    }
}
