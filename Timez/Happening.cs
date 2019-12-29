using System;
using System.Collections.Generic;
using System.Linq;

namespace Timez
{
    public class Happening
    {
        public string Name { get; }

        public DateTime Occasion { get; }

        public List<Participant> Participants { get; } = new List<Participant>();

        public string Date => Occasion.ToString("yyyy-MM-dd");

        public Happening(string name, DateTime occasion, params Participant[] participants)
        {
            Name = name;
            Occasion = occasion;
            Participants = participants.ToList();

            foreach(var involved in Participants)
            {
                involved.Happenings.Add(this);
            }
        }
    }
}
