using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace HvZ.Model.Domain
{
    public class SquadDomain
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public bool isHuman { get; set; }
        [Required]
        public int GameId { get; set; }

        //Relationship
        [ForeignKey("GameId")]
        public GameDomain Game { get; set; }
    }
}
