using AutoMapper;
using Exam.App.Domain;
using Exam.App.Domain.Repositories;
using Exam.App.Services.Dtos;
using Exam.App.Services.Exceptions;

namespace Exam.App.Services;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public SkillService(
        ISkillRepository skillRepository,
        IProjectRepository projectRepository,
        IMapper mapper)
    {
        _skillRepository = skillRepository;
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<List<SkillDto>> GetAllAsync()
    {
        var skills = await _skillRepository.GetAllAsync();
        return _mapper.Map<List<SkillDto>>(skills);
    }

    public async Task<UserProjectSkillDto> AddSkillToProjectAsync(
        int projectId,
        string userId,
        CreateUserProjectSkillDto dto)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project == null)
            throw new NotFoundException(projectId);

        if (project.UserId != userId)
            throw new UnauthorizedAccessException("Nemate dozvolu za ovaj projekat.");

        if (project.Status != ProjectStatus.Draft &&
            project.Status != ProjectStatus.Published)
        {
            throw new BadRequestException(
                "Veština se može dodati samo za projekat koji je u pripremi ili realizaciji.");
        }

        var skillExists = await _skillRepository.SkillExistsAsync(dto.SkillId);

        if (!skillExists)
            throw new BadRequestException("Izabrana veština ne postoji.");

        var alreadyExists = await _skillRepository.ProjectSkillUserExistsAsync(
            userId,
            projectId,
            dto.SkillId);

        if (alreadyExists)
            throw new BadRequestException("Ova veština je već dodata za projekat.");

        var entity = new UserProjectSkill
        {
            UserId = userId,
            ProjectId = projectId,
            SkillId = dto.SkillId,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _skillRepository.CreateAsync(entity);

        return _mapper.Map<UserProjectSkillDto>(created);
    }
}