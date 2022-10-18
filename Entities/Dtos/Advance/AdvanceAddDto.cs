using Core.Entities;

namespace Entities.Dtos.Advance
{
    public class AdvanceAddDto : IDto
    {
        public string? DocNumber { get; set; }
        public string? Tin { get; set; }
        public decimal Amount { get; set; }
    }
}
