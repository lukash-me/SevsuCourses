using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Mentor;

public class GetMentorByLoginPasswordService
{
    private readonly MentorRepository _mentorRepository;

    public GetMentorByLoginPasswordService(MentorRepository mentorRepository)
    {
        _mentorRepository = mentorRepository;
    }

    public async Task<Result<Guid, Error>> Handle(MentorRequest request, CancellationToken cancellationToken = default)
    {
        var mentorResult = await _mentorRepository.GetByLoginPassword(request, cancellationToken);
        var mentorId = mentorResult.Value.Id;
        
        return mentorId;
    }
}

public record MentorRequest(
    string Login,
    string Password);