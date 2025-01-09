using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Delete;

public class DeleteCourseService
{
    private readonly CourseRepository _courseRepository;

    public DeleteCourseService(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        DeleteCourseRequest request, 
        CancellationToken cancellationToken = default)
    {
        var courseResult = await _courseRepository.GetById(request.CourseId, cancellationToken);
        if (courseResult.IsFailure)
            return courseResult.Error;

        _courseRepository.Delete(courseResult.Value, cancellationToken);

        return courseResult.Value.Id;
    }
}