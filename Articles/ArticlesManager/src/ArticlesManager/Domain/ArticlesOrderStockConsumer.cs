namespace ArticlesManager.Domain;

using MapsterMapper;
using MassTransit;
using SharedKernel.Messages;
using System.Threading.Tasks;
using ArticlesManager.Databases;

public class ArticlesOrderStockConsumer : IConsumer<IIOrderStock>
{
    private readonly IMapper _mapper;
    private readonly ArticlesDbContext _db;

    public ArticlesOrderStockConsumer(ArticlesDbContext db, IMapper mapper)
    {
        _mapper = mapper;
        _db = db;
    }

    public Task Consume(ConsumeContext<IIOrderStock> context)
    {
        // do work here

        return Task.CompletedTask;
    }
}