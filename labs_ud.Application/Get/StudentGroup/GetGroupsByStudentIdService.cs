using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;
    
namespace labs_ud.Application.Get.StudentGroup;

public class GetGroupsByStudentIdService
{
    private readonly StudentGroupRepository _studentGroupRepository;

    public GetGroupsByStudentIdService(StudentGroupRepository studentGroupRepository)
    {
        _studentGroupRepository = studentGroupRepository;
    }

    public async Task<Result<List<Guid>, Error>> Handle(StudentId studentId, CancellationToken cancellationToken = default)
    {
        var groupsResult = await _studentGroupRepository.GetByStudentId(studentId, cancellationToken);

        if (groupsResult.IsFailure)
            return groupsResult.Error;
        
        var groups = groupsResult.Value;

        var response = new List<Guid>();
        foreach (var group in groups)
        {
            response.Add(group.GroupId);
        }

        return response;
    }
}