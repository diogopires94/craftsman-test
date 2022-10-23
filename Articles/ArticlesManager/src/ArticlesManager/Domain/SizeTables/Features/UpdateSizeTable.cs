namespace ArticlesManager.Domain.SizeTables.Features;

using ArticlesManager.Domain.SizeTables;
using ArticlesManager.Domain.SizeTables.Dtos;
using ArticlesManager.Domain.SizeTables.Validators;
using ArticlesManager.Domain.SizeTables.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateSizeTable
{
    public class UpdateSizeTableCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly SizeTableForUpdateDto SizeTableToUpdate;

        public UpdateSizeTableCommand(Guid sizeTable, SizeTableForUpdateDto newSizeTableData)
        {
            Id = sizeTable;
            SizeTableToUpdate = newSizeTableData;
        }
    }

    public class Handler : IRequestHandler<UpdateSizeTableCommand, bool>
    {
        private readonly ISizeTableRepository _sizeTableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ISizeTableRepository sizeTableRepository, IUnitOfWork unitOfWork)
        {
            _sizeTableRepository = sizeTableRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateSizeTableCommand request, CancellationToken cancellationToken)
        {
            var sizeTableToUpdate = await _sizeTableRepository.GetById(request.Id, cancellationToken: cancellationToken);

            sizeTableToUpdate.Update(request.SizeTableToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}