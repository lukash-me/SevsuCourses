using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Course;

public class GetPageService
{
    private readonly CourseRepository _courseRepository;

    public GetPageService(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    private CourseResponse makeCourseResponse(Entities.Course course)
    {
        var id = course.Id;
        var title = course.Title;
        var description = course.Description;
        var photo = course.Photo;

        var courseResponse = new CourseResponse(id, title, description, photo);

        return courseResponse;
    }
    
    public async Task<Result<CoursesPageResponse, Error>> Handle(int pageNumber, CancellationToken cancellationToken = default)
    {
        var coursesResult = await _courseRepository.GetAll(cancellationToken);

        if (coursesResult.IsFailure)
        {
            return coursesResult.Error;
        }

        var coursesResponse = new List<CourseResponse>();

        var courses = coursesResult.Value;
        
        var start = pageNumber * Constants.Values.COURSES_ON_PAGE_AMOUNT - 1;
        var finish = start + Constants.Values.COURSES_ON_PAGE_AMOUNT;

        if (pageNumber == 0)
        {
            start = 0;
            finish = 7;
        }
        
        for (int i = start; i < finish; i++)
        {
            if (i > courses.Length - 1)
            {
                break;
            }
            
            var course = courses[i];

            var courseResponse = makeCourseResponse(course);
            
            coursesResponse.Add(courseResponse);
        }
        
        var pagesAmount = (int)Math.Ceiling(Convert.ToDouble((courses.Length + 1) / Convert.ToDouble(Constants.Values.COURSES_ON_PAGE_AMOUNT)));

        var response = new CoursesPageResponse(coursesResponse, pagesAmount);

        return response;
    }
}

