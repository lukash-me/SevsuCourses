using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Student;

public class GetStudentByLoginPasswordService
{
    private readonly StudentRepository _studentRepository;

    public GetStudentByLoginPasswordService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Result<Guid, Error>> Handle(StudentRequest request, CancellationToken cancellationToken = default)
    {
        var studentResult = await _studentRepository.GetByLoginPassword(request, cancellationToken);
        var studentId = studentResult.Value.Id;
        
        return studentId;
    }
}

public record StudentRequest(
    string Email,
    string Password);