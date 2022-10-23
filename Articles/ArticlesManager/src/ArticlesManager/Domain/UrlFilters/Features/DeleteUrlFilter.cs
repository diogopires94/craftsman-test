namespace ArticlesManager.Domain.UrlFilters.Features;

using ArticlesManager.Domain.UrlFilters.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteUrlFilter
{
    public class DeleteUrlFilterCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteUrlFilterCommand(Guid urlFilter)
        {
            Id = urlFilter;
        }
    }

    public class Handler : IRequestHandler<DeleteUrlFilterCommand, bool>
    {
        private readonly IUrlFilterRepository _urlFilterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUrlFilterRepository urlFilterRepository, IUnitOfWork unitOfWork)
        {
            _urlFilterRepository = urlFilterRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUrlFilterCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _urlFilterRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _urlFilterRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}