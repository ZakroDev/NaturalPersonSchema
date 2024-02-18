using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NaturalPerson.Core.Person;
using System.IO;
using System.Text;

namespace NaturalPersonl.Infra.Person
{
    public class FileRepository : IFileService
    {
        private readonly string path;
        public FileRepository(IConfiguration configuration)
        {
            path = configuration["PhotoStorageSettings:Path"];
        }
        public async Task<string> UploadAsync(IFormFile file)
        {

            if (file == null || file.Length == 0)
                throw new Exception("File is null");

            if (!IsAllowedType(file.FileName))
                throw new Exception("File extension type is not allowed");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var fileFullPath = Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

            using (var fileStream = new FileStream(fileFullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileFullPath;
        }
        public static bool IsAllowedType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            string[] allowedFileTypes = [".png", ".jpg", ".jpeg"];
            return allowedFileTypes.Contains(extension);
        }
    }
}
