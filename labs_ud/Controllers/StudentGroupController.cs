using labs_ud.Application.Create;
using labs_ud.Application.Create.StudentGroup;
using labs_ud.Application.Delete;
using labs_ud.Application.Delete.StudentGroup;
using labs_ud.Application.Get.Course;
using labs_ud.Application.Get.StudentGroup;
using labs_ud.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace labs_ud.Controllers;

[ApiController]
[Route("[controller]")]

public class StudentGroupController : ControllerBase
{
    /// <summary>
    /// Создание Студент-Группа связи
    /// </summary>
    /// <param name="service"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateStudentGroupService service,
        [FromBody] CreateStudentGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Добавление множества студентов в группу по id группы и id студентов
    /// </summary>
    /// <param name="service"></param>
    /// <param name="groupId"></param>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("bulk/{groupId:guid}")]
    public async Task<ActionResult<Guid>> CreateBulk(
        [FromServices] CreateBulkService service,
        [FromRoute] Guid groupId,
        [FromBody] CreateBulkStudentGroupDto dto,
        CancellationToken cancellationToken = default)
    {
        var request = new CreateBulkStudentGroupRequest(groupId, dto);
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получение id групп, в которых обучается студент по его id
    /// </summary>
    /// <param name="service"></param>
    /// <param name="studentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("groups/{studentId:guid}")]
    public async Task<ActionResult<Guid>> GetGroupId(
        [FromServices] GetGroupsByStudentIdService service,
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(studentId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получение id студентов, которые обучаются в группе по id группы
    /// </summary>
    /// <param name="service"></param>
    /// <param name="groupId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("students/{groupId:guid}")]
    public async Task<ActionResult<Guid>> GetGroupId(
        [FromServices] GetStudentsByGroupIdService service,
        [FromRoute] Guid groupId,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(groupId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpDelete("{studentGroupId:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteStudentGroupService service,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteStudentGroupRequest(id);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}


