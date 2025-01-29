using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.StudentGroup;

public class GetStudentsByGroupIdService
{
    private readonly StudentGroupRepository _studentGroupRepository;

    public GetStudentsByGroupIdService(StudentGroupRepository studentGroupRepository)
    {
        _studentGroupRepository = studentGroupRepository;
    }
    
    public async Task<Result<List<Guid>, Error>> Handle(GroupId groupId, CancellationToken cancellationToken = default)
    {
        var studentGroupsResult = await _studentGroupRepository.GetByGroupId(groupId, cancellationToken);

        if (studentGroupsResult.IsFailure)
        {
            return studentGroupsResult.Error;
        }
        
        var studentGroups = studentGroupsResult.Value;
        var response = new List<Guid>();
        
        foreach (var studentGroup in studentGroups)
        {
            response.Add(studentGroup.StudentId);
        }

        return response;
    }
}
