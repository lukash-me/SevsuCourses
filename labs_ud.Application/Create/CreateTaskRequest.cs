namespace labs_ud.Application.Create;

public record CreateTaskRequest
(
    Guid ThemeId,
    string Title,
    string Condition,
    int AttempsAmount,
    int MinMark,
    int MaxMark
);