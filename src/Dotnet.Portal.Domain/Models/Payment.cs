using System;

namespace Dotnet.Portal.Domain.Models
{
    public class Payment : Entity
    {
        public DateTime? Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentType Type { get; set; }
        public Guid MemberId { get; set; }

        /* EF Relations */
        public Member Member { get; set; }
    }
}
