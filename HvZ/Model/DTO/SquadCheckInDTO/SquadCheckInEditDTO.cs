using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.DTO.SquadCheckInDTO
{
    public class SquadCheckInEditDTO
    {
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public float Lat { get; set; }
        [Required]
        public float Lng { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public int SquadId { get; set; }
        [Required]
        public int SquadMemberId { get; set; }
    }
}
