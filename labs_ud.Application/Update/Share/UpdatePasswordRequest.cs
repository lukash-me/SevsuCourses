namespace labs_ud.Application.Update.Share;

public record UpdatePasswordRequest(
    Guid UserId,
    PasswordDto Dto
);

public record PasswordDto(
    string Password
    );
