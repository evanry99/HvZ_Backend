using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvZ.Model.Domain
{
    /*
    public class SquadMemberDomain
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Rank { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public int SquadId { get; set; }
        [Required]
        public string PlayerId { get; set; }

        // Relationship
        [ForeignKey("GameId")]
        public GameDomain Game { get; set; }
        [ForeignKey("SquadId")]
        public SquadDomain Squad { get; set; }
        [ForeignKey("PlayerId")]
        public PlayerDomain Player { get; set; }

        public ICollection<SquadCheckInDomain>? SquadCheckIns { get; set; }
    }
    */
}