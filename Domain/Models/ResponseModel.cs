namespace Domain.Models
{
    public class ResponseModel<T>
    {
        public ResponseDataModel<T> resposta { get; set; }
    }

    public class ResponseModel
    {
        public bool success { get; set; }
        public string message { get; set; }
    }

    public class ResponseDataModel<T> : ResponseModel
    {
        public T data { get; set; }
    }
}
