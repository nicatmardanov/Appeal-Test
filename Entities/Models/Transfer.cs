using Core.Entities.Concrete;
using Core.Utilities.Attributes;

namespace Entities.Models
{
    [TableInfo("[Payment].[Transfer]", "[pt].[CreatedAt] desc")]
    public class Transfer : BaseEntity<int>
    {
        public decimal Amount { get; set; }
        public Guid AdvanceId { get; set; }

        public Advance? Advance { get; set; }
    }
}
