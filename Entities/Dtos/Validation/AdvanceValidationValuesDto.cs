using Core.Entities;

namespace Entities.Dtos.Validation
{
    public class AdvanceValidationValuesDto : IDto
    {
        public List<string>? DocNumberPrefixes { get; set; }
        public FileValidationValuesDto? File { get; set; }
    }
}
