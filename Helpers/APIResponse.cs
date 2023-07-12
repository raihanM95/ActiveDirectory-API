namespace ActiveDirectory_API.Helpers
{
    public class APIResponse
    {
        public int status_code { get; set; }
        public string Message { get; set; }
        public object data { get; set; }
        public int Count { get; set; }
    }
}
