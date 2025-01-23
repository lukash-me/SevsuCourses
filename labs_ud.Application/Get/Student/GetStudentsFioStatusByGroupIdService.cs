using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Student;

public class GetStudentsFioStatusByGroupIdService
{
    private readonly StudentRepository _studentRepository;

    public GetStudentsFioStatusByGroupIdService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    
    public async Task<Result<List<StudentFioStatusResponse>, Error>> Handle(GroupId groupId, CancellationToken cancellationToken = default)
    {
        var studentsResult = await _studentRepository.GetByGroupId(groupId, cancellationToken);
        var students = studentsResult.Value;
        
        var response = new List<StudentFioStatusResponse>();
        
        foreach (var student in students)
        {
            var id = student.Id;
            var fio = student.Fio;
            var isAttest = student.IsAttest;

            var studentResponse = new StudentFioStatusResponse(id, fio, isAttest);
            response.Add(studentResponse);
        }

        return response;
    }
}

public record StudentFioStatusResponse(
    Guid Id,
    string Fio,
    bool isAttest
    );