using Core.Entities;

namespace Entities.Dtos.Validation
{
    public class FileValidationValuesDto : IDto
    {
        public List<string>? Extensions { get; set; }
        public int MaxLength { get; set; }
    }
}
