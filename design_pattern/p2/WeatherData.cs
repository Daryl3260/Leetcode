using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.design_pattern.p2
{
    public class WeatherData
    {
        private List<ISubscriber> subscribers = new List<ISubscriber>();

        public void AddSubscriber(ISubscriber subscriber)
        {
            this.subscribers.Add(subscriber);
        }

        public void RemoveSubScriber(ISubscriber subscriber)
        {
            this.subscribers.Remove(subscriber);
        }

        public int GetTemperature()
        {
            return -1;
        }

        public string GetWeather()
        {
            return "Sunny";
        }

        public int GetPressure()
        {
            return 1;
        }

        public void MeasurementChanged()
        {
            foreach (ISubscriber subscriber in this.subscribers)
            {
                subscriber.OnMeasurementsChanged();
            }
        }
    }
}
