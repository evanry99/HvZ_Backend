using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.DTO.PlayerDTO
{
    public class PlayerCreateDTO
    {
        [Required]
        public bool IsPatientZero { get; set; }
        [Required]
        public bool IsHuman { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}