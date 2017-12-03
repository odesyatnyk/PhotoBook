namespace DAL.Interfacies.DTO
{
    public class DalLike : IEntity
    {
        public string UserName { get; set; }
        public int PhotoId { get; set; }
        public int Id { get; set; }
    }
}