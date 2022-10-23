namespace ArticlesManager.Domain.Articles.Features;

using ArticlesManager.Domain.Articles.Services;
using ArticlesManager.Domain.Articles;
using ArticlesManager.Domain.Articles.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddArticle
{
    public class AddArticleCommand : IRequest<ArticleDto>
    {
        public readonly ArticleForCreationDto ArticleToAdd;

        public AddArticleCommand(ArticleForCreationDto articleToAdd)
        {
            ArticleToAdd = articleToAdd;
        }
    }

    public class Handler : IRequestHandler<AddArticleCommand, ArticleDto>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IArticleRepository articleRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ArticleDto> Handle(AddArticleCommand request, CancellationToken cancellationToken)
        {
            var article = Article.Create(request.ArticleToAdd);
            await _articleRepository.Add(article, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var articleAdded = await _articleRepository.GetById(article.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ArticleDto>(articleAdded);
        }
    }
}