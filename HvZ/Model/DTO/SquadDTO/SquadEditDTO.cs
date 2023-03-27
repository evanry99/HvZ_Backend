using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.DTO.SquadDTO
{
    public class SquadEditDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsHuman { get; set; }
    }
}
