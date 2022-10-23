namespace ArticlesManager.SharedTestHelpers.Fakes.SizeTableLine;

using AutoBogus;
using ArticlesManager.Domain.SizeTableLines;
using ArticlesManager.Domain.SizeTableLines.Dtos;

public class FakeSizeTableLine
{
    public static SizeTableLine Generate(SizeTableLineForCreationDto sizeTableLineForCreationDto)
    {
        return SizeTableLine.Create(sizeTableLineForCreationDto);
    }

    public static SizeTableLine Generate()
    {
        return SizeTableLine.Create(new FakeSizeTableLineForCreationDto().Generate());
    }
}