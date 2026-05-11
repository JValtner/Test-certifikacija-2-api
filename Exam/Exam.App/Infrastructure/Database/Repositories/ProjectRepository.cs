using Exam.App.Domain;
using Exam.App.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Exam.App.Infrastructure.Database.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Project> CreateAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<List<Project>> GetByUserIdAsync(string userId, bool onlyActive)
    {
        IQueryable<Project> query = _context.Projects
            .Include(p => p.UserProjectSkills)
                .ThenInclude(ps => ps.Skill)
            .Where(p => p.UserId == userId);

        if (onlyActive)
        {
            query = query.Where(p =>
                p.Status != ProjectStatus.Draft &&
                p.Status != ProjectStatus.Archived);
        }

        return await query.ToListAsync();
    }

    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
    
}
