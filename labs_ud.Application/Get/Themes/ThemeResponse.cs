namespace labs_ud.Application.Get.Themes;

public record ThemeResponse
(
    Guid Id,
    Guid CourseId,
    string Title,
    string? Text,
    string? Photo,
    int Number
);