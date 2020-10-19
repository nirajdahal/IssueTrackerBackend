using Library.Entities.DTO.UserDto;

namespace Library.Entities.DTO.UserProjectsDto
{
    public class UserProjectVmDto
    {
        public string Id { get; set; }
        public ApplicationUserVm ApplicationUser { get; set; }
    }
}