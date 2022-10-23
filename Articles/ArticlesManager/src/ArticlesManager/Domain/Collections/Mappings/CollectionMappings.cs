namespace ArticlesManager.Domain.Collections.Mappings;

using ArticlesManager.Domain.Collections.Dtos;
using ArticlesManager.Domain.Collections;
using Mapster;

public class CollectionMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CollectionDto, Collection>()
            .TwoWays();
        config.NewConfig<CollectionForCreationDto, Collection>()
            .TwoWays();
        config.NewConfig<CollectionForUpdateDto, Collection>()
            .TwoWays();
    }
}