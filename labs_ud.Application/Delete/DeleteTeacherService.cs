using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Delete;

public class DeleteTeacherService
{
    private readonly TeacherRepository _teacherRepository;

    public DeleteTeacherService(TeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        DeleteTeacherRequest request, 
        CancellationToken cancellationToken = default)
    {
        var teacherResult = await _teacherRepository.GetById(request.TeacherId, cancellationToken);
        if (teacherResult.IsFailure)
            return teacherResult.Error;

        _teacherRepository.Delete(teacherResult.Value, cancellationToken);

        return teacherResult.Value.Id;
    }
}