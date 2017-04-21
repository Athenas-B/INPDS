using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PriceService
{
    class BasePriceCalculator : IPriceCalculator
    {
        private const double BasicRate = 2000;
        private const double MultipleDistance = 10;

        public double Calculate(double distance)
        {
            return BasicRate + MultipleDistance * distance;
        }
    }
}