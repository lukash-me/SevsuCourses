using labs_ud.Application.Entities;

namespace labs_ud.Application.Create;

public record CreateCourseRequest(
    Guid TeacherId,
    string Title,
    string? Description,
    string? Photo
);
