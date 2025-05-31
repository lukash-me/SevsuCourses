using CSharpFunctionalExtensions;
using labs_ud.Application.Entities.Validation;
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
    
    public async Task<Result<List<Guid>, Error>> Handle(StudentsRequest request, CancellationToken cancellationToken = default)
    {
        if (request.groupIds.Count == 0)
        {
            return Errors.Errors.General.ValueIsRequired("group id");
        }

        var groups = request.groupIds;
        var studentGroupsAllGroups = new List<List<Entities.StudentGroup>>();

        foreach (var group in groups)
        {
            var validationResult = Ids.CheckGuid(group, "group id");
            if (validationResult.IsFailure)
            {
                return validationResult.Error;
            }
            
            var studentGroupsResult = await _studentGroupRepository.GetByGroupId(Guid.Parse(group), cancellationToken);

            if (studentGroupsResult.IsFailure)
            {
                return studentGroupsResult.Error;
            }
            studentGroupsAllGroups.Add(studentGroupsResult.Value);
        }
        
        var response = new List<Guid>();
        
        foreach (var studentGroups in studentGroupsAllGroups)
        {
            foreach (var studentGroup in studentGroups)
            {
                response.Add(studentGroup.StudentId);
            }
        }

        return response;
    }
}

public record StudentsRequest(
    List<string> groupIds);


