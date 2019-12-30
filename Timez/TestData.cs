using System;
using System.Collections.Generic;
using System.Text;

namespace Timez
{
    public class TestData
    {

        public static TimeLines CreateTestData()
        {
            var geralt = new Participant("Geralt");
            var ciri = new Participant("Ciri");
            //var calanthe = new Participant("Calanthe");

            new Happening("Birth", DateTime.Parse("1971-01-10"), geralt);
            new Happening("Trained", DateTime.Parse("1981-05-10"), geralt);

            new Happening("Birth", new DateTime(1985, 08, 09), ciri);

            //new Happening("Birth", new DateTime(1982, 04, 08), calanthe);

            new Happening("Sacking of Cintra", new DateTime(1995, 03, 02), ciri);

            new Happening("Geralt & Ciri meet", new DateTime(1997, 10, 04), ciri, geralt);
            new Happening("Ciri Trained", new DateTime(2002, 01, 01), ciri, geralt);

            //new Happening("Wild hunt", new DateTime(2003, 05, 11), ciri);

            new Happening("Vilgefortz's betrayal", new DateTime(2003, 07, 21), ciri, geralt);

            new Happening("Dubbed Sir Geralt of Rivia", new DateTime(2005, 09, 28), geralt);

            new Happening("Joins the Rats", new DateTime(2004, 08, 14), ciri);

            return new TimeLines( geralt, ciri);
        }

    }


    public class DesignHappening : Happening
    {
        public DesignHappening() : base("David föds", new DateTime(1981, 01, 10))
        {
        }
    }

    public class DesignParticipant : Participant
    {
        public DesignParticipant() : base("David Rundqvist")
        {
        }
    }
}
