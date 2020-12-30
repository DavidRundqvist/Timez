using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Timez
{
    public class Participant
    {
        private readonly List<Event> _events = new List<Event>();

        public string Name { get; }

        public Event[] Events => _events.OrderBy(h => h.Occasion).ToArray();

        public Color Color { get; }

        public string ColorString => Color.ToString();

        public Participant(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public Participant Happened(string name, string occassion, params Participant[] togetherWith)
        {
            var allParticipants = togetherWith.Concat(new[] { this }).ToArray();
            var h = new Event(name, occassion, allParticipants);
            foreach (var p in allParticipants)
            {
                p._events.Add(h);
            }
            return this;
        }
    }
}
