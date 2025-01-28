using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Answer;

public class GetLastAnswerService
{
    private readonly AnswerRepository _answerRepository;

    public GetLastAnswerService(AnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<Result<AnswerResponse, Error>> Handle(List<AnswerResponse> answers, CancellationToken cancellationToken = default)
    {
        var response = answers.Aggregate((max, current) =>
            current.DateSent > max.DateSent ? current : max);
        
        return response;
    }
}