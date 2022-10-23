namespace ArticlesManager.Domain.SizeTables.Features;

using ArticlesManager.Domain.SizeTables.Services;
using ArticlesManager.Domain.SizeTables;
using ArticlesManager.Domain.SizeTables.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddSizeTable
{
    public class AddSizeTableCommand : IRequest<SizeTableDto>
    {
        public readonly SizeTableForCreationDto SizeTableToAdd;

        public AddSizeTableCommand(SizeTableForCreationDto sizeTableToAdd)
        {
            SizeTableToAdd = sizeTableToAdd;
        }
    }

    public class Handler : IRequestHandler<AddSizeTableCommand, SizeTableDto>
    {
        private readonly ISizeTableRepository _sizeTableRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(ISizeTableRepository sizeTableRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _sizeTableRepository = sizeTableRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SizeTableDto> Handle(AddSizeTableCommand request, CancellationToken cancellationToken)
        {
            var sizeTable = SizeTable.Create(request.SizeTableToAdd);
            await _sizeTableRepository.Add(sizeTable, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var sizeTableAdded = await _sizeTableRepository.GetById(sizeTable.Id, cancellationToken: cancellationToken);
            return _mapper.Map<SizeTableDto>(sizeTableAdded);
        }
    }
}