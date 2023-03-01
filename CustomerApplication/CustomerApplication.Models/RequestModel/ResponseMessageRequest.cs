namespace CustomerApplication.Models.RequestModel;

public class ResponseMessageRequest
{
    public ResponseMessageRequest(string message)
    {
        Message = message;
    }
    public string Message { get; set; }
}
