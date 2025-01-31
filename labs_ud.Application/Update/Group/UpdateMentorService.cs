using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Update.Group;

public class UpdateMentorService
{
    private readonly GroupRepository _groupRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMentorService(GroupRepository groupRepository, IUnitOfWork unitOfWork)
    {
        _groupRepository = groupRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdateMentorRequest request,
        CancellationToken cancellationToken)
    {
        var groupResult = await _groupRepository.GetById(request.GroupId, cancellationToken);
        if (groupResult.IsFailure)
            return groupResult.Error;
        
        groupResult.Value.UpdateMentor(request.Dto.MentorId);
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.GroupId;
    }
}

public record UpdateMentorRequest(
    Guid GroupId ,
    UpdateMentorDto Dto
    );
    
public record UpdateMentorDto(
    Guid MentorId
    );