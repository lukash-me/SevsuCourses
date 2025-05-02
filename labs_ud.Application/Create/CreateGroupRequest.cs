namespace labs_ud.Application.Create;

public record CreateGroupRequest(
    string CourseId,
    string? MentorId,
    DateOnly Start,
    DateOnly End,
    string Status
    );