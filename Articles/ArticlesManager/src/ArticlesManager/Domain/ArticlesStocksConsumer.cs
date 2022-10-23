namespace ArticlesManager.Domain;

using MapsterMapper;
using MassTransit;
using SharedKernel.Messages;
using System.Threading.Tasks;
using ArticlesManager.Databases;
using ArticlesManager.Domain.MaxiRetail;
using Newtonsoft.Json;
using ArticlesManager.Domain.Articles.Dtos;
using ArticlesManager.Domain.Articles.Features;
using MediatR;
using ArticlesManager.Domain.Barcodes.Features;
using ArticlesManager.Domain.Barcodes.Dtos;

public class ArticlesStocksConsumer : IConsumer<IIStocks>
{
    private readonly IMapper _mapper;
    private readonly ArticlesDbContext _db;
    private readonly IMediator _mediator;

    public ArticlesStocksConsumer(ArticlesDbContext db, IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _db = db;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IIStocks> context)
    {
        // Mass sync stocks with Maxi retail

        MaxiRetailStocks response = JsonConvert.DeserializeObject<MaxiRetailStocks>(context.Message.StocksJson); // Change

        var size = response.Result.Length;

        if (response.Success)
        {
            var diferentReferences = response.Result.Select(x => x.Reference).Distinct();

            foreach (string articleReference in diferentReferences)
            {
                double totalStock = 0;
                try
                {
                    // Get the Article based on the reference
                    ArticleParametersDto articleParametersDto = new ArticleParametersDto();

                    articleParametersDto.Filters = $"InternalReference == {articleReference}";

                    var articlesQuery = new GetArticleList.ArticleListQuery(articleParametersDto);
                    var articlesQueryResponse = await _mediator.Send(articlesQuery);

                    if (articlesQueryResponse.Any())
                    {
                        var distinctBarcodesForReference = response.Result.Where(y => y.Reference == articleReference).Select(x => x.Barcode).Distinct();

                        foreach (var distinctBarCode in distinctBarcodesForReference)
                        {
                            var allBarcodeResultsForReference = response.Result.Where(y => y.Reference == articleReference && y.Barcode == distinctBarCode);

                            double stockQuantity = 0;
                            //double reservedQuantity = 0;

                            foreach (MaxiRetailStockResult stockResult in allBarcodeResultsForReference)
                            {

                                totalStock += stockResult.StockQuantity;

                                stockQuantity += stockResult.StockQuantity;
                                //reservedQuantity += stockResult.ReservedQuantity;

                            }

                            // update Barcode info on DB
                            //      update stock for that reference, get that Barcode and update it
                            // Get the barcode
                            BarcodeParametersDto barcodeParametersDto = new BarcodeParametersDto();

                            barcodeParametersDto.Filters = $"BarcodeValue == {distinctBarCode}";

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
                                barcodeForUpdate.StockQuantity = (int?)stockQuantity;
                                barcodeForUpdate.ReservedQuantity = currentBarcode.ReservedQuantity;

                                var command = new UpdateBarcode.UpdateBarcodeCommand(currentBarcode.Id, barcodeForUpdate);
                                await _mediator.Send(command);
                            }
                        }

                        var article = articlesQueryResponse.First();
                        // if stock is low, update the article with low stock = true
                        if (totalStock <= 0)
                        {
                            // update article with low quantity flag and out of stock
                            ArticleForUpdateDto articleForUpdate = new ArticleForUpdateDto();

                            articleForUpdate.InternalReference = article.InternalReference;
                            articleForUpdate.SKU = article.SKU;
                            articleForUpdate.Description = article.Description;
                            articleForUpdate.Price = article.Price;
                            articleForUpdate.PriceWithPromotion = article.PriceWithPromotion;
                            articleForUpdate.BrandId = article.BrandId;
                            articleForUpdate.FamilyId = article.FamilyId;
                            articleForUpdate.SubFamilyId = article.SubFamilyId;
                            articleForUpdate.CollectionId = article.CollectionId;
                            articleForUpdate.Generic1 = article.Generic1;
                            articleForUpdate.RowNumber = article.RowNumber;
                            articleForUpdate.MainArticleImageUrl = article.MainArticleImageUrl;
                            articleForUpdate.Url = article.Url;
                            articleForUpdate.MetaName = article.MetaName;
                            articleForUpdate.MetaDescription = article.MetaDescription;
                            articleForUpdate.IsOutOfStock = true;
                            articleForUpdate.IsLowStock = true;
                            articleForUpdate.IsPublished = article.IsPublished;

                            var command = new UpdateArticle.UpdateArticleCommand(article.Id, articleForUpdate);
                            await _mediator.Send(command);
                        }
                        else if (totalStock > 0 && totalStock <= 1)
                        {
                            // update article with low quantity flag
                            ArticleForUpdateDto articleForUpdate = new ArticleForUpdateDto();

                            articleForUpdate.InternalReference = article.InternalReference;
                            articleForUpdate.SKU = article.SKU; articleForUpdate.Description = article.Description;
                            articleForUpdate.Price = article.Price;
                            articleForUpdate.PriceWithPromotion = article.PriceWithPromotion;
                            articleForUpdate.BrandId = article.BrandId;
                            articleForUpdate.FamilyId = article.FamilyId;
                            articleForUpdate.SubFamilyId = article.SubFamilyId;
                            articleForUpdate.CollectionId = article.CollectionId;
                            articleForUpdate.Generic1 = article.Generic1;
                            articleForUpdate.RowNumber = article.RowNumber;
                            articleForUpdate.MainArticleImageUrl = article.MainArticleImageUrl;
                            articleForUpdate.Url = article.Url;
                            articleForUpdate.MetaName = article.MetaName;
                            articleForUpdate.MetaDescription = article.MetaDescription;
                            articleForUpdate.IsOutOfStock = false;
                            articleForUpdate.IsLowStock = true;
                            articleForUpdate.IsPublished = article.IsPublished;

                            var command = new UpdateArticle.UpdateArticleCommand(article.Id, articleForUpdate);
                            await _mediator.Send(command);
                        }
                        else
                        {
                            // update article with low quantity flag
                            ArticleForUpdateDto articleForUpdate = new ArticleForUpdateDto();

                            articleForUpdate.InternalReference = article.InternalReference;
                            articleForUpdate.SKU = article.SKU;
                            articleForUpdate.Description = article.Description;
                            articleForUpdate.Price = article.Price;
                            articleForUpdate.PriceWithPromotion = article.PriceWithPromotion;
                            articleForUpdate.BrandId = article.BrandId;
                            articleForUpdate.FamilyId = article.FamilyId;
                            articleForUpdate.SubFamilyId = article.SubFamilyId;
                            articleForUpdate.CollectionId = article.CollectionId;
                            articleForUpdate.Generic1 = article.Generic1;
                            articleForUpdate.RowNumber = article.RowNumber;
                            articleForUpdate.MainArticleImageUrl = article.MainArticleImageUrl;
                            articleForUpdate.Url = article.Url;
                            articleForUpdate.MetaName = article.MetaName;
                            articleForUpdate.MetaDescription = article.MetaDescription;
                            articleForUpdate.IsOutOfStock = false;
                            articleForUpdate.IsLowStock = false;
                            articleForUpdate.IsPublished = article.IsPublished;

                            var command = new UpdateArticle.UpdateArticleCommand(article.Id, articleForUpdate);
                            await _mediator.Send(command);
                        }
                    }
                }
                catch (Exception ex)
                {
                    var a = ex;
                }
            }
        }

        await Task.CompletedTask;
    }
}