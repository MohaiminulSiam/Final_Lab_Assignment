using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Lab_Assignment_code.Classes
{
    public class DiaryEvents
    {
        public string event_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime post_date { get; set; }
        public DateTime last_modify_date { get; set; }
        public int priority { get; set; }
        public string image { get; set; }

    }
}
