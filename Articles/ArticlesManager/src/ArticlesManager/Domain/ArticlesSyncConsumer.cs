namespace ArticlesManager.Domain;

using MapsterMapper;
using MassTransit;
using SharedKernel.Messages;
using System.Threading.Tasks;
using ArticlesManager.Databases;
using ArticlesManager.Domain.Barcodes.Features;
using ArticlesManager.Domain.Barcodes.Dtos;
using ArticlesManager.Domain.Articles.Features;
using ArticlesManager.Domain.SubFamilies.Features;
using ArticlesManager.Domain.SubFamilies.Dtos;
using ArticlesManager.Domain.Families.Features;
using ArticlesManager.Domain.Families.Dtos;
using ArticlesManager.Domain.Collections.Features;
using ArticlesManager.Domain.Collections.Dtos;
using ArticlesManager.Domain.Brands.Features;
using ArticlesManager.Domain.Brands.Dtos;
using ArticlesManager.Domain.Articles.Dtos;
using ArticlesManager.Domain.MaxiRetail;
using MediatR;
using Newtonsoft.Json;

public class ArticlesSyncConsumer : IConsumer<IArticles>
{
    private readonly IMapper _mapper;
    private readonly ArticlesDbContext _db;
    private readonly IMediator _mediator;


    public ArticlesSyncConsumer(ArticlesDbContext db, IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _db = db;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IArticles> context)
    {
        MaxiRetailArticles response = JsonConvert.DeserializeObject<MaxiRetailArticles>(context.Message.ArticlesJson);

        var size = response.Result.Length;

        //        Dictionary<string, string> familyUrlMapping = new Dictionary<string, string>{
        //    {"AT","ACESSÓRIOS TEENS"},
        //    {"UDD","DIVERSOS"},
        //    {"CU","CALÇADO UNISSEXO"},
        //    {"RG","ROUPA GIRL"},
        //    {"RM","ROUPA MAN"},
        //    {"RW","ROUPA WOMAN"},
        //    {"AM","ACESSÓRIOS MAN"},
        //    {"AW","ACESSÓRIOS WOMAN"},
        //    {"X15","VALE ANIVERSÁRIO"},
        //    {"CW","CALÇADO WOMAN"},
        //    {"AU","ACESSÓRIOS UNISSEXO"},
        //    {"CT","CALÇADO TEENS"},
        //    {"CM","CALÇADO MAN"},
        //    {"RB","ROUPA WOMAN"},
        //    {"CB","CALÇADO BABY"},
        //    {"CC","CALÇADO CHILD"},
        //    {"Z25","VALE ACUMULADO 25"},
        //    {"UCC","CONSUMIVEIS"},
        //    {"YCB","CHEQUE BRINDE"},
        //};


        foreach (var article in response.Result)
        {

            // Check if article exists if not
            Guid articleId = Guid.Empty;

            try
            {
                articleId = await FindOrCreateArticle(article);
            }
            catch (Exception ex)
            {
                Thread.Sleep(6000);
                try
                {
                    articleId = await FindOrCreateArticle(article);
                }
                catch (Exception ex1)
                {
                    throw ex1;
                }
            }


            //}
            //catch (Exception ex)
            //{
            //    var message = ex.Message;
            //    throw new Exception("Error on finding or creating article.");
            //}

            // Check each barcode for that articles is there, if not create
            foreach (var barcode in article.Barcodes)
            {
                if (!String.IsNullOrEmpty(barcode.Barcode))
                {
                    //      Check if Barcode exists if not create
                    try
                    {
                        await FindOrCreateBarcode(articleId, barcode);
                    }
                    catch (Exception ex)
                    {
                        var message = ex.Message;
                        throw new Exception("Error on creating FindOrCreateBarcode.");
                    }
                }
            }
        }

        await Task.CompletedTask;

    }

    private async Task<Guid> FindOrCreateArticle(MaxiRetailArticlesResult article)
    {
        ArticleParametersDto articleParametersDto = new ArticleParametersDto();

        articleParametersDto.Filters = $"InternalReference == {article.Reference}";

        var articlesQuery = new GetArticleList.ArticleListQuery(articleParametersDto);
        var articlesQueryResponse = await _mediator.Send(articlesQuery);

        //try
        //{
        if (articlesQueryResponse.Any() == false)
        {
            ArticleForCreationDto articleForCreation = new ArticleForCreationDto();

            if (article.Brand != null && !String.IsNullOrEmpty(article.Brand.Code))
            {
                try
                {
                    articleForCreation.BrandId = await FindOrCreateBrand(article);
                }
                catch
                {
                    try
                    {
                        Thread.Sleep(6000);
                        articleForCreation.BrandId = await FindOrCreateBrand(article);
                    }
                    catch
                    {
                        throw new Exception("Error on FindOrCreateBrand.");
                    }
                }
            }

            if (article.Collection != null && !String.IsNullOrEmpty(article.Collection.Code))
            {
                try
                {
                    articleForCreation.CollectionId = await FindOrCreateCollection(article);
                }
                catch
                {
                    try
                    {
                        Thread.Sleep(6000);
                        articleForCreation.CollectionId = await FindOrCreateCollection(article);
                    }
                    catch
                    {
                        throw new Exception("Error on FindOrCreateCollection.");
                    }
                }
            }

            var familyCode = string.Empty;

            if (article.Family != null && !String.IsNullOrEmpty(article.Generic_0))
            {
                //      Check if Sub Family exists if not create
                var family = article.Generic_0.Split(" - ");
                familyCode = family[0];
                var familyDescription = family.Length > 1 ? family[1] : "";

                try
                {
                    articleForCreation.FamilyId = await FindOrCreateFamily(familyCode, familyDescription);
                }
                catch
                {
                    try
                    {
                        Thread.Sleep(6000);
                        articleForCreation.FamilyId = await FindOrCreateFamily(familyCode, familyDescription);
                    }
                    catch
                    {
                        throw new Exception("Error on FindOrCreateFamily.");
                    }
                }
            }

            //      Check if SubFamily exists if not create
            if (!String.IsNullOrEmpty(article.Family.Code))
            {
                try
                {
                    articleForCreation.SubFamilyId = await FindOrCreateSubFamily(article);
                }
                catch
                {
                    try
                    {
                        Thread.Sleep(6000);
                        articleForCreation.SubFamilyId = await FindOrCreateSubFamily(article);
                    }
                    catch
                    {
                        throw new Exception("Error on FindOrCreateSubFamily.");
                    }
                }
            }

            // set other article properties
            articleForCreation.InternalReference = article.Reference;
            articleForCreation.SKU = article.Generic_2;
            articleForCreation.Description = article.Description;
            articleForCreation.Price = article.Price;
            articleForCreation.PriceWithPromotion = article.Price;
            articleForCreation.Generic1 = article.Generic_1;
            articleForCreation.RowNumber = article.ROW_NUMBER.ToString();
            articleForCreation.IsOutOfStock = true;
            articleForCreation.IsLowStock = true;
            articleForCreation.IsPublished = false;

            string articleName = article.Description.Replace(" ", "-").Replace("/", "-").Replace("`", "-").Replace("´", "-").Replace("--", "-").Replace("ç", "c").Replace("é", "e")
                .Replace("ã", "a").Replace("ü", "u").Replace("õ", "o").ToLower();
            //string articleFamily = familyCode.ToLower();//familyUrlMapping.ContainsKey(familyCode) ? familyUrlMapping[familyCode] : "";
            //string articleSubFamily = article.Family.Description.ToLower();


            articleForCreation.Url = $"/{articleName}/{articleForCreation.SKU}";

            if (article.Barcodes != null && article.Barcodes.Any())
            {
                double firstBarCodePrice = article.Barcodes[0].price;
                if (firstBarCodePrice > 0)
                    articleForCreation.Price = article.Barcodes[0].price;
            }

            //      Create article

            try
            {
                var articleCommand = new AddArticle.AddArticleCommand(articleForCreation);
                var articleCommandResponse = await _mediator.Send(articleCommand);

                return articleCommandResponse.Id;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw new Exception("Error on creating article.");
            }

            // End article creation

        }
        else
        {
            return articlesQueryResponse.First().Id;
        }
    }

    private async Task FindOrCreateBarcode(Guid articleId, MaxiRetailArticlesBarcode barcode)
    {
        BarcodeParametersDto barcodeParametersDto = new BarcodeParametersDto();

        barcodeParametersDto.Filters = $"BarcodeValue == {barcode.Barcode}";

        var barcodeQuery = new GetBarcodeList.BarcodeListQuery(barcodeParametersDto);
        var barcodeQueryResponse = await _mediator.Send(barcodeQuery);

        if (barcodeQueryResponse.Any() == false)
        {
            BarcodeForCreationDto barcodeForCreation = new BarcodeForCreationDto();
            barcodeForCreation.ArticleId = articleId;
            barcodeForCreation.BarcodeValue = barcode.Barcode;
            barcodeForCreation.Size_Description = barcode.ColumnDescription;
            barcodeForCreation.Size = barcode.ColumnCode;
            barcodeForCreation.Color_Code = barcode.RowCode;
            barcodeForCreation.Color_Description = barcode.RowDescription;
            barcodeForCreation.Price = barcode.price;
            barcodeForCreation.StockQuantity = 0;
            barcodeForCreation.ReservedQuantity = 0;

            var command = new AddBarcode.AddBarcodeCommand(barcodeForCreation);
            var commandResponse = await _mediator.Send(command);
        }
    }

    private async Task<Guid> FindOrCreateSubFamily(MaxiRetailArticlesResult article)
    {
        SubFamilyParametersDto subFamilysParametersDto = new SubFamilyParametersDto();

        subFamilysParametersDto.Filters = $"Code == {article.Family.Code}";

        var subFamilysQuery = new GetSubFamilyList.SubFamilyListQuery(subFamilysParametersDto);
        var subFamilysQueryResponse = await _mediator.Send(subFamilysQuery);

        if (subFamilysQueryResponse.Any() == false)
        {
            SubFamilyForCreationDto subFamilyForCreation = new SubFamilyForCreationDto();
            subFamilyForCreation.Code = article.Family.Code;
            subFamilyForCreation.Description = article.Family.Description;
            var command = new AddSubFamily.AddSubFamilyCommand(subFamilyForCreation);
            var commandResponse = await _mediator.Send(command);
            return commandResponse.Id;
        }
        else
        {
            return subFamilysQueryResponse.First().Id;
        }
    }

    private async Task<Guid> FindOrCreateFamily(string familyCode, string familyDescription)
    {
        FamilyParametersDto familysParametersDto = new FamilyParametersDto();

        familysParametersDto.Filters = $"Code == {familyCode}";

        var familysQuery = new GetFamilyList.FamilyListQuery(familysParametersDto);
        var familysQueryResponse = await _mediator.Send(familysQuery);

        if (familysQueryResponse.Any() == false)
        {
            FamilyForCreationDto familyForCreation = new FamilyForCreationDto();
            familyForCreation.Code = familyCode;
            familyForCreation.Description = familyDescription;
            var command = new AddFamily.AddFamilyCommand(familyForCreation);
            var commandResponse = await _mediator.Send(command);
            return commandResponse.Id;
        }
        else
        {
            return familysQueryResponse.First().Id;
        }
    }

    private async Task<Guid> FindOrCreateCollection(MaxiRetailArticlesResult article)
    {
        //      Check if Collection exists if not create
        CollectionParametersDto collectionParametersDto = new CollectionParametersDto();

        collectionParametersDto.Filters = $"Code == {article.Collection.Code}";

        var collectionQuery = new GetCollectionList.CollectionListQuery(collectionParametersDto);
        var collectionQueryResponse = await _mediator.Send(collectionQuery);


        if (collectionQueryResponse.Any() == false)
        {
            CollectionForCreationDto collectionForCreation = new CollectionForCreationDto();
            collectionForCreation.Code = article.Collection.Code;
            collectionForCreation.Description = article.Collection.Description;
            var command = new AddCollection.AddCollectionCommand(collectionForCreation);
            var commandResponse = await _mediator.Send(command);
            return commandResponse.Id;
        }
        else
        {
            return collectionQueryResponse.First().Id;
        }
    }

    private async Task<Guid> FindOrCreateBrand(MaxiRetailArticlesResult article)
    {
        //      Check if Brand exists if not create
        BrandParametersDto brandParametersDto = new BrandParametersDto();

        brandParametersDto.Filters = $"Code == {article.Brand.Code}";

        var brandQuery = new GetBrandList.BrandListQuery(brandParametersDto);
        var brandQueryResponse = await _mediator.Send(brandQuery);

        if (brandQueryResponse.Any() == false)
        {
            BrandForCreationDto brandForCreation = new BrandForCreationDto();
            brandForCreation.Code = article.Brand.Code;
            brandForCreation.Description = article.Brand.Description;
            var command = new AddBrand.AddBrandCommand(brandForCreation);
            var commandResponse = await _mediator.Send(command);
            return commandResponse.Id;
        }
        else
        {
            return brandQueryResponse.First().Id;
        }
    }
}