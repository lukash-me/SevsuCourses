using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Create;

public class CreateStudentService
{
    private readonly StudentRepository _studentRepository;

    public CreateStudentService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(CreateStudentRequest request, CancellationToken cancellationToken)
    {

        var fio = request.Fio;
        var isExpelled  = request.IsExpelled;
        var photo = request.Photo;
        var isAttest = request.IsAttest;
        var phone = request.Phone;
        var email = request.Email;
        var password = request.Password;

        var studentResult = Student.Create(
            fio,
            isExpelled,
            photo,
            isAttest,
            phone,
            email,
            password
        );

        if (studentResult.IsFailure)
            return studentResult.Error;
        
        await _studentRepository.Add(studentResult.Value, cancellationToken);

        return studentResult.Value.Id;
    }
}