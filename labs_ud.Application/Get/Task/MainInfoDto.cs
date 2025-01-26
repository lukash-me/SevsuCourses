namespace labs_ud.Application.Get.Task;

public record MainInfoDto(
    string Title,
    string Condition,
    int AttempsAmount,
    int MinMark,
    int MaxMark
);