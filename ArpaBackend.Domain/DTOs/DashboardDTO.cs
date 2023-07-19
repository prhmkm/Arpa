

namespace ArpaBackend.Domain.DTOs
{
    public class DashboardDTO
    {
        public class dataValues
        {
            public int jason0 { get; set; }
        }
        public class labels
        {
            public string jason1 { get; set; }
        }
        public class DatesValue
        {
            public string jason2 { get; set; }
        }
        public class MoneyesValue
        {
            public int jason3 { get; set; }
        }

        public class VisitorsInfo
        {
            public string Country { get; set; }
            public int Count { get; set; }
            public double Percentage { get; set; }
        }

        public class VisitByDate
        {
            public string Date { get; set; }
            public int Count { get; set; }
        }

    }
}
