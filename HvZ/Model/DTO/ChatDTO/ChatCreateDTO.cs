using System.ComponentModel.DataAnnotations;

namespace HvZ.Model.DTO.ChatDTO
{
    public class ChatCreateDTO
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public bool IsHumanGlobal { get; set; }
        [Required]
        public bool IsZombieGlobal { get; set; }
        [Required]
        public DateTime ChatTime { get; set; }
        [Required]
        public int PlayerId { get; set; }
        public int? SquadId { get; set; }
    }
}