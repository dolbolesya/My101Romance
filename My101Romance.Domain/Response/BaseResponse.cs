using My101Romance.Domain.Enum;

namespace My101Romance.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string? ErrDescription { get; set; }
    
    public StatusCode StatusCode { get; set; }
    
    public T Data { get; set; }
}

public interface IBaseResponse<T>
{
    StatusCode StatusCode { get; }
    T Data { get; set; }
}