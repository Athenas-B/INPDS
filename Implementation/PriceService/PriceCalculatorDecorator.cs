using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PriceService
{
    abstract class PriceCalculatorDecorator : IPriceCalculator
    {
        protected readonly IPriceCalculator Calculator;

        protected PriceCalculatorDecorator(IPriceCalculator calculator)
        {
            Calculator = calculator;
        }

        public abstract double Calculate(double distance);
    }
}