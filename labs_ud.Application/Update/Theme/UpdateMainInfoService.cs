using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Update.Theme;

public class UpdateMainInfoService
{
    private readonly ThemeRepository _themeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMainInfoService(ThemeRepository themeRepository, IUnitOfWork unitOfWork)
    {
        _themeRepository = themeRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken)
    {
        var themeResult = await _themeRepository.GetById(request.ThemeId, cancellationToken);
        
        if (themeResult.IsFailure)
            return themeResult.Error;
        
        themeResult.Value.UpdateMainInfo(
            request.Dto.Title,
            request.Dto.Text,
            request.Dto.Photo
        );
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.ThemeId;
    }
}

public record UpdateMainInfoRequest(
    Guid ThemeId,
    MainInfoDto Dto
    );
    
public record MainInfoDto(
    string Title,
    string Text,
    string Photo
);