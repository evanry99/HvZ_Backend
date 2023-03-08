using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvZ.Model.Domain
{
    public class PlayerDomain
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string BiteCode { get; set; }
        [Required]
        public bool IsPatientZero { get; set; }
        [Required]
        public bool IsHuman { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int GameId { get; set; }

        // Relationships
        [Required]
        [ForeignKey("UserId")]
        public UserDomain User { get; set; }
        [Required]
        [ForeignKey("GameId")]
        public GameDomain Game { get; set; }
        public ICollection<KillDomain> Kills { get; set; }
    }
}