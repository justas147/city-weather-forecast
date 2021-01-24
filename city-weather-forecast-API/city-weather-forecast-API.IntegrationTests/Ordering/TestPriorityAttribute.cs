using System;

namespace city_weather_forecast_API.IntegrationTests.Ordering
{
    class TestPriorityAttribute : Attribute
    {
        public TestPriorityAttribute(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; private set; }
    }
}
