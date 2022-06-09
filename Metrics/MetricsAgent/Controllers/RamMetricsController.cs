using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : Controller
    {
        private IRamMetricsRepository _ramMetricsRepository;
        private ILogger<RamMetricsController> _logger;
        public RamMetricsController(
            ILogger<RamMetricsController> logger,
            IRamMetricsRepository ramMetricsRepository)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            RamMetric ramMetric = new RamMetric
            {
                Time = request.Time,
                Value = request.Value
            };

            _ramMetricsRepository.Create(ramMetric);

            // ДОМАШНЕЕ ЗАДАНИЕ
            // TODO: 1. Добавьте логирование всех параметров в каждый контроллер в обоих проектах.

            if (_logger != null)
                _logger.LogDebug("Успешно добавили новую ram метрику: {0}", ramMetric);

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _ramMetricsRepository.GetAll();
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }
            if (_logger != null)
                _logger.LogDebug("Успешно выведена информация о всех метриках ram");
            return Ok(response);
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var metrics = _ramMetricsRepository.GetByTimePeriod(fromTime, toTime);
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }
            if (_logger != null)
                _logger.LogDebug("Успешно выведена информация о метриках ram период с {0} по {1}", fromTime, toTime);
            return Ok(response);
        }
    }
}
