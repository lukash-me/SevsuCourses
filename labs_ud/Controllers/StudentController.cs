using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get.Student;
using labs_ud.Application.Get.StudentGroup;
using labs_ud.Application.Update.Share;
using labs_ud.Application.Update.Student;
using labs_ud.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace labs_ud.Controllers;

[ApiController]
[Route("[controller]")]

public class StudentController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateStudent(
        [FromServices] CreateStudentService service,
        [FromBody] CreateStudentRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получение id студента по логину и паролю
    /// </summary>
    /// <param name="service"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<Guid>> GetByLoginPassword(
        [FromServices] GetStudentByLoginPasswordService service,
        [FromBody] StudentRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
        
    /// <summary>
    /// Получение id, ФИО, статуса всех студентов группы по id группы
    /// </summary>
    /// <param name="infoService"></param>
    /// <param name="allStudentsService"></param>
    /// <param name="groupId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("all-id-fio-status/{groupId:guid}")]
    public async Task<ActionResult<Guid>> GetGroupId(
        [FromServices] GetStudentsFioStatusByStudentIdService infoService,
        [FromServices] GetStudentsByGroupIdService allStudentsService,
        [FromRoute] Guid groupId,
        CancellationToken cancellationToken = default)
    {
        var studentsResult = await allStudentsService.Handle(groupId, cancellationToken);
        
        if (studentsResult.IsFailure)
            return studentsResult.Error.ToResponse();

        var students = studentsResult.Value;
        
        var result = await infoService.Handle(students, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получение основной информации о студенте по id студента
    /// </summary>
    /// <param name="service"></param>
    /// <param name="studentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("main-info/{studentId:guid}")]
    public async Task<ActionResult<Guid>> GetMainInfo(
        [FromServices] GetMainInfoService service,
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(studentId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получение номера телефона по id студента
    /// </summary>
    /// <param name="service"></param>
    /// <param name="studentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("phone/{studentId:guid}")]
    public async Task<ActionResult<Guid>> GetPhone(
        [FromServices] GetPhoneService service,
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(studentId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Изменение информации о студенте по id студента
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="dto"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("main-info/{studentId:guid}")]
    public async Task<ActionResult<Guid>> UpdateMainInfo(
        [FromRoute] Guid studentId,
        [FromBody] MainInfoDto dto,
        [FromServices] UpdateMainInfoService service,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateMainInfoRequest(studentId, dto);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Обновление номера телефона студента по id студента
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="dto"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("phone/{studentId:guid}")]
    public async Task<ActionResult<Guid>> UpdatePhone(
        [FromRoute] Guid studentId,
        [FromBody] PhoneDto dto,
        [FromServices] UpdatePhoneService service,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdatePhoneRequest(studentId, dto);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteStudentService service,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteStudentRequest(id);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    
}