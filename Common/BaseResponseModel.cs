namespace MVC_Task.Common
{

    public class BaseResponseModel
    {
        public bool IsSuccess { get; set; }
        public ICollection<ErrorResponseModel> ErrorList { get; set; }

        protected BaseResponseModel(bool isSuccess)
        {
            IsSuccess = isSuccess;
            ErrorList = new List<ErrorResponseModel>();
        }

        protected BaseResponseModel(bool isSuccess, IList<ErrorResponseModel> errorLists)
        {
            IsSuccess = isSuccess;
            ErrorList = errorLists ?? new List<ErrorResponseModel>();
        }

        public class ErrorResponseModel
        {
            public string PropertyName { get; set; }
            public string Message { get; set; }

            public static ErrorResponseModel Create(string propertyName, string message)
                => new()
                {
                    PropertyName = propertyName,
                    Message = message
                };
        }
    }
}
