namespace ArticlesManager.Domain.SubFamilies.Mappings;

using ArticlesManager.Domain.SubFamilies.Dtos;
using ArticlesManager.Domain.SubFamilies;
using Mapster;

public class SubFamilyMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SubFamilyDto, SubFamily>()
            .TwoWays();
        config.NewConfig<SubFamilyForCreationDto, SubFamily>()
            .TwoWays();
        config.NewConfig<SubFamilyForUpdateDto, SubFamily>()
            .TwoWays();
    }
}