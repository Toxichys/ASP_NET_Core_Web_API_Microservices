using MetricsAgent.Models;
using MetricsAgent.Services;
using System;

namespace MetricsAgent.Controllers
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
    }
}
