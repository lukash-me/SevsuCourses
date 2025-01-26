using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Get.Student;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Update.Student;

public class UpdateMainInfoService
{
    private readonly StudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMainInfoService(StudentRepository studentRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken)
    {
        var studentResult = await _studentRepository.GetById(request.StudentId, cancellationToken);
        if (studentResult.IsFailure)
            return studentResult.Error;
        
        studentResult.Value.UpdateMainInfo(
            request.Dto.Fio,
            request.Dto.Photo,
            request.Dto.Email
            );
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.StudentId;
    }
}

public record UpdateMainInfoRequest(
    Guid StudentId,
    MainInfoDto Dto
);