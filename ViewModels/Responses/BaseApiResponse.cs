namespace GestaoDeResiduos.ViewModels.Responses
{
    public class BaseApiResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        

        public BaseApiResponse(string message, T data, string? token= null)
        {
            Message = message;
            Data = data;
        }
    }
}
