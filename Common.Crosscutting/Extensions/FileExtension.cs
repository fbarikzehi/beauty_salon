using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Common.Crosscutting.Extensions
{
    public static class FileExtension
    {
        public static byte[] FileToByte(this IFormFile file)
        {
            if (file == null)
                return new byte[0];

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();

                return fileBytes;
            }
        }
        public static string ByteToBase64String(this byte[] bytes, string mimeType)
        {
            if (bytes?.Length == 0 || bytes == null)
                return string.Empty;

            string base64 = Convert.ToBase64String(bytes);
            return string.Format("data:{0};base64,{1}", mimeType, base64);
        }
    }
}
