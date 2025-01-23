using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Group;

public class GetGroupByIdService
{
    private readonly GroupRepository _groupRepository;

    public GetGroupByIdService(GroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Result<GroupResponse, Error>> Handle(GroupId groupId, CancellationToken cancellationToken = default)
    {
        var groupResult = await _groupRepository.GetById(groupId, cancellationToken);
        var group = groupResult.Value;
        
        
        var id = group.Id;
        var courseId = group.CourseId;
        var mentorId = group.MentorId;
        var start = group.Start;
        var end = group.End;
        var status = group.Status;

        var response = new GroupResponse(id, courseId, mentorId, start, end, status);

        return response;
    }
}

public record GroupResponse(
    Guid Id,
    Guid CourseId,
    Guid MentorId,
    DateOnly Start,
    DateOnly End,
    string Status
    );