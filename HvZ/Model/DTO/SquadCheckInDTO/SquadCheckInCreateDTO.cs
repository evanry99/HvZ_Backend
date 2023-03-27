using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.DTO.SquadCheckInDTO
{
    public class SquadCheckInCreateDTO
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
        public int SquadMemberId { get; set; }
    }
}
