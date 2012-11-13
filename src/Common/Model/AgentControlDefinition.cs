﻿namespace SignalKo.SystemMonitor.Common.Model
{
	public class AgentControlDefinition
	{
		public bool AgentIsEnabled { get; set; }

		public int CheckIntervalInSeconds { get; set; }

		public string SystemInformationSenderPath { get; set; }

		public string Hostname { get; set; }

		public string Hostaddress { get; set; }

		public SystemPerformanceCollectorDefinition SystemPerformanceCheck { get; set; }

		public HttpStatusCodeCheckDefinition HttpStatusCodeCheck { get; set; }

		public bool IsValid()
		{
			return this.CheckIntervalInSeconds > 0 && string.IsNullOrWhiteSpace(this.SystemInformationSenderPath) == false
				   && string.IsNullOrWhiteSpace(this.Hostaddress) == false;
		}
	}
}