using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Teacher;

public class GetPhoneService
{
    private readonly TeacherRepository _teacherRepository;

    public GetPhoneService(TeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<Result<string, Error>> Handle(TeacherId teacherId, CancellationToken cancellationToken = default)
    {
        var teacherResult = await _teacherRepository.GetById(teacherId, cancellationToken);
        var teacher = teacherResult.Value;

        var phone = teacher.Phone;
        
        return phone;
    }
}