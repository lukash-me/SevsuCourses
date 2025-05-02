using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Create;

public class CreateGroupService
{
    private readonly GroupRepository _groupRepository;

    public CreateGroupService(GroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        var courseId = request.CourseId;

        var mentorId = request.MentorId;
        
        var start  = request.Start;

        var end = request.End;
        
        var status = request.Status;

        var groupResult = Group.Create(
            courseId,
            mentorId,
            start,
            end,
            status
        );

        if (groupResult.IsFailure)
            return groupResult.Error;
        
        await _groupRepository.Add(groupResult.Value, cancellationToken);

        return groupResult.Value.Id;
    }
}