using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Student;

public class GetPhoneService
{
    private readonly StudentRepository _studentRepository;

    public GetPhoneService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Result<string, Error>> Handle(StudentId studentId, CancellationToken cancellationToken = default)
    {
        var studentResult = await _studentRepository.GetById(studentId, cancellationToken);
        var student = studentResult.Value;

        var phone = student.Phone;
        
        return phone;
    }
}