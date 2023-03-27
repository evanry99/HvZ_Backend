using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.DTO.MissionDTO
{
    public class MissionEditDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsHumanVisible { get; set; }
        [Required]
        public bool IsZombieVisible { get; set; }
        public string? Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        [Required]
        public int GameId { get; set; }
    }
}
