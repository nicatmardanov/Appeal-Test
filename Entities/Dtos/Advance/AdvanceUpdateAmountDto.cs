using Core.Entities;

namespace Entities.Dtos.Advance
{
    public class AdvanceUpdateAmountDto : IDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
    }
}
