namespace ArticlesManager.Domain.SizeTableLines.Features;

using ArticlesManager.Domain.SizeTableLines;
using ArticlesManager.Domain.SizeTableLines.Dtos;
using ArticlesManager.Domain.SizeTableLines.Validators;
using ArticlesManager.Domain.SizeTableLines.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateSizeTableLine
{
    public class UpdateSizeTableLineCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly SizeTableLineForUpdateDto SizeTableLineToUpdate;

        public UpdateSizeTableLineCommand(Guid sizeTableLine, SizeTableLineForUpdateDto newSizeTableLineData)
        {
            Id = sizeTableLine;
            SizeTableLineToUpdate = newSizeTableLineData;
        }
    }

    public class Handler : IRequestHandler<UpdateSizeTableLineCommand, bool>
    {
        private readonly ISizeTableLineRepository _sizeTableLineRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ISizeTableLineRepository sizeTableLineRepository, IUnitOfWork unitOfWork)
        {
            _sizeTableLineRepository = sizeTableLineRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateSizeTableLineCommand request, CancellationToken cancellationToken)
        {
            var sizeTableLineToUpdate = await _sizeTableLineRepository.GetById(request.Id, cancellationToken: cancellationToken);

            sizeTableLineToUpdate.Update(request.SizeTableLineToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}