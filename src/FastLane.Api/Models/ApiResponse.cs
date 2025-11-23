using System.Text.Json.Serialization;

namespace FastLane.Api.Models
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; init; }

        [JsonPropertyName("data")]
        public T Data { get; init; }

        [JsonPropertyName("message")]
        public string Message { get; init; }

        public ApiResponse(T data, string message = "")
        {
            Success = true;
            Data = data;
            Message = message;
        }

        public ApiResponse(string message)
        {
            Success = false;
            Message = message;
            Data = default;
        }

        public static ApiResponse<T> Fail(string message) => new ApiResponse<T>(message);
    }
}
