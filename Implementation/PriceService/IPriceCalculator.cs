using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PriceService
{
    interface IPriceCalculator
    {
        double Calculate(double distance);
    }
}