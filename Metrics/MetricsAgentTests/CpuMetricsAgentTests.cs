using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsAgentTests
    {
        private CpuMetricsController _cpuMetricsController;
        public CpuMetricsAgentTests()
        {
            _cpuMetricsController = new CpuMetricsController();
        }
        [Fact]
        public void GetMetrics_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            IActionResult result = _cpuMetricsController.GetMetrics(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
            // TODO: Домашнее задание [Пункт 3]
            //  Добавьте проект с тестами для агента сбора метрик. Напишите простые Unit-тесты на каждый
            // метод отдельно взятого контроллера в обоих тестовых проектах.

            // На данный момент можно просто воспользоваться заглушками (как в проекте MetricsManagerTests)
        }
    }
}
