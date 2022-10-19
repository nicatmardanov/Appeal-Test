using Core.Entities.Dtos;
using Entities.Dtos.Advance;

namespace Entities.Dtos.Transfer
{
    public class TransferGetDto : BaseIdDto<int>
    {
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid AdvanceId { get; set; }

        public AdvanceGetDto? Advance { get; set; }
    }
}
