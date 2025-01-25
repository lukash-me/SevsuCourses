using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get.Answer;
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
    /// Получение ответа по id задачи и id студента
    /// </summary>
    /// <param name="service"></param>
    /// <param name="taskId">id задачи</param>
    /// <param name="studentId">id студента</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("taskId={taskId:guid}&studentId={studentId:guid}")]
    public async Task<ActionResult<Guid>> GetByTaskIdStudentIdService(
        [FromServices] GetAnswerByTaskIdStudentIdService service,
        [FromRoute] Guid taskId, 
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken = default)
    {
        var request = new AnswerRequest(taskId, studentId);
        var result = await service.Handle(request, cancellationToken);

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