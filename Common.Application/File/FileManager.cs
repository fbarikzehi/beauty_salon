using Common.Crosscutting.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace Common.Application.File
{
    public class FileManager
    {
        private IFormFile _file;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileManager(IFormFile file, IHostingEnvironment hostingEnvironmen)
        {
            _hostingEnvironment = hostingEnvironmen;
            _file = file;
        }
        public FileManager(IHostingEnvironment hostingEnvironmen)
        {
            _hostingEnvironment = hostingEnvironmen;
        }

        public FileManagerResult SaveToHost(string containerPath, string fileNameToSave)
        {
            var result = new FileManagerResult();
            try
            {
                if (_file is null)
                {
                    result.IsSuccess = false;
                    result.Message = "فایل ارسال نشده";
                    return result;
                }

                var extension = _file.FileName.Substring(_file.FileName.LastIndexOf('.')).ToLower();
                var phisycalPath = Path.Combine(_hostingEnvironment.WebRootPath, containerPath);

                if (!Directory.Exists(phisycalPath))
                    Directory.CreateDirectory(phisycalPath);

                if (string.IsNullOrEmpty(fileNameToSave))
                    fileNameToSave = $"{RandomUtility.GetAlphaNumRandomString(15)}{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}";

                var fileName = $"{fileNameToSave}{extension}";
                var filePath = Path.Combine(phisycalPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                    _file.CopyTo(fileStream);

                result.SavedFileName = $"/{containerPath}/{fileName}";
                result.IsSuccess = true;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "خطای ذخیره فایل رخ داد";
                return result;
            }

        }

        public FileManagerResult DeleteFromHost(string containerPath, string path)
        {
            var result = new FileManagerResult();
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var phisycalPath = Path.Combine(_hostingEnvironment.WebRootPath, containerPath, path.Split('/').LastOrDefault());

                    if (System.IO.File.Exists(phisycalPath))
                        System.IO.File.Delete(phisycalPath);
                }

                result.IsSuccess = true;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "خطای حذف فایل رخ داد";
                return result;
            }

        }
    }
}
