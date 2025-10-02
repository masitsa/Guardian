namespace Guardian
{
	public class Incident
	{
		public string? Created { get; set; }
		public string? Title { get; set; }
		public string? Body { get; set; }
		public string? Severity { get; set; }
		public string? OwningTeam { get; set; }
		public bool IsCausedByPlatform { get; set; }
        public string? Cluster { get; set; }
        public string? Namespace { get; set; }
    }
}
