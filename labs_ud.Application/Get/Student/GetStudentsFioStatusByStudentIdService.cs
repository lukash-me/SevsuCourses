using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Student;

public class GetStudentsFioStatusByStudentIdService
{
    private readonly StudentRepository _studentRepository;

    public GetStudentsFioStatusByStudentIdService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    
    public async Task<Result<List<StudentFioStatusResponse>, Error>> Handle(List<Guid> studentIds, CancellationToken cancellationToken = default)
    {
        var response = new List<StudentFioStatusResponse>();
        
        foreach (var studentId in studentIds)
        {
            var studentResult = await _studentRepository.GetById(studentId, cancellationToken);

            if (studentResult.IsFailure)
            {
                return studentResult.Error;
            }
            
            var student = studentResult.Value;

            var id = student.Id;
            var fio = student.Fio;
            var isAttest = student.IsAttest;

            var result = new StudentFioStatusResponse(id, fio, isAttest);
            response.Add(result);
        }

        return response;
    }
}

public record StudentFioStatusResponse(
    Guid Id,
    string Fio,
    bool IsAttest
);