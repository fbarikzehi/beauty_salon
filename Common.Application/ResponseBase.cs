namespace Common.Application
{
    public class ResponseBase<DataType>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DataType Data { get; set; }
    }
}