namespace HvZ.Model.DTO.KillDTO
{
    public class KillReadDTO
    {
        public int Id { get; set; }
        public DateTime TimeOfDeath { get; set; }
        public string Story { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public int GameId { get; set; }
        public int KillerId { get; set; }
        public int VictimId { get; set; }
    }
}