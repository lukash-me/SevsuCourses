using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Create.StudentGroup;

public class CreateStudentGroupService
{
    private readonly StudentGroupRepository _studentGroupRepository;

    public CreateStudentGroupService(StudentGroupRepository studentGroupRepository)
    {
        _studentGroupRepository = studentGroupRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(CreateStudentGroupRequest request, CancellationToken cancellationToken)
    {
        var studentId = request.StudentId;
        var groupId = request.GroupId;

        var studentGroupResult = Entities.StudentGroup.Create(
            studentId,
            groupId
        );

        if (studentGroupResult.IsFailure)
            return studentGroupResult.Error;
        
        await _studentGroupRepository.Add(studentGroupResult.Value, cancellationToken);

        return studentGroupResult.Value.Id;
    }
}

public record CreateStudentGroupRequest
(
    Guid StudentId,
    Guid GroupId
);