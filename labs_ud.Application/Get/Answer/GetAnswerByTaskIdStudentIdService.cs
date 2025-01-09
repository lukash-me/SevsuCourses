using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Answer;

public class GetAnswerByTaskIdStudentIdService
{
    private readonly AnswerRepository _answerRepository;

    public GetAnswerByTaskIdStudentIdService(AnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<Result<AnswerResponse, Error>> Handle(AnswerRequest request, CancellationToken cancellationToken = default)
    {
        var answerResult = await _answerRepository.GetByTaskIdStudentId(request, cancellationToken);
        var answer = answerResult.Value;

        if (answer is null)
        {
            return new AnswerResponse(
                null,
                null,
                null,
                null,
                null
            );
        }
        
        var response = new AnswerResponse(
            answer.Id,
            answer.AttempNumber,
            answer.Mark,
            answer.ReplyText,
            answer.AnswerText);
        
        return response;
    }
}

public record AnswerResponse(
    Guid? Id,
    int? AttempNumber,
    int? Mark,
    string? ReplyText,
    string? AnswerText
);

public record AnswerRequest(
    Guid TaskId,
    Guid StudentId);