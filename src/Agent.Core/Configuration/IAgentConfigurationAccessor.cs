using SignalKo.SystemMonitor.Common.Model;

namespace SignalKo.SystemMonitor.Agent.Core.Sender.Configuration
{
    public interface IAgentConfigurationAccessor
    {
        AgentConfiguration GetAgentConfiguration();
    }
}