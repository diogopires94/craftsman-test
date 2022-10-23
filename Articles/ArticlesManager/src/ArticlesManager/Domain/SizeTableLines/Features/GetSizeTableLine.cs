namespace ArticlesManager.Domain.SizeTableLines.Features;

using ArticlesManager.Domain.SizeTableLines.Dtos;
using ArticlesManager.Domain.SizeTableLines.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetSizeTableLine
{
    public class SizeTableLineQuery : IRequest<SizeTableLineDto>
    {
        public readonly Guid Id;

        public SizeTableLineQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<SizeTableLineQuery, SizeTableLineDto>
    {
        private readonly ISizeTableLineRepository _sizeTableLineRepository;
        private readonly IMapper _mapper;

        public Handler(ISizeTableLineRepository sizeTableLineRepository, IMapper mapper)
        {
            _mapper = mapper;
            _sizeTableLineRepository = sizeTableLineRepository;
        }

        public async Task<SizeTableLineDto> Handle(SizeTableLineQuery request, CancellationToken cancellationToken)
        {
            var result = await _sizeTableLineRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<SizeTableLineDto>(result);
        }
    }
}