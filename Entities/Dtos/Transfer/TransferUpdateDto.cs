using Core.Entities.Dtos;

namespace Entities.Dtos.Transfer
{
    public class TransferUpdateDto : BaseIdDto<int>
    {
        public decimal Amount { get; set; }
    }
}
