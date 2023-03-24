using HvZ.Model.DTO.SquadMemberDTO;

namespace HvZ.Model.DTO.SquadDTO
{
    public class SquadCreateDTO
    {
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public SquadMemberCreateDTO SquadMember { get; set; }
    }
}
