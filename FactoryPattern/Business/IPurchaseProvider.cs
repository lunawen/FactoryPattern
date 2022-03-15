using FactoryPattern.Business.Models.Commerce;
using FactoryPattern.Business.Models.Commerce.Invoice;
using FactoryPattern.Business.Models.Commerce.Summary;
using FactoryPattern.Business.Models.Shipping;
using FactoryPattern.Business.Models.Shipping.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern.Business
{
    // Abstract factory (the interface of logically related objects)
    public interface IPurchaseProviderFactory
    {
        ShippingProvider CreateShippingProvider(Order order);
        IInvoice CreateInvoice(Order order);
        ISummary CreateSummary(Order order);
    }

    // implement different factories
    public class AustraliaPurchaseProviderFactory : IPurchaseProviderFactory
    {
        public IInvoice CreateInvoice(Order order)
        {
            return new GSTInvoice();
        }

        public ShippingProvider CreateShippingProvider(Order order)
        {
            var shippingProviderFactory = new StandardShippingProviderFactory();
            return shippingProviderFactory.GetShippingProvider(order.Sender.Country);
        }

        public ISummary CreateSummary(Order order)
        {
            return new CsvSummary();
        }
    }

    public class SwedenPurchaseProviderFactory : IPurchaseProviderFactory
    {
        public IInvoice CreateInvoice(Order order)
        {
            if (order.Recipient.Country != order.Sender.Country)
                return new NoVATInvoice();
            
            return new VATInvoice();
        }

        public ShippingProvider CreateShippingProvider(Order order)
        {
            // use pre-existing factories inside the abstract factory pattern
            ShippingProviderFactory shippingProviderFactory;
            if (order.Sender.Country == order.Recipient.Country)
                shippingProviderFactory = new StandardShippingProviderFactory();
            else
                shippingProviderFactory = new GlobalExpressShippingProviderFactory();

            return shippingProviderFactory.GetShippingProvider(order.Sender.Country);
        }

        public ISummary CreateSummary(Order order)
        {
            return new EmailSummary();
        }
    }
}
