using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timez
{
    public class Participant
    {
        private readonly List<Happening> _happenings = new List<Happening>();

        public string Name { get; }

        public Happening[] Happenings => _happenings.OrderBy(h => h.Occasion).ToArray();
        public Participant(string name)
        {
            Name = name;
        }

        public Participant Happened(string name, string occassion, params Participant[] togetherWith)
        {
            var allParticipants = togetherWith.Concat(new[] { this }).ToArray();
            var h = new Happening(name, occassion, allParticipants);
            foreach (var p in allParticipants)
            {
                p._happenings.Add(h);
            }
            return this;
        }
    }
}
