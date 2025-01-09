using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Delete;

public class DeleteGroupService
{
    private readonly GroupRepository _groupRepository;

    public DeleteGroupService(GroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        DeleteGroupRequest request, 
        CancellationToken cancellationToken = default)
    {
        var groupResult = await _groupRepository.GetById(request.GroupId, cancellationToken);
        if (groupResult.IsFailure)
            return groupResult.Error;

        _groupRepository.Delete(groupResult.Value, cancellationToken);

        return groupResult.Value.Id;
    }
}