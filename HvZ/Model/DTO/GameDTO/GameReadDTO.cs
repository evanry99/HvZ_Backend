namespace HvZ.Model.DTO.GameDTO
{
    public class GameReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GameState { get; set; }
        public string Description { get; set; }
        public float? Nw_Lat { get; set; }
        public float? Nw_Lng { get; set; }
        public float? Se_Lat { get; set; }
        public float? Se_Lng { get; set; }
    }
}
