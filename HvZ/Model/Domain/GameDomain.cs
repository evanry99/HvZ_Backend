using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.Domain
{
    public class GameDomain
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(15)]
        public string GameState { get; set; }
        [Required]
        [MaxLength(400)]
        public string Description { get; set; }


        //Coordinates
        public float? Nw_Lat { get; set; }
        public float? Nw_Lng { get; set; }
        public float? Se_Lat { get; set; }
        public float? Se_Lng { get; set; }

        // Relationships
        public ICollection<PlayerDomain> Players { get; set; }
        public ICollection<KillDomain> Kills { get; set; }
    }
}