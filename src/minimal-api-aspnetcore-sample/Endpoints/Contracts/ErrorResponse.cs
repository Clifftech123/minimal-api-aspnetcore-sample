namespace minimal_api_aspnetcore_sample.Endpoints.Contracts

{
    public class ErrorResponse
    {
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

    }
}


