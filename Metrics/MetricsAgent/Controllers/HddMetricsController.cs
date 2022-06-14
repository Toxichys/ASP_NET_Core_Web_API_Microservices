using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : Controller
    {
        private IHddMetricsRepository _hddMetricsRepository;
        private ILogger<HddMetricsController> _logger;
        public HddMetricsController(
            ILogger<HddMetricsController> logger,
            IHddMetricsRepository hddMetricsRepository)
        {
            _hddMetricsRepository = hddMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            HddMetric hddMetric = new HddMetric
            {
                Time = request.Time,
                Value = request.Value
            };

            _hddMetricsRepository.Create(hddMetric);

            // ДОМАШНЕЕ ЗАДАНИЕ
            // TODO: 1. Добавьте логирование всех параметров в каждый контроллер в обоих проектах.

            if (_logger != null)
                _logger.LogDebug("Успешно добавили новую dot hdd метрику: {0}", hddMetric);

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _hddMetricsRepository.GetAll();
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }
            if (_logger != null)
                _logger.LogDebug("Успешно выведена информация о всех метриках hdd");
            return Ok(response);
        }

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var metrics = _hddMetricsRepository.GetByTimePeriod(fromTime, toTime);
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }
            if (_logger != null)
                _logger.LogDebug("Успешно выведена информация о метриках hdd период с {0} по {1}", fromTime, toTime);
            return Ok(response);
        }
    }
}
