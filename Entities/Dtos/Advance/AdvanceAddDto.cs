using Core.Entities;

namespace Entities.Dtos.Advance
{
    public class AdvanceAddDto : IDto
    {
        public Microsoft.AspNetCore.Http.IFormFile? File { get; set; }
        public string? DocNumber { get; set; }
        public string? Tin { get; set; }
        public decimal Amount { get; set; }
    }
}
