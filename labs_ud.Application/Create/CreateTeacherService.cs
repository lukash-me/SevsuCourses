using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Create;

public class CreateTeacherService
{
    private readonly TeacherRepository _teacherRepository;

    public CreateTeacherService(TeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<Result<Guid, Error>> Handle(CreateTeacherRequest request, CancellationToken cancellationToken)
    {
        var fio = request.Fio;

        var experience = request.Experience;
        
        var phone  = request.Phone;

        var email = request.Email;

        var education = request.Education;

        var photo = request.Photo;

        var description = request.Description;

        var login = request.Login;
        
        var password = request.Password;

        var teacherResult = Teacher.Create(
            fio,
            experience,
            phone,
            email,
            education,
            photo,
            description,
            login,
            password);

        if (teacherResult.IsFailure)
            return teacherResult.Error;
        
        await _teacherRepository.Add(teacherResult.Value, cancellationToken);

        return teacherResult.Value.Id;
    }
}