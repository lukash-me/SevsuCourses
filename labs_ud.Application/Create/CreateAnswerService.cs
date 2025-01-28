using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Create;

public class CreateAnswerService
{
    private readonly AnswerRepository _answerRepository;

    public CreateAnswerService(AnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(CreateAnswerRequest request, CancellationToken cancellationToken)
    {
        var taskId = request.TaskId;
        var studentId = request.StudentId;
        var mark = request.Mark;
        var replyText = request.ReplyText;
        var answerText = request.AnswerText;
        var dateSent = request.DateSent;

        var answerResult = Answer.Create(
            taskId,
            studentId,
            mark,
            replyText,
            answerText,
            dateSent
        );

        if (answerResult.IsFailure)
            return answerResult.Error;
        
        await _answerRepository.Add(answerResult.Value, cancellationToken);

        return answerResult.Value.Id;
    }
}