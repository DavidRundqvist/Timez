using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timez
{
    public class TimeLines
    {

        public Participant[] Participants { get; } = new Participant[0];

        public Happening[] Happenings => Participants.SelectMany(i => i.Happenings).Distinct().OrderBy(h => h.Occasion).ToArray();

        public DateTime Start => Happenings.Select(h => h.Occasion).Min();
        public DateTime End => Happenings.Select(h => h.Occasion).Max();

        public TimeLines(params Participant[] participants)
        {
            Participants = participants;
        }
    }
}
