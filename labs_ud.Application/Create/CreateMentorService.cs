using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Create;

public class CreateMentorService
{
    private readonly MentorRepository _mentorRepository;

    public CreateMentorService(MentorRepository mentorRepository)
    {
        _mentorRepository = mentorRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(CreateMentorRequest request, CancellationToken cancellationToken)
    {
        var fio = request.Fio;
        var phone = request.Phone;
        var email  = request.Email;
        var education = request.Education;
        var photo = request.Photo;
        var description = request.Description;
        var password = request.Password;

        var mentorResult = Mentor.Create(
            fio,
            phone,
            email,
            education,
            photo,
            description,
            password
            );

        if (mentorResult.IsFailure)
            return mentorResult.Error;
        
        await _mentorRepository.Add(mentorResult.Value, cancellationToken);

        return mentorResult.Value.Id;
    }
}

