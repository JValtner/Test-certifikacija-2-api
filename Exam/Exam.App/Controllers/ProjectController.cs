using System.Security.Claims;
using Exam.App.Services;
using Exam.App.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.App.Controllers;

[Route("api/projects")]
[ApiController]
[Authorize(Roles = "User")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectDto dto)
    {
        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);//1-6 zadatak nedozvoljen pristup
        var result = await _projectService.CreateAsync(dto, username);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProjectDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized("Nedozvoljen pristup");

        var result = await _projectService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userName == null)
            return Unauthorized("Nedozvoljen pristup");

        await _projectService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetByUser(string userId)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUserId == null)
            return Unauthorized("Nedozvoljen pristup");

        if (userId != currentUserId)
            return Forbid("Nemate dozvolu za pristup projektima drugog korisnika.");

        var result = await _projectService.GetByUserIdAsync(userId, false);
        return Ok(result);
    }

    [HttpGet("mine")]
    public async Task<IActionResult> GetMine()
    {

        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (username == null)
        {
            return Unauthorized("Nedozvoljen pristup");
        }
        var result = await _projectService.GetOwnedAsync(username);
        return Ok(result);
    }

    [HttpGet("active/{userId}")]
    public async Task<IActionResult> GetActive(string userId)
    {
        var result = await _projectService.GetActiveAsync(userId);
        return Ok(result);
    }
    [HttpPost("{projectId}/start")]
    public async Task<IActionResult> StartProject(int projectId)
    {
        var result = await _projectService.StartProjectAsync(projectId);
        return Ok(result);
    }

    [HttpPost("{projectId}/lock")]
    public async Task<IActionResult> LockProject(int projectId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var result = await _projectService.LockProjectAsync(projectId);
        return Ok(result);
    }

    [HttpPost("{projectId}/unlock")]
    public async Task<IActionResult> UnLockProject(int projectId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        var result = await _projectService.UnLockProjectAsync(projectId);
        return Ok(result);
    }

    [HttpGet("users/{userId}/completed-by-skill/{skillId}")]
    public async Task<IActionResult> GetCompletedByUserAndSkill(string userId, int skillId)
    {
        var result = await _projectService.GetCompletedByUserAndSkillAsync(userId, skillId);
        return Ok(result);
    }
}
