﻿namespace HvZ.Model.DTO.MissionDTO
{
    public class MissionEditDTO
    {
        public string Name { get; set; }
        public bool IsHumanVisible { get; set; }
        public bool IsZombieVisible { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int GameId { get; set; }
    }
}