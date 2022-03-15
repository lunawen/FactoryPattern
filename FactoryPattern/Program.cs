#region Create Order
using FactoryPattern.Business;
using FactoryPattern.Business.Models.Commerce;
using FactoryPattern.Business.Models.Shipping.Factories;

Console.Write("Recipient Country: ");
var recipientCountry = Console.ReadLine().Trim();

Console.Write("Sender Country: ");
var senderCountry = Console.ReadLine().Trim();

Console.Write("Total Order Weight: ");
var totalWeight = Convert.ToInt32(Console.ReadLine().Trim());

var order = new Order
{
    Recipient = new Address
    {
        To = "Filip Ekberg",
        Country = recipientCountry
    },

    Sender = new Address
    {
        To = "Someone else",
        Country = senderCountry
    },

    TotalWeight = totalWeight
};

order.LineItems.Add(new Item("CSHARP_SMORGASBORD", "C# Smorgasbord", 100m), 1);
order.LineItems.Add(new Item("CONSULTING", "Building a website", 100m), 1);
#endregion

// the shopping cart is decoupled from the creation of the particular shipping provider.
var cart = new ShoppingCart(order, new StandardShippingProviderFactory());
//var cart = new ShoppingCart(order, new GlobalExpressShippingProviderFactory());

var shippingLabel = cart.Finalize();

Console.WriteLine(shippingLabel);