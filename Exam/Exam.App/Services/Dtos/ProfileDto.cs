using Exam.App.Domain;

namespace Exam.App.Services.Dtos;

public class ProfileDto
{
    public required string Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required int TotalProjects { get; set; }
    public required int CompletedProjects { get; set; }
    public required string? LastCompletedProjectName { get; set; }

}