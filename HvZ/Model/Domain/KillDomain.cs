using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvZ.Model.Domain
{
    public class KillDomain
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime TimeOfDeath { get; set; }
        [MaxLength(50)]
        public string? Story { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public int KillerId { get; set; }
        [Required]
        public int VictimId { get; set; }

        // Relationships
        [ForeignKey("GameId")]
        public GameDomain Game { get; set; }
        [ForeignKey("KillerId")]
        public PlayerDomain Killer { get; set; }
        [ForeignKey("VictimId")]
        public PlayerDomain Victim { get; set; }
    }
}