using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timez
{
    public static class EventExtensions
    {
        public static IEnumerable<TimeSpan> GetPeriods(this IEnumerable<Event> self)
        {
            self = self.ToArray();

            return self.Zip(self.Skip(1)).Select(p => p.Second.Occasion - p.First.Occasion);
        }

    }
}
