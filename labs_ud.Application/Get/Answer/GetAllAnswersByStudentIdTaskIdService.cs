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
        var answers = answerResult.Value;

        var response = new List<AnswerResponse>();
        foreach (var answer in answers)
        {
            var mark = answer.Mark;
            var replyText = answer.ReplyText;
            var answerText = answer.AnswerText;
            var dateSent = answer.DateSent;
            var answerResponse = new AnswerResponse(mark, replyText, answerText, dateSent); 
            
            response.Add(answerResponse);
        }
        
        return response;
    }
}

public record AnswerResponse(
    int Mark,
    string ReplyText,
    string AnswerText,
    DateTime DateSent
    );

public record AnswersRequest(
    Guid StudentId,
    Guid TaskId
    );