using Core.Entities;

namespace Entities.Dtos.Advance
{
    public class AdvanceGetByIdRequestDto : IDto
    {
        public Guid Id { get; set; }
    }
}
