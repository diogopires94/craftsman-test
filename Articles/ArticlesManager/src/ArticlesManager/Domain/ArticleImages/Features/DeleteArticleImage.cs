namespace ArticlesManager.Domain.ArticleImages.Features;

using ArticlesManager.Domain.ArticleImages.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteArticleImage
{
    public class DeleteArticleImageCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteArticleImageCommand(Guid articleImage)
        {
            Id = articleImage;
        }
    }

    public class Handler : IRequestHandler<DeleteArticleImageCommand, bool>
    {
        private readonly IArticleImageRepository _articleImageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IArticleImageRepository articleImageRepository, IUnitOfWork unitOfWork)
        {
            _articleImageRepository = articleImageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteArticleImageCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _articleImageRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _articleImageRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}