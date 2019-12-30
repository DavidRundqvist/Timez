using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Timez
{
    public class TestData
    {

        public static TimeLines CreateTestData()
        {
            return CreateRundqvistTimeLine();
        }

        public static TimeLines CreateGeraltTimeline()
        {
            var geralt = new Participant("Geralt", Colors.Black);
            var ciri = new Participant("Ciri", Colors.Pink);
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

            return new TimeLines(geralt, ciri);
        }

        public static TimeLines CreateRundqvistTimeLine()
        {
            var david = new Participant("David", Colors.Blue);
            var darja = new Participant("Darja", Colors.Red);
            var alva = new Participant("Alva", Colors.Aqua);
            

            david
                .Happened("Föds", "1981-01-10")
                .Happened("Flyttar till Upplands Väsby", "1982-07-01")
                .Happened("Börjar skolan", "1988-08-16")
                .Happened("Gör lumpen", "2000-09-03")
                .Happened("Flyttar till Linköping", "2001-08-01")
                .Happened("Börjar jobba på Sectra", "2006-05-01")
                .Happened("David och Darja träffas","2004-06-19", darja)
                .Happened("Vårdinge folkhögskola", "2015-09-01");

            darja
                .Happened("Föds", "1985-08-09")
                .Happened("Flyttar till Liivalaia", "2000-09-23")
                .Happened("David och Darja flyttar ihop", "2007-02-01", david)
                .Happened("Börjar jobba på universitetet", "2011-06-01");

            alva
                .Happened("Föds", "2017-05-24");

               

            return new TimeLines(david, darja, alva);
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
        public DesignParticipant() : base("David Rundqvist", Colors.Red)
        {
        }
    }
}
