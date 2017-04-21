using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PriceService
{
    public class PriceService : IPriceService
    {
        public double CalculatePrice(double distance, string systemId)
        {
            IPriceCalculator calculator = new BasePriceCalculator();
            if (systemId == "ReturnFreight")
            {
                calculator = new DiscountCalculator(calculator);
            }
            return calculator.Calculate(distance);
        }
    }
}