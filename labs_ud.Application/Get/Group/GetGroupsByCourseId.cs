using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Group;

public class GetGroupsByCourseId
{
    private readonly GroupRepository _groupRepository;

    public GetGroupsByCourseId(GroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Result<GroupsResponse, Error>> Handle(Guid CourseId, CancellationToken cancellationToken = default)
    {
        var groupsResult = await _groupRepository.GetByCourseId(CourseId, cancellationToken);

        if (groupsResult.IsFailure)
        {
            return groupsResult.Error;
        }
        
        var groupIds = groupsResult.Value.Select(x => x.Id).ToList();
        var response = new GroupsResponse(groupIds);
        
        return response;
    }
}