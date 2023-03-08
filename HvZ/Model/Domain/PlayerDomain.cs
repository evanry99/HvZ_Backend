namespace HvZ.Model.Domain
{
    public class PlayerDomain
    {
        public int Id { get; set; }
        public string BiteCode { get; set; }
        public bool IsPatientZeroero { get; set; }
        public bool IsHuman { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
    }
}