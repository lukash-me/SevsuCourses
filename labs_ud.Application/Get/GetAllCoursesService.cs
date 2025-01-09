using CSharpFunctionalExtensions;
using labs_ud.Application.Create;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get;

public class GetAllCoursesService
{
    private readonly CourseRepository _courseRepository;

    public GetAllCoursesService(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }


    public async Task<Result<List<CourseResponse>, Error>> Handle(CancellationToken cancellationToken = default)
    {
        var courses = await _courseRepository.GetAll(cancellationToken);

        var response = new List<CourseResponse>();
        
        foreach (var course in courses.Value)
        {
            var id = course.Id;
            var title = course.Title;
            var description = course.Description;
            var photo = course.Photo;

            var courseResponse = new CourseResponse(id, title, description, photo);
            response.Add(courseResponse);
        }

        return response;
    }
}