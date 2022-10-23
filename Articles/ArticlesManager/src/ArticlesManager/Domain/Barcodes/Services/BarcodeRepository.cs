namespace ArticlesManager.Domain.Barcodes.Services;

using ArticlesManager.Domain.Barcodes;
using ArticlesManager.Databases;
using ArticlesManager.Services;

public interface IBarcodeRepository : IGenericRepository<Barcode>
{
}

public class BarcodeRepository : GenericRepository<Barcode>, IBarcodeRepository
{
    private readonly ArticlesDbContext _dbContext;

    public BarcodeRepository(ArticlesDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
