using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Get.Task;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Update.Answer;

public class UpdateReplyAndMarkService
{
    private readonly AnswerRepository _answerRepository;
    private readonly GetMainInfoService _taskMainInfoService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateReplyAndMarkService(
        AnswerRepository answerRepository, 
        IUnitOfWork unitOfWork, 
        GetMainInfoService taskMainInfoService)
    {
        _answerRepository = answerRepository;
        _unitOfWork = unitOfWork;
        _taskMainInfoService = taskMainInfoService;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdateReplyAndMarkRequest request,
        CancellationToken cancellationToken)
    {
        var answerResult = await _answerRepository.GetById(request.AnswerId, cancellationToken);
        if (answerResult.IsFailure)
            return answerResult.Error;

        var taskMainInfoResult = await _taskMainInfoService.Handle(answerResult.Value.TaskId);
        if (taskMainInfoResult.IsFailure)
        {
            return taskMainInfoResult.Error;
        }

        var taskMainInfo = taskMainInfoResult.Value;

        if (request.Dto.Mark > taskMainInfo.MaxMark || request.Dto.Mark < taskMainInfo.MinMark)
        {
            return Errors.Errors.General.ValueIsInvalid("mark");
        }

        if (request.Dto.ReplyText.Length > Constants.Values.HUGE_TEXT)
        {
            return Errors.Errors.General.InvalidLength("reply text");
        }

        if (string.IsNullOrWhiteSpace(request.Dto.ReplyText))
        {
            return Errors.Errors.General.ValueIsRequired("reply text");
        }
        
        answerResult.Value.UpdateReplyAndMark(request.Dto.ReplyText, request.Dto.Mark);
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.AnswerId;
    }
}

public record UpdateReplyAndMarkRequest(
    Guid AnswerId,
    UpdateReplyAndMarkDto Dto
    );
    
public record UpdateReplyAndMarkDto(
    string ReplyText,
    int Mark);