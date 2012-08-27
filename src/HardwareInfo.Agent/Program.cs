﻿using System;
using System.Configuration;
using System.Diagnostics;
using System.Management;
using System.Threading;

using HardwareStatus.Common.Model;

using RestSharp;

namespace HardwareStatus.Agent
{
    class Program
    {
        private static PerformanceCounter processorCounter;

        private static PerformanceCounter memoryCounter;

        static void Main(string[] args)
        {
            processorCounter = new PerformanceCounter
                {
                    CategoryName = "Processor",
                    CounterName = "% Processor Time",
                    InstanceName = "_Total"
                };

            memoryCounter = new PerformanceCounter("Memory", "Available KBytes");

            string baseUrl = ConfigurationManager.AppSettings["ServiceBaseUrl"];
            var restClient = new RestClient(baseUrl);

            do
            {
                var hardwareInfo = GetHardwareInfo();
                var request = new RestRequest("api/HardwareInfo/", Method.PUT);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(hardwareInfo);
                restClient.ExecuteAsync<HardwareInfo>(request, response => { });

                Thread.Sleep(500);
            }
            while (true);
        }

        static HardwareInfo GetHardwareInfo()
        {
            var processorTime = (double)processorCounter.NextValue();
            var memUsage = (ulong)memoryCounter.NextValue();
            ulong totalMemory = 0;

            // Get total memory from WMI
            var memQuery = new ObjectQuery("SELECT * FROM CIM_OperatingSystem");
            var searcher = new ManagementObjectSearcher(memQuery);
            foreach (ManagementObject item in searcher.Get())
            {
                totalMemory += (ulong)item["TotalVisibleMemorySize"];
            }

            return new HardwareInfo
                {
                    MachineName = Environment.MachineName, 
                    MemUsage = memUsage, 
                    Processor = processorTime,
                    TotalMemory = totalMemory
                };
        }
    }
}
