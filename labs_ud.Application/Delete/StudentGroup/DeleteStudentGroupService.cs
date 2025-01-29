using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Delete.StudentGroup;

public class DeleteStudentGroupService
{
    private readonly StudentGroupRepository _studentGroupRepository;

    public DeleteStudentGroupService(StudentGroupRepository studentGroupRepository)
    {
        _studentGroupRepository = studentGroupRepository;
    }

    public async Task<Result<Guid, Error>> Handle(
        DeleteStudentGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        var studentGroupResult = await _studentGroupRepository.GetById(request.StudentGroupId, cancellationToken);
        if (studentGroupResult.IsFailure)
            return studentGroupResult.Error;

        _studentGroupRepository.Delete(studentGroupResult.Value, cancellationToken);
        
        return studentGroupResult.Value.Id;
    }
}

public record DeleteStudentGroupRequest(Guid StudentGroupId);