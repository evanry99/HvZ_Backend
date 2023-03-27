using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.DTO.SquadMemberDTO
{
    public class SquadMemberCreateDTO
    {
        [Required]
        public int PlayerId { get; set; }
    }
}
