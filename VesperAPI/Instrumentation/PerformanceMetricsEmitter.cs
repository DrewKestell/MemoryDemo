using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System.Diagnostics;

namespace VesperAPI.Instrumentation
{
    public class PerformanceMetricsEmitter(TelemetryClient telemetryClient)
    {
        static readonly List<Junk> junk = [];

        private readonly TelemetryClient telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));

        public async void StartEmittingMetrics()
        {
            var process = Process.GetCurrentProcess();

            ApplyMemoryPressure();

            while (true)
            {
                double ramUsageBytes = GC.GetTotalMemory(false) / 1024;
                var ramMetric = new MetricTelemetry("Working Set", ramUsageBytes);
                telemetryClient.TrackMetric(ramMetric);

                await Task.Delay(15000);
            }
        }

        public async Task ApplyMemoryPressure()
        {
            while (true)
            {
                var newJunk = new Junk();
                junk.Add(newJunk);
                await Task.Delay(500);
            }
        }
    }

    public class Junk
    {
        public byte[] Bytes = new byte[1000000];
    }
}
