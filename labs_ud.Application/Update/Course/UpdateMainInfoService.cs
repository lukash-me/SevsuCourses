using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Update.Course;

public class UpdateMainInfoService
{
    private readonly CourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMainInfoService(CourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken)
    {
        var courseResult = await _courseRepository.GetById(request.CourseId, cancellationToken);
        
        if (courseResult.IsFailure)
            return courseResult.Error;
        
        var updateResult = courseResult.Value.UpdateMainInfo(
            request.Dto.Title,
            request.Dto.Description,
            request.Dto.Photo
        );

        if (updateResult.IsFailure)
        {
            return updateResult.Error;
        }
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.CourseId;
    }
}

public record UpdateMainInfoRequest(
    Guid CourseId,
    MainInfoDto Dto
);
    
public record MainInfoDto(
    string Title,
    string Description,
    string Photo
);