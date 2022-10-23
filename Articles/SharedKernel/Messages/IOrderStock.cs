namespace SharedKernel.Messages
{
    using System;
    using System.Text;

    public interface IIOrderStock
    {
        public string Barcode { get; set; }

public string Reference { get; set; }

public int Quantity { get; set; }
    }

    public class IOrderStock : IIOrderStock
    {
        public string Barcode { get; set; }

public string Reference { get; set; }

public int Quantity { get; set; }
    }
}