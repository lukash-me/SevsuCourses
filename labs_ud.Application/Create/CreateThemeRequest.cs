namespace labs_ud.Application.Create;

public record CreateThemeRequest(
    Guid CourseId,
    string Title,
    string Description,
    string Text,
    string Photo,
    int Number
);
