namespace labs_ud.Application.Create;

public record CreateAnswerRequest
(
    Guid TaskId,
    Guid StudentId,
    int AttempNumber,
    int Mark,
    string ReplyText,
    string AnswerText);