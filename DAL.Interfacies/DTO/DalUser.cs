namespace DAL.Interfacies.DTO
{
    public class DalUser : IEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; } //MD5 hash
        public string Email { get; set; }
        public int ProfileId { get; set; }
        public int RoleId { get; set; }
        public DalProfile Profile { get; set; }
        public int Id { get; set; }
    }
}