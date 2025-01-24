using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Student;

public class GetMainInfoService
{
    private readonly StudentRepository _studentRepository;

    public GetMainInfoService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Result<MainInfoResponse, Error>> Handle(StudentId studentId, CancellationToken cancellationToken = default)
    {
        var studentResult = await _studentRepository.GetById(studentId, cancellationToken);
        var student = studentResult.Value;

        var fio = student.Fio;
        var photo = student.Photo;
        var email = student.Email;

        var response = new MainInfoResponse(fio, photo, email);
        
        return response;
    }
}

public record MainInfoResponse(
    string Fio,
    string Photo,
    string Email
    );
