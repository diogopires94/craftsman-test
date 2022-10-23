namespace SharedKernel.Messages
{
    using System;
    using System.Text;

    public interface IIArticles
    {
        public string ArticlesJson { get; set; }
    }

    public class IArticles : IIArticles
    {
        public string ArticlesJson { get; set; }
    }
}