using HvZ.Model.DTO.SquadMemberDTO;
using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.DTO.SquadDTO
{
    public class SquadCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsHuman { get; set; }
        [Required]
        public SquadMemberCreateDTO SquadMember { get; set; }
    }
}
