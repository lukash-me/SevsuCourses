using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;
using labs_ud.Application.Update.Share;

namespace labs_ud.Application.Update.Student;

public class UpdatePasswordService
{
    private readonly StudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePasswordService(StudentRepository studentRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdatePasswordRequest request,
        CancellationToken cancellationToken)
    {
        var studentResult = await _studentRepository.GetById(request.UserId, cancellationToken);
        if (studentResult.IsFailure)
            return studentResult.Error;
        
        studentResult.Value.UpdatePassword(request.Dto.Password);
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.UserId;
    }
}