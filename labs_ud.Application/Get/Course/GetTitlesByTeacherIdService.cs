using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Course;

public class GetTitlesByTeacherIdService
{
    private readonly CourseRepository _courseRepository;

    public GetTitlesByTeacherIdService(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<List<string>, Error>> Handle(TeacherId teacherId, CancellationToken cancellationToken = default)
    {
        var coursesResult = await _courseRepository.GetByTeacherId(teacherId, cancellationToken);
        var courses = coursesResult.Value;
        
        var response = new List<string>();
        
        foreach (var course in courses)
        {
            var title = course.Title;
            
            response.Add(title);
        }

        return response;
    }
}