using System.Collections.Generic;
using System.Text;

namespace Timez
{
    public class Participant
    {
        public string Name { get; }

        public List<Happening> Happenings { get; } = new List<Happening>();

        public Participant(string name)
        {
            Name = name;
        }
    }
}
