namespace labs_ud.Application.Create;

public record CreateAnswerRequest
(
    Guid TaskId,
    Guid StudentId,
    int Mark,
    string ReplyText,
    string AnswerText,
    DateTime DateSent);