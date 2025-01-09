using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get;

public class GetCourseByIdService
{
    private readonly CourseRepository _courseRepository;

    public GetCourseByIdService(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<CourseResponse, Error>> Handle(CourseID courseId, CancellationToken cancellationToken = default)
    {
        var courseResult = await _courseRepository.GetById(courseId, cancellationToken);
        var course = courseResult.Value;
        
        
        var id = course.Id;
        var title = course.Title;
        var description = course.Description;
        var photo = course.Photo;

        var response = new CourseResponse(id, title, description, photo);

        return response;
    }
}