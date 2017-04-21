using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PriceService
{
    [ServiceContract]
    public interface IPriceService
    {
        [OperationContract]
        double CalculatePrice(double distance, string systemId);
    }
}