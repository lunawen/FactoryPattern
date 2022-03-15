using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern.Business.Models.Shipping.Factories
{
    public abstract class ShippingProviderFactory
    {
        // the factory method
        // we can choose the access modifier per use case, e.g., protected, public
        protected abstract ShippingProvider CreateShippingProvider(string country);

        // this method allows us to change common things before the object is returned to the client
        // you can also inject things into the creation of provider before it requests an instantiation of the product
        public ShippingProvider GetShippingProvider(string country)
        {
            var provider = CreateShippingProvider(country);

            // inject things before returning the object
            if (country == "Sweden" && provider.InsuranceOptions.ProviderHasInsurance)
                provider.RequireSignature = false;

            return provider;
        }
    }
    public class StandardShippingProviderFactory : ShippingProviderFactory
    {
        protected override ShippingProvider CreateShippingProvider(string country)
        {
            #region Create Shipping Provider
            ShippingProvider shippingProvider;

            if (country == "Australia")
            {
                #region Australia Post Shipping Provider
                var shippingCostCalculator = new ShippingCostCalculator(
                    internationalShippingFee: 250,
                    extraWeightFee: 500
                )
                {
                    ShippingType = ShippingType.Standard
                };

                var customsHandlingOptions = new CustomsHandlingOptions
                {
                    TaxOptions = TaxOptions.PrePaid
                };

                var insuranceOptions = new InsuranceOptions
                {
                    ProviderHasInsurance = false,
                    ProviderHasExtendedInsurance = false,
                    ProviderRequiresReturnOnDamange = false
                };

                shippingProvider = new AustraliaPostShippingProvider("CLIENT_ID",
                    "SECRET",
                    shippingCostCalculator,
                    customsHandlingOptions,
                    insuranceOptions);
                #endregion
            }
            else if (country == "Sweden")
            {
                #region Swedish Postal Service Shipping Provider
                var shippingCostCalculator = new ShippingCostCalculator(
                    internationalShippingFee: 50,
                    extraWeightFee: 100
                )
                {
                    ShippingType = ShippingType.Express
                };

                var customsHandlingOptions = new CustomsHandlingOptions
                {
                    TaxOptions = TaxOptions.PayOnArrival
                };

                var insuranceOptions = new InsuranceOptions
                {
                    ProviderHasInsurance = true,
                    ProviderHasExtendedInsurance = false,
                    ProviderRequiresReturnOnDamange = false
                };

                shippingProvider = new SwedishPostalServiceShippingProvider("API_KEY",
                    shippingCostCalculator,
                    customsHandlingOptions,
                    insuranceOptions);
                #endregion
            }
            else
            {
                throw new NotSupportedException("No shipping provider found for origin country");
            }
            #endregion

            return shippingProvider;
        }
    }

    public class GlobalExpressShippingProviderFactory : ShippingProviderFactory
    {
        protected override ShippingProvider CreateShippingProvider(string country)
        {
            return new GlobalExpressShippingProvider();
        }
    }
}
