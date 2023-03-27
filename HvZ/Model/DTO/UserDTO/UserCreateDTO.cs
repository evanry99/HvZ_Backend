using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.DTO.UserDTO
{
    public class UserCreateDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
    }
}
