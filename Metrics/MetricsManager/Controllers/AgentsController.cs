using MetricsManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {

        private IAgentPool<AgentInfo> _agentPool;
        private ILogger<AgentsController> _logger;

        public AgentsController(ILogger<AgentsController> logger, IAgentPool<AgentInfo> agentPool)
        {
            _agentPool = agentPool;
            _logger = logger;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            if (agentInfo != null)
            {
                _agentPool.Add(agentInfo);
                if (_logger != null)
                    _logger.LogDebug("Успешно добавление агента {0}", agentInfo);
            }
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            if (_agentPool.Values.ContainsKey(agentId))
            {
                _agentPool.Values[agentId].Enable = true;
                if (_logger != null)
                    _logger.LogDebug("Успешно включение агента {0}", agentId);
            }
            return Ok();
        }
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            if (_agentPool.Values.ContainsKey(agentId))
            {
                _agentPool.Values[agentId].Enable = false;
                if (_logger != null)
                    _logger.LogDebug("Успешно выключение агента {0}", agentId);
            }
            return Ok();
        }

        // TODO: Домашнее задание [Пункт 1]
        // Добавьте метод в контроллер агентов проекта, относящегося к менеджеру метрик, который
        // позволяет получить список зарегистрированных в системе объектов.

        [HttpGet("get")]
        public IActionResult GetAllAgents()
        {
            var arragentPool = _agentPool.Get();
            if (_logger != null)
                _logger.LogDebug("Успешно получение всех агентов");
            return Ok(arragentPool);
        }

    }
}
