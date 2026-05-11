using System.Security.Claims;
using Exam.App.Services;
using Exam.App.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.App.Controllers;

[Route("api/skills")]
[ApiController]
[Authorize]
public class SkillController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _skillService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost("projects/{projectId}")]
    public async Task<IActionResult> AddSkillToProject(
        int projectId,
        [FromBody] CreateUserProjectSkillDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized("Nedozvoljen pristup");

        var result = await _skillService.AddSkillToProjectAsync(
            projectId,
            userId,
            dto);

        return Ok(result);
    }
}