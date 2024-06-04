namespace SmartEats.Models
{
    public class ErrorModel
    {        
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
    public class ErrorResponse
    {
        public List<ErrorModel> Error { get; set; } = new List<ErrorModel>();
        public bool Successful { get; set; }
    }
}
