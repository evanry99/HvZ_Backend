﻿using System.ComponentModel.DataAnnotations;

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
        public double? Nw_Lat { get; set; }
        public double? Nw_Lng { get; set; }
        public double? Se_Lat { get; set; }
        public double? Se_Lng { get; set; }

        // Relationships
        public ICollection<PlayerDomain> Players { get; set; }
        public ICollection<KillDomain> Kills { get; set; }
    }
}