namespace ArticlesManager.Domain.SizeTableLines.Features;

using ArticlesManager.Domain.SizeTableLines.Services;
using ArticlesManager.Domain.SizeTableLines;
using ArticlesManager.Domain.SizeTableLines.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddSizeTableLine
{
    public class AddSizeTableLineCommand : IRequest<SizeTableLineDto>
    {
        public readonly SizeTableLineForCreationDto SizeTableLineToAdd;

        public AddSizeTableLineCommand(SizeTableLineForCreationDto sizeTableLineToAdd)
        {
            SizeTableLineToAdd = sizeTableLineToAdd;
        }
    }

    public class Handler : IRequestHandler<AddSizeTableLineCommand, SizeTableLineDto>
    {
        private readonly ISizeTableLineRepository _sizeTableLineRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(ISizeTableLineRepository sizeTableLineRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _sizeTableLineRepository = sizeTableLineRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SizeTableLineDto> Handle(AddSizeTableLineCommand request, CancellationToken cancellationToken)
        {
            var sizeTableLine = SizeTableLine.Create(request.SizeTableLineToAdd);
            await _sizeTableLineRepository.Add(sizeTableLine, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var sizeTableLineAdded = await _sizeTableLineRepository.GetById(sizeTableLine.Id, cancellationToken: cancellationToken);
            return _mapper.Map<SizeTableLineDto>(sizeTableLineAdded);
        }
    }
}