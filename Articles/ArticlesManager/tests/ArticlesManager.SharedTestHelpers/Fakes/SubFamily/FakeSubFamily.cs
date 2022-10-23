namespace ArticlesManager.SharedTestHelpers.Fakes.SubFamily;

using AutoBogus;
using ArticlesManager.Domain.SubFamilies;
using ArticlesManager.Domain.SubFamilies.Dtos;

public class FakeSubFamily
{
    public static SubFamily Generate(SubFamilyForCreationDto subFamilyForCreationDto)
    {
        return SubFamily.Create(subFamilyForCreationDto);
    }

    public static SubFamily Generate()
    {
        return SubFamily.Create(new FakeSubFamilyForCreationDto().Generate());
    }
}