using labs_ud.Application.Entities;

namespace labs_ud.Application.Create;

public record CreateCourseRequest(
    string TeacherId,
    string Title,
    string? Description,
    string? Photo
);
