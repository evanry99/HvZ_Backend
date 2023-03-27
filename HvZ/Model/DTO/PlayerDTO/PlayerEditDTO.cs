using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.DTO.PlayerDTO
{
    public class PlayerEditDTO
    {
        [Required]
        public string BiteCode { get; set; }
        [Required]
        public bool IsPatientZero { get; set; }
        [Required]
        public bool IsHuman { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GameId { get; set; }
    }
}