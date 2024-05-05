using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace VesperAPI.Instrumentation
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class InstrumentationAttribute(string endpointIdentifier) : ActionFilterAttribute
    {
        private readonly string _endpointIdentifier = endpointIdentifier ?? throw new ArgumentNullException(nameof(endpointIdentifier));

        private Stopwatch _stopwatch = new();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            _stopwatch = Stopwatch.StartNew();
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();

            var requestDuration = _stopwatch.ElapsedMilliseconds;
            var statusCode = context.HttpContext.Response.StatusCode;

            var telemetryClient = context.HttpContext.RequestServices.GetRequiredService<TelemetryClient>();
            var metric = new MetricTelemetry("API Request Duration", requestDuration);
            metric.Properties.Add("StatusCode", statusCode.ToString());
            metric.Properties.Add("Endpoint", _endpointIdentifier);
            telemetryClient.TrackMetric(metric);

            base.OnActionExecuted(context);
        }
    }

}
