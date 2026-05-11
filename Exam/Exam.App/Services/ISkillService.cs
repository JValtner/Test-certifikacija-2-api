using Exam.App.Services.Dtos;

namespace Exam.App.Services;

public interface ISkillService
{
    Task<List<SkillDto>> GetAllAsync();

    Task<UserProjectSkillDto> AddSkillToProjectAsync(
        int projectId,
        string userId,
        CreateUserProjectSkillDto dto);
}