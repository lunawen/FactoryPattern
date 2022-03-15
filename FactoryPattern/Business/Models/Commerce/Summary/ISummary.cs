using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern.Business.Models.Commerce.Summary
{
    public interface ISummary
    {
        string CreateOrderSummary(Order order);

        void Send();
    }

    public class EmailSummary : ISummary
    {
        public string CreateOrderSummary(Order order)
        {
            return "Email Summary";
        }

        public void Send()
        {
            throw new NotImplementedException();
        }
    }

    public class CsvSummary : ISummary
    {
        public string CreateOrderSummary(Order order)
        {
            return "CSV Summary";
        }

        public void Send()
        {
            Console.WriteLine("Sent");
        }
    }
}
