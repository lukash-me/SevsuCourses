namespace labs_ud.Application.Get;

public record CourseResponse(
    Guid id,
    string title,
    string description,
    string photo
);