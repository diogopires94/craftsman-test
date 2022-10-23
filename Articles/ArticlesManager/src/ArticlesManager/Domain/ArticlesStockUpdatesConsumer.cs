namespace ArticlesManager.Domain;

using MapsterMapper;
using MassTransit;
using SharedKernel.Messages;
using System.Threading.Tasks;
using ArticlesManager.Databases;
using MediatR;
using ArticlesManager.Domain.MaxiRetail;
using Newtonsoft.Json;
using ArticlesManager.Domain.Barcodes.Dtos;
using ArticlesManager.Domain.Barcodes.Features;

public class ArticlesStockUpdatesConsumer : IConsumer<IIArticleStockUpdate>
{
    private readonly IMapper _mapper;
    private readonly ArticlesDbContext _db;
    private readonly IMediator _mediator;

    public ArticlesStockUpdatesConsumer(ArticlesDbContext db, IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _db = db;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IIArticleStockUpdate> context)
    {
        var stockUpdate = context.Message;

        BarcodeParametersDto barcodeParametersDto = new BarcodeParametersDto();

        barcodeParametersDto.Filters = $"BarcodeValue == {stockUpdate.Barcode}";

        var queryBarcodeGet = new GetBarcodeList.BarcodeListQuery(barcodeParametersDto);
        var queryBarcodeGetResponse = await _mediator.Send(queryBarcodeGet);

        if (queryBarcodeGetResponse.Any())
        {
            var currentBarcode = queryBarcodeGetResponse.First();
            // Barcode exists, update
            BarcodeForUpdateDto barcodeForUpdate = new BarcodeForUpdateDto();

            barcodeForUpdate.BarcodeValue = currentBarcode.BarcodeValue;
            barcodeForUpdate.ArticleId = currentBarcode.ArticleId;
            barcodeForUpdate.Size = currentBarcode.Size;
            barcodeForUpdate.Size_Description = currentBarcode.Size_Description;
            barcodeForUpdate.Price = currentBarcode.Price;
            barcodeForUpdate.Color_Code = currentBarcode.Color_Code;
            barcodeForUpdate.Color_Description = currentBarcode.Color_Description;
            barcodeForUpdate.StockQuantity = (int?)stockUpdate.Stock;
            barcodeForUpdate.ReservedQuantity = currentBarcode.ReservedQuantity;

            var command = new UpdateBarcode.UpdateBarcodeCommand(currentBarcode.Id, barcodeForUpdate);
            await _mediator.Send(command);
        }

        await Task.CompletedTask;
    }
}