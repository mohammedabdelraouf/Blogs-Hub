namespace BlogsAPI.Models
{

    public class ResultViewModel<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public ResultViewModel()
        {
            IsSuccess = true;
            Message = string.Empty;
            Data = default(T);
        }

        public ResultViewModel(T data)
        {
            IsSuccess = true;
            Message = string.Empty;
            Data = data;
        }
    }

    public class ResultViewModel{

        public bool IsSuccess { get; set; }
        public string Message { get; set; }  
        public ResultViewModel()
        {
            IsSuccess = true;
            Message = string.Empty;
        }
    }
}
