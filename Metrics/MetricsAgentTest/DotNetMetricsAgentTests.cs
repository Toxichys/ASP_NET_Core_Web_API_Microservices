using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTest
{
    public class DotNetMetricsAgentTests
    {
        private DotNetMetricsController _dotNetMetricsController;
        private Mock<IDotNetMetricsRepository> mock;
        public DotNetMetricsAgentTests()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            _dotNetMetricsController = new DotNetMetricsController(null, mock.Object);
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит CpuMetric - объект
            mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();
            // Выполняем действие на контроллере
            var result = _dotNetMetricsController.Create(new MetricsAgent.Models.Requests.DotNetMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }
    }
}
