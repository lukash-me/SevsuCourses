namespace labs_ud.Application.Create;

public record CreateAnswerRequest
(
    string TaskId,
    string StudentId,
    string AnswerText);