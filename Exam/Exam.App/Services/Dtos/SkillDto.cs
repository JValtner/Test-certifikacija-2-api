namespace Exam.App.Services.Dtos
{
    public class SkillDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
    public class CreateUserProjectSkillDto
    {
        public int SkillId { get; set; }
        public required string Description { get; set; }
    }
    public class UserProjectSkillDto
    {
        public int Id { get; set; }
        public int SkillId { get; set; }
        public required string SkillName { get; set; }
        public required string Description { get; set; }
    }
}
