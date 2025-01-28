namespace labs_ud.Application.Create;

public record CreateAnswerRequest
(
    Guid TaskId,
    Guid StudentId,
    string AnswerText);