namespace Guardian.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Route("[Controller]")]
	public class IncidentController : ControllerBase
	{
		private readonly ILogger<IncidentController> _logger;

		public IncidentController(ILogger<IncidentController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public ActionResult<Incident> PostIncident(Incident incident)
		{
			var incidentReport = new Incident
			{
				Created = incident.Created,
				IsCausedByPlatform = true,
				Title = incident.Title,
				Body = incident.Body,
				Severity = incident.Severity,
				OwningTeam = incident.OwningTeam,
				Cluster = incident.Cluster,
				Namespace = incident.Namespace,
			};

			return Ok(incidentReport);
		}
	}
}
