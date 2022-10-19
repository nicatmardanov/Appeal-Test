using Core.Entities.Dtos;

namespace Entities.Dtos.Advance
{
    public class AdvanceGetDto : BaseIdDto<Guid>
    {
        public string? DocNumber { get; set; }
        public string? Tin { get; set; }
        public decimal Amount { get; set; }
        public string? FilePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
