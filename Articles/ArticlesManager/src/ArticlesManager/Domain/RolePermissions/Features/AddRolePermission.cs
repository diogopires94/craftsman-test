namespace ArticlesManager.Domain.RolePermissions.Features;

using ArticlesManager.Domain.RolePermissions.Services;
using ArticlesManager.Domain.RolePermissions;
using ArticlesManager.Domain.RolePermissions.Dtos;
using ArticlesManager.Services;
using SharedKernel.Exceptions;
using ArticlesManager.Domain;
using HeimGuard;
using MapsterMapper;
using MediatR;

public static class AddRolePermission
{
    public class AddRolePermissionCommand : IRequest<RolePermissionDto>
    {
        public readonly RolePermissionForCreationDto RolePermissionToAdd;

        public AddRolePermissionCommand(RolePermissionForCreationDto rolePermissionToAdd)
        {
            RolePermissionToAdd = rolePermissionToAdd;
        }
    }

    public class Handler : IRequestHandler<AddRolePermissionCommand, RolePermissionDto>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHeimGuardClient _heimGuard;

        public Handler(IRolePermissionRepository rolePermissionRepository, IUnitOfWork unitOfWork, IMapper mapper, IHeimGuardClient heimGuard)
        {
            _mapper = mapper;
            _rolePermissionRepository = rolePermissionRepository;
            _unitOfWork = unitOfWork;
            _heimGuard = heimGuard;
        }

        public async Task<RolePermissionDto> Handle(AddRolePermissionCommand request, CancellationToken cancellationToken)
        {
            await _heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanAddRolePermission);

            var rolePermission = RolePermission.Create(request.RolePermissionToAdd);
            await _rolePermissionRepository.Add(rolePermission, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var rolePermissionAdded = await _rolePermissionRepository.GetById(rolePermission.Id, cancellationToken: cancellationToken);
            return _mapper.Map<RolePermissionDto>(rolePermissionAdded);
        }
    }
}