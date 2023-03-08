namespace HvZ.Model.DTO.PlayerDTO
{
    public class PlayerEditDTO
    {
        public int Id { get; set; }
        public string BiteCode { get; set; }
        public bool IsPatientZero { get; set; }
        public bool IsHuman { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
    }
}