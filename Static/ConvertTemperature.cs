using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Static
{
    public static class ConvertTemperature
    {
        public static double CelsiusToFahrenheit(double degrees)
        {
            return (degrees * 9) / 5 + 32;
        }

        public static double FahrenhenheitToCelsius(double degrees)
        {
            return (degrees - 32) * 5 / 9;
        }
    }
}
