using Exam.App.Services.Dtos;

namespace Exam.App.Services;

public interface IProjectService
{
    Task<ProjectDto> CreateAsync(ProjectDto dto, string username);
    Task<ProjectDto> UpdateAsync(int id, ProjectDto dto);
    Task DeleteAsync(int id);
    Task<List<ProjectDto>> GetByUserIdAsync(string userId, bool onlyActive);
    Task<List<ProjectDto>> GetActiveAsync(string userId);
    Task<List<ProjectDto>> GetOwnedAsync(string username);
    Task<ProjectDto> StartProjectAsync(int id);
    Task<ProjectDto> LockProjectAsync(int id);
    Task<ProjectDto> UnLockProjectAsync(int id);
    Task<List<ProjectDto>> GetCompletedByUserAndSkillAsync(string userId, int skillId);
}
