using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTest
{
    public class HddMetricsAgentTests
    {
        private HddMetricsController _hddMetricsController;
        private Mock<IHddMetricsRepository> mock;
        public HddMetricsAgentTests()
        {
            mock = new Mock<IHddMetricsRepository>();
            _hddMetricsController = new HddMetricsController(null, mock.Object);
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит CpuMetric - объект
            mock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();
            // Выполняем действие на контроллере
            var result = _hddMetricsController.Create(new MetricsAgent.Models.Requests.HddMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
        }
    }
}
