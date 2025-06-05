using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Answer;

public class GetStatusBestMarkDatesByStudentIdTaskIdService
{
    private readonly AnswerRepository _answerRepository;
    

    public GetStatusBestMarkDatesByStudentIdTaskIdService(AnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<Result<AnswerSuccessResponse, Error>> Handle(List<AnswerResponse> answers, CancellationToken cancellationToken = default)
    {
        var bestAnswer = answers.Aggregate((max, current) =>
            current.Mark > max.Mark ? current : max);

        if (bestAnswer.Mark == 0)
        {
            var status = "На проверке";
            var bestMark = bestAnswer.Mark;
            var dateSent = bestAnswer.DateSent;
            DateTime? dateReplied = null;
            
            var response = new AnswerSuccessResponse(status, bestMark, dateSent, dateReplied);
            
            return response;
        }
        else
        {
            var status = "Проверено";
            var bestMark = bestAnswer.Mark;
            var dateSent = bestAnswer.DateSent;
            var dateReplied = bestAnswer.DateReplied;
            
            var response = new AnswerSuccessResponse(status, bestMark, dateSent, dateReplied);
            
            return response;
        }
    }
}

public record AnswerSuccessResponse(
    string Status,
    int? BestMark,
    DateTime DateSent,
    DateTime? DateReplied
    );