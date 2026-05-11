using AutoMapper;
using Exam.App.Domain;
using Exam.App.Services.Dtos;

namespace Exam.App.Services.Mappers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ProfileDto>();
            CreateMap<Project, ProjectDto>();
            CreateMap<UserProjectSkill, UserProjectSkillDto>()
                 .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill.Name));
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.UserProjectSkills));
        }
    }
}
