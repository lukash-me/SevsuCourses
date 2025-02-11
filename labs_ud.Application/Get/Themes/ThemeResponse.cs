namespace labs_ud.Application.Get.Themes;

public record ThemeResponse
(
    Guid Id,
    string Title,
    string Text,
    string Photo,
    int Number
);