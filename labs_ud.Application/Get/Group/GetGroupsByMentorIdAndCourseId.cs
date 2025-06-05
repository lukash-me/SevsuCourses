using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Group;

public class GetGroupsByMentorIdAndCourseId
{
    private readonly GroupRepository _groupRepository;

    public GetGroupsByMentorIdAndCourseId(GroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Result<GroupsResponse, Error>> Handle(GroupsRequest request, CancellationToken cancellationToken = default)
    {
        var groupsResult = await _groupRepository.GetByMentorIdAndCourseId(request, cancellationToken);

        if (groupsResult.IsFailure)
        {
            return groupsResult.Error;
        }
        
        var groupIds = groupsResult.Value.Select(x => x.Id).ToList();
        var response = new GroupsResponse(groupIds);
        
        return response;
    }
}

public record GroupsResponse(
    List<Guid> GroupIds
);

public record GroupsRequest(
    MentorId MentorId,
    CourseID CourseId
);