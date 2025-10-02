namespace Guardian
{
    public class Statistics
    {
        public string? CustomerNumber { get; set; }
        public string? Status { get; set; }
        public int? Churned { get; set; }
        public DateOnly DateInstalled { get; set; }
        public DateOnly DisconnectionDate { get; set; }
        public int DisconnectionDays { get; set; }
        public int MonthsStayed { get; set; }
        public int NumberOfComplaints { get; set; }
        public int NumberOfPayments { get; set; }
        public int MissedPayments { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
