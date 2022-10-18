using Core.Entities;

namespace Entities.Dtos.Transfer
{
    public class TransferAddDto : IDto
    {
        public decimal Amount { get; set; }
        public Guid AdvanceId { get; set; }
    }
}
