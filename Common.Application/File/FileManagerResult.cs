namespace Common.Application.File
{
    public class FileManagerResult
    {
        public string SavedFileName { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; internal set; }
    }
}
