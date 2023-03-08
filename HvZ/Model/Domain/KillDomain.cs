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
        public float? Lat { get; set; }
        public float? Lng { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]
        public int KillerId { get; set; }
        [Required]
        public int VictimId { get; set; }

        //
        [Required]
        [ForeignKey("GameId")]
        public int Game { get; set; }
        [Required]
        [ForeignKey("KillerId")]
        public int Killer { get; set; }
        [Required]
        [ForeignKey("VictimId")]
        public int Victim { get; set; }
    }
}