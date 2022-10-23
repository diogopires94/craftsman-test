namespace ArticlesManager.Domain.Families.Mappings;

using ArticlesManager.Domain.Families.Dtos;
using ArticlesManager.Domain.Families;
using Mapster;

public class FamilyMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FamilyDto, Family>()
            .TwoWays();
        config.NewConfig<FamilyForCreationDto, Family>()
            .TwoWays();
        config.NewConfig<FamilyForUpdateDto, Family>()
            .TwoWays();
    }
}