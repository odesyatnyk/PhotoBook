namespace BLL.Interfacies.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } //MD5 hash
        public string Email { get; set; }
        public int ProfileId { get; set; }
        public int RoleId { get; set; }
        public ProfileEntity Profile { get; set; }
    }
}