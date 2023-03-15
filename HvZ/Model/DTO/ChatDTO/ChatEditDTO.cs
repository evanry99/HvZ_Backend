namespace HvZ.Model.DTO.ChatDTO
{
    public class ChatEditDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsHumanGlobal { get; set; }
        public bool IsZombieGlobal { get; set; }
        public DateTime ChatTime { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int? SquadId { get; set; }
    }
}