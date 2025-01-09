using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Delete;

public class DeleteMentorService
{
    private readonly MentorRepository _mentorRepository;

    public DeleteMentorService(MentorRepository mentorRepository)
    {
        _mentorRepository = mentorRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        DeleteMentorRequest request, 
        CancellationToken cancellationToken = default)
    {
        var mentorResult = await _mentorRepository.GetById(request.MentorId, cancellationToken);
        if (mentorResult.IsFailure)
            return mentorResult.Error;

        _mentorRepository.Delete(mentorResult.Value, cancellationToken);

        return mentorResult.Value.Id;
    }
}