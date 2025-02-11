namespace labs_ud.Application.Create;

public record CreateThemeDto(
    string Title,
    string? Text,
    string? Photo
);

public record CreateThemeRequest(
    Guid CourseId,
    int Number,
    CreateThemeDto Dto
);
