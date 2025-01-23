using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Mentor;

public class GetMentorFioByIdService
{
    private readonly MentorRepository _mentorRepository;

    public GetMentorFioByIdService(MentorRepository mentorRepository)
    {
        _mentorRepository = mentorRepository;
    }

    public async Task<Result<string, Error>> Handle(MentorId mentorId, CancellationToken cancellationToken = default)
    {
        var mentorResult = await _mentorRepository.GetById(mentorId, cancellationToken);
        var mentor = mentorResult.Value;

        return mentor.Fio;
    }
}