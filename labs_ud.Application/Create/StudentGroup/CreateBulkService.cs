using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Create.StudentGroup;

public class CreateBulkService
{
    private readonly StudentGroupRepository _studentGroupRepository;

    public CreateBulkService(StudentGroupRepository studentGroupRepository)
    {
        _studentGroupRepository = studentGroupRepository;
    }
    
    public async Task<Result<List<Guid>, Error>> Handle(CreateBulkStudentGroupRequest request, CancellationToken cancellationToken)
    {
        var students = request.Dto.StudentIds;
        var groupId = request.GroupId;

        var response = new List<Guid>();
        foreach (var student in students)
        {
            var studentGroupResult = Entities.StudentGroup.Create(student, groupId);
            
            if (studentGroupResult.IsFailure)
                return studentGroupResult.Error;
            
            await _studentGroupRepository.Add(studentGroupResult.Value, cancellationToken);
            
            response.Add(studentGroupResult.Value.Id);
        }

        return response;
    }
}

public record CreateBulkStudentGroupRequest(
    Guid GroupId,
    CreateBulkStudentGroupDto Dto
    );
    
public record CreateBulkStudentGroupDto(
    List<Guid> StudentIds
    );