using System;
using System.Collections.Generic;

namespace MyHomework_Lesson_1.Models
{
    public class ValuesHolder
    {
        private List<Weather> _values;
        public ValuesHolder()
        {
            _values = new List<Weather>();
        }
        public void Add(DateTime valueD, int valueI)
        {
            Weather wether = new Weather() { Date = Convert.ToDateTime(valueD).Date, TemperatureC = valueI };
            _values.Add(wether);
        }
        public string Get()
        {
            string str = "";
            foreach (Weather _value in _values)
                str = str
                      + "Дата: "
                      + _value.Date.ToString("dd.MM.yyyy")
                      + " температура: "
                      + _value.TemperatureC.ToString()
                      + "\n";
            return str;
        }
        public List<Weather> Values
        {
            get { return _values; }
            set { _values = value; }
        }
    }
    public class Weather
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
    }
}
