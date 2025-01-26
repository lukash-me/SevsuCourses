using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Mentor;

public class GetMainInfoService
{
    private readonly MentorRepository _mentorRepository;

    public GetMainInfoService(MentorRepository mentorRepository)
    {
        _mentorRepository = mentorRepository;
    }

    public async Task<Result<MainInfoDto, Error>> Handle(MentorId mentorId, CancellationToken cancellationToken = default)
    {
        var mentorResult = await _mentorRepository.GetById(mentorId, cancellationToken);
        var mentor = mentorResult.Value;

        var fio = mentor.Fio;
        var email = mentor.Email;
        var education = mentor.Education;
        var photo = mentor.Photo;
        var description = mentor.Description;
        
        var response = new MainInfoDto(fio, email, education, photo, description);
        
        return response;
    }
}