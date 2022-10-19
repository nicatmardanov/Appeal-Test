using Microsoft.AspNetCore.Http;

namespace Core.Utilities.File
{
    public class FileHelper
    {
        public static async Task<string> SaveAsync(IFormFile formFile)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);

            string path = Path.Combine(
              Directory.GetCurrentDirectory(),
              "Public/Attachments",
              fileName);

            using FileStream stream = new(path, FileMode.Create);
            await formFile.CopyToAsync(stream);

            return path;
        }
    }
}
