namespace TravelRequisitionSystem.Models
{
    public class Response
    {
        public Response(bool isSuccess)
        {
            IsSuccessful = isSuccess;
        }

        public dynamic Data {   get; set; }
        public bool IsSuccessful { get; set; }
        public ErrorResponse Error { get; set; }
    }

    public class ErrorResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
}
