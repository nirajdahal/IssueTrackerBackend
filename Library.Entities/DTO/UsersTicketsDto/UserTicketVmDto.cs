using Library.Entities.DTO.UserDto;

namespace Library.Entities.DTO.UsersTicketsDto
{
    public class UserTicketVmDto
    {
        public string Id { get; set; }
        public ApplicationUserVm ApplicationUser { get; set; }
    }
}