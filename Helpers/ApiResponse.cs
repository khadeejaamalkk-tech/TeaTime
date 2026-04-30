namespace TeaTimeDelivery.Helpers
{
    public class ApiResponse<T>
    {
        public bool status { get; set; }
        public int StatusCode { get; set;  }
        
        public string ?StatusMessage { get; set; }
        public T? Data { get; set; }


        public static ApiResponse<T> Success(T data, string message= "Success")
        {
            return new ApiResponse<T>
            {
                status = true,
                StatusCode = 200,
                StatusMessage = message,
                Data = data
            };
        }
        public static ApiResponse<T> Created(T data, string message="Created successfully")
        {
            return new ApiResponse<T>
            {
                status = true,
                StatusCode = 201,
                StatusMessage = message,
                Data = data
            };
        }
        public static ApiResponse<T> SuccessNoData(string message = "Success")
        {
            return new ApiResponse<T>
            {
                status=true,
                StatusCode = 200,
                StatusMessage = message,
                Data = default
            };
        }
        public static ApiResponse<T> NotFound(string message = "Not found")
        {
            return new ApiResponse<T>
            {
                status= false,
                StatusCode = 404,
                StatusMessage = message,
                Data = default
            };
        }
        public static ApiResponse<T> BadRequest(string message = "Bad request")
        {
            return new ApiResponse<T>
            {
                status = false,
                StatusCode = 400,
                StatusMessage = message,
                Data = default
            };
        }
        public static ApiResponse<T> Error(string message = "Internal server error")
        {
            return new ApiResponse<T>
            {
                status = false,
                StatusCode = 500,
                StatusMessage = message,
                Data = default
            };
        }
    }
}
