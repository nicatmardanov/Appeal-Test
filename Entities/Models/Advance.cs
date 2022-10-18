using Core.Entities.Concrete;
using Core.Utilities.Attributes;

namespace Entities.Models
{
    [TableInfo("Payment.Advance")]
    public class Advance : BaseEntity<Guid>
    {
        public string? DocNumber { get; set; }
        public string? Tin { get; set; }
        public decimal Amount { get; set; }
    }
}
