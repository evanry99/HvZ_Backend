using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.DTO.KillDTO
{
    public class KillEditDTO
    {
        [Required]
        public DateTime TimeOfDeath { get; set; }
        public string? Story { get; set; }
        public float? Lat { get; set; }
        public float? Lng { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public int KillerId { get; set; }
        [Required]
        public int VictimId { get; set; }
    }
}
