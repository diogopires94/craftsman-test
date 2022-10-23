namespace ArticlesManager.Domain.SizeTables.Features;

using ArticlesManager.Domain.SizeTables.Dtos;
using ArticlesManager.Domain.SizeTables.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetSizeTable
{
    public class SizeTableQuery : IRequest<SizeTableDto>
    {
        public readonly Guid Id;

        public SizeTableQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<SizeTableQuery, SizeTableDto>
    {
        private readonly ISizeTableRepository _sizeTableRepository;
        private readonly IMapper _mapper;

        public Handler(ISizeTableRepository sizeTableRepository, IMapper mapper)
        {
            _mapper = mapper;
            _sizeTableRepository = sizeTableRepository;
        }

        public async Task<SizeTableDto> Handle(SizeTableQuery request, CancellationToken cancellationToken)
        {
            var result = await _sizeTableRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<SizeTableDto>(result);
        }
    }
}