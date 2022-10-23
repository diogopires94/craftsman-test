namespace ArticlesManager.Domain.UrlFilters.Features;

using ArticlesManager.Domain.UrlFilters;
using ArticlesManager.Domain.UrlFilters.Dtos;
using ArticlesManager.Domain.UrlFilters.Validators;
using ArticlesManager.Domain.UrlFilters.Services;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateUrlFilter
{
    public class UpdateUrlFilterCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly UrlFilterForUpdateDto UrlFilterToUpdate;

        public UpdateUrlFilterCommand(Guid urlFilter, UrlFilterForUpdateDto newUrlFilterData)
        {
            Id = urlFilter;
            UrlFilterToUpdate = newUrlFilterData;
        }
    }

    public class Handler : IRequestHandler<UpdateUrlFilterCommand, bool>
    {
        private readonly IUrlFilterRepository _urlFilterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUrlFilterRepository urlFilterRepository, IUnitOfWork unitOfWork)
        {
            _urlFilterRepository = urlFilterRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateUrlFilterCommand request, CancellationToken cancellationToken)
        {
            var urlFilterToUpdate = await _urlFilterRepository.GetById(request.Id, cancellationToken: cancellationToken);

            urlFilterToUpdate.Update(request.UrlFilterToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}