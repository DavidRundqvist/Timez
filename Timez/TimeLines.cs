using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timez
{
    public class TimeLines
    {

        public Participant[] Participants { get; } = new Participant[0];

        public Event[] Events => Participants.SelectMany(i => i.Events).Distinct().OrderBy(h => h.Occasion).ToArray();

        public DateTime Start => Events.Select(h => h.Occasion).Min();
        public DateTime End => Events.Select(h => h.Occasion).Max();

        public TimeLines(params Participant[] participants)
        {
            Participants = participants;
        }
    }
}
