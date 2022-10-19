using Core.Entities;

namespace Entities.Dtos.Transfer
{
    public class TransferGetListRequestDto : IDto
    {
        public Guid AdvanceId { get; set; }
        public string? DocNumber { get; set; }
        public string? Tin { get; set; }
    }
}
