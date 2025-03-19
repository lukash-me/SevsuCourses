using System.Security.Cryptography;
using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Answer;

public class GetAllAnswersByStudentIdTaskIdService
{
    private readonly AnswerRepository _answerRepository;

    public GetAllAnswersByStudentIdTaskIdService(AnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<Result<List<AnswerResponse>, Error>> Handle(AnswersRequest request, CancellationToken cancellationToken = default)
    {
        var answerResult = await _answerRepository.GetByTaskIdStudentId(request, cancellationToken);

        if (answerResult.IsFailure) 
            return answerResult.Error;
        
        var answers = answerResult.Value;

        var response = new List<AnswerResponse>();
        foreach (var answer in answers)
        {
            var id = answer.Id;
            var mark = answer.Mark;
            var replyText = answer.ReplyText;
            var answerText = answer.AnswerText;
            var dateSent = answer.DateSent;
            var dateReplied = answer.DateReplied;
            var answerResponse = new AnswerResponse(id, mark, replyText, answerText, dateSent, dateReplied); 
            
            response.Add(answerResponse);
        }
        
        return response;
    }
}

public record AnswerResponse(
    Guid Id,
    int Mark,
    string? ReplyText,
    string AnswerText,
    DateTime DateSent,
    DateTime? DateReplied
    );

public record AnswersRequest(
    Guid StudentId,
    Guid TaskId
    );