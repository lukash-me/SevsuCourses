namespace labs_ud.Application.Create;

public record CreateTaskRequest
(
    string ThemeId,
    string Title,
    string Condition,
    int MinMark,
    int MaxMark
);