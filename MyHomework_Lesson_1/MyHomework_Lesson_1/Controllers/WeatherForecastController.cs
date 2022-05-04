using Microsoft.AspNetCore.Mvc;
using MyHomework_Lesson_1.Models;
using System;
using System.Linq;

namespace MyHomework_Lesson_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ValuesHolder _holder;
        public WeatherForecastController(ValuesHolder holder)
        {
            _holder = holder;
        }
        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(_holder.Get());
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime inputD, [FromQuery] int inputI)
        {
            _holder.Add(inputD, inputI);
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime inputDBeg,
            [FromQuery] DateTime inputDEnd,
             [FromQuery] int newValue)
        {
            for (int i = 0; i < _holder.Values.Count; i++)
            {
                if (_holder.Values[i].Date >= Convert.ToDateTime(inputDBeg).Date && _holder.Values[i].Date <= Convert.ToDateTime(inputDEnd).Date)
                    _holder.Values[i].TemperatureC = newValue;
            }
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime dateToDeleteBeg, [FromQuery] DateTime dateToDeleteEnd)
        {
            _holder.Values = _holder.Values.Where(w => w.Date <
            Convert.ToDateTime(dateToDeleteBeg).Date || w.Date >
            Convert.ToDateTime(dateToDeleteEnd).Date).ToList();
            return Ok();
        }
    }
}
