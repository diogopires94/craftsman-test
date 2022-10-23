namespace ArticlesManager.Domain.ArticleImages.Features;

using ArticlesManager.Domain.ArticleImages.Services;
using ArticlesManager.Domain.ArticleImages;
using ArticlesManager.Domain.ArticleImages.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddArticleImage
{
    public class AddArticleImageCommand : IRequest<ArticleImageDto>
    {
        public readonly ArticleImageForCreationDto ArticleImageToAdd;

        public AddArticleImageCommand(ArticleImageForCreationDto articleImageToAdd)
        {
            ArticleImageToAdd = articleImageToAdd;
        }
    }

    public class Handler : IRequestHandler<AddArticleImageCommand, ArticleImageDto>
    {
        private readonly IArticleImageRepository _articleImageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IArticleImageRepository articleImageRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _articleImageRepository = articleImageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ArticleImageDto> Handle(AddArticleImageCommand request, CancellationToken cancellationToken)
        {
            var articleImage = ArticleImage.Create(request.ArticleImageToAdd);
            await _articleImageRepository.Add(articleImage, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var articleImageAdded = await _articleImageRepository.GetById(articleImage.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ArticleImageDto>(articleImageAdded);
        }
    }
}