using System.ComponentModel.DataAnnotations;

namespace Shop.Context.Table
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public long UserId { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }        
        public DateTime Launch { get; set; }
       
    }
}
