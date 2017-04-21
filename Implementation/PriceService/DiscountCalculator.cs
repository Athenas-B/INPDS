using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PriceService
{
    class DiscountCalculator : PriceCalculatorDecorator
    {
        private const double Discount = 0.8;

        public DiscountCalculator(IPriceCalculator calculator) : base(calculator)
        {
        }

        public override double Calculate(double distance)
        {
            return Calculator.Calculate(distance) * Discount;
        }
    }
}