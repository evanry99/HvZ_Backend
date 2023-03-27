using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.DTO.GameDTO
{
    public class GameEditDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string GameState { get; set; }
        [Required]
        public string Description { get; set; }
        public float? Nw_Lat { get; set; }
        public float? Nw_Lng { get; set; }
        public float? Se_Lat { get; set; }
        public float? Se_Lng { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
