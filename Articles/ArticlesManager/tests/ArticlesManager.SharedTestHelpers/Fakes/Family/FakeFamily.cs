namespace ArticlesManager.SharedTestHelpers.Fakes.Family;

using AutoBogus;
using ArticlesManager.Domain.Families;
using ArticlesManager.Domain.Families.Dtos;

public class FakeFamily
{
    public static Family Generate(FamilyForCreationDto familyForCreationDto)
    {
        return Family.Create(familyForCreationDto);
    }

    public static Family Generate()
    {
        return Family.Create(new FakeFamilyForCreationDto().Generate());
    }
}