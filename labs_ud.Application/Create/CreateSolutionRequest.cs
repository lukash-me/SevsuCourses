namespace labs_ud.Application.Create;

public record CreateSolutionRequest (
    Guid TaskId,
    int Mark,
    string Answer);