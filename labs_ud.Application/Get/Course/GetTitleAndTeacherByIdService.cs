using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Course;

public class GetTitleAndTeacherByIdService
{
    private readonly CourseRepository _courseRepository;

    public GetTitleAndTeacherByIdService(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<CourseTitleAndTeacherResponse, Error>> Handle(CourseID courseId, CancellationToken cancellationToken = default)
    {
        var courseResult = await _courseRepository.GetById(courseId, cancellationToken);
        var course = courseResult.Value;
        
        var title = course.Title;
        var teacherId = course.TeacherId;

        var response = new CourseTitleAndTeacherResponse(title, teacherId);

        return response;
    }
}

public record CourseTitleAndTeacherResponse(
    string Title,
    Guid TeacherId
    );