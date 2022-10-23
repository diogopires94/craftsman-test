namespace ArticlesManager.Domain.Urls.Features;

using ArticlesManager.Domain.Urls.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteUrl
{
    public class DeleteUrlCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteUrlCommand(Guid url)
        {
            Id = url;
        }
    }

    public class Handler : IRequestHandler<DeleteUrlCommand, bool>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUrlRepository urlRepository, IUnitOfWork unitOfWork)
        {
            _urlRepository = urlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUrlCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _urlRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _urlRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}