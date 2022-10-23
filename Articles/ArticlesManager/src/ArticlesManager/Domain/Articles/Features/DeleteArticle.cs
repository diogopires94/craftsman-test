namespace ArticlesManager.Domain.Articles.Features;

using ArticlesManager.Domain.Articles.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteArticle
{
    public class DeleteArticleCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteArticleCommand(Guid article)
        {
            Id = article;
        }
    }

    public class Handler : IRequestHandler<DeleteArticleCommand, bool>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IArticleRepository articleRepository, IUnitOfWork unitOfWork)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _articleRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _articleRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}