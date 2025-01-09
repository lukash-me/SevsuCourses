namespace labs_ud.Application.Get.Themes;

public record ThemeResponse
(
    Guid Id,
    string Title,
    string Description,
    string Text,
    string Photo,
    int Number
);