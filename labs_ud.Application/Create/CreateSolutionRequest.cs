namespace labs_ud.Application.Create;

public record CreateSolutionRequest (
    string TaskId,
    int Mark,
    string Answer);