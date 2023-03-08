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

        //
        [Required]
        [ForeignKey("UserId")]
        public int User { get; set; }
        [Required]
        [ForeignKey("GameId")]
        public int Game { get; set; }
    }
}