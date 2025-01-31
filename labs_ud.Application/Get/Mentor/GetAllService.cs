using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Mentor;

public class GetAllService
{
    private readonly MentorRepository _mentorRepository;

    public GetAllService(MentorRepository mentorRepository)
    {
        _mentorRepository = mentorRepository;
    }

    public async Task<Result<List<AllMentorsResponse>, Error>> Handle(CancellationToken cancellationToken = default)
    {
        var mentorsResult = await _mentorRepository.GetAll(cancellationToken);
        
        if (mentorsResult.IsFailure)
            return mentorsResult.Error;
        
        var mentors = mentorsResult.Value;
        var response = new List<AllMentorsResponse>();
        
        foreach (var mentor in mentors)
        {
            var mentorId = mentor.Id;
            var fio = mentor.Fio;
            
            var result = new AllMentorsResponse(mentorId, fio);
            
            response.Add(result);
        }
        
        response = response.OrderBy(r=>r.Fio).ToList();
        
        return response;
    }
}

public record AllMentorsResponse(
    Guid MentorId,
    string Fio
    );

public record AllMentorsRequest(
    List<Guid> MentorIds
    );