namespace ArticlesManager.SharedTestHelpers.Fakes.Collection;

using AutoBogus;
using ArticlesManager.Domain.Collections;
using ArticlesManager.Domain.Collections.Dtos;

public class FakeCollection
{
    public static Collection Generate(CollectionForCreationDto collectionForCreationDto)
    {
        return Collection.Create(collectionForCreationDto);
    }

    public static Collection Generate()
    {
        return Collection.Create(new FakeCollectionForCreationDto().Generate());
    }
}