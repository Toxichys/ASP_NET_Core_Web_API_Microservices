﻿using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsAgentTests
    {
        private RamMetricsController _ramMetricsController;
        public RamMetricsAgentTests()
        {
            _ramMetricsController = new RamMetricsController();
        }
        [Fact]
        public void GetMetrics_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            IActionResult result = _ramMetricsController.GetMetrics(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
