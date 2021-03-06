using SignalKo.SystemMonitor.Common.Services;
using SignalKo.SystemMonitor.Monitor.Web.Core.Configuration;
using SignalKo.SystemMonitor.Monitor.Web.Core.DataAccess;
using SignalKo.SystemMonitor.Monitor.Web.Core.Services;
using SignalKo.SystemMonitor.Monitor.Web.ViewModelOrchestrators;

using StructureMap;

namespace SignalKo.SystemMonitor.Monitor.Web.DependencyResolution
{
	public static class StructureMapSetup
	{
		public static IContainer Initialize()
		{
			ObjectFactory.Initialize(x =>
				{
					x.Scan(scan =>
						{
							scan.TheCallingAssembly();
							scan.WithDefaultConventions();
						});

					/* configuration */
					x.For<IFileSystemDataStoreConfigurationProvider>().Use<AppConfigFileSystemDataStoreConfigurationProvider>();
					x.For<IDefaultAgentConfigurationProvider>().Use<AppConfigDefaultAgentConfigurationProvider>();
					x.For<IServerConfigurationRepository>().Use<JsonFileConfigurationRepository>();

					/* data access */
					x.For<IAgentConfigurationDataAccessor>().Use<JsonAgentConfigurationDataAccessor>();

					/* archive */
					x.For<ISystemInformationArchiveAccessor>().Singleton().Use<FilesystemSystemInformationArchiveAccessor>();

					/* services */
					x.For<IAgentConfigurationDataAccessor>().Use<JsonAgentConfigurationDataAccessor>();
					x.For<IKnownAgentsProvider>().Use<KnownAgentsProvider>();
					x.For<IAgentConfigurationService>().Use<AgentConfigurationService>();
					x.For<IAgentControlDefinitionService>().Use<AgentControlDefinitionService>();

					/* view model orchestrators */
					x.For<IMemoryStatusOrchestrator>().Use<MemoryStatusOrchestrator>();
					x.For<IProcessorStatusOrchestrator>().Use<ProcessorStatusOrchestrator>();
					x.For<IStorageStatusOrchestrator>().Use<StorageStatusOrchestrator>();
					x.For<ISystemStatusOrchestrator>().Use<SystemStatusOrchestrator>();

					/* common services */
					x.For<IEncodingProvider>().Use<DefaultEncodingProvider>();
					x.For<IMemoryUnitConverter>().Use<MemoryUnitConverter>();
					x.For<ITimeProvider>().Use<UTCTimeProvider>();
				});

			return ObjectFactory.Container;
		}
	}
}