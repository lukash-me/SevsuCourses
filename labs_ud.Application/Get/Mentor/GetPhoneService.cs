using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Mentor;

public class GetPhoneService
{
    private readonly MentorRepository _mentorRepository;

    public GetPhoneService(MentorRepository mentorRepository)
    {
        _mentorRepository = mentorRepository;
    }

    public async Task<Result<string, Error>> Handle(MentorId mentorId, CancellationToken cancellationToken = default)
    {
        var mentorResult = await _mentorRepository.GetById(mentorId, cancellationToken);
        var mentor = mentorResult.Value;

        var phone = mentor.Phone;
        
        return phone;
    }
}