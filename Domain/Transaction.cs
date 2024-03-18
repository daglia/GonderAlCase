using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [ForeignKey("SenderUser")]
        public Guid Sender { get; set; }

        [ForeignKey("ReceiverUser")]
        public Guid Receiver { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }

        public virtual User SenderUser { get; set; }
        public virtual User ReceiverUser { get; set; }
    }
}
