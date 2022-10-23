namespace ArticlesManager.Domain.ArticleImages.Features;

using ArticlesManager.Domain.ArticleImages;
using ArticlesManager.Domain.ArticleImages.Dtos;
using ArticlesManager.Domain.ArticleImages.Validators;
using ArticlesManager.Domain.ArticleImages.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateArticleImage
{
    public class UpdateArticleImageCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly ArticleImageForUpdateDto ArticleImageToUpdate;

        public UpdateArticleImageCommand(Guid articleImage, ArticleImageForUpdateDto newArticleImageData)
        {
            Id = articleImage;
            ArticleImageToUpdate = newArticleImageData;
        }
    }

    public class Handler : IRequestHandler<UpdateArticleImageCommand, bool>
    {
        private readonly IArticleImageRepository _articleImageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IArticleImageRepository articleImageRepository, IUnitOfWork unitOfWork)
        {
            _articleImageRepository = articleImageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateArticleImageCommand request, CancellationToken cancellationToken)
        {
            var articleImageToUpdate = await _articleImageRepository.GetById(request.Id, cancellationToken: cancellationToken);

            articleImageToUpdate.Update(request.ArticleImageToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}