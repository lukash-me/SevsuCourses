using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Course;

public class GetTitlesAndIdsByTeacherIdService
{
    private readonly CourseRepository _courseRepository;

    public GetTitlesAndIdsByTeacherIdService(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<List<IdTitleResponse>, Error>> Handle(TeacherId teacherId, CancellationToken cancellationToken = default)
    {
        var coursesResult = await _courseRepository.GetByTeacherId(teacherId, cancellationToken);

        if (coursesResult.IsFailure)
            return coursesResult.Error;
        
        var courses = coursesResult.Value;
        var response = new List<IdTitleResponse>();
        
        foreach (var course in courses)
        {
            var courseId = course.Id;
            var title = course.Title;

            var result = new IdTitleResponse(courseId, title);
            
            response.Add(result);
        }
        
        response = response.OrderBy(r=>r.Title).ToList();

        return response;
    }
}

public record IdTitleResponse(
    Guid CourseId,
    string Title
    );