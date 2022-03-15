using FactoryPattern.Business.Models.Commerce;
using FactoryPattern.Business.Models.Shipping;
using FactoryPattern.Business.Models.Shipping.Factories;
using System;

namespace FactoryPattern.Business
{
    public class ShoppingCart
    {
        private readonly Order order;
        private readonly ShippingProviderFactory shippingProviderFactory;

        public ShoppingCart(Order order, ShippingProviderFactory shippingProviderFactory)
        {
            this.order = order;
            this.shippingProviderFactory = shippingProviderFactory;
        }

        public string Finalize()    
        {
            var shippingProvider = shippingProviderFactory.GetShippingProvider(order.Sender.Country);

            order.ShippingStatus = ShippingStatus.ReadyForShippment;

            return shippingProvider.GenerateShippingLabelFor(order);
        }
    }
}
