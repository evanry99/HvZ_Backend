﻿namespace HvZ.Model.DTO.SquadCheckInDTO
{
    public class SquadCheckInCreateDTO
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public int GameId { get; set; }
        public int SquadId { get; set; }
        public int SquadMemberId { get; set; }
    }
}
