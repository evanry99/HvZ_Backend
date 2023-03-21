namespace HvZ.Model.DTO.UserDTO
{
    public class UserEditDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
