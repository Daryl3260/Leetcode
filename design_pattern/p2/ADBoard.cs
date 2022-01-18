using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.design_pattern.p2
{
    public class ADBoard : ISubscriber
    {
        private WeatherData weatherData;

        public ADBoard(WeatherData weatherData)
        {
            this.weatherData = weatherData;
            this.weatherData.AddSubscriber(this);
        }

        public void OnMeasurementsChanged()
        {
            int temperature = this.weatherData.GetTemperature();
            string weather = this.weatherData.GetWeather();
            int pressure = this.weatherData.GetPressure();

            //// Display with ADBoard
        }
    }
}
