namespace SharedKernel.Messages
{
    using System;
    using System.Text;

    public interface IIStocks
    {
        public string StocksJson { get; set; }
    }

    public class IStocks : IIStocks
    {
        public string StocksJson { get; set; }
    }
}