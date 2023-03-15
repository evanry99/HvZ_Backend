using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvZ.Model.Domain
{
    public class ChatDomain
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public bool IsHumanGlobal { get; set; }
        [Required]
        public bool IsZombieGlobal { get; set; }
        [Required]
        public DateTime ChatTime { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public int PlayerId { get; set; }
        public int? SquadId { get; set; }

        // Relationships
        [ForeignKey("GameId")]
        public GameDomain Game { get; set; }
        [ForeignKey("PlayerId")]
        public PlayerDomain Player { get; set; }
        [ForeignKey("SquadId")]
        public SquadDomain? Squad { get; set; }
    }
}