namespace HvZ.Model.DTO.SquadMemberDTO
{
    public class SquadMemberReadDTO
    {
        public int Id { get; set; }
        public string Rank { get; set; }
        public int GameId { get; set; }
        public int SquadId { get; set; }
        public int PlayerId { get; set; }
    }
}
