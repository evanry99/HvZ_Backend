using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.Domain
{
    public class UserDomain
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        public bool IsAdmin { get; set; }

        // Relationships
        public ICollection<PlayerDomain>? Players { get; set; } //add nullable for players in userdomain
    }
}