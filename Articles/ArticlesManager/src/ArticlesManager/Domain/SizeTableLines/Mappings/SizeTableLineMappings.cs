namespace ArticlesManager.Domain.SizeTableLines.Mappings;

using ArticlesManager.Domain.SizeTableLines.Dtos;
using ArticlesManager.Domain.SizeTableLines;
using Mapster;

public class SizeTableLineMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SizeTableLineDto, SizeTableLine>()
            .TwoWays();
        config.NewConfig<SizeTableLineForCreationDto, SizeTableLine>()
            .TwoWays();
        config.NewConfig<SizeTableLineForUpdateDto, SizeTableLine>()
            .TwoWays();
    }
}