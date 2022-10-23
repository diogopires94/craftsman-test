namespace ArticlesManager.Domain.Urls.Features;

using ArticlesManager.Domain.Urls;
using ArticlesManager.Domain.Urls.Dtos;
using ArticlesManager.Domain.Urls.Validators;
using ArticlesManager.Domain.Urls.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateUrl
{
    public class UpdateUrlCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly UrlForUpdateDto UrlToUpdate;

        public UpdateUrlCommand(Guid url, UrlForUpdateDto newUrlData)
        {
            Id = url;
            UrlToUpdate = newUrlData;
        }
    }

    public class Handler : IRequestHandler<UpdateUrlCommand, bool>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUrlRepository urlRepository, IUnitOfWork unitOfWork)
        {
            _urlRepository = urlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateUrlCommand request, CancellationToken cancellationToken)
        {
            var urlToUpdate = await _urlRepository.GetById(request.Id, cancellationToken: cancellationToken);

            urlToUpdate.Update(request.UrlToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}