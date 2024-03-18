using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Surname { get; set; }
        public decimal Balance { get; set; }

        public virtual ICollection<Transaction> SentTransactions { get; set; }
        public virtual ICollection<Transaction> ReceivedTransactions { get; set; }
    }
}
