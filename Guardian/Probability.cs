namespace Guardian
{
    public class Probability
    {
        public string? CustomerNumber { get; set; }
        public string? Status { get; set; }
        public int DaysSinceDisconnection { get; set; }
        public int MonthsStayed { get; set; }
        public int NumberOfPayments { get; set; }
        public int MissedPayments { get; set; }
        public int NumberOfComplaintsPerMonth { get; set; }
    }
}
