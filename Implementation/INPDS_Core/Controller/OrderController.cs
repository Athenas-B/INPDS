using System;
using INPDS_Core.DataAccess;
using INPDS_Core.DTO;
using INPDS_Core.Model;

namespace INPDS_Core.Controller
{
    public class OrderController : IOrderController
    {
        public ValidationResult RegisterOrder(Order order)
        {
            var validationResult = Validate(order);
            if (validationResult.IsValid)
            {
                using (var context = new ReturnFreightContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }
            return validationResult;
        }

        private static ValidationResult Validate(Order order)
        {
            ValidationResult result = ValidationResult.Ok();
            if (order.Customer == null)
            {
                result = result.JoinResults(ValidationResult.Error("Uživatel není definován."));
            }
            if (DateTime.Now > order.DeliveryDeadline)
            {
                result = result.JoinResults(ValidationResult.Error("Termín nejpozdìjšího dodání musí být v budoucnosti."));
            }
            if (DateTime.Now > order.PickupDate)
            {
                result = result.JoinResults(ValidationResult.Error("Termín vyzvednutí zboží musí být v budoucnosti."));
            }
            if (order.PickupDate > order.DeliveryDeadline)
            {
                result = result.JoinResults(ValidationResult.Error("Termín nejpozdìjšího dodání musí být vìtší než termín vyzvednutí."));
            }
            if (string.IsNullOrWhiteSpace(order.From) || string.IsNullOrWhiteSpace(order.To))
            {
                result = result.JoinResults(ValidationResult.Error("Beginning or destination of the order is invalid."));
            }
            return result;
        }
    }
}

public class PriceService
{
    double CalculatePrice(double km, string id)
    {
        IPriceCalculator calculator = new BasePriceCalculator();
        if (id == "ReturnFreight")
        {
            calculator = new DiscountCalculator(calculator);
        }
        return calculator.GetPrice(km);
    }
}

public interface IPriceCalculator
{
    double GetPrice(double km);
}

public class BasePriceCalculator : IPriceCalculator
{
    public double GetPrice(double km)
    {
        return 2000 + 10*km;
    }
}

public abstract class PriceCalculatorDecorator : IPriceCalculator
{
    protected IPriceCalculator _calculator;

    protected PriceCalculatorDecorator(IPriceCalculator calculator)
    {
        _calculator = calculator;
    }

    public abstract double GetPrice(double km);
}

public class DiscountCalculator : PriceCalculatorDecorator
{
    public DiscountCalculator(IPriceCalculator calc) : base(calc){}
    public override double GetPrice(double km)
    {
        return _calculator.GetPrice(km)*0.8;
    }
}