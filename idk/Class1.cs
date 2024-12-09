using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idk
{
    public class Event
    {
        public string Date { get; set; }
        public string Title { get; set; }
        public string Participant { get; set; }

        public override string ToString()
        {
            return $"{Date}: {Title} (Участник: {Participant})";
        }
    }
}
