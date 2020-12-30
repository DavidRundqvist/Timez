using System;
using System.Collections.Generic;
using System.Linq;

namespace Timez
{   
    public class Event
    {
        public string Name { get; }

        public DateTime Occasion { get; }

        public List<Participant> Participants { get; } = new List<Participant>();

        public string Date => Occasion.ToString("yyyy-MM-dd");

        public Event(string name, DateTime occasion, params Participant[] participants)
        {
            Name = name;
            Occasion = occasion;
            Participants = participants.ToList();
        }

        public Event(string name, string occassion, params Participant[] participants) 
        {
            Name = name;
            Occasion = DateTime.Parse(occassion);
            Participants = participants.ToList();
        }
    }
}
