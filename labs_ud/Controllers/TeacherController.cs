using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get.Teacher;
using labs_ud.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace labs_ud.Controllers;

[ApiController]
[Route("[controller]")]

public class TeacherController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateTeacher(
        [FromServices] CreateTeacherService service,
        [FromBody] CreateTeacherRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получить id преподавателя по логину и паролю
    /// </summary>
    /// <param name="service"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<Guid>> GetByLoginPassword(
        [FromServices] GetTeacherByLoginPasswordService service,
        [FromBody] TeacherRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получение ФИО учителя по id учителя
    /// </summary>
    /// <param name="teacherId"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("fio/{teacherId:guid}")]
    public async Task<ActionResult<Guid>> GetFioByTeacherId(
        [FromRoute] Guid teacherId,
        [FromServices] GetTeacherFioByIdService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(teacherId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получение основной информации о преподавателе по id преподавателя
    /// </summary>
    /// <param name="teacherId"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("main-info/{teacherId:guid}")]
    public async Task<ActionResult<Guid>> GetMainInfo(
        [FromRoute] Guid teacherId,
        [FromServices] GetMainInfoService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(teacherId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получение номера телефона по id преподавателя
    /// </summary>
    /// <param name="teacherId"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("phone/{teacherId:guid}")]
    public async Task<ActionResult<Guid>> GetPhone(
        [FromRoute] Guid teacherId,
        [FromServices] GetPhoneService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(teacherId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteTeacherService service,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteTeacherRequest(id);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}