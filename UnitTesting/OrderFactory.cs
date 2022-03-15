using FactoryPattern.Business.Models.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    public abstract class OrderFactory
    {
        protected abstract Order CreateOrder();

        public Order GetOrder()
        {
            var order = CreateOrder();

            order.LineItems.Add(new Item("CSHARP_SMORGASBORD", "C# Smorgasbord", 100m), 1);
            order.LineItems.Add(new Item("CONSULTING", "Build a website", 200m), 1);

            return order;
        }
    }

    public class StandardOrderFactory : OrderFactory
    {
        protected override Order CreateOrder()
        {
            var order = new Order
            {
                Recipient = new Address
                {
                    To = "Luna",
                    Country = "Sweden"
                },
                Sender = new Address
                {
                    To = "Amy",
                    Country = "Sweden"
                },
                TotalWeight = 100
            };

            return order;
        }
    }

    public class InternationalOrderFactory : OrderFactory
    {
        protected override Order CreateOrder()
        {
            var order = new Order
            {
                Recipient = new Address
                {
                    To = "Luna",
                    Country = "Australia"
                },
                Sender = new Address
                {
                    To = "Amy",
                    Country = "Sweden"
                },
                TotalWeight = 100
            };

            return order;
        }
    }
}
