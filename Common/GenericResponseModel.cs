using Microsoft.VisualBasic;
using static MVC_Task.Common.BaseResponseModel;

namespace MVC_Task.Common
{
    public class GenericResponseModel<TResult> : BaseResponseModel
    {
        public TResult Data { get; set; }
        protected GenericResponseModel(IList<ErrorResponseModel> errorLists)
        : base(false ,errorLists)
        {
            Type t = typeof(TResult);
            if (t.GetConstructor(Type.EmptyTypes) != null)
            {
                Data = Activator.CreateInstance<TResult>();
            }
        }

        protected GenericResponseModel(TResult data)
            : base(true) => Data = data;


        public static GenericResponseModel<TResult> Success(TResult data)
            => new(data);

        public static GenericResponseModel<TResult> Failure(IList<ErrorResponseModel> errorLists)
            => new(errorLists);
    }
}
