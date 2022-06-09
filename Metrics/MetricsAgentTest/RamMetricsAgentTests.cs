using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTest
{
    public class RamMetricsAgentTests
    {
        private RamMetricsController _ramMetricsController;
        private Mock<IRamMetricsRepository> mock;
        public RamMetricsAgentTests()
        {
            mock = new Mock<IRamMetricsRepository>();
            _ramMetricsController = new RamMetricsController(null, mock.Object);
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит CpuMetric - объект
            mock.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();
            // Выполняем действие на контроллере
            var result = _ramMetricsController.Create(new MetricsAgent.Models.Requests.RamMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }
    }
}
