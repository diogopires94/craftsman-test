namespace ArticlesManager.Domain.ArticleImages.Features;

using ArticlesManager.Domain.ArticleImages.Dtos;
using ArticlesManager.Domain.ArticleImages.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetArticleImage
{
    public class ArticleImageQuery : IRequest<ArticleImageDto>
    {
        public readonly Guid Id;

        public ArticleImageQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<ArticleImageQuery, ArticleImageDto>
    {
        private readonly IArticleImageRepository _articleImageRepository;
        private readonly IMapper _mapper;

        public Handler(IArticleImageRepository articleImageRepository, IMapper mapper)
        {
            _mapper = mapper;
            _articleImageRepository = articleImageRepository;
        }

        public async Task<ArticleImageDto> Handle(ArticleImageQuery request, CancellationToken cancellationToken)
        {
            var result = await _articleImageRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ArticleImageDto>(result);
        }
    }
}