using Exam.App.Domain;
using Exam.App.Domain.Repositories;
using Exam.App.Services.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Exam.App.Infrastructure.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ProfileDto>> GetAllUsersAsync(int page, int pageSize, int? skillId)
    {
        //Izbaci Admine
        var administratorRoleId = await _context.Roles
            .Where(r => r.NormalizedName == "ADMINISTRATOR")
            .Select(r => r.Id)
            .FirstOrDefaultAsync();

        var usersQuery = _context.Users.AsQueryable();

        if (administratorRoleId != null)
        {
            usersQuery = usersQuery.Where(u =>
                !_context.UserRoles.Any(ur =>
                    ur.UserId == u.Id &&
                    ur.RoleId == administratorRoleId));
        }
        if (skillId.HasValue)
        {
            usersQuery = usersQuery.Where(u =>
                _context.UserProjectSkills.Any(ups =>
                    ups.UserId == u.Id &&
                    ups.SkillId == skillId.Value &&
                    ups.Project.Status == ProjectStatus.Completed));
        }
        //ukljuci statistike
        var query = usersQuery.Select(u => new ProfileDto
        {
            Id = u.Id,
            Username = u.UserName!,
            Email = u.Email!,
            Name = u.Name,
            Surname = u.Surname,

            TotalProjects = _context.Projects
                .Count(p => p.UserId == u.Id),

            CompletedProjects = _context.Projects
                .Count(p =>
                    p.UserId == u.Id &&
                    p.Status == ProjectStatus.Completed),

            LastCompletedProjectName = _context.Projects
                .Where(p =>
                    p.UserId == u.Id &&
                    p.Status == ProjectStatus.Completed &&
                    p.CompletedAt != null)
                .OrderByDescending(p => p.CompletedAt)
                .Select(p => p.Name)
                .FirstOrDefault() ?? "N/A"
        });

        var totalCount = await usersQuery.CountAsync();
        //sortiraj
        var items = await query
            .OrderByDescending(u => u.CompletedProjects)
            .ThenByDescending(u => u.TotalProjects)
            .ThenBy(u => u.Username)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<ProfileDto>(items, page, pageSize, totalCount);
    }
    public async Task<ApplicationUser?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == username);
    }
}
