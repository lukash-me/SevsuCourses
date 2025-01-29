using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get.Answer;
using labs_ud.Application.Get.Task;
using labs_ud.Application.Update.Answer;
using labs_ud.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace labs_ud.Controllers;

[ApiController]
[Route("[controller]")]

public class AnswerController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAnswer(
        [FromServices] CreateAnswerService service,
        [FromBody] CreateAnswerRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получение всех ответов по id студента и id задачи
    /// </summary>
    /// <param name="service"></param>
    /// <param name="taskId">id задачи</param>
    /// <param name="studentId">id студента</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("all/taskId={taskId:guid}&studentId={studentId:guid}")]
    public async Task<ActionResult<Guid>> GetAllByStudentIdTaskId(
        [FromRoute] Guid studentId,
        [FromRoute] Guid taskId, 
        [FromServices] GetAllAnswersByStudentIdTaskIdService service,
        CancellationToken cancellationToken = default)
    {
        var request = new AnswersRequest(studentId, taskId);
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получить лучший ответ на задачу по id студента и id задачи
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="taskId"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("best/taskId={taskId:guid}&studentId={studentId:guid}")]
    public async Task<ActionResult<Guid>> GetStatusBestMarkDatesByStudentIdTaskId(
        [FromRoute] Guid studentId,
        [FromRoute] Guid taskId, 
        [FromServices] GetStatusBestMarkDatesByStudentIdTaskIdService bestAnswerService,
        [FromServices] GetAllAnswersByStudentIdTaskIdService allAnswersService,
        CancellationToken cancellationToken = default)
    {
        var request = new AnswersRequest(studentId, taskId);
        
        var answersResult = allAnswersService.Handle(request, cancellationToken).Result;

        if (answersResult.IsFailure)
            return answersResult.Error.ToResponse();
        
        var result = await bestAnswerService.Handle(answersResult.Value, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получить все лучшие ответы для темы по id темы и id пользователя
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="themeId"></param>
    /// <param name="allTasksByThemeService"></param>
    /// <param name="bestAnswersToThemeService"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("best/theme/themeId={themeId:guid}&studentId={studentId:guid}")]
    public async Task<ActionResult<Guid>> GetBestAnswersToThemeTasks(
        [FromRoute] Guid studentId,
        [FromRoute] Guid themeId,
        [FromServices] GetTasksByThemeService allTasksByThemeService,
        [FromServices] GetBestAnswersToThemeService bestAnswersToThemeService,
        CancellationToken cancellationToken = default)
    {
        var tasksResult = allTasksByThemeService.Handle(themeId, cancellationToken).Result;
        
        if (tasksResult.IsFailure)
            return tasksResult.Error.ToResponse();
        
        var request = new BulkAnswersRequest(studentId, tasksResult.Value);
        
        var result = bestAnswersToThemeService.Handle(request, cancellationToken).Result;

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получить последний по дате ответ по id студента и id задачи
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="taskId"></param>
    /// <param name="allAnswersService"></param>
    /// <param name="lastAnswerService"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("last/taskId={taskId:guid}&studentId={studentId:guid}")]
    public async Task<ActionResult<Guid>> GetLastByStudentIdTaskId(
        [FromRoute] Guid studentId,
        [FromRoute] Guid taskId, 
        [FromServices] GetAllAnswersByStudentIdTaskIdService allAnswersService,
        [FromServices] GetLastAnswerService lastAnswerService,
        CancellationToken cancellationToken = default)
    {
        var request = new AnswersRequest(studentId, taskId);
        var allAnswers = await allAnswersService.Handle(request, cancellationToken);
        
        if (allAnswers.IsFailure)
            return allAnswers.Error.ToResponse();
        
        var result = await lastAnswerService.Handle(allAnswers.Value, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Обновление текста ответа по id ответа
    /// </summary>
    /// <param name="answerId"></param>
    /// <param name="dto"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{answerId:guid}/text")]
    public async Task<ActionResult<Guid>> UpdateAnswerText(
        [FromRoute] Guid answerId,
        [FromBody] UpdateAnswerTextDto dto,
        [FromServices] UpdateAnswerTextService service,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateAnswerTextRequest(answerId, dto);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Обновление ответа ментора и оценки по id ответа студента
    /// </summary>
    /// <param name="answerId"></param>
    /// <param name="dto"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{answerId:guid}/reply-mark")]
    public async Task<ActionResult<Guid>> UpdateReplyAndMark(
        [FromRoute] Guid answerId,
        [FromBody] UpdateReplyAndMarkDto dto,
        [FromServices] UpdateReplyAndMarkService service,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateReplyAndMarkRequest(answerId, dto);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteAnswerService service,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteAnswerRequest(id);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}