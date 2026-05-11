using Exam.App.Domain;

namespace Exam.App.Domain.Repositories;

public interface ISkillRepository
{
    Task<List<Skill>> GetAllAsync();

    Task<bool> SkillExistsAsync(int skillId);

    Task<bool> ProjectSkillUserExistsAsync(
        string userId,
        int projectId,
        int skillId);

    Task<UserProjectSkill> CreateAsync(UserProjectSkill entity);
}