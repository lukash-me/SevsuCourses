using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Create;

public class CreateCourseService
{
    private readonly CourseRepository _courseRepository;

    public CreateCourseService(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(CreateCourseRequest request, CancellationToken cancellationToken)
    {
        var title = request.Title;

        var description = request.Description;
        
        var photo  = request.Photo;

        var teacherId = request.TeacherId;

        var courseResult = Course.Create(
            teacherId, title, description, photo);

        if (courseResult.IsFailure)
            return courseResult.Error;
        
        await _courseRepository.Add(courseResult.Value, cancellationToken);

        return courseResult.Value.Id;
    }
}