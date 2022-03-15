using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern.Business.Models.Commerce.Invoice
{
    public interface IInvoice
    {
        public byte[] GenerateInvoice();
    }

    public class GSTInvoice : IInvoice
    {
        public byte[] GenerateInvoice()
        {
            return Encoding.Default.GetBytes("Hello world from GST Invoice");
        }
    }

    public class VATInvoice : IInvoice
    {
        public byte[] GenerateInvoice()
        {
            return Encoding.Default.GetBytes("Hello world from VAT Invoice");
        }
    }

    public class NoVATInvoice : IInvoice
    {
        public byte[] GenerateInvoice()
        {
            return Encoding.Default.GetBytes("Hello world from No VAT Invoice");
        }
    }
}
