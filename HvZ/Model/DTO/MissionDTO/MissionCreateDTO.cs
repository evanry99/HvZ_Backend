namespace HvZ.Model.DTO.MissionDTO
{
    public class MissionCreateDTO
    {
        public string Name { get; set; }
        public bool IsHumanVisible { get; set; }
        public bool IsZombieVisible { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
