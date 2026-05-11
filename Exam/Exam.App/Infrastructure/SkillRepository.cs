using Exam.App.Domain;
using Exam.App.Domain.Repositories;
using Exam.App.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Exam.App.Infrastructure;

public class SkillRepository : ISkillRepository
{
    private readonly AppDbContext _context;

    public SkillRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Skill>> GetAllAsync()
    {
        return await _context.Skills
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<bool> SkillExistsAsync(int skillId)
    {
        return await _context.Skills
            .AnyAsync(s => s.Id == skillId);
    }

    public async Task<bool> ProjectSkillUserExistsAsync(
        string userId,
        int projectId,
        int skillId)
    {
        return await _context.UserProjectSkills
            .AnyAsync(x =>
                x.UserId == userId &&
                x.ProjectId == projectId &&
                x.SkillId == skillId);
    }

    public async Task<UserProjectSkill> CreateAsync(UserProjectSkill entity)
    {
        _context.UserProjectSkills.Add(entity);
        await _context.SaveChangesAsync();

        return await _context.UserProjectSkills
            .Include(x => x.Skill)
            .FirstAsync(x => x.Id == entity.Id);
    }
}