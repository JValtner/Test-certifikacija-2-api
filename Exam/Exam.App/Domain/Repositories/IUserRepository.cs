using Exam.App.Services.Dtos;

namespace Exam.App.Domain.Repositories;

public interface IUserRepository
{
    Task<PaginatedList<ProfileDto>> GetAllUsersAsync(
    int page,
    int pageSize);
}
