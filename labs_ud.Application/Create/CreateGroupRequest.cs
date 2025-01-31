namespace labs_ud.Application.Create;

public record CreateGroupRequest(
    Guid CourseId,
    Guid? MentorId,
    DateOnly Start,
    DateOnly End,
    string Status
    );