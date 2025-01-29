using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Get.Task;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Answer;

public class GetBestAnswersToThemeService
{
    private readonly GetStatusBestMarkDatesByStudentIdTaskIdService _bestAnswerInfoService;
    private readonly GetAllAnswersByStudentIdTaskIdService _allAnswersService;

    public GetBestAnswersToThemeService(GetStatusBestMarkDatesByStudentIdTaskIdService bestAnswerInfoService, GetAllAnswersByStudentIdTaskIdService allAnswersService)
    {
        _bestAnswerInfoService = bestAnswerInfoService;
        _allAnswersService = allAnswersService;
    }

    public async Task<Result<List<BestAnswerInfo>, Error>> Handle(BulkAnswersRequest request,
        CancellationToken cancellationToken = default)
    {
        var tasks = request.Tasks;
        
        var response = new List<BestAnswerInfo>();
        
        foreach (var task in tasks)
        {
            var allAnswersRequest = new AnswersRequest(request.StudentId, task.Id);
            var allAnswersResult = _allAnswersService.Handle(allAnswersRequest).Result;
            
            if (allAnswersResult.IsFailure)
                return allAnswersResult.Error;
            
            var bestAnswerResult = _bestAnswerInfoService.Handle(allAnswersResult.Value, cancellationToken).Result;
            
            if (bestAnswerResult.IsFailure)
                return bestAnswerResult.Error;
            
            var bestAnswer = bestAnswerResult.Value;
            
            var title = task.Title;
            var status = bestAnswer.Status;
            var bestMark = bestAnswer.BestMark;
            var dateSent = bestAnswer.DateSent;
            var dateReplied = bestAnswer.DateReplied;
            
            var bestAnswerInfo = new BestAnswerInfo(title, status, bestMark, dateSent, dateReplied);
            
            response.Add(bestAnswerInfo);
        }

        return response;
    }
}

public record BestAnswerInfo(
    string Title,
    string Status,
    int BestMark,
    DateTime DateSent,
    DateTime? DateReplied
    );

public record BulkAnswersRequest(
        Guid StudentId,
        List<TaskByThemeResponse> Tasks);