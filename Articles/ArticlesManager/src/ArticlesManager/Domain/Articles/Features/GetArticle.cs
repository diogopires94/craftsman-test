namespace ArticlesManager.Domain.Articles.Features;

using ArticlesManager.Domain.Articles.Dtos;
using ArticlesManager.Domain.Articles.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetArticle
{
    public class ArticleQuery : IRequest<ArticleDto>
    {
        public readonly Guid Id;

        public ArticleQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<ArticleQuery, ArticleDto>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public Handler(IArticleRepository articleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
        }

        public async Task<ArticleDto> Handle(ArticleQuery request, CancellationToken cancellationToken)
        {
            var result = await _articleRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ArticleDto>(result);
        }
    }
}