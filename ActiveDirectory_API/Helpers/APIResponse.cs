namespace ActiveDirectory_API.Helpers
{
    public class APIResponses
    {
        public int status_code { get; set; }
        public string Message { get; set; }
        public object data { get; set; }
        public object designations { get; set; }
        public object departments { get; set; }
        public object branches { get; set; }
        public int Count { get; set; }
    }
    public class APIResponse
    {
        public int status_code { get; set; }
        public string Message { get; set; }
        public object data { get; set; }
        public int Count { get; set; }
    }
}
