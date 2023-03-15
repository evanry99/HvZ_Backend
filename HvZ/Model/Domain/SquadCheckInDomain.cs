using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvZ.Model.Domain
{
    
    public class SquadCheckInDomain
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public double Lat { get; set; }
        [Required]
        public double Lng { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public int SquadId { get; set; }
        [Required]
        public int SquadMemberId { get; set; }

        // Relationships
        [ForeignKey("GameId")]
        public GameDomain Game { get; set; }
        [ForeignKey("SquadId")]
        public SquadDomain Squad { get; set; }
        [ForeignKey("SquadMemberId")]
        public SquadCheckInDomain SquadMember { get; set; }
    }
    
}
