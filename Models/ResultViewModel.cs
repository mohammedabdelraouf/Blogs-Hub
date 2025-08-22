namespace BlogsAPI.Models
{

    public class ResultViewModel<T>
    {
        public bool IsSuccess;
        public string? Message;
        public T? Data;

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

        public bool IsSuccess;
        public string? Message;
        public ResultViewModel()
        {
            IsSuccess = true;
            Message = string.Empty;
        }
    }
}
