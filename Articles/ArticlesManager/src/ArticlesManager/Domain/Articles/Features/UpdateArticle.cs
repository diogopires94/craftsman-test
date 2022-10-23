namespace ArticlesManager.Domain.Articles.Features;

using ArticlesManager.Domain.Articles;
using ArticlesManager.Domain.Articles.Dtos;
using ArticlesManager.Domain.Articles.Validators;
using ArticlesManager.Domain.Articles.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateArticle
{
    public class UpdateArticleCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly ArticleForUpdateDto ArticleToUpdate;

        public UpdateArticleCommand(Guid article, ArticleForUpdateDto newArticleData)
        {
            Id = article;
            ArticleToUpdate = newArticleData;
        }
    }

    public class Handler : IRequestHandler<UpdateArticleCommand, bool>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IArticleRepository articleRepository, IUnitOfWork unitOfWork)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var articleToUpdate = await _articleRepository.GetById(request.Id, cancellationToken: cancellationToken);

            articleToUpdate.Update(request.ArticleToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}