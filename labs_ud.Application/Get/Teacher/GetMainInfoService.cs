using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Teacher;

public class GetMainInfoService
{
    private readonly TeacherRepository _teacherRepository;

    public GetMainInfoService(TeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<Result<MainInfoDto, Error>> Handle(TeacherId teacherId, CancellationToken cancellationToken = default)
    {
        var teacherResult = await _teacherRepository.GetById(teacherId, cancellationToken);
        var teacher = teacherResult.Value;

        var fio = teacher.Fio;
        var experience = teacher.Experience;
        var email = teacher.Email;
        var education = teacher.Education;
        var description = teacher.Description;
        var photo = teacher.Photo;

        var response = new MainInfoDto(fio, experience, email, education, description, photo);
        return response;
    }
}