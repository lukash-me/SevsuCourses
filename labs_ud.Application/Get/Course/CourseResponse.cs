namespace labs_ud.Application.Get.Course;

public record CourseResponse(
    Guid id,
    string title,
    string description,
    string photo
);

public record CoursesPageResponse(
    List<CourseResponse> Courses,
    int PagesAmount
);