using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvZ.Model.Domain
{
    public class MissionDomain
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public bool IsHumanVisible { get; set; }
        [Required]
        public bool IsZombieVisible { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Required]
        public int GameId { get; set; }

        // Relaionships
        [ForeignKey("GameId")]
        public GameDomain Game { get; set; }
    }
}
