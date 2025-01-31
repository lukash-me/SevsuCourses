using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Get.Student;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Teacher;

public class GetTeacherByLoginPasswordService
{
    private readonly TeacherRepository _teacherRepository;

    public GetTeacherByLoginPasswordService(TeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(TeacherRequest request, CancellationToken cancellationToken = default)
    {
        var teacherResult = await _teacherRepository.GetByLoginPassword(request, cancellationToken);
        var teacherId = teacherResult.Value.Id;
        
        return teacherId;
    }
}

public record TeacherRequest(
    string Email,
    string Password);