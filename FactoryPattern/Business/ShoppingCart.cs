using FactoryPattern.Business.Models.Commerce;
using FactoryPattern.Business.Models.Shipping;
using FactoryPattern.Business.Models.Shipping.Factories;
using System;

namespace FactoryPattern.Business
{
    public class ShoppingCart
    {
        private readonly Order order;
        private readonly IPurchaseProviderFactory purchaseProviderFactory;

        public ShoppingCart(Order order, IPurchaseProviderFactory purchaseProviderFactory)
        {
            this.order = order;
            this.purchaseProviderFactory = purchaseProviderFactory;
        }

        public string Finalize()    
        {
            var shippingProvider = purchaseProviderFactory.CreateShippingProvider(order);

            var invoice = purchaseProviderFactory.CreateInvoice(order);

            var summary = purchaseProviderFactory.CreateSummary(order);

            summary.Send();

            order.ShippingStatus = ShippingStatus.ReadyForShippment;

            return shippingProvider.GenerateShippingLabelFor(order);
        }
    }
}
