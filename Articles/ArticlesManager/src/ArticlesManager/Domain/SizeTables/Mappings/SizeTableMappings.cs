namespace ArticlesManager.Domain.SizeTables.Mappings;

using ArticlesManager.Domain.SizeTables.Dtos;
using ArticlesManager.Domain.SizeTables;
using Mapster;

public class SizeTableMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SizeTableDto, SizeTable>()
            .TwoWays();
        config.NewConfig<SizeTableForCreationDto, SizeTable>()
            .TwoWays();
        config.NewConfig<SizeTableForUpdateDto, SizeTable>()
            .TwoWays();
    }
}