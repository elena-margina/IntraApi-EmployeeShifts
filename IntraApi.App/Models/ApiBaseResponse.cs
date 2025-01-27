namespace IntraApi.App.Models
{
    public class ApiBaseResponse
    {
        public ApiBaseResponse()
        {
            Success = true;
        }

        public ApiBaseResponse(string message)
        {
            Success = true;
            Message = message;
        }

        public ApiBaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string>? ValidationErrors { get; set; }
    }

}
