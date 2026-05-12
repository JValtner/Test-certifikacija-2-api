using AutoMapper;
using Exam.App.Domain;
using Exam.App.Services.Dtos;

namespace Exam.App.Services.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ProfileDto>();

            CreateMap<Skill, SkillDto>();

            CreateMap<UserProjectSkill, UserProjectSkillDto>()
                .ForMember(
                    dest => dest.SkillName,
                    opt => opt.MapFrom(src => src.Skill.Name)
                );

            CreateMap<CreateUserProjectSkillDto, UserProjectSkill>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Skill, opt => opt.Ignore());

            CreateMap<Project, ProjectDto>()
                .ForMember(
                    dest => dest.Skills,
                    opt => opt.MapFrom(src => src.UserProjectSkills)
                );

            CreateMap<ProjectDto, Project>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.UserProjectSkills, opt => opt.Ignore());
        }
    }
}