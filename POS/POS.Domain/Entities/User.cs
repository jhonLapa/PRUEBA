namespace POS.Domain.Entities
{
    public partial class User :BaseEntity
    {
        public User()
        {
            Sales = new HashSet<Sale>();
        }

       // public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }


        public virtual ICollection<Sale> Sales { get; set; }
    }
}
