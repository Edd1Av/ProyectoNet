namespace WebApi.Models
{
    public class ResponseApiModel
    {
        public bool Success { get; set; }
        public object? Content { get; set; }
        public string? Error { get; set; }
    }
}
