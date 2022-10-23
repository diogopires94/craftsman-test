namespace ArticlesManager.SharedTestHelpers.Fakes.SizeTable;

using AutoBogus;
using ArticlesManager.Domain.SizeTables;
using ArticlesManager.Domain.SizeTables.Dtos;

public class FakeSizeTable
{
    public static SizeTable Generate(SizeTableForCreationDto sizeTableForCreationDto)
    {
        return SizeTable.Create(sizeTableForCreationDto);
    }

    public static SizeTable Generate()
    {
        return SizeTable.Create(new FakeSizeTableForCreationDto().Generate());
    }
}